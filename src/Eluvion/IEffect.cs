namespace Eluvion;

public interface IEffect<TIn>
{
    /// <summary>Applies this effect to the given input.</summary>
    Task Fire(TIn ipt);

    /// <summary>This effect with the given trigger appended to fire after acting.</summary>
    IEffect<TIn> Trigger(ITrigger trigger);

    /// <summary>This effect with the given effect appended to act on the same input.</summary>
    IEffect<TIn> Effect(IEffect<TIn> effect);

    /// <summary>A craft that applies this effect to its input before passing it through the given craft.</summary>
    ICraft<TIn, TCrafted> Craft<TCrafted>(ICraft<TIn, TCrafted> craft);
}