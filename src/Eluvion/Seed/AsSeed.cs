using Eluvion.Weave;
using Tonga;

namespace Eluvion.Seed;

public sealed class AsSeed<TSeed>(Func<Task<TSeed>> spawnAsync) : ISeed<TSeed>
{
    public AsSeed(Task<TSeed> seed) : this(() => seed)
    { }
    
    public AsSeed(TSeed seed) : this(() => Task.FromResult(seed))
    { }
    
    public AsSeed(Func<TSeed> seed) : this(() => Task.FromResult(seed()))
    { }
    
    public AsSeed(IScalar<TSeed> seed) : this(() => Task.FromResult(seed.Value()))
    { }
    
    public async Task<TSeed> Yield() => await spawnAsync();

    public ISeed<TSeed> Trigger(ITrigger trigger) =>
        new SeedLink<TSeed>(this, trigger);

    public ISeed<TSeed> Effect(IEffect<TSeed> effect) =>
        new SeedLink<TSeed>(this, effect);

    public ISeed<TNewSpan> Weave<TNewSpan>(IWeave<TSeed, TNewSpan> weave) =>
        new AsSeed<TNewSpan>(async () =>
            await weave.Act(await Yield())
        );
}

public static partial class SeedSmarts
{
    public static ISeed<TSeed> AsSeed<TSeed>(this TSeed seed) => new AsSeed<TSeed>(seed);
    
    public static ISeed<TMapped> Mapped<TSeed, TMapped>(this ISeed<TSeed> seed, ImplicitWeave<TSeed, TMapped> mapping) =>
        seed.Weave(mapping);
    
    public static ISeed<TMapped> Mapped<TSeed, TMapped>(this ISeed<TSeed> seed, Func<TSeed, TMapped> mapping) =>
        seed.Weave(new AsWeave<TSeed, TMapped>(mapping));
}