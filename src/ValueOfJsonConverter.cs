using System.Text.Json;
using System.Text.Json.Serialization;

namespace Philiprehberger.ValueOf;

/// <summary>
/// A <see cref="JsonConverterFactory"/> that handles serialization and deserialization
/// of any type derived from <see cref="ValueOf{TValue, TSelf}"/>.
/// Reads and writes the underlying <c>TValue</c> directly.
/// </summary>
public class ValueOfJsonConverterFactory : JsonConverterFactory
{
    /// <inheritdoc />
    public override bool CanConvert(Type typeToConvert)
    {
        var baseType = typeToConvert.BaseType;
        while (baseType is not null)
        {
            if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(ValueOf<,>))
                return true;
            baseType = baseType.BaseType;
        }
        return false;
    }

    /// <inheritdoc />
    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var baseType = typeToConvert.BaseType;
        while (baseType is not null)
        {
            if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(ValueOf<,>))
            {
                var typeArgs = baseType.GetGenericArguments();
                var converterType = typeof(ValueOfJsonConverter<,>).MakeGenericType(typeArgs[0], typeToConvert);
                return (JsonConverter?)Activator.CreateInstance(converterType);
            }
            baseType = baseType.BaseType;
        }
        return null;
    }
}

internal class ValueOfJsonConverter<TValue, TSelf> : JsonConverter<TSelf>
    where TValue : notnull
    where TSelf : ValueOf<TValue, TSelf>, new()
{
    public override TSelf? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = JsonSerializer.Deserialize<TValue>(ref reader, options);
        if (value is null) return null;
        return ValueOf<TValue, TSelf>.From(value);
    }

    public override void Write(Utf8JsonWriter writer, TSelf value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.Value, options);
    }
}
