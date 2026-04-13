using Eluvion.Effect;

namespace Eluvion.Weave;

public sealed class WeaveAsEffect<TIn,TOut>(
    IWeave<TIn, TOut> weave
) : EffectEnvelope<TIn>(async ipt => await weave.Act(ipt))
{ }