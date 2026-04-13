using Eluvion.Weave;

namespace Eluvion.Effect;

public abstract class EffectEnvelope<TIn>(IEffect<TIn> origin) : IEffect<TIn>
{
    public EffectEnvelope(Func<TIn, Task> act) : this(
        new AsEffect<TIn>(async ipt => await act(ipt))
    )
    { }
    
    public EffectEnvelope(Action<TIn> act) : this(
        new AsEffect<TIn>(ipt =>
        {
            act(ipt);
            return Task.CompletedTask;
        })
    )
    { }
    
    public Task Act(TIn ipt) => origin.Act(ipt);
    
    public IEffect<TIn> Trigger(ITrigger trigger) =>
        new EffectLink<TIn>(
            this,
            new AsEffect<TIn>(async _ => await trigger.Act())
        );

    public IEffect<TIn> Effect(IEffect<TIn> effect) => new EffectLink<TIn>(this, effect);

    public IWeave<TIn, TOut> Weave<TOut>(IWeave<TIn, TOut> weave) =>
        new WeaveLink<TIn, TIn, TOut>(
            new AsWeave<TIn, TIn>(async ipt =>
            {
                await Act(ipt);
                return ipt;
            }),
            weave
        );
}

