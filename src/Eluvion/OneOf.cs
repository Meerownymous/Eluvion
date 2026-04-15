namespace Eluvion;

/// <summary>A value that is either of type T1 or T2, but not both.</summary>
public abstract class OneOf<T1, T2>
{
    private OneOf() { }

    /// <summary>The first-type case of a <see cref="OneOf{T1,T2}"/>.</summary>
    public sealed class AsFirst(T1 value) : OneOf<T1, T2>
    {
        internal T1 Value => value;
    }

    /// <summary>The second-type case of a <see cref="OneOf{T1,T2}"/>.</summary>
    public sealed class AsSecond(T2 value) : OneOf<T1, T2>
    {
        internal T2 Value => value;
    }

    /// <summary>Transforms this union into TOut by applying the matching function.</summary>
    public TOut Match<TOut>(Func<T1, TOut> onFirst, Func<T2, TOut> onSecond)
        => this is AsFirst f ? onFirst(f.Value) : onSecond(((AsSecond)this).Value);

    /// <summary>Executes one of the given async actions based on which case is present.</summary>
    public Task Match(Func<T1, Task> onFirst, Func<T2, Task> onSecond)
        => this is AsFirst f ? onFirst(f.Value) : onSecond(((AsSecond)this).Value);

    public static implicit operator OneOf<T1, T2>(T1 value) => new AsFirst(value);
    public static implicit operator OneOf<T1, T2>(T2 value) => new AsSecond(value);
}
