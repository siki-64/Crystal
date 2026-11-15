# Crystal.Audio

A software mixer, the sounds it plays, and the device that sends its output to the speakers. The mixer works without a device, so its output can be rendered into a buffer and checked.

## AudioMixer
`public sealed class AudioMixer`

Mixes every playing sound into interleaved stereo 32-bit float frames. All members are safe to call from any thread.

| Member | Meaning |
|---|---|
| `const int ChannelCount = 2` | The mixer's output is stereo. |
| `const int DefaultSampleRate = 48000` | The default output rate in frames per second. |
| `AudioMixer(int sampleRate = DefaultSampleRate)` | Throws `ArgumentOutOfRangeException` for zero or negative rates. |
| `int SampleRate { get; }` | The output rate in frames per second. |
| `int PlayingCount { get; }` | The number of sounds playing now, counting both `Play` calls and instances. |
| `float MasterVolume { get; set; }` | Scales the whole mix. `1` by default. A change ramps across the next `Render`. Throws `ArgumentOutOfRangeException` for negative, infinite or NaN values. |
| `void Play(SoundEffect sound, float volume = 1f, float pan = 0f, float pitch = 0f)` | Plays the sound once from the start, with no handle to it. The arguments follow the rules of `SoundEffectInstance.Volume`, `Pan` and `Pitch`. |
| `void Render(Span<float> output)` | Overwrites `output` with the next frames of the mix, left sample first, clamped to -1 to 1. Advances every playing sound. Throws `ArgumentException` for an odd length. An `AudioDevice` calls it on the audio thread; call it yourself only on a mixer without a device. |

**Resampling.** A sound whose rate differs from the mixer's, or that plays with a pitch, is resampled with linear interpolation.

**Allocations.** Once as many sounds have played at the same time as will ever play, `Play`, `Render` and the instance members allocate no managed memory.

## SoundEffect
`public sealed class SoundEffect`

Decoded sound data held in memory. It is immutable, belongs to no mixer, and can be created on any thread.

| Member | Meaning |
|---|---|
| `SoundEffect(ReadOnlySpan<float> samples, int sampleRate, int channelCount)` | Copies interleaved float samples. `channelCount` is 1 or 2. Throws `ArgumentOutOfRangeException` for other channel counts and for zero or negative rates, and `ArgumentException` when the sample count isn't a multiple of the channel count. |
| `static SoundEffect FromWav(Stream stream)` | Reads a RIFF WAVE file from the stream's current position to its end. |
| `static SoundEffect FromWav(string path)` | Reads a RIFF WAVE file from disk. |
| `int SampleRate { get; }` | Frames per second. |
| `int ChannelCount { get; }` | 1 or 2. |
| `int FrameCount { get; }` | The length in frames. |
| `TimeSpan Duration { get; }` | The length at normal pitch. |

**WAVE files.** Mono or stereo; 8, 16, 24 or 32-bit integer PCM, or 32 or 64-bit float, also in a `WAVE_FORMAT_EXTENSIBLE` header. `FromWav` throws `InvalidDataException` when the data isn't a WAVE file or lacks a format or data chunk, and `NotSupportedException` for other encodings and channel counts.

## SoundEffectInstance
`public sealed class SoundEffectInstance`

One controllable playback of a `SoundEffect`. All members are safe to call from any thread.

| Member | Meaning |
|---|---|
| `SoundEffectInstance(AudioMixer mixer, SoundEffect sound)` | An instance in the `Stopped` state. |
| `SoundEffect Sound { get; }` | The sound it plays. |
| `float Volume { get; set; }` | Gain, `1` by default. `0` is silent and values above `1` amplify. Throws `ArgumentOutOfRangeException` for negative, infinite or NaN values. |
| `float Pan { get; set; }` | -1 (left) to 1 (right), `0` by default. Panning right fades the left channel out and leaves the right channel as it is, and the other way round. Throws `ArgumentOutOfRangeException` outside that range. |
| `float Pitch { get; set; }` | Octaves, `0` by default: `1` plays at double speed, `-1` at half speed. Throws `ArgumentOutOfRangeException` for infinite or NaN values. |
| `bool IsLooped { get; set; }` | `true` starts over at the end instead of stopping. |
| `SoundState State { get; }` | `Stopped`, `Playing` or `Paused`. Becomes `Stopped` when a sound that isn't looped reaches its end. |
| `void Play()` | Starts from the beginning when stopped and resumes when paused. Does nothing while playing. |
| `void Pause()` | Keeps the position. Does nothing unless playing. |
| `void Stop()` | Stops and rewinds. |

**Ramps.** Changes to `Volume` and `Pan` of a playing instance ramp across the next `Render`, so they don't click. `Pause` and `Stop` cut the sound off at once.

## SoundState
`public enum SoundState`

`Stopped`, `Playing`, `Paused`.

## AudioDevice
`public sealed class AudioDevice : IDisposable`

Opens the system's default playback device and feeds it from a mixer. It follows the default device when the user changes it.

| Member | Meaning |
|---|---|
| `AudioDevice(AudioMixer mixer, bool isSilent = false)` | Opens the device and starts playback. `isSilent` uses a driver that discards the audio at the right pace, for tests and machines without a sound card; `CRYSTAL_AUDIO_SILENT=1` does the same. The choice applies to the first device opened; devices opened while it is alive share its driver. Throws `AudioException` when no device can be opened. |
| `AudioMixer Mixer { get; }` | The mixer it plays. |
| `bool IsSilent { get; }` | Whether the silent driver was requested. |
| `void Dispose()` | Stops playback and closes the device. The mixer keeps its playing sounds. |

**Threads.** The device calls `AudioMixer.Render` on an audio thread that SDL owns. Don't call `Render` on a mixer that has a device.

## AudioException
`public sealed class AudioException : Exception`

Thrown by `AudioDevice` when the audio subsystem or the playback device can't be opened.
