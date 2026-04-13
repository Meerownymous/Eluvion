using Xunit;
using Eluvion.Misc;

namespace Slydrix.Tests.Misc;

public sealed class FromJsonTests
{
    [Fact]
    public void Value_DeserializesInt()
        => Assert.Equal(42, new FromJSON<int>("42").Value());

    [Fact]
    public void Value_DeserializesString()
        => Assert.Equal("hello", new FromJSON<string>("\"hello\"").Value());

    [Fact]
    public void Value_DeserializesList()
        => Assert.Equal(3, new FromJSON<List<int>>("[1,2,3]").Value().Count);

    [Fact]
    public void Value_IsCaseInsensitive()
        => Assert.Equal(42, new FromJSON<TheAnswer>("{\"VALUE\":42}").Value().Value);
}

file record TheAnswer(int Value);
