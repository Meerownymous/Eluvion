namespace Eluvion;

public interface IEffect<TIn>
{
    /// <summary>Applies this effect to the given input.</summary>
    Task Act(TIn ipt);

    /// <summary>This effect with the given trigger appended to fire after acting.</summary>
    IEffect<TIn> Trigger(ITrigger trigger);

    /// <summary>This effect with the given effect appended to act on the same input.</summary>
    IEffect<TIn> Effect(IEffect<TIn> effect);

    /// <summary>A weave that applies this effect to its input before passing it through the given weave.</summary>
    IWeave<TIn, TOut> Weave<TOut>(IWeave<TIn, TOut> weave);
}