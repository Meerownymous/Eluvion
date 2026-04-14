using Xunit;
using Eluvion.Trigger;
using Eluvion.Effect;
using Eluvion.Forge;
using Eluvion.Weave;

namespace Slydrix.Tests.Trigger;

public sealed class TriggerLinkTests
{
    [Fact]
    public async Task Act_FirstTriggerExecutes()
    {
        var called = false;
        await new TriggerLink(new AsTrigger(() => called = true), new AsTrigger(() => { })).Act();
        Assert.True(called);
    }

    [Fact]
    public async Task Act_SecondTriggerExecutes()
    {
        var called = false;
        await new TriggerLink(new AsTrigger(() => { }), new AsTrigger(() => called = true)).Act();
        Assert.True(called);
    }

    [Fact]
    public async Task Act_FirstExecutesBeforeSecond()
    {
        var order = new List<int>();
        await new TriggerLink(
            new AsTrigger(() => order.Add(1)),
            new AsTrigger(() => order.Add(2))
        ).Act();
        Assert.Equal(1, order[0]);
    }

    [Fact]
    public async Task Trigger_AllThreeTriggersExecute()
    {
        var count = 0;
        await new TriggerLink(new AsTrigger(() => count++), new AsTrigger(() => count++))
            .Trigger(new AsTrigger(() => count++))
            .Act();
        Assert.Equal(3, count);
    }

    [Fact]
    public async Task Effect_TriggersExecuteViaEffect()
    {
        var count = 0;
        await new TriggerLink(new AsTrigger(() => count++), new AsTrigger(() => count++))
            .Effect(new AsEffect<int>(_ => { }))
            .Act(0);
        Assert.Equal(2, count);
    }

    [Fact]
    public async Task Weave_InputPassesThroughUnchanged()
        => Assert.Equal(42, await new TriggerLink(new AsTrigger(() => { }), new AsTrigger(() => { }))
            .Weave<int, int>(new AsCraft<int, int>(x => x))
            .Act(42));
}
