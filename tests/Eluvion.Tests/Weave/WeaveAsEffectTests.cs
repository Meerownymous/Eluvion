using Xunit;
using Eluvion.Weave;
using Eluvion.Effect;
using Eluvion.Trigger;

namespace Slydrix.Tests.Weave;

public sealed class WeaveAsEffectTests
{
    [Fact]
    public async Task Act_ExecutesWrappedWeave()
    {
        var called = false;
        await new WeaveAsEffect<int, int>(new AsWeave<int, int>(x => { called = true; return x; })).Act(0);
        Assert.True(called);
    }

    [Fact]
    public async Task Act_PassesCorrectInputToWeave()
    {
        var received = 0;
        await new WeaveAsEffect<int, int>(new AsWeave<int, int>(x => { received = x; return x; })).Act(42);
        Assert.Equal(42, received);
    }

    [Fact]
    public async Task Effect_ChainedEffectReceivesOriginalInput()
    {
        var received = 0;
        await new WeaveAsEffect<int, int>(new AsWeave<int, int>(x => x * 2))
            .Effect(new AsEffect<int>(ipt => received = ipt))
            .Act(42);
        Assert.Equal(42, received);
    }

    [Fact]
    public async Task Trigger_ChainedTriggerExecutes()
    {
        var called = false;
        await new WeaveAsEffect<int, int>(new AsWeave<int, int>(x => x))
            .Trigger(new AsTrigger(() => called = true))
            .Act(0);
        Assert.True(called);
    }
}
