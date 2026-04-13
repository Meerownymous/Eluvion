using Xunit;
using Eluvion.Seed;

namespace Slydrix.Tests.Seed;

public sealed class SeedExtensionsTests
{
    [Fact]
    public async Task Trigger_WithImplicitTrigger_ExecutesTrigger()
    {
        var called = false;
        await 42.AsSeed().Trigger((Action)(() => called = true)).Yield();
        Assert.True(called);
    }

    [Fact]
    public async Task Trigger_WithImplicitTrigger_PreservesYieldedValue()
        => Assert.Equal(42, await 42.AsSeed().Trigger((Action)(() => { })).Yield());
}
