namespace Eluvion;

public interface ISeed<TSpawned>
{
    /// <summary>The value this seed holds.</summary>
    Task<TSpawned> Yield();

    /// <summary>This seed with the given trigger appended to fire after yielding.</summary>
    ISeed<TSpawned> Trigger(ITrigger trigger);

    /// <summary>This seed with the given effect applied to its value after yielding.</summary>
    ISeed<TSpawned> Effect(IEffect<TSpawned> effect);

    /// <summary>This seed with its yielded value transformed through the given weave.</summary>
    ISeed<TNewSpan> Weave<TNewSpan>(IWeave<TSpawned, TNewSpan> weave);
}