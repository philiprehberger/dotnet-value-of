# Philiprehberger.ValueOf

[![CI](https://github.com/philiprehberger/dotnet-value-of/actions/workflows/ci.yml/badge.svg)](https://github.com/philiprehberger/dotnet-value-of/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/Philiprehberger.ValueOf.svg)](https://www.nuget.org/packages/Philiprehberger.ValueOf)
[![Last updated](https://img.shields.io/github/last-commit/philiprehberger/dotnet-value-of)](https://github.com/philiprehberger/dotnet-value-of/commits/main)

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

### Non-throwing creation with `TryFrom`

```csharp
using Philiprehberger.ValueOf;

if (PositiveInt.TryFrom(userInput, out var positive))
{
    Console.WriteLine($"Got {positive!.Value}");
}
else
{
    Console.WriteLine("Invalid value, no exception raised.");
}
```

### Additional built-in types

```csharp
using Philiprehberger.ValueOf;

var clicks = NonNegativeInt.From(0);
var ratio = UnitInterval.From(0.75m);          // 0 ≤ value ≤ 1
var name = NonEmptyTrimmedString.From("  hi "); // stores "hi"
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
| `From(TValue)` | Creates a validated instance (throws on null/invalid) |
| `TryFrom(TValue, out TSelf?)` | Non-throwing variant; returns `false` on validation failure |
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
| `NonEmptyTrimmedString` | `string` | Not null/empty after trimming whitespace; stored trimmed |
| `PositiveInt` | `int` | Greater than 0 |
| `NonNegativeInt` | `int` | Greater than or equal to 0 |
| `Percentage` | `decimal` | Between 0 and 100 inclusive |
| `UnitInterval` | `decimal` | Between 0 and 1 inclusive |

### `ValueOfAttribute`

Marker attribute for decorating value object types. Reserved for future source generator support.

## Development

```bash
dotnet build src/Philiprehberger.ValueOf.csproj --configuration Release
```

## Support

If you find this project useful:

⭐ [Star the repo](https://github.com/philiprehberger/dotnet-value-of)

🐛 [Report issues](https://github.com/philiprehberger/dotnet-value-of/issues?q=is%3Aissue+is%3Aopen+label%3Abug)

💡 [Suggest features](https://github.com/philiprehberger/dotnet-value-of/issues?q=is%3Aissue+is%3Aopen+label%3Aenhancement)

❤️ [Sponsor development](https://github.com/sponsors/philiprehberger)

🌐 [All Open Source Projects](https://philiprehberger.com/open-source-packages)

💻 [GitHub Profile](https://github.com/philiprehberger)

🔗 [LinkedIn Profile](https://www.linkedin.com/in/philiprehberger)

## License

[MIT](LICENSE)
