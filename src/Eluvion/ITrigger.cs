namespace Eluvion;


public interface ITrigger
{
    Task Act();
    ITrigger Trigger(ITrigger trigger);
    IEffect<TIn> Effect<TIn>(IEffect<TIn> effect);
    IWeave<TIn, TOut> Weave<TIn,TOut>(IWeave<TIn, TOut> weave);
}