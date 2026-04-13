namespace Eluvion.Seed;

/// <summary>A seed delegating to another seed, provided by a factory.</summary>
public abstract class SeedEnvelope<TSeed>(Func<ISeed<TSeed>> seed) : ISeed<TSeed>
{
    /// <summary>A seed delegating to the given async factory.</summary>
    public SeedEnvelope(Func<Task<TSeed>> seed) : this(new AsSeed<TSeed>(seed))
    { }

    /// <summary>A seed delegating to the given seed.</summary>
    public SeedEnvelope(ISeed<TSeed> seed) : this(() => seed)
    { }
    
    public Task<TSeed> Yield() => seed().Yield();

    public ISeed<TSeed> Trigger(ITrigger trigger) =>
        new SeedLink<TSeed>(this, trigger);

    public ISeed<TSeed> Effect(IEffect<TSeed> effect) =>
        new SeedLink<TSeed>(this, effect);

    public ISeed<TOut> Weave<TOut>(IWeave<TSeed, TOut> weave) =>
        new AsSeed<TOut>(async () => await weave.Act(await Yield()));
}