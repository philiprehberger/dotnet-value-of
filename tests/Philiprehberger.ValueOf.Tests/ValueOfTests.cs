using Xunit;
using Philiprehberger.ValueOf;

namespace Philiprehberger.ValueOf.Tests;

public class ValueOfTests
{
    private class TestStringValue : ValueOf<string, TestStringValue>
    {
        protected override void Validate()
        {
            if (string.IsNullOrEmpty(Value))
                throw new ValueOfValidationException(typeof(TestStringValue), "Value must not be empty.");
        }
    }

    private class TestIntValue : ValueOf<int, TestIntValue> { }

    [Fact]
    public void From_WithValidValue_CreatesInstance()
    {
        var result = TestStringValue.From("hello");

        Assert.Equal("hello", result.Value);
    }

    [Fact]
    public void From_WithInvalidValue_ThrowsValidationException()
    {
        Assert.Throws<ValueOfValidationException>(() => TestStringValue.From(""));
    }

    [Fact]
    public void Equals_SameValue_ReturnsTrue()
    {
        var a = TestIntValue.From(42);
        var b = TestIntValue.From(42);

        Assert.True(a.Equals(b));
        Assert.True(a == b);
    }

    [Fact]
    public void Equals_DifferentValue_ReturnsFalse()
    {
        var a = TestIntValue.From(1);
        var b = TestIntValue.From(2);

        Assert.False(a.Equals(b));
        Assert.True(a != b);
    }

    [Fact]
    public void ImplicitConversion_ToUnderlyingType_ReturnsValue()
    {
        var vo = TestIntValue.From(42);

        int value = vo;

        Assert.Equal(42, value);
    }

    [Fact]
    public void ToString_ReturnsUnderlyingValueString()
    {
        var vo = TestStringValue.From("hello");

        Assert.Equal("hello", vo.ToString());
    }

    [Fact]
    public void CompareTo_OrdersCorrectly()
    {
        var a = TestIntValue.From(1);
        var b = TestIntValue.From(2);

        Assert.True(a.CompareTo(b) < 0);
        Assert.True(b.CompareTo(a) > 0);
        Assert.Equal(0, a.CompareTo(TestIntValue.From(1)));
    }

    [Fact]
    public void GetHashCode_SameValue_ReturnsSameHash()
    {
        var a = TestIntValue.From(42);
        var b = TestIntValue.From(42);

        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }
}
