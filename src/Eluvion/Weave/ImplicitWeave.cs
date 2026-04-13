namespace Eluvion.Weave;

public sealed class ImplicitWeave<TIn,TOut>(IWeave<TIn,TOut> weave) : WeaveEnvelope<TIn,TOut>(weave)
{
    public static implicit operator ImplicitWeave<TIn,TOut>(Func<TIn,TOut> morph) => 
        new(new AsWeave<TIn,TOut>(morph));
}