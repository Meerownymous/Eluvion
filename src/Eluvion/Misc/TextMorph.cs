using Tonga;
using Tonga.Text;

namespace Eluvion.Misc;

/// <summary>
/// Adapt multiple types as IText automatically.
/// </summary>
public sealed class TextMorph(IText text) : TextEnvelope(text)
{
    public static implicit operator TextMorph(AsText str) => new(str);
    public static implicit operator TextMorph(Base64Decoded str) => new(str);
    public static implicit operator TextMorph(Base64Encoded str) => new(str);
    public static implicit operator TextMorph(string str) => new(new AsText(str));
    public static implicit operator TextMorph(Stream stream) => new(new AsText(stream));
}