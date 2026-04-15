using Eluvion.Craft;
using Eluvion.Effect;
using Eluvion.Trigger;
using Xunit;

namespace Eluvion.Tests.Craft;

public sealed class CraftEnvelopeTests
{
    [Fact]
    public async Task Act_DelegatesToWrappedCraft()
        => Assert.Equal(42, await new CraftMorph<int, int>(new AsCraft<int, int>(x => x * 2)).Yield(21));

    [Fact]
    public async Task Trigger_DelegatesToOrigin()
    {
        var called = false;
        await new CraftMorph<int, int>(new AsCraft<int, int>(x => x))
            .Trigger(new AsTrigger(() => called = true))
            .Yield(0);
        Assert.True(called);
    }

    [Fact]
    public async Task Effect_DelegatesToOrigin()
    {
        var received = 0;
        await new CraftMorph<int, int>(new AsCraft<int, int>(x => x * 2))
            .Effect(new AsEffect<int>(ipt => received = ipt))
            .Yield(21);
        Assert.Equal(42, received);
    }

    [Fact]
    public async Task DelegatesToOrigin()
        => Assert.Equal("42", await new CraftMorph<int, int>(new AsCraft<int, int>(x => x * 2))
            .Craft(new AsCraft<int, string>(x => x.ToString()))
            .Yield(21));
}
