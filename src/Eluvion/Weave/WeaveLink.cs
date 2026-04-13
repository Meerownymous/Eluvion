namespace Eluvion.Weave;

/// <summary>Two weaves whose transformations are composed in sequence.</summary>
public sealed class WeaveLink<TIn,TInAndOut,TOut>(
    IWeave<TIn,TInAndOut> first,
    IWeave<TInAndOut,TOut> second
) : IWeave<TIn, TOut>
{
    public async Task<TOut> Act(TIn ipt) => await second.Act(await first.Act(ipt));
    public IWeave<TIn, TOut> Trigger(ITrigger trigger) =>
        new WeaveLink<TIn, TOut, TOut>(
            this,
            new AsWeave<TOut, TOut>(async resultFromFirst =>
            {
                await trigger.Act();
                return resultFromFirst;
            })
        );

    public IWeave<TIn, TOut> Effect(IEffect<TOut> effect) =>
        new WeaveLink<TIn, TOut, TOut>(
            this,
            new AsWeave<TOut, TOut>(async resultFromFirst =>
            {
                await effect.Act(resultFromFirst);
                return resultFromFirst;
            })
        );

    public IWeave<TIn, TOutNext> Weave<TOutNext>(IWeave<TOut, TOutNext> next) =>
        new WeaveLink<TIn, TOut, TOutNext>(this, next);
}