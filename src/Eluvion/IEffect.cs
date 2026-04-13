namespace Eluvion;

public interface IEffect<TIn>
{
    Task Act(TIn ipt);
    IEffect<TIn> Trigger(ITrigger trigger);
    IEffect<TIn> Effect(IEffect<TIn> effect);
    IWeave<TIn, TOut> Weave<TOut>(IWeave<TIn, TOut> weave);
}