namespace Philiprehberger.ValueOf;

/// <summary>
/// A string value object that ensures the value is never null or empty.
/// </summary>
public class NonEmptyString : ValueOf<string, NonEmptyString>
{
    /// <inheritdoc />
    protected override void Validate()
    {
        if (string.IsNullOrEmpty(Value))
            throw new ValueOfValidationException(typeof(NonEmptyString), "Value must not be null or empty.");
    }
}

/// <summary>
/// An integer value object that ensures the value is strictly greater than zero.
/// </summary>
public class PositiveInt : ValueOf<int, PositiveInt>
{
    /// <inheritdoc />
    protected override void Validate()
    {
        if (Value <= 0)
            throw new ValueOfValidationException(typeof(PositiveInt), $"Value must be greater than 0, but was {Value}.");
    }
}

/// <summary>
/// A decimal value object that ensures the value is between 0 and 100 inclusive.
/// </summary>
public class Percentage : ValueOf<decimal, Percentage>
{
    /// <inheritdoc />
    protected override void Validate()
    {
        if (Value < 0 || Value > 100)
            throw new ValueOfValidationException(typeof(Percentage), $"Value must be between 0 and 100, but was {Value}.");
    }
}

/// <summary>
/// An integer value object that ensures the value is greater than or equal to zero.
/// </summary>
public class NonNegativeInt : ValueOf<int, NonNegativeInt>
{
    /// <inheritdoc />
    protected override void Validate()
    {
        if (Value < 0)
            throw new ValueOfValidationException(typeof(NonNegativeInt), $"Value must be greater than or equal to 0, but was {Value}.");
    }
}

/// <summary>
/// A decimal value object that ensures the value is in the unit interval <c>[0, 1]</c>.
/// Complements <see cref="Percentage"/> for normalized fractions.
/// </summary>
public class UnitInterval : ValueOf<decimal, UnitInterval>
{
    /// <inheritdoc />
    protected override void Validate()
    {
        if (Value < 0m || Value > 1m)
            throw new ValueOfValidationException(typeof(UnitInterval), $"Value must be between 0 and 1, but was {Value}.");
    }
}

/// <summary>
/// A string value object that trims surrounding whitespace and ensures the result is non-empty.
/// </summary>
public class NonEmptyTrimmedString : ValueOf<string, NonEmptyTrimmedString>
{
    /// <inheritdoc />
    protected override void Validate()
    {
        Value = Value?.Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(Value))
            throw new ValueOfValidationException(typeof(NonEmptyTrimmedString), "Value must contain at least one non-whitespace character.");
    }
}
