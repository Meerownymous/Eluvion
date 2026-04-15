namespace Eluvion.Trigger;

/// <summary>A trigger delegating to the given trigger.</summary>
public abstract class TriggerEnvelope(ITrigger origin) : ITrigger
{
    /// <summary>A trigger delegating to the given synchronous action.</summary>
    public TriggerEnvelope(Action act) : this(new AsTrigger(act))
    { }

    /// <summary>A trigger delegating to the given async action.</summary>
    public TriggerEnvelope(Func<Task> act) : this(new AsTrigger(act))
    { }

    public Task Act() => origin.Act();

    public ITrigger Trigger(ITrigger trigger) => origin.Trigger(trigger);

    public IEffect<TIn> Effect<TIn>(IEffect<TIn> effect) => origin.Effect(effect);

    public ICraft<TIn, TOut> Craft<TIn, TOut>(ICraft<TIn, TOut> craft) => origin.Craft(craft);
}