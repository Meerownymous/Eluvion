namespace Eluvion.Craft;

/// <summary>A craft delegating to the given weave.</summary>
public abstract class CraftEnvelope<TIn, TOut>(ICraft<TIn,TOut> origin) : ICraft<TIn, TOut>
{
    /// <summary>A craft delegating to the given async function.</summary>
    public CraftEnvelope(Func<TIn, Task<TOut>> act) : this(new AsCraft<TIn, TOut>(act))
    { }

    /// <summary>A craft delegating to the given synchronous function.</summary>
    public CraftEnvelope(Func<TIn, TOut> act) : this(new AsCraft<TIn, TOut>(act))
    { }
    
    public Task<TOut> Yield(TIn ipt) => origin.Yield(ipt);
    
    public ICraft<TIn, TOut> Trigger(ITrigger trigger) => origin.Trigger(trigger);

    public ICraft<TIn, TOut> Effect(IEffect<TOut> effect) => origin.Effect(effect);

    public ICraft<TIn, TOutNext> Craft<TOutNext>(ICraft<TOut, TOutNext> craft) => origin.Craft(craft);
}