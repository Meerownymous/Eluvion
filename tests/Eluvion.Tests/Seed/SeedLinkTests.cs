using Eluvion.Effect;
using Eluvion.Forge;
using Eluvion.Seed;
using Eluvion.Trigger;
using Xunit;

namespace Eluvion.Tests.Seed;

public sealed class SeedLinkTests
{
    [Fact]
    public async Task Yield_ReturnsOriginalSeedValue()
        => Assert.Equal(42, await new SeedLink<int>(
            new AsSeed<int>(42),
            new AsEffect<int>(_ => { })
        ).Yield());

    [Fact]
    public async Task Yield_EffectReceivesSeedValue()
    {
        var received = 0;
        await new SeedLink<int>(new AsSeed<int>(42), new AsEffect<int>(ipt => received = ipt)).Yield();
        Assert.Equal(42, received);
    }

    [Fact]
    public async Task Yield_WithTrigger_ExecutesTrigger()
    {
        var called = false;
        await new SeedLink<int>(new AsSeed<int>(0), new AsTrigger(() => called = true)).Yield();
        Assert.True(called);
    }

    [Fact]
    public async Task Trigger_ChainedTriggerExecutes()
    {
        var called = false;
        await new SeedLink<int>(new AsSeed<int>(0), new AsEffect<int>(_ => { }))
            .Trigger(new AsTrigger(() => called = true))
            .Yield();
        Assert.True(called);
    }

    [Fact]
    public async Task Weave_TransformsYieldedValue()
        => Assert.Equal("42", await new SeedLink<int>(new AsSeed<int>(42), new AsEffect<int>(_ => { }))
            .Weave(new AsCraft<int, string>(x => x.ToString()))
            .Yield());
}
