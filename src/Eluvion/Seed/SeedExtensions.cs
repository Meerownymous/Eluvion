namespace Eluvion.Seed;

public static class SeedExtensions
{
    public static ISeed<TSeed> Trigger<TSeed>(this ISeed<TSeed> origin, ImplicitTrigger trigger) => origin.Trigger(trigger); 
}