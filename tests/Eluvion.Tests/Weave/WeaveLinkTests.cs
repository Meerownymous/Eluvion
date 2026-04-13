using Xunit;
using Eluvion.Weave;
using Eluvion.Trigger;
using Eluvion.Effect;

namespace Slydrix.Tests.Weave;

public sealed class WeaveLinkTests
{
    [Fact]
    public async Task Act_ComposesWeaves()
        => Assert.Equal("42", await new WeaveLink<int, int, string>(
            new AsWeave<int, int>(x => x * 2),
            new AsWeave<int, string>(x => x.ToString())
        ).Act(21));

    [Fact]
    public async Task Act_FirstWeaveOutputFeedsSecond()
        => Assert.Equal(84, await new WeaveLink<int, int, int>(
            new AsWeave<int, int>(x => x * 2),
            new AsWeave<int, int>(x => x * 2)
        ).Act(21));

    [Fact]
    public async Task Trigger_ExecutesAfterComposition()
    {
        var called = false;
        await new WeaveLink<int, int, int>(
            new AsWeave<int, int>(x => x),
            new AsWeave<int, int>(x => x)
        ).Trigger(new AsTrigger(() => called = true)).Act(0);
        Assert.True(called);
    }

    [Fact]
    public async Task Effect_ExecutedWithComposedResult()
    {
        var received = 0;
        await new WeaveLink<int, int, int>(
            new AsWeave<int, int>(x => x * 2),
            new AsWeave<int, int>(x => x)
        ).Effect(new AsEffect<int>(ipt => received = ipt)).Act(21);
        Assert.Equal(42, received);
    }

    [Fact]
    public async Task Weave_ChainsAnotherWeave()
        => Assert.Equal(44, await new WeaveLink<int, int, int>(
            new AsWeave<int, int>(x => x * 2),
            new AsWeave<int, int>(x => x)
        ).Weave(new AsWeave<int, int>(x => x + 2)).Act(21));
}
