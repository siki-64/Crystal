namespace Crystal.Audio;

internal interface IAudioVoice
{
    SoundState State { get; set; }

    void OnStarted();

    void OnStopped();

    bool MixInto(Span<float> output, int outputSampleRate);
}
