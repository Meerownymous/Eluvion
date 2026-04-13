namespace Eluvion;

public interface IWeave<in TIn, TOut>
{
    /// <summary>
    /// Act on input.
    /// </summary>
    Task<TOut> Act(TIn ipt);
    
    /// <summary>
    /// Build a flow which will act on the next take which has no input. 
    /// </summary>
    IWeave<TIn, TOut> Trigger(ITrigger trigger);
    
    /// <summary>
    /// Build a flow which will handover result of this take to the next take,
    /// but the next take has no result and so the result of this take will be returned.
    /// </summary>
    IWeave<TIn, TOut> Effect(IEffect<TOut> effect);
    
    /// <summary>
    /// Build a flow which will handover result of this take to the next take.
    /// </summary>
    IWeave<TIn, TOutNext> Weave<TOutNext>(IWeave<TOut, TOutNext> weave);
}