using Xunit;
using Eluvion.Weave;

namespace Slydrix.Tests.Weave;

public sealed class ImplicitWeaveTests
{
    [Fact]
    public async Task Act_WithExplicitConstructor_TransformsInput()
        => Assert.Equal(42, await new ImplicitWeave<int, int>(new AsWeave<int, int>(x => x * 2)).Act(21));

    [Fact]
    public async Task ImplicitFromFunc_TransformsInput()
    {
        ImplicitWeave<int, int> weave = (Func<int, int>)(x => x * 2);
        Assert.Equal(42, await weave.Act(21));
    }

    [Fact]
    public async Task ImplicitFromFunc_PreservesIdentity()
    {
        ImplicitWeave<int, int> weave = (Func<int, int>)(x => x);
        Assert.Equal(42, await weave.Act(42));
    }
}
