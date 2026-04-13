using Xunit;
using Eluvion.Seed;
using Eluvion.Trigger;

namespace Slydrix.Tests.Seed;

public sealed class ImplicitTriggerTests
{
    [Fact]
    public async Task Act_DelegatesToWrappedTrigger()
    {
        var called = false;
        await new ImplicitTrigger(new AsTrigger(() => called = true)).Act();
        Assert.True(called);
    }

    [Fact]
    public async Task ImplicitFromAction_ExecutesOnAct()
    {
        var called = false;
        ImplicitTrigger trigger = (Action)(() => called = true);
        await trigger.Act();
        Assert.True(called);
    }

    [Fact]
    public async Task Trigger_ChainedTriggerExecutes()
    {
        var called = false;
        await new ImplicitTrigger(new AsTrigger(() => { }))
            .Trigger(new AsTrigger(() => called = true))
            .Act();
        Assert.True(called);
    }
}
