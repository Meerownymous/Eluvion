namespace Eluvion.Weave;

/// <summary>A weave delegating to the given weave, implicitly constructible from a function.</summary>
public sealed class WeaveMorph<TIn,TOut>(IWeave<TIn,TOut> weave) : WeaveEnvelope<TIn,TOut>(weave)
{
    public static implicit operator WeaveMorph<TIn,TOut>(Func<TIn,TOut> morph) => 
        new(new AsWeave<TIn,TOut>(morph));
}