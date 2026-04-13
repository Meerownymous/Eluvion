using Eluvion.Effect;
using Eluvion.Weave;

namespace Eluvion.Trigger;

/// <summary>
/// Two triggers linked together.
/// </summary>
/// <param name="first"></param>
/// <param name="second"></param>
public sealed class TriggerLink(ITrigger first, ITrigger second) : ITrigger
{
    public async Task Act()
    {
        await first.Act();
        await second.Act();
    }

    /// <summary>
    /// Triggers.
    /// </summary>
    /// <param name="trigger"></param>
    /// <returns></returns>
    public ITrigger Trigger(ITrigger trigger) => new TriggerLink(this, trigger);

    /// <summary>
    /// Has given Effect from <see cref="TIn"/>
    /// </summary>
    /// <param name="effect">desired Effect</param>
    /// <typeparam name="TIn">Payload</typeparam>
    /// <returns></returns>
    public IEffect<TIn> Effect<TIn>(IEffect<TIn> effect) =>
        new EffectLink<TIn>(
            new AsEffect<TIn>(async _ => await Act()),
            effect
        );

    /// <summary>
    /// Morphs <see cref="TIn"/> to <see cref="TOut"/>
    /// </summary>
    /// <param name="weave"></param>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <returns><see cref="TOut"/></returns>
    public IWeave<TIn, TOut> Weave<TIn, TOut>(IWeave<TIn, TOut> weave) =>
        new WeaveLink<TIn, TIn, TOut>(
            new AsWeave<TIn, TIn>(async ipt =>
            {
                await Act();
                return ipt;
            }),
            weave
        );
}