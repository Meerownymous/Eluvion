using Eluvion.Effect;
using Eluvion.Forge;
using Eluvion.Trigger;
using Xunit;

namespace Eluvion.Tests.Effect;

public sealed class AsEffectTests
{
    [Fact]
    public async Task Act_WithAsyncFunc_ReceivesCorrectInput()
    {
        var received = 0;
        await new AsEffect<int>(ipt => received = ipt).Act(42);
        Assert.Equal(42, received);
    }

    [Fact]
    public async Task Act_WithSyncAction_ReceivesCorrectInput()
    {
        var received = 0;
        await new AsEffect<int>(ipt => received = ipt).Act(42);
        Assert.Equal(42, received);
    }

    [Fact]
    public async Task Act_WithParamlessFuncTask_Executes()
    {
        var called = false;
        await new AsEffect<int>(() => { called = true; return Task.CompletedTask; }).Act(0);
        Assert.True(called);
    }

    [Fact]
    public async Task Act_WithTrigger_ExecutesTrigger()
    {
        var called = false;
        await new AsEffect<int>(new AsTrigger(() => called = true)).Act(0);
        Assert.True(called);
    }

    [Fact]
    public async Task Effect_BothEffectsReceiveSameInput()
    {
        var sum = 0;
        await new AsEffect<int>(ipt => sum += ipt)
            .Effect(new AsEffect<int>(ipt => sum += ipt))
            .Act(21);
        Assert.Equal(42, sum);
    }

    [Fact]
    public async Task Trigger_ChainedTriggerExecutes()
    {
        var called = false;
        await new AsEffect<int>(_ => { })
            .Trigger(new AsTrigger(() => called = true))
            .Act(0);
        Assert.True(called);
    }

    [Fact]
    public async Task Weave_InputFlowsThroughWeave()
        => Assert.Equal(42, await new AsEffect<int>(_ => { })
            .Weave(new AsCraft<int, int>(x => x))
            .Act(42));
}
