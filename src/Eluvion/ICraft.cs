namespace Eluvion;

public interface ICraft<in TIn, TOut>
{
    /// <summary>The result of transforming the given input.</summary>
    Task<TOut> Yield(TIn ipt);

    /// <summary>This craft with the given trigger appended to fire after transforming. Result is unchanged.</summary>
    ICraft<TIn, TOut> Trigger(ITrigger trigger);

    /// <summary>This craft with the given effect applied to its output after transforming. Result is unchanged.</summary>
    ICraft<TIn, TOut> Effect(IEffect<TOut> effect);

    /// <summary>This craft with its output further transformed by the given craft.</summary>
    ICraft<TIn, TOutNext> Craft<TOutNext>(ICraft<TOut, TOutNext> craft);
}