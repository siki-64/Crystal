namespace Crystal.Audio;

public sealed class Music : IAudioVoice, IDisposable
{
    private const int ChunkFrameCount = 4096;
    private const int NoLoopEnd = -1;
    private static readonly TimeSpan PlayingPollInterval = TimeSpan.FromMilliseconds(5);

    private readonly AudioMixer _mixer;
    private readonly AudioSource _source;
    private readonly Lock _sourceGate = new();
    private readonly AutoResetEvent _wake = new(false);
    private readonly Thread? _decodeThread;
    private readonly int _sourceSampleRate;
    private readonly int _sourceChannelCount;
    private readonly int _capacity;
    private readonly float[] _ring;
    private readonly float[] _chunk;

    private long _sourceFrame;
    private long _loopStart;
    private long _loopEnd;
    private volatile bool _isLooped;
    private volatile bool _isDisposed;
    private volatile Exception? _error;
    private float _volume = 1f;

    private int _readIndex;
    private int _bufferedCount;
    private bool _hasEnded;
    private bool _isRewindPending;
    private double _fraction;
    private float _gain;
    private bool _hasGain;

    public Music(AudioMixer mixer, AudioSource source) : this(mixer, source, startsDecodeThread: true)
    {
    }

    internal Music(AudioMixer mixer, AudioSource source, bool startsDecodeThread)
    {
        ArgumentNullException.ThrowIfNull(mixer);
        ArgumentNullException.ThrowIfNull(source);
        if (source.SampleRate <= 0) throw new ArgumentException("The source's sample rate is positive.", nameof(source));
        if (source.ChannelCount is not (1 or 2)) throw new ArgumentException("The source is mono or stereo.", nameof(source));

        _mixer = mixer;
        _source = source;
        _sourceSampleRate = source.SampleRate;
        _sourceChannelCount = source.ChannelCount;
        _loopStart = Math.Max(source.LoopStart, 0);
        _loopEnd = source.LoopEnd ?? NoLoopEnd;
        _capacity = Math.Max(_sourceSampleRate / 2, ChunkFrameCount * 2);
        _ring = new float[_capacity * AudioMixer.ChannelCount];
        _chunk = new float[ChunkFrameCount * _sourceChannelCount];

        if (!startsDecodeThread) return;
        _decodeThread = new Thread(DecodeLoop) { IsBackground = true, Name = "CRYSTAL_MusicDecode" };
        _decodeThread.Start();
    }

    public SoundState State { get; private set; }
    public Exception? Error => _error;

    SoundState IAudioVoice.State
    {
        get => State;
        set => State = value;
    }

    public float Volume
    {
        get => _volume;
        set
        {
            if (!(value >= 0f) || float.IsInfinity(value)) throw new ArgumentOutOfRangeException(nameof(value), value, "The volume is zero or a positive number.");
            _volume = value;
        }
    }

    public bool IsLooped
    {
        get => _isLooped;
        set => _isLooped = value;
    }

