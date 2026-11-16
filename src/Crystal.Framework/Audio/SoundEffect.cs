using System.Buffers;

namespace Crystal.Audio;

public sealed class SoundEffect
{
    private const int SourceChunkFrameCount = 4096;

    private readonly float[] _samples;

    public SoundEffect(ReadOnlySpan<float> samples, int sampleRate, int channelCount)
        : this(samples.ToArray(), sampleRate, channelCount)
    {
    }

    internal SoundEffect(float[] samples, int sampleRate, int channelCount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(sampleRate);
        if (channelCount is not (1 or 2)) throw new ArgumentOutOfRangeException(nameof(channelCount), channelCount, "A sound effect is mono or stereo.");
        if (samples.Length % channelCount != 0) throw new ArgumentException("The sample count is not a multiple of the channel count.", nameof(samples));
        _samples = samples;
        SampleRate = sampleRate;
        ChannelCount = channelCount;
        FrameCount = samples.Length / channelCount;
    }

    public int SampleRate { get; }
    public int ChannelCount { get; }
    public int FrameCount { get; }
    public TimeSpan Duration => TimeSpan.FromSeconds((double)FrameCount / SampleRate);

    internal ReadOnlySpan<float> Samples => _samples;

    public static SoundEffect FromSource(AudioSource source)
    {
        ArgumentNullException.ThrowIfNull(source);
        if (source.ChannelCount is not (1 or 2)) throw new ArgumentException("The source is mono or stereo.", nameof(source));
        var samples = new ArrayBufferWriter<float>();
        while (true)
        {
            var chunk = samples.GetSpan(SourceChunkFrameCount * source.ChannelCount);
            var frameCount = source.Read(chunk[..(SourceChunkFrameCount * source.ChannelCount)]);
            if (frameCount == 0) break;
            samples.Advance(frameCount * source.ChannelCount);
        }
        return new SoundEffect(samples.WrittenSpan.ToArray(), source.SampleRate, source.ChannelCount);
    }

    public static SoundEffect FromWav(Stream stream)
    {
        ArgumentNullException.ThrowIfNull(stream);
        return WavFile.Read(stream);
    }

    public static SoundEffect FromWav(string path)
    {
        using var stream = File.OpenRead(path);
        return WavFile.Read(stream);
    }
}
