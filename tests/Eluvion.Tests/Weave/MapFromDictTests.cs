using Xunit;
using Eluvion.Weave;

namespace Slydrix.Tests.Weave;

public sealed class MapFromDictTests
{
    [Fact]
    public async Task Act_MapsValueByKey()
        => Assert.Equal("hello", await new MapFromDict<string>(d => d["key"])
            .Act(new Dictionary<string, string> { ["key"] = "hello" }));

    [Fact]
    public async Task Act_CountsEntries()
        => Assert.Equal(3, await new MapFromDict<int>(d => d.Count)
            .Act(new Dictionary<string, string> { ["a"] = "1", ["b"] = "2", ["c"] = "3" }));

    [Fact]
    public async Task Act_CombinesValues()
        => Assert.Equal("ab", await new MapFromDict<string>(d => d["x"] + d["y"])
            .Act(new Dictionary<string, string> { ["x"] = "a", ["y"] = "b" }));
}
