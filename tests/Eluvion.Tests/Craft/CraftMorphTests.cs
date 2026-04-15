using Eluvion.Craft;
using Xunit;

namespace Eluvion.Tests.Craft;

public sealed class CraftMorphTests
{
    [Fact]
    public async Task Act_WithExplicitConstructor_TransformsInput()
        => Assert.Equal(42, await new CraftMorph<int, int>(new AsCraft<int, int>(x => x * 2)).Yield(21));

    [Fact]
    public async Task ImplicitFromFunc_TransformsInput()
    {
        CraftMorph<int, int> craftMorph = (Func<int, int>)(x => x * 2);
        Assert.Equal(42, await craftMorph.Yield(21));
    }

    [Fact]
    public async Task ImplicitFromFunc_PreservesIdentity()
    {
        CraftMorph<int, int> craftMorph = (Func<int, int>)(x => x);
        Assert.Equal(42, await craftMorph.Yield(42));
    }
}
