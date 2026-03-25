using Xunit;
using Philiprehberger.ValueOf;

namespace Philiprehberger.ValueOf.Tests;

public class BuiltInTypesTests
{
    [Fact]
    public void NonEmptyString_WithValidValue_Creates()
    {
        var result = NonEmptyString.From("hello");

        Assert.Equal("hello", result.Value);
    }

    [Fact]
    public void NonEmptyString_WithEmptyString_ThrowsValidationException()
    {
        Assert.Throws<ValueOfValidationException>(() => NonEmptyString.From(""));
    }

    [Fact]
    public void NonEmptyString_WithNull_ThrowsValidationException()
    {
        Assert.Throws<ValueOfValidationException>(() => NonEmptyString.From(null!));
    }

    [Fact]
    public void PositiveInt_WithPositiveValue_Creates()
    {
        var result = PositiveInt.From(5);

        Assert.Equal(5, result.Value);
    }

    [Fact]
    public void PositiveInt_WithZero_ThrowsValidationException()
    {
        Assert.Throws<ValueOfValidationException>(() => PositiveInt.From(0));
    }

    [Fact]
    public void PositiveInt_WithNegative_ThrowsValidationException()
    {
        Assert.Throws<ValueOfValidationException>(() => PositiveInt.From(-1));
    }

    [Fact]
    public void Percentage_WithValidValue_Creates()
    {
        var result = Percentage.From(50m);

        Assert.Equal(50m, result.Value);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(100)]
    [InlineData(50.5)]
    public void Percentage_WithBoundaryValues_Creates(decimal value)
    {
        var result = Percentage.From(value);

        Assert.Equal(value, result.Value);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void Percentage_WithOutOfRangeValues_ThrowsValidationException(decimal value)
    {
        Assert.Throws<ValueOfValidationException>(() => Percentage.From(value));
    }
}
