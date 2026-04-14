using Eluvion.Effect;
using Eluvion.Forge;
using Eluvion.Seed;
using Eluvion.Trigger;
using Xunit;

namespace Eluvion.Tests.Seed;

public sealed class SeedEnvelopeTests
{
    [Fact]
    public async Task Yield_DelegatesToWrappedSeed()
        => Assert.Equal(42, await new SeedSwitch<int>((() => true, () => 42)).Yield());

    [Fact]
    public async Task Effect_ReceivesYieldedValue()
    {
        var received = 0;
        await new SeedSwitch<int>((() => true, () => 42))
            .Effect(new AsEffect<int>(ipt => received = ipt))
            .Yield();
        Assert.Equal(42, received);
    }

    [Fact]
    public async Task Trigger_ExecutesTrigger()
    {
        var called = false;
        await new SeedSwitch<int>((() => true, () => 0))
            .Trigger(new AsTrigger(() => called = true))
            .Yield();
        Assert.True(called);
    }

    [Fact]
    public async Task Weave_TransformsYieldedValue()
        => Assert.Equal("42", await new SeedSwitch<int>((() => true, () => 42))
            .Weave(new AsCraft<int, string>(x => x.ToString()))
            .Yield());
}
