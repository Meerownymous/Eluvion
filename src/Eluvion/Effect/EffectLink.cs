using Eluvion.Weave;

namespace Eluvion.Effect;

public sealed class EffectLink<TIn>(
    IEffect<TIn> first, 
    IEffect<TIn> second
) : IEffect<TIn>
{
    public async Task Act(TIn ipt)
    {
        await first.Act(ipt);
        await second.Act(ipt);
    }

    public IEffect<TIn> Trigger(ITrigger trigger) =>
        new EffectLink<TIn>(
            this,
            new AsEffect<TIn>(_ => trigger.Act())
        );

    public IEffect<TIn> Effect(IEffect<TIn> effect) =>
        new EffectLink<TIn>(this, effect);

    public IWeave<TIn, TOut> Weave<TOut>(IWeave<TIn, TOut> weave) =>
        new WeaveLink<TIn, TIn, TOut>(
            new AsWeave<TIn, TIn>(async ipt =>
            {
                await this.Act(ipt);
                return ipt;
            }),
            weave
        );
}