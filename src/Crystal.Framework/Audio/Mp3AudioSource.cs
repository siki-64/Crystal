using NLayer;

namespace Crystal.Audio;

public sealed class Mp3AudioSource : AudioSource
{
    private const int SkipChunkFrameCount = 1152;

    private readonly byte[] _data;
    private readonly int _sampleRate;
    private readonly int _channelCount;
    private readonly float[] _skipBuffer;
    private MpegFile _decoder;
    private long _frame;
    private bool _isClosed;

    public Mp3AudioSource(string path) : this(File.OpenRead(path))
    {
    }

    public Mp3AudioSource(Stream stream, bool leaveOpen = false)
    {
        ArgumentNullException.ThrowIfNull(stream);
        try
        {
            using var buffer = new MemoryStream();
            stream.CopyTo(buffer);
            _data = buffer.ToArray();
        }
        finally
        {
            if (!leaveOpen) stream.Dispose();
        }

        var decoder = OpenDecoder();
        if (decoder.Channels is not (1 or 2))
        {
            decoder.Dispose();
            throw new NotSupportedException($"MP3 streams with {decoder.Channels} channels are not supported; use mono or stereo.");
        }

        _decoder = decoder;
        _sampleRate = decoder.SampleRate;
        _channelCount = decoder.Channels;
        _skipBuffer = new float[SkipChunkFrameCount * _channelCount];
        FrameCount = decoder.Length / (sizeof(float) * _channelCount);
    }

    public override int SampleRate => _sampleRate;
    public override int ChannelCount => _channelCount;
    public long FrameCount { get; }

    public override int Read(Span<float> samples)
    {
        ObjectDisposedException.ThrowIf(_isClosed, this);
        if (samples.Length % _channelCount != 0) throw new ArgumentException("The span holds whole frames.", nameof(samples));

        var frameCount = Decode(samples[..((int)Math.Min(samples.Length / _channelCount, FrameCount - _frame) * _channelCount)]);
        _frame += frameCount;
        return frameCount;
    }

    public override void Seek(long frame)
    {
        ObjectDisposedException.ThrowIf(_isClosed, this);
        ArgumentOutOfRangeException.ThrowIfNegative(frame);
        var target = Math.Min(frame, FrameCount);
        if (target < _frame)
        {
            _decoder.Dispose();
            _decoder = OpenDecoder();
            _frame = 0;
        }
        while (_frame < target)
        {
            var skipped = Decode(_skipBuffer.AsSpan(0, (int)Math.Min(SkipChunkFrameCount, target - _frame) * _channelCount));
            if (skipped == 0) throw new InvalidDataException($"The MP3 stream can't be positioned at frame {target}.");
            _frame += skipped;
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (_isClosed) return;
        _isClosed = true;
        if (disposing) _decoder.Dispose();
    }

    private MpegFile OpenDecoder()
    {
        MpegFile decoder;
        try
        {
            decoder = new MpegFile(new MemoryStream(_data, writable: false));
        }
        catch (Exception exception) when (exception is not OutOfMemoryException)
        {
            throw new InvalidDataException("The data is not an MP3 stream.", exception);
        }
        if (decoder.SampleRate > 0 && decoder.Channels > 0 && decoder.Length > 0) return decoder;
        decoder.Dispose();
        throw new InvalidDataException("The data is not an MP3 stream.");
    }

    private int Decode(Span<float> samples)
    {
        var read = 0;
        while (read < samples.Length)
        {
            var decoded = _decoder.ReadSamples(samples[read..]);
            if (decoded <= 0) break;
            read += decoded;
        }
        return read / _channelCount;
    }
}
