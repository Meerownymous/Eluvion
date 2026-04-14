using Xunit;
using Eluvion.Weave;
using Eluvion.Trigger;
using Eluvion.Effect;
using Eluvion.Forge;

namespace Slydrix.Tests.Weave;

public sealed class CraftLinkTests
{
    [Fact]
    public async Task Act_ComposesWeaves()
        => Assert.Equal("42", await new CraftLink<int, int, string>(
            new AsCraft<int, int>(x => x * 2),
            new AsCraft<int, string>(x => x.ToString())
        ).Act(21));

    [Fact]
    public async Task Act_FirstWeaveOutputFeedsSecond()
        => Assert.Equal(84, await new CraftLink<int, int, int>(
            new AsCraft<int, int>(x => x * 2),
            new AsCraft<int, int>(x => x * 2)
        ).Act(21));

    [Fact]
    public async Task Trigger_ExecutesAfterComposition()
    {
        var called = false;
        await new CraftLink<int, int, int>(
            new AsCraft<int, int>(x => x),
            new AsCraft<int, int>(x => x)
        ).Trigger(new AsTrigger(() => called = true)).Act(0);
        Assert.True(called);
    }

    [Fact]
    public async Task Effect_ExecutedWithComposedResult()
    {
        var received = 0;
        await new CraftLink<int, int, int>(
            new AsCraft<int, int>(x => x * 2),
            new AsCraft<int, int>(x => x)
        ).Effect(new AsEffect<int>(ipt => received = ipt)).Act(21);
        Assert.Equal(42, received);
    }

    [Fact]
    public async Task Weave_ChainsAnotherWeave()
        => Assert.Equal(44, await new CraftLink<int, int, int>(
            new AsCraft<int, int>(x => x * 2),
            new AsCraft<int, int>(x => x)
        ).Weave(new AsCraft<int, int>(x => x + 2)).Act(21));
}
