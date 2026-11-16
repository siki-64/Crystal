namespace Crystal.Audio;

public abstract class AudioSource : IDisposable
{
    public abstract int SampleRate { get; }
    public abstract int ChannelCount { get; }
    public virtual long LoopStart => 0;
    public virtual long? LoopEnd => null;

    public abstract int Read(Span<float> samples);

    public abstract void Seek(long frame);

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
    }
}
