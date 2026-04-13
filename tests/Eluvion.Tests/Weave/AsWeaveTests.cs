using Xunit;
using Eluvion.Weave;
using Eluvion.Trigger;
using Eluvion.Effect;

namespace Slydrix.Tests.Weave;

public sealed class AsWeaveTests
{
    [Fact]
    public async Task Act_WithSyncFunc_TransformsInput()
        => Assert.Equal(42, await new AsWeave<int, int>(x => x * 2).Act(21));

    [Fact]
    public async Task Act_WithAsyncFunc_TransformsInput()
        => Assert.Equal(42, await new AsWeave<int, int>(async x => await Task.FromResult(x)).Act(42));

    [Fact]
    public async Task Weave_ComposesTransformations()
        => Assert.Equal("42", await new AsWeave<int, int>(x => x * 2)
            .Weave(new AsWeave<int, string>(x => x.ToString()))
            .Act(21));

    [Fact]
    public async Task Effect_ExecutedWithTransformedValue()
    {
        var received = 0;
        await new AsWeave<int, int>(x => x * 2)
            .Effect(new AsEffect<int>(ipt => received = ipt))
            .Act(21);
        Assert.Equal(42, received);
    }

    [Fact]
    public async Task Trigger_ExecutesAfterTransform()
    {
        var called = false;
        await new AsWeave<int, int>(x => x)
            .Trigger(new AsTrigger(() => called = true))
            .Act(0);
        Assert.True(called);
    }

    [Fact]
    public async Task Trigger_ResultPreservedAfterTrigger()
        => Assert.Equal(42, await new AsWeave<int, int>(x => x)
            .Trigger(new AsTrigger(() => { }))
            .Act(42));
}
