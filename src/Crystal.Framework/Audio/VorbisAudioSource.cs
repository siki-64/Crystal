using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using StbVorbisSharp;

namespace Crystal.Audio;

public sealed unsafe class VorbisAudioSource : AudioSource
{
    private readonly byte[] _pinnedData;
    private readonly StbVorbis.stb_vorbis _decoder;
    private readonly int _sampleRate;
    private readonly int _channelCount;
    private long _frame;
    private bool _isClosed;

    public VorbisAudioSource(string path) : this(File.OpenRead(path))
    {
    }

    public VorbisAudioSource(Stream stream, bool leaveOpen = false)
    {
        ArgumentNullException.ThrowIfNull(stream);
        try
        {
            _pinnedData = ReadPinned(stream);
        }
        finally
        {
            if (!leaveOpen) stream.Dispose();
        }

        int error;
        var decoder = _pinnedData.Length == 0 ? null : StbVorbis.stb_vorbis_open_memory(DataPointer, _pinnedData.Length, &error);
        if (decoder is null)
        {
            GC.SuppressFinalize(this);
            throw new InvalidDataException("The data is not an Ogg Vorbis stream.");
        }

        var info = StbVorbis.stb_vorbis_get_info(decoder);
        if (info.channels is not (1 or 2))
        {
            StbVorbis.stb_vorbis_close(decoder);
            GC.SuppressFinalize(this);
            throw new NotSupportedException($"Ogg Vorbis streams with {info.channels} channels are not supported; use mono or stereo.");
        }

        _decoder = decoder;
        _sampleRate = (int)info.sample_rate;
        _channelCount = info.channels;
        FrameCount = StbVorbis.stb_vorbis_stream_length_in_samples(decoder);
        LoopStart = TagFrame("LOOPSTART") ?? 0;
        LoopEnd = TagFrame("LOOPEND") ?? (TagFrame("LOOPLENGTH") is { } loopLength ? LoopStart + loopLength : null);
    }

    ~VorbisAudioSource() => Dispose(disposing: false);

    public override int SampleRate => _sampleRate;
    public override int ChannelCount => _channelCount;
    public override long LoopStart { get; }
    public override long? LoopEnd { get; }
    public long FrameCount { get; }

    private byte* DataPointer => (byte*)Unsafe.AsPointer(ref MemoryMarshal.GetArrayDataReference(_pinnedData));

    public override int Read(Span<float> samples)
    {
        ObjectDisposedException.ThrowIf(_isClosed, this);
        if (samples.Length % _channelCount != 0) throw new ArgumentException("The span holds whole frames.", nameof(samples));

        var wanted = (int)Math.Min(samples.Length / _channelCount, FrameCount - _frame);
        var read = 0;
        fixed (float* output = samples)
        {
            while (read < wanted)
            {
                var decoded = StbVorbis.stb_vorbis_get_samples_float_interleaved(_decoder, _channelCount, output + read * _channelCount, (wanted - read) * _channelCount);
                if (decoded <= 0) break;
                read += decoded;
            }
        }
        _frame += read;
        return read;
    }

    public override void Seek(long frame)
    {
        ObjectDisposedException.ThrowIf(_isClosed, this);
        ArgumentOutOfRangeException.ThrowIfNegative(frame);
        var target = Math.Min(frame, FrameCount);
        if (StbVorbis.stb_vorbis_seek(_decoder, (uint)target) == 0)
            throw new InvalidDataException($"The Ogg Vorbis stream can't be positioned at frame {target}.");
        _frame = target;
    }

    protected override void Dispose(bool disposing)
    {
        if (_isClosed) return;
        _isClosed = true;
        StbVorbis.stb_vorbis_close(_decoder);
    }

    private static byte[] ReadPinned(Stream stream)
    {
        using var buffer = new MemoryStream();
        stream.CopyTo(buffer);
        var pinned = GC.AllocateUninitializedArray<byte>((int)buffer.Length, pinned: true);
        buffer.GetBuffer().AsSpan(0, pinned.Length).CopyTo(pinned);
        return pinned;
    }

    private long? TagFrame(string name)
    {
        foreach (var comment in _decoder.comment_list ?? [])
        {
            if (comment.Length <= name.Length || comment[name.Length] != '=' || !comment.StartsWith(name, StringComparison.OrdinalIgnoreCase)) continue;
            if (long.TryParse(comment.AsSpan(name.Length + 1), NumberStyles.None, CultureInfo.InvariantCulture, out var frame)) return frame;
        }
        return null;
    }
}
