using System.Diagnostics;
using Crystal.Audio;

namespace Crystal.Tests;

[TestClass]
public sealed class MusicTests
{
    private const float Tolerance = 1e-5f;
    private const string SineLoopPath = "Audio/SineLoop.ogg";
    private const int SineLoopFrameCount = 11025;
    private const int SineLoopStart = 2205;
    private const int SineLoopEnd = 8820;
    private const string NoisePath = "Audio/Noise.ogg";
    private const int NoiseFrameCount = 88200;

    [TestMethod]
    public void StreamedSourceLongerThanTheBufferPlaysGaplessAndStops()
    {
        const int frameCount = 60000;
        var mixer = new AudioMixer();
        using var music = new Music(mixer, new RampSource(frameCount, AudioMixer.DefaultSampleRate, 2), startsDecodeThread: false);
        music.Play();

        var output = new float[2000];
        var frame = 0;
        while (music.State == SoundState.Playing)
        {
            Pump(music);
            mixer.Render(output);
            for (var index = 0; index < output.Length / 2 && frame < frameCount; index++, frame++)
            {
                Assert.AreEqual(RampSource.ValueOf(frame), output[index * 2], Tolerance, $"Left of frame {frame}");
                Assert.AreEqual(-RampSource.ValueOf(frame), output[index * 2 + 1], Tolerance, $"Right of frame {frame}");
            }
        }
        Assert.AreEqual(frameCount, frame);
        Assert.AreEqual(SoundState.Stopped, music.State);
        Assert.AreEqual(0, mixer.PlayingCount);
    }

    [TestMethod]
    public void LoopPointsRepeatTheLoopBodyAfterTheIntro()
    {
        var mixer = new AudioMixer();
        using var music = new Music(mixer, new RampSource(100, AudioMixer.DefaultSampleRate, 1), startsDecodeThread: false)
        {
            IsLooped = true,
            LoopStart = 20,
            LoopEnd = 60,
        };
        music.Play();
        Pump(music);

        var output = new float[280];
        mixer.Render(output);
        for (var frame = 0; frame < 140; frame++)
        {
            var sourceFrame = frame < 60 ? frame : 20 + (frame - 60) % 40;
            Assert.AreEqual(RampSource.ValueOf(sourceFrame), output[frame * 2], Tolerance, $"Frame {frame}");
            Assert.AreEqual(output[frame * 2], output[frame * 2 + 1]);
        }
        Assert.AreEqual(SoundState.Playing, music.State);
    }

    [TestMethod]
    public void LoopWithoutPointsRepeatsTheWholeSourceAndTakesTheSourceLoopPoints()
    {
        var mixer = new AudioMixer();
        using var whole = new Music(mixer, new RampSource(50, AudioMixer.DefaultSampleRate, 1), startsDecodeThread: false) { IsLooped = true };
        Assert.AreEqual((0L, (long?)null), (whole.LoopStart, whole.LoopEnd));
        whole.Play();
        Pump(whole);
        var output = new float[240];
        mixer.Render(output);
        for (var frame = 0; frame < 120; frame++) Assert.AreEqual(RampSource.ValueOf(frame % 50), output[frame * 2], Tolerance, $"Frame {frame}");
        whole.Stop();

        using var tagged = new Music(mixer, new RampSource(50, AudioMixer.DefaultSampleRate, 1) { TaggedLoopStart = 10, TaggedLoopEnd = 30 }, startsDecodeThread: false);
        Assert.AreEqual((10L, (long?)30L), (tagged.LoopStart, tagged.LoopEnd));
        Assert.IsFalse(tagged.IsLooped);
    }

