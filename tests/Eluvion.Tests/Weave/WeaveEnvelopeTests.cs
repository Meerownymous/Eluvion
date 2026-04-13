using Xunit;
using Eluvion.Weave;
using Eluvion.Trigger;
using Eluvion.Effect;

namespace Slydrix.Tests.Weave;

public sealed class WeaveEnvelopeTests
{
    [Fact]
    public async Task Act_DelegatesToWrappedWeave()
        => Assert.Equal(42, await new ImplicitWeave<int, int>(new AsWeave<int, int>(x => x * 2)).Act(21));

    [Fact]
    public async Task Trigger_DelegatesToOrigin()
    {
        var called = false;
        await new ImplicitWeave<int, int>(new AsWeave<int, int>(x => x))
            .Trigger(new AsTrigger(() => called = true))
            .Act(0);
        Assert.True(called);
    }

    [Fact]
    public async Task Effect_DelegatesToOrigin()
    {
        var received = 0;
        await new ImplicitWeave<int, int>(new AsWeave<int, int>(x => x * 2))
            .Effect(new AsEffect<int>(ipt => received = ipt))
            .Act(21);
        Assert.Equal(42, received);
    }

    [Fact]
    public async Task Weave_DelegatesToOrigin()
        => Assert.Equal("42", await new ImplicitWeave<int, int>(new AsWeave<int, int>(x => x * 2))
            .Weave(new AsWeave<int, string>(x => x.ToString()))
            .Act(21));
}
