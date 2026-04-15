using OneOf;

namespace Eluvion.Effect;

/// <summary>
/// An effect that handles a <see cref="OneOf{TSuccess, TError}"/> by firing
/// the matching effect handler for the present case.
/// </summary>
public sealed class Cases<TSuccess, TError>(
    Func<TSuccess, IEffect<TSuccess>> onSuccess,
    Func<TError, IEffect<TError>> onError
) : EffectEnvelope<OneOf<TSuccess, TError>>(
    oneOf => oneOf.Match(
        success => onSuccess(success).Fire(success),
        error   => onError(error).Fire(error)
    )
)
{ }
