namespace Eluvion.Craft;

/// <summary>A craft delegating to the given craft, implicitly constructible from a function.</summary>
public sealed class CraftMorph<TIn,TOut>(ICraft<TIn,TOut> craft) : CraftEnvelope<TIn,TOut>(craft)
{
    public static implicit operator CraftMorph<TIn,TOut>(Func<TIn,TOut> craft) => 
        new(new AsCraft<TIn,TOut>(craft));
}