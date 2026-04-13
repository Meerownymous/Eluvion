namespace Eluvion.Seed;

public class SeedLink<TSeed>(ISeed<TSeed> first, Func<TSeed, Task> second) : ISeed<TSeed>
{
    public SeedLink(ISeed<TSeed> first, ITrigger trigger) : this(
        first, async _ => await trigger.Act()
    )
    { }
    
    public SeedLink(ISeed<TSeed> first, IEffect<TSeed> effect) : this(
        first, async seed => await effect.Act(seed)
    )
    { }
    
    public async Task<TSeed> Yield()
    {
        var result = await first.Yield();
        await second(result);
        return result;
    }

    public ISeed<TSeed> Trigger(ITrigger trigger) =>
        new AsSeed<TSeed>(async () =>
            {
                var result = await Yield();
                await trigger.Act();
                return result;
            }
        );

    public ISeed<TSeed> Effect(IEffect<TSeed> effect) =>
        new AsSeed<TSeed>(async () =>
        {
            var seed = await Yield();
            await effect.Act(seed);
            return seed;
        });

    public ISeed<TNewSeed> Weave<TNewSeed>(IWeave<TSeed, TNewSeed> weave) =>
        new AsSeed<TNewSeed>(async () => await weave.Act(await Yield()));
}