using Eluvion.Effect;

namespace Eluvion.Craft;

/// <summary>An effect that runs the given craft on its input, discarding the output.</summary>
public sealed class CraftAsEffect<TIn,TOut>(
    ICraft<TIn, TOut> craft
) : EffectEnvelope<TIn>(async ipt => await craft.Yield(ipt))
{ }