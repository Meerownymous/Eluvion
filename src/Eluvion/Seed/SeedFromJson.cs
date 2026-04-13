using Eluvion.Misc;

namespace Eluvion.Seed;

public sealed class SeedFromJson<TSeed>(TextMorph txt) : SeedEnvelope<TSeed>(new AsSeed<TSeed>(() =>
        Task.FromResult(new FromJSON<TSeed>(txt).Value())
    )
);