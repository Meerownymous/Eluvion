using Eluvion.Effect;
using Eluvion.Forge;
using Eluvion.Trigger;
using Eluvion.Weave;
using Xunit;

namespace Eluvion.Tests.Craft;

public sealed class CraftEnvelopeTests
{
    [Fact]
    public async Task Act_DelegatesToWrappedWeave()
        => Assert.Equal(42, await new CraftMorph<int, int>(new AsCraft<int, int>(x => x * 2)).Act(21));

    [Fact]
    public async Task Trigger_DelegatesToOrigin()
    {
        var called = false;
        await new CraftMorph<int, int>(new AsCraft<int, int>(x => x))
            .Trigger(new AsTrigger(() => called = true))
            .Act(0);
        Assert.True(called);
    }

    [Fact]
    public async Task Effect_DelegatesToOrigin()
    {
        var received = 0;
        await new CraftMorph<int, int>(new AsCraft<int, int>(x => x * 2))
            .Effect(new AsEffect<int>(ipt => received = ipt))
            .Act(21);
        Assert.Equal(42, received);
    }

    [Fact]
    public async Task Weave_DelegatesToOrigin()
        => Assert.Equal("42", await new CraftMorph<int, int>(new AsCraft<int, int>(x => x * 2))
            .Weave(new AsCraft<int, string>(x => x.ToString()))
            .Act(21));
}
