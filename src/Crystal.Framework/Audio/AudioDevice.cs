using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Crystal.Platform;

namespace Crystal.Audio;

public sealed class AudioException(string message) : Exception(message);

public sealed unsafe class AudioDevice : IDisposable
{
    private const int RenderFrameCount = 1024;
    private const string SilentEnvironmentVariable = "CRYSTAL_AUDIO_SILENT";

    private readonly float[] _renderBuffer = new float[RenderFrameCount * AudioMixer.ChannelCount];
    private GCHandle _handle;
    private SdlAudioStream _stream;

    public AudioDevice(AudioMixer mixer, bool isSilent = false)
    {
        ArgumentNullException.ThrowIfNull(mixer);
        Mixer = mixer;
        IsSilent = isSilent || Environment.GetEnvironmentVariable(SilentEnvironmentVariable) is "1" or "true";

        SdlRuntime.Acquire();
        Sdl.SetHint(Sdl.AudioDriverHint, IsSilent ? Sdl.DummyAudioDriver : string.Empty);
        if (!Sdl.InitSubSystem(Sdl.InitAudio))
        {
            var failure = new AudioException($"SDL_InitSubSystem(SDL_INIT_AUDIO) failed: {Sdl.GetError()}");
            SdlRuntime.Release();
            throw failure;
        }

        _handle = GCHandle.Alloc(this);
        var specification = new SdlAudioSpecification
        {
            Format = Sdl.AudioFormatFloat32,
            ChannelCount = AudioMixer.ChannelCount,
            SampleRate = mixer.SampleRate,
        };
        _stream = Sdl.OpenAudioDeviceStream(Sdl.DefaultPlaybackAudioDevice, &specification, &Fill, (void*)GCHandle.ToIntPtr(_handle));
        if (_stream.IsNull || !Sdl.ResumeAudioStreamDevice(_stream))
        {
            var failure = new AudioException($"The default audio playback device could not be opened: {Sdl.GetError()}");
            Dispose();
            throw failure;
        }
    }

    public AudioMixer Mixer { get; }
    public bool IsSilent { get; }

    public void Dispose()
    {
        if (!_handle.IsAllocated) return;
        if (!_stream.IsNull) Sdl.DestroyAudioStream(_stream);
        _stream = default;
        _handle.Free();
        Sdl.QuitSubSystem(Sdl.InitAudio);
        SdlRuntime.Release();
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void Fill(void* userData, nint stream, int additionalByteCount, int totalByteCount)
    {
        var device = (AudioDevice)GCHandle.FromIntPtr((nint)userData).Target!;
        var remainingFrameCount = additionalByteCount / (sizeof(float) * AudioMixer.ChannelCount);
        fixed (float* buffer = device._renderBuffer)
        {
            while (remainingFrameCount > 0)
            {
                var frameCount = Math.Min(remainingFrameCount, RenderFrameCount);
                var sampleCount = frameCount * AudioMixer.ChannelCount;
                device.Mixer.Render(device._renderBuffer.AsSpan(0, sampleCount));
                if (!Sdl.PutAudioStreamData(new SdlAudioStream(stream), buffer, sampleCount * sizeof(float))) return;
                remainingFrameCount -= frameCount;
            }
        }
    }
}
