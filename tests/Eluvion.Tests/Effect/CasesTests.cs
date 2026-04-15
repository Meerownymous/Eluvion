using Eluvion.Effect;
using Eluvion.Seed;
using OneOf;
using Xunit;

namespace Eluvion.Tests.Effect;

public sealed class CasesTests
{
    [Fact]
    public async Task FiresSuccessEffectWhenFirstCase()
    {
        var fired = "";
        await new Cases<string, int>(
            s => new AsEffect<string>(v => fired = $"success:{v}"),
            n => new AsEffect<int>(v => fired = $"error:{v}")
        ).Fire(OneOf<string, int>.FromT0("hello"));
        Assert.Equal("success:hello", fired);
    }

    [Fact]
    public async Task FiresErrorEffectWhenSecondCase()
    {
        var fired = "";
        await new Cases<string, int>(
            s => new AsEffect<string>(v => fired = $"success:{v}"),
            n => new AsEffect<int>(v => fired = $"error:{v}")
        ).Fire(OneOf<string, int>.FromT1(42));
        Assert.Equal("error:42", fired);
    }

    [Fact]
    public async Task WorksInSeedPipeline()
    {
        var fired = "";
        await new AsSeed<OneOf<string, int>>(OneOf<string, int>.FromT0("hello"))
            .Effect(new Cases<string, int>(
                s => new AsEffect<string>(v => fired = $"success:{v}"),
                n => new AsEffect<int>(v => fired = $"error:{v}")
            ))
            .Yield();
        Assert.Equal("success:hello", fired);
    }

    [Fact]
    public async Task ImplicitConversionProducesCorrectCase()
    {
        var fired = "";
        OneOf<string, int> oneOf = "implicit";
        await new Cases<string, int>(
            s => new AsEffect<string>(v => fired = $"success:{v}"),
            n => new AsEffect<int>(v => fired = $"error:{v}")
        ).Fire(oneOf);
        Assert.Equal("success:implicit", fired);
    }
}
