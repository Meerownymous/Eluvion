using Eluvion.Effect;

namespace Eluvion.Weave;

/// <summary>An effect that runs the given weave on its input, discarding the output.</summary>
public sealed class WeaveAsEffect<TIn,TOut>(
    IWeave<TIn, TOut> weave
) : EffectEnvelope<TIn>(async ipt => await weave.Act(ipt))
{ }