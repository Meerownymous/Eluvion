using Eluvion.Craft;
using Xunit;

namespace Eluvion.Tests.Craft;

public sealed class CraftExtensionTests
{
    [Fact]
    public async Task TriggersWithAsyncFunction()
    {
        var triggered = false;

        await new AsCraft<int, int>(_ => _)
            .Trigger(async () =>
            {
                await Task.FromResult(triggered = true);
            })
            .Yield(1);
        Assert.True(triggered);
    }
    
    [Fact]
    public async Task TriggersWithSyncFunction()
    {
        var triggered = false;

        await new AsCraft<int, int>(_ => _)
            .Trigger(() => triggered = true)
            .Yield(1);
            
        Assert.True(triggered);
    }
    
    [Fact]
    public async Task HasEffectWithAsyncFunction()
    {
        var triggered = false;

        await new AsCraft<int, int>(_ => _)
            .Effect(async number =>
            {
                await Task.FromResult(triggered = true);
            })
            .Yield(1);
        Assert.True(triggered);
    }
    
    [Fact]
    public async Task HasEffectWithSyncFunction()
    {
        var triggered = false;

        await new AsCraft<int, int>(_ => _)
            .Effect(number => triggered = true)
            .Yield(1);
            
        Assert.True(triggered);
    }
    
    [Fact]
    public async Task CraftsWithAsyncFunction()
    {
        Assert.Equal(
            2,
            await new AsCraft<int, int>(_ => _)
                .Craft(async number => await Task.FromResult(++number))
                .Yield(1)
        );
    }
    
    [Fact]
    public async Task CraftsWithSyncFunction()
    {
        Assert.Equal(
            2,
            await new AsCraft<int, int>(_ => _)
                .Craft(number => ++number)
                .Yield(1)
        );
    }
}