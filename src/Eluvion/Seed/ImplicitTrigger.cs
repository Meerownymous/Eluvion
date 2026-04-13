using Eluvion.Trigger;

namespace Eluvion.Seed;

public sealed class ImplicitTrigger(ITrigger trigger) : TriggerEnvelope(trigger)
{
    public static implicit operator ImplicitTrigger(Action trigger) => 
        new(new AsTrigger(trigger));
}