namespace Eluvion;

/// <summary>A value that is either of type T1 or T2, but not both.</summary>
public readonly struct OneOf<T1, T2>
{
    private readonly T1? _first;
    private readonly T2? _second;
    private readonly bool _isFirst;

    /// <summary>A union holding a value of the first type.</summary>
    public OneOf(T1 value)
    {
        _first = value;
        _second = default;
        _isFirst = true;
    }

    /// <summary>A union holding a value of the second type.</summary>
    public OneOf(T2 value)
    {
        _first = default;
        _second = value;
        _isFirst = false;
    }

    /// <summary>Transforms this union into a value of TOut by applying the matching function.</summary>
    public TOut Match<TOut>(Func<T1, TOut> onFirst, Func<T2, TOut> onSecond)
        => _isFirst ? onFirst(_first!) : onSecond(_second!);

    /// <summary>Executes one of the given async actions based on which case is present.</summary>
    public Task Match(Func<T1, Task> onFirst, Func<T2, Task> onSecond)
        => _isFirst ? onFirst(_first!) : onSecond(_second!);

    public static implicit operator OneOf<T1, T2>(T1 value) => new(value);
    public static implicit operator OneOf<T1, T2>(T2 value) => new(value);
}
