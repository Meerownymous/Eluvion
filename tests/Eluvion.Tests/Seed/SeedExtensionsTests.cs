using Eluvion.Seed;
using Xunit;

namespace Eluvion.Tests.Seed;

public sealed class SeedExtensionsTests
{
    [Fact]
    public async Task Trigger_WithLambdaTrigger_ExecutesTrigger()
    {
        var called = false;
        await 42.AsSeed().Trigger(() => called = true).Yield();
        Assert.True(called);
    }

    [Fact]
    public async Task Trigger_WithLambdaTrigger_PreservesYieldedValue()
        => Assert.Equal(42, await 42.AsSeed().Trigger(() => { }).Yield());
}
