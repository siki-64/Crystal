using System.Numerics;
using System.Runtime.CompilerServices;

namespace Crystal.Input;

public struct KeyboardState : IEquatable<KeyboardState>
{
    public const int KeyCount = 512;

    private KeyBits _bits;

    public KeyboardState(params ReadOnlySpan<Keys> pressedKeys)
    {
        foreach (var key in pressedKeys) SetKey(key, true);
    }

    public readonly bool IsKeyDown(Keys key) => IsValid(key) && (_bits[(int)key >> 6] & (1UL << ((int)key & 63))) != 0;

    public readonly bool IsKeyUp(Keys key) => !IsKeyDown(key);

    public readonly int GetPressedKeyCount()
    {
        var count = 0;
        foreach (var word in _bits) count += BitOperations.PopCount(word);
        return count;
    }

    public readonly int GetPressedKeys(Span<Keys> destination)
    {
        var written = 0;
        for (var wordIndex = 0; wordIndex < KeyBits.Length; wordIndex++)
        {
            var word = _bits[wordIndex];
            while (word != 0)
            {
                if (written == destination.Length) return written;
                destination[written++] = (Keys)(wordIndex * 64 + BitOperations.TrailingZeroCount(word));
                word &= word - 1;
            }
        }
        return written;
    }

    public readonly bool Equals(KeyboardState other) => ((ReadOnlySpan<ulong>)_bits).SequenceEqual(other._bits);

    public override readonly bool Equals(object? obj) => obj is KeyboardState other && Equals(other);

    public override readonly int GetHashCode()
    {
        var hash = new HashCode();
        foreach (var word in _bits) hash.Add(word);
        return hash.ToHashCode();
    }

    public static bool operator ==(KeyboardState left, KeyboardState right) => left.Equals(right);

    public static bool operator !=(KeyboardState left, KeyboardState right) => !left.Equals(right);

    internal void SetKey(Keys key, bool down)
    {
        if (!IsValid(key)) return;
        var mask = 1UL << ((int)key & 63);
        if (down) _bits[(int)key >> 6] |= mask;
        else _bits[(int)key >> 6] &= ~mask;
    }

    private static bool IsValid(Keys key) => (uint)key < KeyCount;

    [InlineArray(Length)]
    private struct KeyBits
    {
        public const int Length = KeyCount / 64;

        private ulong _element;
    }
}
