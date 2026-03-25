using Xunit;
using Philiprehberger.ValueOf;

namespace Philiprehberger.ValueOf.Tests;

public class ValueOfValidationExceptionTests
{
    [Fact]
    public void Constructor_WithTypeAndMessage_SetsProperties()
    {
        var ex = new ValueOfValidationException(typeof(NonEmptyString), "Value is invalid.");

        Assert.Equal(typeof(NonEmptyString), ex.ValueObjectType);
        Assert.Contains("NonEmptyString", ex.Message);
        Assert.Contains("Value is invalid.", ex.Message);
    }

    [Fact]
    public void Constructor_WithInnerException_SetsInnerException()
    {
        var inner = new InvalidOperationException("inner");
        var ex = new ValueOfValidationException(typeof(PositiveInt), "failed", inner);

        Assert.Equal(inner, ex.InnerException);
        Assert.Equal(typeof(PositiveInt), ex.ValueObjectType);
    }

    [Fact]
    public void Exception_IsBaseException()
    {
        var ex = new ValueOfValidationException(typeof(Percentage), "out of range");

        Assert.IsAssignableFrom<Exception>(ex);
    }
}
