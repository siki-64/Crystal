using System.Buffers.Binary;
using System.Diagnostics;
using Crystal.Audio;

namespace Crystal.Tests;

[TestClass]
public sealed class AudioTests
{
    private const float Tolerance = 1e-6f;

    [TestMethod]
    public void RenderIsSilentWhenNothingPlays()
    {
        var mixer = new AudioMixer();
        var output = new float[8];
        Array.Fill(output, 0.5f);
        mixer.Render(output);
        AssertSamples(new float[8], output);
    }

    [TestMethod]
    public void MonoSoundPlaysOnBothChannelsAndThenStops()
    {
        var mixer = new AudioMixer();
        mixer.Play(new SoundEffect([0.1f, 0.2f, 0.3f, 0.4f], AudioMixer.DefaultSampleRate, 1));
        Assert.AreEqual(1, mixer.PlayingCount);

        var output = new float[12];
        mixer.Render(output);
        AssertSamples([0.1f, 0.1f, 0.2f, 0.2f, 0.3f, 0.3f, 0.4f, 0.4f, 0f, 0f, 0f, 0f], output);
        Assert.AreEqual(0, mixer.PlayingCount);
    }

    [TestMethod]
    public void VolumeAndPanScaleTheChannelsOfAStereoSound()
    {
        var mixer = new AudioMixer();
        var sound = new SoundEffect([0.2f, 0.4f, 0.6f, 0.8f], AudioMixer.DefaultSampleRate, 2);
        var output = new float[4];

        var right = new SoundEffectInstance(mixer, sound) { Volume = 0.5f, Pan = 1f };
        right.Play();
        mixer.Render(output);
        AssertSamples([0f, 0.2f, 0f, 0.4f], output);

        var halfLeft = new SoundEffectInstance(mixer, sound) { Pan = -0.5f };
        halfLeft.Play();
        mixer.Render(output);
        AssertSamples([0.2f, 0.2f, 0.6f, 0.4f], output);
    }

    [TestMethod]
    public void LoopedInstanceWrapsUntilStopped()
    {
        var mixer = new AudioMixer();
        var instance = new SoundEffectInstance(mixer, new SoundEffect([0.1f, 0.2f, 0.3f], AudioMixer.DefaultSampleRate, 1)) { IsLooped = true };
        instance.Play();

        var output = new float[14];
        mixer.Render(output);
        AssertSamples([0.1f, 0.1f, 0.2f, 0.2f, 0.3f, 0.3f, 0.1f, 0.1f, 0.2f, 0.2f, 0.3f, 0.3f, 0.1f, 0.1f], output);
        Assert.AreEqual(SoundState.Playing, instance.State);

        instance.Stop();
        Assert.AreEqual(SoundState.Stopped, instance.State);
        mixer.Render(output);
        AssertSamples(new float[14], output);

        instance.Play();
        mixer.Render(output.AsSpan(0, 2));
        AssertSamples([0.1f, 0.1f], output.AsSpan(0, 2));
    }

    [TestMethod]
    public void PitchOfOneOctavePlaysAtDoubleSpeed()
    {
        var mixer = new AudioMixer();
        mixer.Play(new SoundEffect([0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f], AudioMixer.DefaultSampleRate, 1), pitch: 1f);
        var output = new float[8];
        mixer.Render(output);
        AssertSamples([0f, 0f, 0.2f, 0.2f, 0.4f, 0.4f, 0f, 0f], output);
    }

    [TestMethod]
    public void SoundAtHalfTheMixerRateIsInterpolated()
    {
        var mixer = new AudioMixer();
        mixer.Play(new SoundEffect([0f, 0.4f, 0.8f], AudioMixer.DefaultSampleRate / 2, 1));
        var output = new float[14];
        mixer.Render(output);
        AssertSamples([0f, 0f, 0.2f, 0.2f, 0.4f, 0.4f, 0.6f, 0.6f, 0.8f, 0.8f, 0.8f, 0.8f, 0f, 0f], output);
        Assert.AreEqual(0, mixer.PlayingCount);
    }

    [TestMethod]
    public void PauseKeepsThePositionAndPlayResumes()
    {
        var mixer = new AudioMixer();
        var instance = new SoundEffectInstance(mixer, new SoundEffect([0.1f, 0.2f, 0.3f, 0.4f], AudioMixer.DefaultSampleRate, 1));
        var output = new float[4];

        instance.Play();
        mixer.Render(output);
        instance.Pause();
        Assert.AreEqual(SoundState.Paused, instance.State);
        mixer.Render(output);
        AssertSamples(new float[4], output);

        instance.Play();
        mixer.Render(output);
        AssertSamples([0.3f, 0.3f, 0.4f, 0.4f], output);
        Assert.AreEqual(SoundState.Stopped, instance.State);
    }

