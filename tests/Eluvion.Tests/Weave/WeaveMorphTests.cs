using Xunit;
using Eluvion.Weave;

namespace Slydrix.Tests.Weave;

public sealed class WeaveMorphTests
{
    [Fact]
    public async Task Act_WithExplicitConstructor_TransformsInput()
        => Assert.Equal(42, await new WeaveMorph<int, int>(new AsWeave<int, int>(x => x * 2)).Act(21));

    [Fact]
    public async Task ImplicitFromFunc_TransformsInput()
    {
        WeaveMorph<int, int> weaveMorph = (Func<int, int>)(x => x * 2);
        Assert.Equal(42, await weaveMorph.Act(21));
    }

    [Fact]
    public async Task ImplicitFromFunc_PreservesIdentity()
    {
        WeaveMorph<int, int> weaveMorph = (Func<int, int>)(x => x);
        Assert.Equal(42, await weaveMorph.Act(42));
    }
}
