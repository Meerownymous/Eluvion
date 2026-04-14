using Eluvion.Forge;

namespace Eluvion.Weave;

/// <summary>Two weaves whose transformations are composed in sequence.</summary>
public sealed class CraftLink<TIn,TInAndOut,TOut>(
    IWeave<TIn,TInAndOut> first,
    IWeave<TInAndOut,TOut> second
) : IWeave<TIn, TOut>
{
    public async Task<TOut> Act(TIn ipt) => await second.Act(await first.Act(ipt));
    public IWeave<TIn, TOut> Trigger(ITrigger trigger) =>
        new CraftLink<TIn, TOut, TOut>(
            this,
            new AsCraft<TOut, TOut>(async resultFromFirst =>
            {
                await trigger.Act();
                return resultFromFirst;
            })
        );

    public IWeave<TIn, TOut> Effect(IEffect<TOut> effect) =>
        new CraftLink<TIn, TOut, TOut>(
            this,
            new AsCraft<TOut, TOut>(async resultFromFirst =>
            {
                await effect.Act(resultFromFirst);
                return resultFromFirst;
            })
        );

    public IWeave<TIn, TOutNext> Weave<TOutNext>(IWeave<TOut, TOutNext> next) =>
        new CraftLink<TIn, TOut, TOutNext>(this, next);
}