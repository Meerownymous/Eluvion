using Eluvion.Craft;
using Eluvion.Effect;
using Eluvion.Trigger;
using Xunit;

namespace Eluvion.Tests.Trigger;

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
            .Fire(0);
        Assert.True(called);
    }

    [Fact]
    public async Task Effect_InputFlowsThroughUnchanged()
        => Assert.Equal(42, await new AsTrigger(() => { })
            .Effect(new AsEffect<int>(_ => { }))
            .Craft(new AsCraft<int, int>(x => x))
            .Yield(42));

    [Fact]
    public async Task TriggerExecutesDuringAct()
    {
        var called = false;
        await new AsTrigger(() => called = true)
            .Craft<int, int>(new AsCraft<int, int>(x => x))
            .Yield(0);
        Assert.True(called);
    }

    [Fact]
    public async Task InputPassesThroughUnchanged()
        => Assert.Equal(42, await new AsTrigger(() => { })
            .Craft<int, int>(new AsCraft<int, int>(x => x))
            .Yield(42));
}
