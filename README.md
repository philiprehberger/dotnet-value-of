# Philiprehberger.ValueOf

[![CI](https://github.com/philiprehberger/dotnet-value-of/actions/workflows/ci.yml/badge.svg)](https://github.com/philiprehberger/dotnet-value-of/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/Philiprehberger.ValueOf.svg)](https://www.nuget.org/packages/Philiprehberger.ValueOf)
[![License](https://img.shields.io/github/license/philiprehberger/dotnet-value-of)](LICENSE)

Strongly-typed value objects with built-in validation and JSON support — eliminate primitive obsession.

## Installation

```bash
dotnet add package Philiprehberger.ValueOf
```

## Usage

### Define a custom value object

```csharp
using Philiprehberger.ValueOf;

public class EmailAddress : ValueOf<string, EmailAddress>
{
    protected override void Validate()
    {
        if (string.IsNullOrWhiteSpace(Value) || !Value.Contains('@'))
            throw new ValueOfValidationException(typeof(EmailAddress), "Invalid email address.");
    }
}
```

### Create and use instances

```csharp
var email = EmailAddress.From("user@example.com");
string raw = email;                    // implicit conversion to string
var copy = (EmailAddress)"a@b.com";    // explicit conversion from string

Console.WriteLine(email);              // "user@example.com"
Console.WriteLine(email == copy);      // False
```

### Built-in types

```csharp
var name = NonEmptyString.From("Alice");
var count = PositiveInt.From(42);
var rate = Percentage.From(99.5m);

// Validation throws on invalid values
NonEmptyString.From("");     // throws ValueOfValidationException
PositiveInt.From(-1);        // throws ValueOfValidationException
Percentage.From(101m);       // throws ValueOfValidationException
```

### JSON serialization

```csharp
using System.Text.Json;

var options = new JsonSerializerOptions();
options.Converters.Add(new ValueOfJsonConverterFactory());

var json = JsonSerializer.Serialize(email, options);   // "\"user@example.com\""
var back = JsonSerializer.Deserialize<EmailAddress>(json, options);
```

## API

### `ValueOf<TValue, TSelf>`

| Member | Description |
|--------|-------------|
| `Value` | The underlying primitive value |
| `From(TValue)` | Creates a validated instance |
| `Validate()` | Override to add custom validation logic |
| `Equals(TSelf)` | Value-based equality |
| `CompareTo(TSelf)` | Value-based comparison |
| `ToString()` | Returns string representation of the value |
| `operator ==` / `!=` | Equality operators |
| `implicit operator TValue` | Unwraps to the underlying value |
| `explicit operator ValueOf` | Wraps a primitive into the value object |

### `ValueOfJsonConverterFactory`

| Member | Description |
|--------|-------------|
| `CanConvert(Type)` | Returns true for any `ValueOf<,>` derived type |
| `CreateConverter(Type, JsonSerializerOptions)` | Creates a converter that reads/writes the underlying value |

### `ValueOfValidationException`

| Member | Description |
|--------|-------------|
| `ValueObjectType` | The type that failed validation |
| `Message` | Formatted as `"{TypeName}: {message}"` |

### Built-in Types

| Type | Wraps | Validation |
|------|-------|------------|
| `NonEmptyString` | `string` | Not null or empty |
| `PositiveInt` | `int` | Greater than 0 |
| `Percentage` | `decimal` | Between 0 and 100 inclusive |

### `ValueOfAttribute`

Marker attribute for decorating value object types. Reserved for future source generator support.

## Development

```bash
dotnet build src/Philiprehberger.ValueOf.csproj --configuration Release
```

## License

MIT
