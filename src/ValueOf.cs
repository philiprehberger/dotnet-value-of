namespace Philiprehberger.ValueOf;

/// <summary>
/// Abstract base class for creating strongly-typed value objects that wrap a primitive value.
/// Provides equality, comparison, hashing, and implicit/explicit conversions out of the box.
/// </summary>
/// <typeparam name="TValue">The underlying primitive type being wrapped.</typeparam>
/// <typeparam name="TSelf">The concrete value object type (CRTP pattern).</typeparam>
public abstract class ValueOf<TValue, TSelf> : IEquatable<TSelf>, IComparable<TSelf>
    where TValue : notnull
    where TSelf : ValueOf<TValue, TSelf>, new()
{
    /// <summary>
    /// Gets the underlying primitive value.
    /// </summary>
    public TValue Value { get; protected set; } = default!;

    /// <summary>
    /// Creates a new instance of <typeparamref name="TSelf"/> from the given value,
    /// running any validation defined in <see cref="Validate"/>.
    /// </summary>
    /// <param name="value">The underlying value to wrap.</param>
    /// <returns>A validated instance of the value object.</returns>
    /// <exception cref="ValueOfValidationException">Thrown when validation fails.</exception>
    public static TSelf From(TValue value)
    {
        var instance = new TSelf();
        instance.Value = value;
        instance.Validate();
        return instance;
    }

    /// <summary>
    /// Override this method to add custom validation logic.
    /// Throw <see cref="ValueOfValidationException"/> if the value is invalid.
    /// </summary>
    protected virtual void Validate() { }

    /// <inheritdoc />
    public bool Equals(TSelf? other)
    {
        if (other is null) return false;
        return EqualityComparer<TValue>.Default.Equals(Value, other.Value);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is TSelf other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return EqualityComparer<TValue>.Default.GetHashCode(Value);
    }

    /// <inheritdoc />
    public int CompareTo(TSelf? other)
    {
        if (other is null) return 1;
        return Comparer<TValue>.Default.Compare(Value, other.Value);
    }

    /// <summary>
    /// Returns the string representation of the underlying value.
    /// </summary>
    public override string ToString()
    {
        return Value?.ToString() ?? string.Empty;
    }

    /// <summary>
    /// Determines whether two value objects are equal.
    /// </summary>
    public static bool operator ==(ValueOf<TValue, TSelf>? left, ValueOf<TValue, TSelf>? right)
    {
        if (left is null) return right is null;
        return left.Equals(right as TSelf);
    }

    /// <summary>
    /// Determines whether two value objects are not equal.
    /// </summary>
    public static bool operator !=(ValueOf<TValue, TSelf>? left, ValueOf<TValue, TSelf>? right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Implicitly converts a value object to its underlying primitive value.
    /// </summary>
    public static implicit operator TValue(ValueOf<TValue, TSelf> valueOf)
    {
        return valueOf.Value;
    }

    /// <summary>
    /// Explicitly converts a primitive value to the corresponding value object.
    /// </summary>
    public static explicit operator ValueOf<TValue, TSelf>(TValue value)
    {
        return From(value);
    }
}
