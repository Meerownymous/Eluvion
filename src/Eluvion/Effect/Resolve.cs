namespace Eluvion.Effect;

/// <summary>
/// An effect that resolves a <see cref="OneOf{TSuccess, TError}"/> by firing
/// the matching effect handler for the present case.
/// </summary>
public sealed class Resolve<TSuccess, TError>(
    Func<TSuccess, IEffect<TSuccess>> onSuccess,
    Func<TError, IEffect<TError>> onError
) : EffectEnvelope<OneOf<TSuccess, TError>>(
    oneOf => oneOf.Match(
        success => onSuccess(success).Fire(success),
        error => onError(error).Fire(error)
    )
)
{ }
