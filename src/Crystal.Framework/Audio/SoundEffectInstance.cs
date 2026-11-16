namespace Crystal.Audio;

public enum SoundState
{
    Stopped,
    Playing,
    Paused,
}

public sealed class SoundEffectInstance : IAudioVoice
{
    private readonly AudioMixer _mixer;
    private SoundEffect _sound;
    private float _volume = 1f;
    private float _pan;
    private float _pitch;
    private double _position;
    private float _leftGain;
    private float _rightGain;
    private bool _hasGains;

    public SoundEffectInstance(AudioMixer mixer, SoundEffect sound) : this(mixer, sound, isPooled: false)
    {
    }

    internal SoundEffectInstance(AudioMixer mixer, SoundEffect sound, bool isPooled)
    {
        ArgumentNullException.ThrowIfNull(mixer);
        ArgumentNullException.ThrowIfNull(sound);
        _mixer = mixer;
        _sound = sound;
        IsPooled = isPooled;
    }

    public SoundEffect Sound => _sound;
    public bool IsLooped { get; set; }
    public SoundState State { get; private set; }

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

    public float Pan
    {
        get => _pan;
        set
        {
            if (!(value >= -1f && value <= 1f)) throw new ArgumentOutOfRangeException(nameof(value), value, "The pan is between -1 and 1.");
            _pan = value;
        }
    }

    public float Pitch
    {
        get => _pitch;
        set
        {
            if (!float.IsFinite(value)) throw new ArgumentOutOfRangeException(nameof(value), value, "The pitch is a finite number of octaves.");
            _pitch = value;
        }
    }

    internal bool IsPooled { get; }

    public void Play() => _mixer.Start(this);

    public void Pause() => _mixer.Pause(this);

    public void Stop() => _mixer.Stop(this);

    internal void Reuse(SoundEffect sound, float volume, float pan, float pitch)
    {
        _sound = sound;
        Volume = volume;
        Pan = pan;
        Pitch = pitch;
    }

    void IAudioVoice.OnStarted() => _hasGains = false;

    void IAudioVoice.OnStopped() => _position = 0;

    bool IAudioVoice.MixInto(Span<float> output, int outputSampleRate)
    {
        var sound = _sound;
        var samples = sound.Samples;
        var frameCount = sound.FrameCount;
        if (frameCount == 0) return false;

        var isStereo = sound.ChannelCount == 2;
        var isLooped = IsLooped;
        var step = (double)sound.SampleRate / outputSampleRate * Math.Pow(2.0, _pitch);
        var volume = _volume;
        var pan = _pan;
        var targetLeftGain = volume * MathF.Min(1f, 1f - pan);
        var targetRightGain = volume * MathF.Min(1f, 1f + pan);
        if (!_hasGains)
        {
            _leftGain = targetLeftGain;
            _rightGain = targetRightGain;
            _hasGains = true;
        }

        var outputFrameCount = output.Length / AudioMixer.ChannelCount;
        var leftGainStep = (targetLeftGain - _leftGain) / outputFrameCount;
        var rightGainStep = (targetRightGain - _rightGain) / outputFrameCount;
        var position = _position;
        var leftGain = _leftGain;
        var rightGain = _rightGain;

        for (var frame = 0; frame < outputFrameCount; frame++)
        {
            if (position >= frameCount)
            {
                if (!isLooped) break;
                position %= frameCount;
            }

            var index = (int)position;
            var fraction = (float)(position - index);
            var next = index + 1;
            if (next == frameCount) next = isLooped ? 0 : index;

            float left;
            float right;
            if (isStereo)
            {
                left = samples[index * 2] + (samples[next * 2] - samples[index * 2]) * fraction;
                right = samples[index * 2 + 1] + (samples[next * 2 + 1] - samples[index * 2 + 1]) * fraction;
            }
            else
            {
                left = right = samples[index] + (samples[next] - samples[index]) * fraction;
            }

            leftGain += leftGainStep;
            rightGain += rightGainStep;
            output[frame * 2] += left * leftGain;
            output[frame * 2 + 1] += right * rightGain;
            position += step;
        }

        _position = position;
        _leftGain = targetLeftGain;
        _rightGain = targetRightGain;
        return isLooped || position < frameCount;
    }
}
