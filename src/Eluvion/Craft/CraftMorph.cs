using Eluvion.Forge;

namespace Eluvion.Weave;

/// <summary>A weave delegating to the given weave, implicitly constructible from a function.</summary>
public sealed class CraftMorph<TIn,TOut>(IWeave<TIn,TOut> weave) : CraftEnvelope<TIn,TOut>(weave)
{
    public static implicit operator CraftMorph<TIn,TOut>(Func<TIn,TOut> morph) => 
        new(new AsCraft<TIn,TOut>(morph));
}