using System.Numerics;
using System.Text;

namespace Crystal.Graphics;

public readonly record struct SpriteFontGlyph(char Character, Rectangle BoundsInTexture, Rectangle Cropping, float LeftSideBearing, float Width, float RightSideBearing);

public sealed class SpriteFont
{
    private const int DirectLookupCount = 128;

    private readonly SpriteFontGlyph[] _glyphs;
    private readonly char[] _characters;
    private readonly int[] _directLookup = new int[DirectLookupCount];
    private char? _defaultCharacter;

    public SpriteFont(Texture2D texture, ReadOnlySpan<SpriteFontGlyph> glyphs, int lineSpacing, float spacing = 0f, char? defaultCharacter = null)
    {
        ArgumentNullException.ThrowIfNull(texture);
        if (glyphs.IsEmpty) throw new ArgumentException("A font needs at least one glyph.", nameof(glyphs));

        _glyphs = glyphs.ToArray();
        Array.Sort(_glyphs, static (left, right) => left.Character.CompareTo(right.Character));
        _characters = new char[_glyphs.Length];
        Array.Fill(_directLookup, -1);
        for (var index = 0; index < _glyphs.Length; index++)
        {
            var glyph = _glyphs[index];
            if (index > 0 && glyph.Character == _characters[index - 1])
                throw new ArgumentException($"The character '{glyph.Character}' has more than one glyph.", nameof(glyphs));
            var bounds = glyph.BoundsInTexture;
            if (bounds.X < 0 || bounds.Y < 0 || bounds.Width < 0 || bounds.Height < 0 || bounds.Right > texture.Width || bounds.Bottom > texture.Height)
                throw new ArgumentException($"The glyph of '{glyph.Character}' lies outside the {texture.Width}x{texture.Height} texture.", nameof(glyphs));
            _characters[index] = glyph.Character;
            if (glyph.Character < DirectLookupCount) _directLookup[glyph.Character] = index;
        }

        Texture = texture;
        LineSpacing = lineSpacing;
        Spacing = spacing;
        DefaultCharacter = defaultCharacter;
    }

    public Texture2D Texture { get; }
    public int LineSpacing { get; set; }
    public float Spacing { get; set; }
    public IReadOnlyList<char> Characters => _characters;
    public IReadOnlyList<SpriteFontGlyph> Glyphs => _glyphs;

    public char? DefaultCharacter
    {
        get => _defaultCharacter;
        set
        {
            if (value is { } character && FindGlyph(character) < 0)
                throw new ArgumentException($"The font has no glyph for the default character '{character}'.", nameof(value));
            _defaultCharacter = value;
        }
    }

    public bool TryGetGlyph(char character, out SpriteFontGlyph glyph)
    {
        var index = FindGlyph(character);
        glyph = index >= 0 ? _glyphs[index] : default;
        return index >= 0;
    }

    public Vector2 MeasureString(ReadOnlySpan<char> text)
    {
        var measurer = new TextMeasurer(this);
        measurer.Append(text);
        return measurer.Size;
    }

    public Vector2 MeasureString(StringBuilder text)
    {
        ArgumentNullException.ThrowIfNull(text);
        var measurer = new TextMeasurer(this);
        foreach (var chunk in text.GetChunks()) measurer.Append(chunk.Span);
        return measurer.Size;
    }

    internal ref readonly SpriteFontGlyph GetGlyph(char character)
    {
        var index = FindGlyph(character);
        if (index < 0 && _defaultCharacter is { } fallback) index = FindGlyph(fallback);
        if (index < 0) throw new ArgumentException($"The font has no glyph for '{character}' (U+{(int)character:X4}) and no DefaultCharacter.", "text");
        return ref _glyphs[index];
    }

    private int FindGlyph(char character)
    {
        if (character < DirectLookupCount) return _directLookup[character];
        var index = Array.BinarySearch(_characters, character);
        return index >= 0 ? index : -1;
    }

    private struct TextMeasurer(SpriteFont font)
    {
        private float _width;
        private float _finalLineHeight = font.LineSpacing;
        private Vector2 _offset;
        private bool _firstGlyphOfLine = true;
        private bool _isEmpty = true;

        public readonly Vector2 Size => _isEmpty ? Vector2.Zero : new Vector2(_width, _offset.Y + _finalLineHeight);

        public void Append(ReadOnlySpan<char> text)
        {
            foreach (var character in text)
            {
                _isEmpty = false;
                if (character == '\r') continue;
                if (character == '\n')
                {
                    _finalLineHeight = font.LineSpacing;
                    _offset.X = 0f;
                    _offset.Y += font.LineSpacing;
                    _firstGlyphOfLine = true;
                    continue;
                }

                ref readonly var glyph = ref font.GetGlyph(character);
                if (_firstGlyphOfLine)
                {
                    _offset.X = MathF.Max(glyph.LeftSideBearing, 0f);
                    _firstGlyphOfLine = false;
                }
                else
                {
                    _offset.X += font.Spacing + glyph.LeftSideBearing;
                }
                _offset.X += glyph.Width;
                _width = MathF.Max(_width, _offset.X + MathF.Max(glyph.RightSideBearing, 0f));
                _offset.X += glyph.RightSideBearing;
                _finalLineHeight = MathF.Max(_finalLineHeight, glyph.Cropping.Height);
            }
        }
    }
}
