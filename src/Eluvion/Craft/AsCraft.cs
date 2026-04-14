using Eluvion.Weave;

namespace Eluvion.Forge;

/// <summary>A weave built from the given async transformation function.</summary>
public sealed class AsCraft<TIn,TOut>(Func<TIn,Task<TOut>> act) : IWeave<TIn,TOut>
{
    /// <summary>A weave built from the given synchronous transformation function.</summary>
    public AsCraft(Func<TIn,TOut> act) : this(ipt => Task.FromResult(act(ipt)))
    { }

    public async Task<TOut> Act(TIn ipt) => await act(ipt);

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
    

    public IWeave<TIn, TOutNext> Weave<TOutNext>(IWeave<TOut, TOutNext> weave) =>
        new CraftLink<TIn, TOut, TOutNext>(this, weave);
}