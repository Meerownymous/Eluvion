using Eluvion.Seed;
using Xunit;

namespace Eluvion.Tests.Seed;

public sealed class SeedFromJsonTests
{
    [Fact]
    public async Task Yield_DeserializesInt()
        => Assert.Equal(42, await new SeedFromJson<int>("42").Yield());

    [Fact]
    public async Task Yield_DeserializesString()
        => Assert.Equal("hello", await new SeedFromJson<string>("\"hello\"").Yield());

    [Fact]
    public async Task Yield_DeserializesList()
        => Assert.Equal(3, (await new SeedFromJson<List<int>>("[1,2,3]").Yield()).Count);

    [Fact]
    public async Task Yield_IsCaseInsensitive()
        => Assert.Equal(42, (await new SeedFromJson<Wrapper>("{\"VALUE\":42}").Yield()).Value);
}

file record Wrapper(int Value);
