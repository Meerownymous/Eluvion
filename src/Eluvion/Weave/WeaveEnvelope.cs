namespace Eluvion.Weave;

/// <summary>A weave delegating to the given weave.</summary>
public abstract class WeaveEnvelope<TIn, TOut>(IWeave<TIn,TOut> origin) : IWeave<TIn, TOut>
{
    /// <summary>A weave delegating to the given async function.</summary>
    public WeaveEnvelope(Func<TIn, Task<TOut>> act) : this(new AsWeave<TIn, TOut>(act))
    { }

    /// <summary>A weave delegating to the given synchronous function.</summary>
    public WeaveEnvelope(Func<TIn, TOut> act) : this(new AsWeave<TIn, TOut>(act))
    { }
    
    public Task<TOut> Act(TIn ipt) => origin.Act(ipt);
    
    public IWeave<TIn, TOut> Trigger(ITrigger trigger) => origin.Trigger(trigger);

    public IWeave<TIn, TOut> Effect(IEffect<TOut> effect) => origin.Effect(effect);

    public IWeave<TIn, TOutNext> Weave<TOutNext>(IWeave<TOut, TOutNext> weave) => origin.Weave(weave);
}