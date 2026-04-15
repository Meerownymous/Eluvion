using Eluvion.Craft;
using Eluvion.Effect;
using Eluvion.Trigger;
using Xunit;

namespace Eluvion.Tests.Craft;

public sealed class CraftLinkTests
{
    [Fact]
    public async Task Act_ComposesCraft()
        => Assert.Equal("42", await new CraftLink<int, int, string>(
            new AsCraft<int, int>(x => x * 2),
            new AsCraft<int, string>(x => x.ToString())
        ).Yield(21));

    [Fact]
    public async Task Act_FirstCraftOutputFeedsSecond()
        => Assert.Equal(84, await new CraftLink<int, int, int>(
            new AsCraft<int, int>(x => x * 2),
            new AsCraft<int, int>(x => x * 2)
        ).Yield(21));

    [Fact]
    public async Task Trigger_ExecutesAfterComposition()
    {
        var called = false;
        await new CraftLink<int, int, int>(
            new AsCraft<int, int>(x => x),
            new AsCraft<int, int>(x => x)
        ).Trigger(new AsTrigger(() => called = true)).Yield(0);
        Assert.True(called);
    }

    [Fact]
    public async Task Effect_ExecutedWithComposedResult()
    {
        var received = 0;
        await new CraftLink<int, int, int>(
            new AsCraft<int, int>(x => x * 2),
            new AsCraft<int, int>(x => x)
        ).Effect(new AsEffect<int>(ipt => received = ipt)).Yield(21);
        Assert.Equal(42, received);
    }

    [Fact]
    public async Task ChainsAnotherCraft()
        => Assert.Equal(44, await new CraftLink<int, int, int>(
            new AsCraft<int, int>(x => x * 2),
            new AsCraft<int, int>(x => x)
        ).Craft(new AsCraft<int, int>(x => x + 2)).Yield(21));
}