    [TestMethod]
    public void VolumeChangesRampAcrossOneRender()
    {
        var mixer = new AudioMixer();
        var instance = new SoundEffectInstance(mixer, new SoundEffect([1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f], AudioMixer.DefaultSampleRate, 1));
        var output = new float[8];

        instance.Play();
        mixer.Render(output);
        AssertSamples([1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f], output);

        instance.Volume = 0f;
        mixer.Render(output);
        AssertSamples([0.75f, 0.75f, 0.5f, 0.5f, 0.25f, 0.25f, 0f, 0f], output);
    }

    [TestMethod]
    public void MasterVolumeRampsAndTheSumIsClamped()
    {
        var mixer = new AudioMixer();
        var sound = new SoundEffect([0.8f, 0.8f, 0.8f, 0.8f, 0.8f, 0.8f, 0.8f, 0.8f], AudioMixer.DefaultSampleRate, 1);
        var output = new float[8];

        mixer.Play(sound);
        mixer.Play(sound);
        mixer.Render(output);
        AssertSamples([1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f], output);

        mixer.MasterVolume = 0.5f;
        mixer.Render(output);
        AssertSamples([1f, 1f, 1f, 1f, 1f, 1f, 0.8f, 0.8f], output);

        mixer.Play(sound);
        mixer.Render(output);
        AssertSamples([0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f], output);
    }

    [TestMethod]
    public void PlayingAndRenderingAllocateNothingOnceWarmedUp()
    {
        var mixer = new AudioMixer();
        var sound = new SoundEffect([0.1f, 0.2f], AudioMixer.DefaultSampleRate, 1);
        var output = new float[64];
        for (var round = 0; round < 4; round++)
        {
            mixer.Play(sound);
            mixer.Play(sound, 0.5f, -1f, 0.5f);
            mixer.Render(output);
        }

        var before = GC.GetAllocatedBytesForCurrentThread();
        for (var round = 0; round < 100; round++)
        {
            mixer.Play(sound);
            mixer.Play(sound, 0.5f, -1f, 0.5f);
            mixer.Render(output);
        }
        Assert.AreEqual(0, GC.GetAllocatedBytesForCurrentThread() - before);
        Assert.AreEqual(0, mixer.PlayingCount);
    }

