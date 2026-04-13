using Eluvion.Effect;
using Eluvion.Weave;

namespace Eluvion.Trigger;

public sealed class AsTrigger(Func<Task> act) : ITrigger
{
    public AsTrigger(Action act) : this(() =>
    {
        act();
        return Task.CompletedTask;
    }) 
    { }
    public async Task Act() => await act();

    public ITrigger Trigger(ITrigger trigger) => new TriggerLink(this, trigger);

    public IEffect<TIn> Effect<TIn>(IEffect<TIn> effect) =>
        new EffectLink<TIn>(
            new AsEffect<TIn>(_ => Act()),
            effect
        );

    public IWeave<TIn, TOut> Weave<TIn, TOut>(IWeave<TIn, TOut> weave) =>
        new WeaveLink<TIn, TIn, TOut>(
            new AsWeave<TIn, TIn>(async ipt =>
            {
                await Act();
                return ipt;
            }),
            weave
        );
}