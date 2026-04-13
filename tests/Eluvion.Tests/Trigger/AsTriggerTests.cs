using Xunit;
using Eluvion.Trigger;
using Eluvion.Effect;
using Eluvion.Weave;

namespace Slydrix.Tests.Trigger;

public sealed class AsTriggerTests
{
    [Fact]
    public async Task Act_WithSyncAction_Executes()
    {
        var called = false;
        await new AsTrigger(() => called = true).Act();
        Assert.True(called);
    }

    [Fact]
    public async Task Act_WithAsyncFunc_Executes()
    {
        var called = false;
        await new AsTrigger(async () => { await Task.CompletedTask; called = true; }).Act();
        Assert.True(called);
    }

    [Fact]
    public async Task Trigger_BothTriggersExecute()
    {
        var count = 0;
        await new AsTrigger(() => count++)
            .Trigger(new AsTrigger(() => count++))
            .Act();
        Assert.Equal(2, count);
    }

    [Fact]
    public async Task Effect_TriggerExecutesOnAct()
    {
        var called = false;
        await new AsTrigger(() => called = true)
            .Effect(new AsEffect<int>(_ => { }))
            .Act(0);
        Assert.True(called);
    }

    [Fact]
    public async Task Effect_InputFlowsThroughUnchanged()
        => Assert.Equal(42, await new AsTrigger(() => { })
            .Effect(new AsEffect<int>(_ => { }))
            .Weave(new AsWeave<int, int>(x => x))
            .Act(42));

    [Fact]
    public async Task Weave_TriggerExecutesDuringAct()
    {
        var called = false;
        await new AsTrigger(() => called = true)
            .Weave<int, int>(new AsWeave<int, int>(x => x))
            .Act(0);
        Assert.True(called);
    }

    [Fact]
    public async Task Weave_InputPassesThroughUnchanged()
        => Assert.Equal(42, await new AsTrigger(() => { })
            .Weave<int, int>(new AsWeave<int, int>(x => x))
            .Act(42));
}