    [TestMethod]
    public void InvalidArgumentsThrow()
    {
        var mixer = new AudioMixer();
        var sound = new SoundEffect([0f, 0f], AudioMixer.DefaultSampleRate, 2);
        var instance = new SoundEffectInstance(mixer, sound);

        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new AudioMixer(0));
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new SoundEffect([0f, 0f, 0f], AudioMixer.DefaultSampleRate, 3));
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new SoundEffect([0f], 0, 1));
        Assert.ThrowsExactly<ArgumentException>(() => new SoundEffect([0f, 0f, 0f], AudioMixer.DefaultSampleRate, 2));
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => instance.Volume = -0.1f);
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => instance.Volume = float.NaN);
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => instance.Pan = 1.5f);
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => instance.Pitch = float.PositiveInfinity);
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => mixer.MasterVolume = -1f);
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => mixer.Play(sound, pan: -2f));
        Assert.ThrowsExactly<ArgumentException>(() => mixer.Render(new float[3]));
        Assert.AreEqual(0, mixer.PlayingCount);
    }

    [TestMethod]
    public void WavIntegerAndFloatEncodingsDecodeToFloat()
    {
        var sixteenBitStereo = SoundEffect.FromWav(new MemoryStream(Wav(1, 2, 44100, 16, [0x00, 0x40, 0x00, 0xC0, 0xFF, 0x7F, 0x00, 0x80])));
        Assert.AreEqual((44100, 2, 2), (sixteenBitStereo.SampleRate, sixteenBitStereo.ChannelCount, sixteenBitStereo.FrameCount));
        AssertSamples([0.5f, -0.5f, 32767f / 32768f, -1f], sixteenBitStereo.Samples);
        Assert.AreEqual(TimeSpan.FromSeconds(2.0 / 44100), sixteenBitStereo.Duration);

        var eightBitMono = SoundEffect.FromWav(new MemoryStream(Wav(1, 1, 8000, 8, [128, 192, 0])));
        AssertSamples([0f, 0.5f, -1f], eightBitMono.Samples);

        var twentyFourBitMono = SoundEffect.FromWav(new MemoryStream(Wav(1, 1, 48000, 24, [0x00, 0x00, 0x40, 0x00, 0x00, 0x80])));
        AssertSamples([0.5f, -1f], twentyFourBitMono.Samples);

        var thirtyTwoBitMono = SoundEffect.FromWav(new MemoryStream(Wav(1, 1, 48000, 32, [0x00, 0x00, 0x00, 0xC0])));
        AssertSamples([-0.5f], thirtyTwoBitMono.Samples);

        var floatData = new byte[8];
        BinaryPrimitives.WriteSingleLittleEndian(floatData, 0.25f);
        BinaryPrimitives.WriteSingleLittleEndian(floatData.AsSpan(4), -0.75f);
        var floatMono = SoundEffect.FromWav(new MemoryStream(Wav(3, 1, 48000, 32, floatData)));
        AssertSamples([0.25f, -0.75f], floatMono.Samples);

        var extensible = SoundEffect.FromWav(new MemoryStream(Wav(0xFFFE, 1, 48000, 32, floatData, extensibleSubFormat: 3, leadingChunkSize: 3)));
        AssertSamples([0.25f, -0.75f], extensible.Samples);
    }

    [TestMethod]
    public void WavErrorsNameTheProblem()
    {
        Assert.ThrowsExactly<InvalidDataException>(() => SoundEffect.FromWav(new MemoryStream([1, 2, 3, 4])));
        Assert.ThrowsExactly<InvalidDataException>(() => SoundEffect.FromWav(new MemoryStream(Wav(1, 1, 48000, 16, [0, 0])[..36])));
        Assert.ThrowsExactly<NotSupportedException>(() => SoundEffect.FromWav(new MemoryStream(Wav(2, 1, 48000, 4, [0, 0]))));
        Assert.ThrowsExactly<NotSupportedException>(() => SoundEffect.FromWav(new MemoryStream(Wav(1, 6, 48000, 16, new byte[12]))));
    }

    [TestMethod]
    public void SilentDevicePullsTheMixerUntilTheSoundEnds()
    {
        var mixer = new AudioMixer();
        using var device = new AudioDevice(mixer, isSilent: true);
        Assert.IsTrue(device.IsSilent);
        Assert.AreSame(mixer, device.Mixer);

        mixer.Play(new SoundEffect(new float[AudioMixer.DefaultSampleRate / 20], AudioMixer.DefaultSampleRate, 1));
        var started = Stopwatch.GetTimestamp();
        while (mixer.PlayingCount > 0 && Stopwatch.GetElapsedTime(started) < TimeSpan.FromSeconds(10)) Thread.Sleep(5);
        Assert.AreEqual(0, mixer.PlayingCount);
    }

    private static void AssertSamples(ReadOnlySpan<float> expected, ReadOnlySpan<float> actual)
    {
        Assert.AreEqual(expected.Length, actual.Length);
        for (var index = 0; index < expected.Length; index++)
            Assert.AreEqual(expected[index], actual[index], Tolerance, $"Sample {index}");
    }

    private static byte[] Wav(ushort encoding, ushort channelCount, int sampleRate, ushort bitsPerSample, byte[] data, ushort extensibleSubFormat = 0, int leadingChunkSize = 0)
    {
        var formatSize = encoding == 0xFFFE ? 40 : 16;
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        writer.Write("RIFF"u8);
        writer.Write(0);
        writer.Write("WAVE"u8);
        writer.Write("fmt "u8);
        writer.Write(formatSize);
        writer.Write(encoding);
        writer.Write(channelCount);
        writer.Write(sampleRate);
        writer.Write(sampleRate * channelCount * bitsPerSample / 8);
        writer.Write((ushort)(channelCount * bitsPerSample / 8));
        writer.Write(bitsPerSample);
        if (encoding == 0xFFFE)
        {
            writer.Write((ushort)22);
            writer.Write(bitsPerSample);
            writer.Write(0);
            writer.Write(extensibleSubFormat);
            writer.Write(new byte[14]);
        }
        if (leadingChunkSize > 0)
        {
            writer.Write("LIST"u8);
            writer.Write(leadingChunkSize);
            writer.Write(new byte[leadingChunkSize + (leadingChunkSize & 1)]);
        }
        writer.Write("data"u8);
        writer.Write(data.Length);
        writer.Write(data);
        writer.Flush();
        var file = stream.ToArray();
        BinaryPrimitives.WriteInt32LittleEndian(file.AsSpan(4), file.Length - 8);
        return file;
    }
}
