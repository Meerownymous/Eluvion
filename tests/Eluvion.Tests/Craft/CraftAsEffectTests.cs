using Eluvion.Craft;
using Eluvion.Effect;
using Eluvion.Trigger;
using Xunit;

namespace Eluvion.Tests.Craft;

public sealed class CraftAsEffectTests
{
    [Fact]
    public async Task Act_ExecutesWrappedCraft()
    {
        var called = false;
        await new CraftAsEffect<int, int>(new AsCraft<int, int>(x => { called = true; return x; })).Fire(0);
        Assert.True(called);
    }

    [Fact]
    public async Task Act_PassesCorrectInputToCraft()
    {
        var received = 0;
        await new CraftAsEffect<int, int>(new AsCraft<int, int>(x => { received = x; return x; })).Fire(42);
        Assert.Equal(42, received);
    }

    [Fact]
    public async Task Effect_ChainedEffectReceivesOriginalInput()
    {
        var received = 0;
        await new CraftAsEffect<int, int>(new AsCraft<int, int>(x => x * 2))
            .Effect(new AsEffect<int>(ipt => received = ipt))
            .Fire(42);
        Assert.Equal(42, received);
    }

    [Fact]
    public async Task Trigger_ChainedTriggerExecutes()
    {
        var called = false;
        await new CraftAsEffect<int, int>(new AsCraft<int, int>(x => x))
            .Trigger(new AsTrigger(() => called = true))
            .Fire(0);
        Assert.True(called);
    }
}
