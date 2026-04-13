using Xunit;
using Eluvion.Seed;

namespace Slydrix.Tests.Seed;

public sealed class SeedCasesTests
{
    [Fact]
    public async Task Yield_FirstConditionTrue_ReturnsFirst()
        => Assert.Equal(1, await new SeedIf<int>(
            (() => true, () => 1),
            (() => true, () => 2)
        ).Yield());

    [Fact]
    public async Task Yield_FirstConditionFalse_ReturnsSecond()
        => Assert.Equal(2, await new SeedIf<int>(
            (() => false, () => 1),
            (() => true,  () => 2)
        ).Yield());

    [Fact]
    public async Task Yield_WithISeed_ReturnsValue()
        => Assert.Equal(42, await new SeedIf<int>(
            (() => true, new AsSeed<int>(42))
        ).Yield());

    [Fact]
    public async Task Yield_NoMatchingCondition_ThrowsInvalidOperationException()
        => await Assert.ThrowsAsync<InvalidOperationException>(() =>
            new SeedIf<int>((() => false, () => 0)).Yield()
        );
}
