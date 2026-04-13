namespace Eluvion.Seed;

public abstract class SeedEnvelope<TSeed>(Func<ISeed<TSeed>> seed) : ISeed<TSeed>
{
    public SeedEnvelope(Func<Task<TSeed>> seed) : this(new AsSeed<TSeed>(seed))
    { }
    
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