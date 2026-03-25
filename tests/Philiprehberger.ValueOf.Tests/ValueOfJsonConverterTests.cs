using Xunit;
using System.Text.Json;
using Philiprehberger.ValueOf;

namespace Philiprehberger.ValueOf.Tests;

public class ValueOfJsonConverterTests
{
    private static readonly JsonSerializerOptions Options = new()
    {
        Converters = { new ValueOfJsonConverterFactory() }
    };

    [Fact]
    public void Serialize_NonEmptyString_WritesUnderlyingValue()
    {
        var value = NonEmptyString.From("hello");

        var json = JsonSerializer.Serialize(value, Options);

        Assert.Equal("\"hello\"", json);
    }

    [Fact]
    public void Deserialize_NonEmptyString_ReadsUnderlyingValue()
    {
        var result = JsonSerializer.Deserialize<NonEmptyString>("\"world\"", Options);

        Assert.NotNull(result);
        Assert.Equal("world", result!.Value);
    }

    [Fact]
    public void Serialize_PositiveInt_WritesUnderlyingValue()
    {
        var value = PositiveInt.From(42);

        var json = JsonSerializer.Serialize(value, Options);

        Assert.Equal("42", json);
    }

    [Fact]
    public void RoundTrip_Percentage_PreservesValue()
    {
        var original = Percentage.From(75.5m);

        var json = JsonSerializer.Serialize(original, Options);
        var deserialized = JsonSerializer.Deserialize<Percentage>(json, Options);

        Assert.NotNull(deserialized);
        Assert.Equal(original.Value, deserialized!.Value);
    }

    [Fact]
    public void CanConvert_ValueOfDerived_ReturnsTrue()
    {
        var factory = new ValueOfJsonConverterFactory();

        Assert.True(factory.CanConvert(typeof(NonEmptyString)));
        Assert.True(factory.CanConvert(typeof(PositiveInt)));
        Assert.True(factory.CanConvert(typeof(Percentage)));
    }

    [Fact]
    public void CanConvert_NonValueOf_ReturnsFalse()
    {
        var factory = new ValueOfJsonConverterFactory();

        Assert.False(factory.CanConvert(typeof(string)));
        Assert.False(factory.CanConvert(typeof(int)));
    }
}
