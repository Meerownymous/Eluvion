namespace Eluvion.Trigger;

public abstract class TriggerEnvelope(ITrigger origin) : ITrigger
{
    public TriggerEnvelope(Action act) : this(new AsTrigger(act))
    { }
    
    public TriggerEnvelope(Func<Task> act) : this(new AsTrigger(act))
    { }

    public Task Act() => origin.Act();

    public ITrigger Trigger(ITrigger trigger) => origin.Trigger(trigger);

    public IEffect<TIn> Effect<TIn>(IEffect<TIn> effect) => origin.Effect(effect);

    public IWeave<TIn, TOut> Weave<TIn, TOut>(IWeave<TIn, TOut> weave) => origin.Weave(weave);
}