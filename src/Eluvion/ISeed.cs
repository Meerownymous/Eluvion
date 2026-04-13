namespace Eluvion;

public interface ISeed<TSpawned>
{
    Task<TSpawned> Yield();
    ISeed<TSpawned> Trigger(ITrigger trigger);
    ISeed<TSpawned> Effect(IEffect<TSpawned> effect);
    ISeed<TNewSpan> Weave<TNewSpan>(IWeave<TSpawned, TNewSpan> weave);
}