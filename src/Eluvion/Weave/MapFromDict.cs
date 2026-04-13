namespace Eluvion.Weave;

/// <summary>A weave that maps the given string dictionary to an object using the provided function.</summary>
public sealed class MapFromDict<TObject>(Func<IDictionary<string, string>, TObject> mapping) :
    WeaveEnvelope<IDictionary<string, string>, TObject>(mapping);