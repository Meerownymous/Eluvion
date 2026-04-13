namespace Eluvion.Weave;

public sealed class MapFromDict<TObject>(Func<IDictionary<string, string>, TObject> mapping) :
    WeaveEnvelope<IDictionary<string, string>, TObject>(mapping);