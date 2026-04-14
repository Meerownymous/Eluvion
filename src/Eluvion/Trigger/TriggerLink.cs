using Eluvion.Effect;
using Eluvion.Forge;
using Eluvion.Weave;

namespace Eluvion.Trigger;

/// <summary>Two triggers that fire in sequence.</summary>
public sealed class TriggerLink(ITrigger first, ITrigger second) : ITrigger
{
    public async Task Act()
    {
        await first.Act();
        await second.Act();
    }

    public ITrigger Trigger(ITrigger trigger) => new TriggerLink(this, trigger);

    public IEffect<TIn> Effect<TIn>(IEffect<TIn> effect) =>
        new EffectLink<TIn>(
            new AsEffect<TIn>(async _ => await Act()),
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