namespace Eluvion;

public interface IWeave<in TIn, TOut>
{
    /// <summary>The result of transforming the given input.</summary>
    Task<TOut> Act(TIn ipt);

    /// <summary>This weave with the given trigger appended to fire after transforming. Result is unchanged.</summary>
    IWeave<TIn, TOut> Trigger(ITrigger trigger);

    /// <summary>This weave with the given effect applied to its output after transforming. Result is unchanged.</summary>
    IWeave<TIn, TOut> Effect(IEffect<TOut> effect);

    /// <summary>This weave with its output further transformed by the given weave.</summary>
    IWeave<TIn, TOutNext> Weave<TOutNext>(IWeave<TOut, TOutNext> weave);
}