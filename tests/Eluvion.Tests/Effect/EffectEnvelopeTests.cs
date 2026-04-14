using Eluvion.Craft;
using Eluvion.Effect;
using Eluvion.Forge;
using Eluvion.Trigger;
using Xunit;

namespace Eluvion.Tests.Effect;

public sealed class EffectEnvelopeTests
{
    [Fact]
    public async Task Act_DelegatesToWrappedWeave()
    {
        var called = false;
        await new CraftAsEffect<int, int>(new AsCraft<int, int>(x => { called = true; return x; })).Act(0);
        Assert.True(called);
    }

    [Fact]
    public async Task Act_PassesCorrectInputToWeave()
    {
        var received = 0;
        await new CraftAsEffect<int, int>(new AsCraft<int, int>(x => { received = x; return x; })).Act(42);
        Assert.Equal(42, received);
    }

    [Fact]
    public async Task Trigger_ChainedTriggerExecutes()
    {
        var called = false;
        await new CraftAsEffect<int, int>(new AsCraft<int, int>(x => x))
            .Trigger(new AsTrigger(() => called = true))
            .Act(0);
        Assert.True(called);
    }

    [Fact]
    public async Task Effect_ChainedEffectReceivesInput()
    {
        var received = 0;
        await new CraftAsEffect<int, int>(new AsCraft<int, int>(x => x))
            .Effect(new AsEffect<int>(ipt => received = ipt))
            .Act(42);
        Assert.Equal(42, received);
    }

    [Fact]
    public async Task Weave_InputFlowsThroughToWeave()
        => Assert.Equal(42, await new CraftAsEffect<int, int>(new AsCraft<int, int>(x => x))
            .Weave(new AsCraft<int, int>(x => x))
            .Act(42));
}
