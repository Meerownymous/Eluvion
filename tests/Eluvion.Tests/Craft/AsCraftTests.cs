using Eluvion.Craft;
using Eluvion.Effect;
using Eluvion.Trigger;
using Xunit;

namespace Eluvion.Tests.Craft;

public sealed class AsCraftTests
{
    [Fact]
    public async Task Act_WithSyncFunc_TransformsInput()
        => Assert.Equal(42, await new AsCraft<int, int>(x => x * 2).Yield(21));

    [Fact]
    public async Task Act_WithAsyncFunc_TransformsInput()
        => Assert.Equal(42, await new AsCraft<int, int>(async x => await Task.FromResult(x)).Yield(42));

    [Fact]
    public async Task ComposesTransformations()
        => Assert.Equal("42", await new AsCraft<int, int>(x => x * 2)
            .Craft(new AsCraft<int, string>(x => x.ToString()))
            .Yield(21));

    [Fact]
    public async Task Effect_ExecutedWithTransformedValue()
    {
        var received = 0;
        await new AsCraft<int, int>(x => x * 2)
            .Effect(new AsEffect<int>(ipt => received = ipt))
            .Yield(21);
        Assert.Equal(42, received);
    }

    [Fact]
    public async Task Trigger_ExecutesAfterTransform()
    {
        var called = false;
        await new AsCraft<int, int>(x => x)
            .Trigger(new AsTrigger(() => called = true))
            .Yield(0);
        Assert.True(called);
    }

    [Fact]
    public async Task Trigger_ResultPreservedAfterTrigger()
        => Assert.Equal(42, await new AsCraft<int, int>(x => x)
            .Trigger(new AsTrigger(() => { }))
            .Yield(42));
}
