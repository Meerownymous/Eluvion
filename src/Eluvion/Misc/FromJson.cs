using System.Text.Json;
using Tonga;
using Tonga.Scalar;

namespace Eluvion.Misc;

public sealed class FromJSON<TResult>(TextMorph txt) : ScalarEnvelope<TResult>(() =>
    JsonSerializer.Deserialize<TResult>(txt.Str(), new JsonSerializerOptions()
    {
        PropertyNameCaseInsensitive = true
    })
)
{
    public FromJSON(IText text) : this(new TextMorph(text))
    { }
}