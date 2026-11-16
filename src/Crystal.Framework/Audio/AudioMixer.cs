namespace Crystal.Audio;

public sealed class AudioMixer
{
    public const int ChannelCount = 2;
    public const int DefaultSampleRate = 48000;

    private const int InitialVoiceCapacity = 64;

    private readonly Lock _gate = new();
    private readonly List<IAudioVoice> _playing = new(InitialVoiceCapacity);
    private readonly Stack<SoundEffectInstance> _pool = new(InitialVoiceCapacity);
    private float _masterVolume = 1f;
    private float _masterGain = 1f;

    public AudioMixer(int sampleRate = DefaultSampleRate)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(sampleRate);
        SampleRate = sampleRate;
    }

    public int SampleRate { get; }

    public int PlayingCount
    {
        get
        {
            lock (_gate) return _playing.Count;
        }
    }

    public float MasterVolume
    {
        get => _masterVolume;
        set
        {
            if (!(value >= 0f) || float.IsInfinity(value)) throw new ArgumentOutOfRangeException(nameof(value), value, "The volume is zero or a positive number.");
            _masterVolume = value;
        }
    }

    public void Play(SoundEffect sound, float volume = 1f, float pan = 0f, float pitch = 0f)
    {
        ArgumentNullException.ThrowIfNull(sound);
        lock (_gate)
        {
            if (!_pool.TryPop(out var instance)) instance = new SoundEffectInstance(this, sound, isPooled: true);
            try
            {
                instance.Reuse(sound, volume, pan, pitch);
            }
            catch
            {
                _pool.Push(instance);
                throw;
            }
            StartLocked(instance);
        }
    }

    public void Render(Span<float> output)
    {
        if (output.Length % ChannelCount != 0) throw new ArgumentException("The output holds whole stereo frames.", nameof(output));
        output.Clear();
        if (output.IsEmpty) return;

        lock (_gate)
        {
            for (var index = _playing.Count - 1; index >= 0; index--)
            {
                var voice = _playing[index];
                if (voice.MixInto(output, SampleRate)) continue;
                _playing.RemoveAt(index);
                voice.State = SoundState.Stopped;
                voice.OnStopped();
                if (voice is SoundEffectInstance { IsPooled: true } pooled) _pool.Push(pooled);
            }
        }

        var targetGain = _masterVolume;
        var gain = _masterGain;
        var gainStep = (targetGain - gain) / (output.Length / ChannelCount);
        for (var index = 0; index < output.Length; index += ChannelCount)
        {
            gain += gainStep;
            output[index] = Math.Clamp(output[index] * gain, -1f, 1f);
            output[index + 1] = Math.Clamp(output[index + 1] * gain, -1f, 1f);
        }
        _masterGain = targetGain;
    }

    internal Lock Gate => _gate;

    internal void Start(IAudioVoice voice)
    {
        lock (_gate) StartLocked(voice);
    }

    internal void Pause(IAudioVoice voice)
    {
        lock (_gate)
        {
            if (voice.State != SoundState.Playing) return;
            _playing.Remove(voice);
            voice.State = SoundState.Paused;
        }
    }

    internal void Stop(IAudioVoice voice)
    {
        lock (_gate)
        {
            if (voice.State == SoundState.Playing) _playing.Remove(voice);
            voice.State = SoundState.Stopped;
            voice.OnStopped();
        }
    }

    private void StartLocked(IAudioVoice voice)
    {
        if (voice.State == SoundState.Playing) return;
        voice.OnStarted();
        voice.State = SoundState.Playing;
        _playing.Add(voice);
    }
}
