using Eluvion.Effect;
using Eluvion.Seed;
using OneOf;
using Xunit;

namespace Eluvion.Tests.Effect;

public sealed class CasesTests
{
    [Fact]
    public async Task TwoCases_FiresFirstHandler()
    {
        var fired = "";
        await new Cases<string, int>(
            t0 => new AsEffect<string>(v => fired = $"t0:{v}"),
            t1 => new AsEffect<int>(v => fired = $"t1:{v}")
        ).Fire(OneOf<string, int>.FromT0("hello"));
        Assert.Equal("t0:hello", fired);
    }

    [Fact]
    public async Task TwoCases_FiresSecondHandler()
    {
        var fired = "";
        await new Cases<string, int>(
            t0 => new AsEffect<string>(v => fired = $"t0:{v}"),
            t1 => new AsEffect<int>(v => fired = $"t1:{v}")
        ).Fire(OneOf<string, int>.FromT1(42));
        Assert.Equal("t1:42", fired);
    }

    [Fact]
    public async Task ThreeCases_FiresMatchingHandler()
    {
        var fired = "";
        await new Cases<string, int, bool>(
            t0 => new AsEffect<string>(v => fired = $"t0:{v}"),
            t1 => new AsEffect<int>(v => fired = $"t1:{v}"),
            t2 => new AsEffect<bool>(v => fired = $"t2:{v}")
        ).Fire(OneOf<string, int, bool>.FromT2(true));
        Assert.Equal("t2:True", fired);
    }

    [Fact]
    public async Task WorksInSeedPipeline()
    {
        var fired = "";
        await new AsSeed<OneOf<string, int>>(OneOf<string, int>.FromT0("hello"))
            .Effect(new Cases<string, int>(
                t0 => new AsEffect<string>(v => fired = $"t0:{v}"),
                t1 => new AsEffect<int>(v => fired = $"t1:{v}")
            ))
            .Yield();
        Assert.Equal("t0:hello", fired);
    }

    [Fact]
    public async Task ImplicitConversionProducesCorrectCase()
    {
        var fired = "";
        OneOf<string, int> oneOf = "implicit";
        await new Cases<string, int>(
            t0 => new AsEffect<string>(v => fired = $"t0:{v}"),
            t1 => new AsEffect<int>(v => fired = $"t1:{v}")
        ).Fire(oneOf);
        Assert.Equal("t0:implicit", fired);
    }
}
