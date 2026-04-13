using Eluvion.Weave;

namespace Eluvion.Effect;

public sealed class AsEffect<TIn>(Func<TIn,Task> act) : IEffect<TIn>
{
    public AsEffect(Action<TIn> act) : this(ipt =>
    {
        act(ipt);
        return Task.CompletedTask;
    })
    { }
    
    public AsEffect(Func<Task> act) : this(async _ =>
    {
        await act();
    })
    { }
    
    public AsEffect(Func<TIn> act) : this(_ =>
    {
        act();
        return Task.CompletedTask;
    })
    { }

    public AsEffect(ITrigger trigger) : this(async _ => await trigger.Act())
    { }
    
    public async Task Act(TIn ipt) => await act(ipt);

    /// <summary>
    /// Will act on the given take and return result from this take.
    /// </summary>
    public IEffect<TIn> Trigger(ITrigger trigger) =>
        new EffectLink<TIn>(this, new AsEffect<TIn>(trigger));

    public IEffect<TIn> Effect(IEffect<TIn> effect) =>
        new EffectLink<TIn>(
            this,
            new AsEffect<TIn>(async ipt => await effect.Act(ipt))
        );

    public IWeave<TIn, TOut> Weave<TOut>(IWeave<TIn, TOut> weave) =>
        new WeaveLink<TIn, TIn, TOut>(
            new AsWeave<TIn, TIn>(ipt => Task.FromResult(ipt)),
            new AsWeave<TIn, TOut>(ipt => weave.Act(ipt))
        );

}

