using Eluvion.Craft;
using Eluvion.Effect;
using Eluvion.Trigger;
using Xunit;

namespace Eluvion.Tests.Trigger;

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
            .Fire(0);
        Assert.True(called);
    }

    [Fact]
    public async Task InputPassesThroughUnchanged()
        => Assert.Equal(42, await new TriggerMorph(new AsTrigger(() => { }))
            .Craft<int, int>(new AsCraft<int, int>(x => x))
            .Yield(42));
}