    public long LoopStart
    {
        get => Volatile.Read(ref _loopStart);
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value);
            Volatile.Write(ref _loopStart, value);
        }
    }

    public long? LoopEnd
    {
        get => Volatile.Read(ref _loopEnd) is var loopEnd && loopEnd == NoLoopEnd ? null : loopEnd;
        set
        {
            if (value is { } loopEnd) ArgumentOutOfRangeException.ThrowIfNegativeOrZero(loopEnd);
            Volatile.Write(ref _loopEnd, value ?? NoLoopEnd);
        }
    }

    public void Play()
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);
        _mixer.Start(this);
        _wake.Set();
    }

    public void Pause() => _mixer.Pause(this);

    public void Stop() => _mixer.Stop(this);

    public void Dispose()
    {
        if (_isDisposed) return;
        _isDisposed = true;
        _wake.Set();
        _decodeThread?.Join();
        _mixer.Stop(this);
        lock (_sourceGate) _source.Dispose();
        _wake.Dispose();
    }

    internal bool FillBuffer()
    {
        lock (_sourceGate)
        {
            if (_isDisposed) return false;

            bool rewinds;
            int freeFrameCount;
            lock (_mixer.Gate)
            {
                rewinds = _isRewindPending;
                if (rewinds)
                {
                    _isRewindPending = false;
                    _hasEnded = false;
                }
                if (_hasEnded) return false;
                freeFrameCount = _capacity - _bufferedCount;
            }

            if (rewinds)
            {
                _source.Seek(0);
                _sourceFrame = 0;
            }
            if (freeFrameCount < ChunkFrameCount) return false;

            var frameCount = Decode(out var hasEnded);
            lock (_mixer.Gate)
            {
                if (_isRewindPending) return true;
                Append(frameCount);
                _hasEnded = hasEnded;
            }
            return frameCount > 0;
        }
    }

    void IAudioVoice.OnStarted() => _hasGain = false;

    void IAudioVoice.OnStopped()
    {
        _readIndex = 0;
        _bufferedCount = 0;
        _fraction = 0;
        _isRewindPending = true;
        if (!_isDisposed) _wake.Set();
    }

    bool IAudioVoice.MixInto(Span<float> output, int outputSampleRate)
    {
        var step = (double)_sourceSampleRate / outputSampleRate;
        var targetGain = _volume;
        if (!_hasGain)
        {
            _gain = targetGain;
            _hasGain = true;
        }

        var outputFrameCount = output.Length / AudioMixer.ChannelCount;
        var gainStep = (targetGain - _gain) / outputFrameCount;
        var gain = _gain;
        var ring = _ring;

        for (var frame = 0; frame < outputFrameCount; frame++)
        {
            if (_bufferedCount == 0 || (_bufferedCount == 1 && !_hasEnded)) break;

            var current = _readIndex * 2;
            var next = _bufferedCount > 1 ? (_readIndex + 1) % _capacity * 2 : current;
            var fraction = (float)_fraction;
            gain += gainStep;
            output[frame * 2] += (ring[current] + (ring[next] - ring[current]) * fraction) * gain;
            output[frame * 2 + 1] += (ring[current + 1] + (ring[next + 1] - ring[current + 1]) * fraction) * gain;

            _fraction += step;
            var consumed = Math.Min((int)_fraction, _bufferedCount);
            _fraction -= (int)_fraction;
            _readIndex = (_readIndex + consumed) % _capacity;
            _bufferedCount -= consumed;
        }

        _gain = targetGain;
        return !(_hasEnded && !_isRewindPending && _bufferedCount == 0);
    }

    private int Decode(out bool hasEnded)
    {
        hasEnded = false;
        var frameCount = 0;
        var hasJustSought = false;
        while (frameCount < ChunkFrameCount)
        {
            var isLooped = _isLooped;
            var loopStart = Volatile.Read(ref _loopStart);
            var loopEnd = Volatile.Read(ref _loopEnd);
            var hasLoopEnd = isLooped && loopEnd > loopStart;
            var wanted = (long)(ChunkFrameCount - frameCount);
            if (hasLoopEnd)
            {
                if (_sourceFrame >= loopEnd)
                {
                    if (hasJustSought)
                    {
                        hasEnded = true;
                        break;
                    }
                    SeekSource(loopStart);
                    hasJustSought = true;
                    continue;
                }
                wanted = Math.Min(wanted, loopEnd - _sourceFrame);
            }

            var read = _source.Read(_chunk.AsSpan(frameCount * _sourceChannelCount, (int)wanted * _sourceChannelCount));
            if (read > 0)
            {
                frameCount += read;
                _sourceFrame += read;
                hasJustSought = false;
                continue;
            }

            if (!isLooped || hasJustSought)
            {
                hasEnded = true;
                break;
            }
            SeekSource(loopStart);
            hasJustSought = true;
        }
        return frameCount;
    }

    private void SeekSource(long frame)
    {
        _source.Seek(frame);
        _sourceFrame = frame;
    }

    private void Append(int frameCount)
    {
        var writeIndex = (_readIndex + _bufferedCount) % _capacity;
        for (var frame = 0; frame < frameCount; frame++)
        {
            var left = _chunk[frame * _sourceChannelCount];
            var right = _sourceChannelCount == 2 ? _chunk[frame * 2 + 1] : left;
            _ring[writeIndex * 2] = left;
            _ring[writeIndex * 2 + 1] = right;
            writeIndex = (writeIndex + 1) % _capacity;
        }
        _bufferedCount += frameCount;
    }

    private void DecodeLoop()
    {
        while (!_isDisposed)
        {
            try
            {
                if (FillBuffer()) continue;
            }
            catch (Exception exception)
            {
                _error = exception;
                lock (_mixer.Gate) _hasEnded = true;
            }
            if (State == SoundState.Playing) _wake.WaitOne(PlayingPollInterval);
            else _wake.WaitOne();
        }
    }
}