    [TestMethod]
    public void UnderrunIsSilentAndPlaybackContinuesOnceDataArrives()
    {
        var mixer = new AudioMixer();
        using var music = new Music(mixer, new RampSource(100, AudioMixer.DefaultSampleRate, 1), startsDecodeThread: false);
        music.Play();

        var output = new float[20];
        mixer.Render(output);
        foreach (var sample in output) Assert.AreEqual(0f, sample);
        Assert.AreEqual(SoundState.Playing, music.State);

        Pump(music);
        mixer.Render(output);
        for (var frame = 0; frame < 10; frame++) Assert.AreEqual(RampSource.ValueOf(frame), output[frame * 2], Tolerance);
    }

    [TestMethod]
    public void PauseKeepsThePositionAndStopRewinds()
    {
        var mixer = new AudioMixer();
        var source = new RampSource(1000, AudioMixer.DefaultSampleRate, 1);
        using var music = new Music(mixer, source, startsDecodeThread: false);
        var output = new float[20];

        music.Play();
        Pump(music);
        mixer.Render(output);
        music.Pause();
        Assert.AreEqual(SoundState.Paused, music.State);
        mixer.Render(output);
        Assert.AreEqual(0f, output[2]);

        music.Play();
        mixer.Render(output);
        Assert.AreEqual(RampSource.ValueOf(10), output[0], Tolerance);

        music.Stop();
        Assert.AreEqual(SoundState.Stopped, music.State);
        music.Play();
        Pump(music);
        mixer.Render(output);
        Assert.AreEqual(RampSource.ValueOf(0), output[0], Tolerance);
        Assert.AreEqual(RampSource.ValueOf(9), output[18], Tolerance);
        Assert.AreEqual(1, source.SeekCount);
    }

    [TestMethod]
    public void SourceAtHalfTheMixerRateIsInterpolatedAndVolumeRamps()
    {
        var mixer = new AudioMixer();
        using var music = new Music(mixer, new RampSource(1000, AudioMixer.DefaultSampleRate / 2, 1), startsDecodeThread: false);
        music.Play();
        Pump(music);

        var output = new float[16];
        mixer.Render(output);
        for (var frame = 0; frame < 8; frame++) Assert.AreEqual(RampSource.ValueOf(1) * frame / 2f, output[frame * 2], Tolerance, $"Frame {frame}");

        music.Volume = 0f;
        mixer.Render(output);
        for (var frame = 0; frame < 8; frame++)
            Assert.AreEqual(RampSource.ValueOf(1) * (frame + 8) / 2f * (1f - (frame + 1) / 8f), output[frame * 2], Tolerance, $"Ramped frame {frame}");
    }

    [TestMethod]
    public void RenderingMusicAllocatesNothing()
    {
        var mixer = new AudioMixer();
        using var music = new Music(mixer, new RampSource(100, AudioMixer.DefaultSampleRate, 2), startsDecodeThread: false) { IsLooped = true };
        music.Play();
        var output = new float[512];
        Pump(music);
        mixer.Render(output);

        var before = GC.GetAllocatedBytesForCurrentThread();
        for (var round = 0; round < 200; round++)
        {
            Pump(music);
            mixer.Render(output);
        }
        Assert.AreEqual(0, GC.GetAllocatedBytesForCurrentThread() - before);
    }

    [TestMethod]
    public void DecodeThreadFeedsASilentDeviceUntilTheMusicEnds()
    {
        var mixer = new AudioMixer();
        using var device = new AudioDevice(mixer, isSilent: true);
        using var music = new Music(mixer, new RampSource(AudioMixer.DefaultSampleRate / 5, AudioMixer.DefaultSampleRate, 2));
        music.Play();
        Assert.AreEqual(SoundState.Playing, music.State);
        WaitUntil(() => music.State == SoundState.Stopped);
        Assert.AreEqual(SoundState.Stopped, music.State);
        Assert.IsNull(music.Error);

        music.Play();
        WaitUntil(() => music.State == SoundState.Stopped);
        Assert.AreEqual(SoundState.Stopped, music.State);
    }

