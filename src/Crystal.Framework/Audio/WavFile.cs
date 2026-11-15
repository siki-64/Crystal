using System.Buffers.Binary;

namespace Crystal.Audio;

internal static class WavFile
{
    private const uint RiffTag = 0x46464952;
    private const uint WaveTag = 0x45564157;
    private const uint FormatTag = 0x20746D66;
    private const uint DataTag = 0x61746164;

    private const ushort IntegerEncoding = 1;
    private const ushort FloatEncoding = 3;
    private const ushort ExtensibleEncoding = 0xFFFE;
    private const int ExtensibleFormatSize = 40;
    private const int ExtensibleSubFormatOffset = 24;

    public static SoundEffect Read(Stream stream)
    {
        using var memory = new MemoryStream();
        stream.CopyTo(memory);
        return Read(memory.GetBuffer().AsSpan(0, (int)memory.Length));
    }

    public static SoundEffect Read(ReadOnlySpan<byte> file)
    {
        if (file.Length < 12 || ReadUInt32(file) != RiffTag || ReadUInt32(file[8..]) != WaveTag)
            throw new InvalidDataException("The data is not a RIFF WAVE file.");

        var hasFormat = false;
        ushort encoding = 0;
        var channelCount = 0;
        var sampleRate = 0;
        var bitsPerSample = 0;

        var offset = 12;
        while (offset + 8 <= file.Length)
        {
            var tag = ReadUInt32(file[offset..]);
            var size = ReadUInt32(file[(offset + 4)..]);
            var body = file[(offset + 8)..];
            if (tag == FormatTag)
            {
                if (size < 16 || body.Length < size) throw new InvalidDataException("The WAVE format chunk is truncated.");
                encoding = BinaryPrimitives.ReadUInt16LittleEndian(body);
                channelCount = BinaryPrimitives.ReadUInt16LittleEndian(body[2..]);
                sampleRate = (int)ReadUInt32(body[4..]);
                bitsPerSample = BinaryPrimitives.ReadUInt16LittleEndian(body[14..]);
                if (encoding == ExtensibleEncoding)
                {
                    if (size < ExtensibleFormatSize) throw new InvalidDataException("The WAVE extensible format chunk is truncated.");
                    encoding = BinaryPrimitives.ReadUInt16LittleEndian(body[ExtensibleSubFormatOffset..]);
                }
                hasFormat = true;
            }
            else if (tag == DataTag)
            {
                if (!hasFormat) throw new InvalidDataException("The WAVE data chunk comes before the format chunk.");
                var data = body[..(int)Math.Min(size, (uint)body.Length)];
                return new SoundEffect(Decode(data, encoding, channelCount, bitsPerSample), sampleRate, channelCount);
            }

            var next = offset + 8L + size + (size & 1);
            if (next > file.Length) break;
            offset = (int)next;
        }

        throw new InvalidDataException("The WAVE file has no data chunk.");
    }

    private static float[] Decode(ReadOnlySpan<byte> data, ushort encoding, int channelCount, int bitsPerSample)
    {
        if (channelCount is not (1 or 2)) throw new NotSupportedException($"WAVE files with {channelCount} channels are not supported; use mono or stereo.");
        var isSupported = encoding switch
        {
            IntegerEncoding => bitsPerSample is 8 or 16 or 24 or 32,
            FloatEncoding => bitsPerSample is 32 or 64,
            _ => false,
        };
        if (!isSupported) throw new NotSupportedException($"WAVE encoding {encoding} with {bitsPerSample} bits per sample is not supported.");

        var bytesPerSample = bitsPerSample / 8;
        var sampleCount = data.Length / bytesPerSample;
        sampleCount -= sampleCount % channelCount;
        var samples = new float[sampleCount];
        for (var index = 0; index < sampleCount; index++)
        {
            var sample = data[(index * bytesPerSample)..];
            samples[index] = (encoding, bitsPerSample) switch
            {
                (IntegerEncoding, 8) => (sample[0] - 128) / 128f,
                (IntegerEncoding, 16) => BinaryPrimitives.ReadInt16LittleEndian(sample) / 32768f,
                (IntegerEncoding, 24) => ((sample[0] << 8 | sample[1] << 16 | sample[2] << 24) >> 8) / 8388608f,
                (IntegerEncoding, _) => BinaryPrimitives.ReadInt32LittleEndian(sample) / 2147483648f,
                (_, 32) => BinaryPrimitives.ReadSingleLittleEndian(sample),
                _ => (float)BinaryPrimitives.ReadDoubleLittleEndian(sample),
            };
        }
        return samples;
    }

    private static uint ReadUInt32(ReadOnlySpan<byte> bytes) => BinaryPrimitives.ReadUInt32LittleEndian(bytes);
}
