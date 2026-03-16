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