    [TestMethod]
    public void DecodeErrorEndsTheMusicAndIsReported()
    {
        var mixer = new AudioMixer();
        using var device = new AudioDevice(mixer, isSilent: true);
        using var music = new Music(mixer, new RampSource(1000, AudioMixer.DefaultSampleRate, 1) { FailsOnRead = true });
        music.Play();
        WaitUntil(() => music.State == SoundState.Stopped);
        Assert.AreEqual(SoundState.Stopped, music.State);
        Assert.IsInstanceOfType<InvalidDataException>(music.Error);
    }

    [TestMethod]
    public void InvalidArgumentsThrow()
    {
        var mixer = new AudioMixer();
        Assert.ThrowsExactly<ArgumentException>(() => new Music(mixer, new RampSource(10, AudioMixer.DefaultSampleRate, 6)));
        Assert.ThrowsExactly<ArgumentException>(() => new Music(mixer, new RampSource(10, 0, 1)));
        using var music = new Music(mixer, new RampSource(10, AudioMixer.DefaultSampleRate, 1), startsDecodeThread: false);
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => music.Volume = -1f);
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => music.LoopStart = -1);
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => music.LoopEnd = 0);
        music.Dispose();
        Assert.ThrowsExactly<ObjectDisposedException>(music.Play);
    }

    [TestMethod]
    public void VorbisSourceDecodesSeeksAndReadsLoopTags()
    {
        using var source = new VorbisAudioSource(SineLoopPath);
        Assert.AreEqual((44100, 2, (long)SineLoopFrameCount), (source.SampleRate, source.ChannelCount, source.FrameCount));
        Assert.AreEqual(((long)SineLoopStart, (long?)SineLoopEnd), (source.LoopStart, source.LoopEnd));

        var decoded = ReadAll(source);
        Assert.HasCount(SineLoopFrameCount * 2, decoded);
        for (var frame = 0; frame < SineLoopFrameCount; frame++)
        {
            var seconds = frame / 44100.0;
            Assert.AreEqual(0.5 * Math.Sin(2 * Math.PI * 441 * seconds), decoded[frame * 2], 0.05, $"Left of frame {frame}");
            Assert.AreEqual(0.25 * Math.Sin(2 * Math.PI * 882 * seconds), decoded[frame * 2 + 1], 0.05, $"Right of frame {frame}");
        }

        source.Seek(SineLoopStart);
        var afterSeek = new float[2000];
        Assert.AreEqual(1000, ReadFully(source, afterSeek));
        for (var index = 0; index < afterSeek.Length; index++)
            Assert.AreEqual(decoded[SineLoopStart * 2 + index], afterSeek[index], 1e-4f, $"Sample {index} after the seek");

        source.Seek(0);
        var sound = SoundEffect.FromSource(source);
        Assert.AreEqual((44100, 2, SineLoopFrameCount), (sound.SampleRate, sound.ChannelCount, sound.FrameCount));
    }

    [TestMethod]
    public void VorbisSourceSeeksToTheExactFrameAcrossPages()
    {
        using var source = new VorbisAudioSource(NoisePath);
        Assert.AreEqual((22050, 1, (long)NoiseFrameCount), (source.SampleRate, source.ChannelCount, source.FrameCount));
        Assert.AreEqual((0L, (long?)null), (source.LoopStart, source.LoopEnd));
        var decoded = ReadAll(source);
        Assert.HasCount(NoiseFrameCount, decoded);

        var random = new Random(5);
        var afterSeek = new float[3000];
        long[] edgeFrames = [0, 1, 100, NoiseFrameCount / 2, NoiseFrameCount - 3000, NoiseFrameCount - 100, NoiseFrameCount - 1, NoiseFrameCount, NoiseFrameCount + 500];
        foreach (var frame in edgeFrames.Concat(Enumerable.Range(0, 200).Select(_ => random.NextInt64(NoiseFrameCount))))
        {
            source.Seek(frame);
            var expectedCount = (int)Math.Clamp(NoiseFrameCount - frame, 0, afterSeek.Length);
            Assert.AreEqual(expectedCount, ReadFully(source, afterSeek), $"Frames read after seeking to {frame}");
            Assert.IsTrue(afterSeek.AsSpan(0, expectedCount).SequenceEqual(decoded.AsSpan((int)Math.Min(frame, NoiseFrameCount), expectedCount)), $"Samples after seeking to {frame}");
        }
    }

    [TestMethod]
    public void VorbisMusicLoopsAtTheTaggedPoints()
    {
        float[] decoded;
        using (var source = new VorbisAudioSource(SineLoopPath)) decoded = ReadAll(source);

        var mixer = new AudioMixer(44100);
        using var music = new Music(mixer, new VorbisAudioSource(SineLoopPath), startsDecodeThread: false) { IsLooped = true };
        music.Play();

        var output = new float[2000];
        var frame = 0;
        while (frame < SineLoopEnd + 2 * (SineLoopEnd - SineLoopStart))
        {
            Pump(music);
            mixer.Render(output);
            for (var index = 0; index < output.Length / 2; index++, frame++)
            {
                var sourceFrame = frame < SineLoopEnd ? frame : SineLoopStart + (frame - SineLoopEnd) % (SineLoopEnd - SineLoopStart);
                Assert.AreEqual(decoded[sourceFrame * 2], output[index * 2], 1e-4f, $"Left of frame {frame}");
                Assert.AreEqual(decoded[sourceFrame * 2 + 1], output[index * 2 + 1], 1e-4f, $"Right of frame {frame}");
            }
        }
    }

    [TestMethod]
    public void VorbisSourceRejectsOtherData()
    {
        Assert.ThrowsExactly<InvalidDataException>(() => new VorbisAudioSource(new MemoryStream(new byte[256])));
    }

    private static void Pump(Music music)
    {
        while (music.FillBuffer())
        {
        }
    }

    private static void WaitUntil(Func<bool> condition)
    {
        var started = Stopwatch.GetTimestamp();
        while (!condition() && Stopwatch.GetElapsedTime(started) < TimeSpan.FromSeconds(10)) Thread.Sleep(5);
    }

    private static float[] ReadAll(AudioSource source)
    {
        var samples = new List<float>();
        var chunk = new float[4096];
        while (source.Read(chunk) is var frameCount && frameCount > 0) samples.AddRange(chunk.AsSpan(0, frameCount * source.ChannelCount));
        return [.. samples];
    }

    private static int ReadFully(AudioSource source, Span<float> samples)
    {
        var frameCount = 0;
        while (frameCount * source.ChannelCount < samples.Length)
        {
            var read = source.Read(samples[(frameCount * source.ChannelCount)..]);
            if (read == 0) break;
            frameCount += read;
        }
        return frameCount;
    }

    private sealed class RampSource(int frameCount, int sampleRate, int channelCount) : AudioSource
    {
        private long _frame;

        public override int SampleRate => sampleRate;
        public override int ChannelCount => channelCount;
        public override long LoopStart => TaggedLoopStart;
        public override long? LoopEnd => TaggedLoopEnd;
        public long TaggedLoopStart { get; init; }
        public long? TaggedLoopEnd { get; init; }
        public bool FailsOnRead { get; init; }
        public int SeekCount { get; private set; }

        public static float ValueOf(long frame) => frame / 100000f;

        public override int Read(Span<float> samples)
        {
            if (FailsOnRead) throw new InvalidDataException("The test source is broken.");
            var count = (int)Math.Min(samples.Length / channelCount, frameCount - _frame);
            for (var index = 0; index < count; index++, _frame++)
            {
                samples[index * channelCount] = ValueOf(_frame);
                if (channelCount == 2) samples[index * 2 + 1] = -ValueOf(_frame);
            }
            return count;
        }

        public override void Seek(long frame)
        {
            _frame = frame;
            SeekCount++;
        }
    }
}
