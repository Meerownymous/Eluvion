namespace Eluvion;


public interface ITrigger
{
    /// <summary>Fires this trigger.</summary>
    Task Act();

    /// <summary>This trigger with the given trigger appended to fire after itself.</summary>
    ITrigger Trigger(ITrigger trigger);

    /// <summary>An effect that fires this trigger before delegating to the given effect.</summary>
    IEffect<TIn> Effect<TIn>(IEffect<TIn> effect);

    /// <summary>A craft that fires this trigger before passing input through the given craft.</summary>
    ICraft<TIn, TOut> Craft<TIn,TOut>(ICraft<TIn, TOut> craft);
}