using Xunit;
using Eluvion.Seed;
using Eluvion.Trigger;
using Eluvion.Effect;
using Eluvion.Forge;
using Eluvion.Weave;

namespace Slydrix.Tests.Trigger;

public sealed class TriggerEnvelopeTests
{
    [Fact]
    public async Task Act_DelegatesToWrappedTrigger()
    {
        var called = false;
        await new TriggerMorph(new AsTrigger(() => called = true)).Act();
        Assert.True(called);
    }

    [Fact]
    public async Task Trigger_ChainedTriggerExecutes()
    {
        var called = false;
        await new TriggerMorph(new AsTrigger(() => { }))
            .Trigger(new AsTrigger(() => called = true))
            .Act();
        Assert.True(called);
    }

    [Fact]
    public async Task Effect_TriggerExecutesViaEffect()
    {
        var called = false;
        await new TriggerMorph(new AsTrigger(() => called = true))
            .Effect(new AsEffect<int>(_ => { }))
            .Act(0);
        Assert.True(called);
    }

    [Fact]
    public async Task Weave_InputPassesThroughUnchanged()
        => Assert.Equal(42, await new TriggerMorph(new AsTrigger(() => { }))
            .Weave<int, int>(new AsCraft<int, int>(x => x))
            .Act(42));
}
