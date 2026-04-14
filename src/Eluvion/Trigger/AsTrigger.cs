using Eluvion.Effect;
using Eluvion.Forge;
using Eluvion.Weave;

namespace Eluvion.Trigger;

/// <summary>A trigger built from the given async action.</summary>
public sealed class AsTrigger(Func<Task> act) : ITrigger
{
    /// <summary>A trigger built from the given synchronous action.</summary>
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
        new CraftLink<TIn, TIn, TOut>(
            new AsCraft<TIn, TIn>(async ipt =>
            {
                await Act();
                return ipt;
            }),
            weave
        );
}