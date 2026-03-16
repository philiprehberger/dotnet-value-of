# Changelog

## 0.1.2

- Add badges, Development section to README
- Add GenerateDocumentationFile, RepositoryType, PackageReadmeFile to .csproj

## 0.1.0 (2026-03-15)

- Initial release
- Abstract `ValueOf<TValue, TSelf>` base class with equality, comparison, and conversions
- `ValueOfJsonConverterFactory` for System.Text.Json serialization
- `ValueOfAttribute` marker attribute for future source generator support
- Built-in types: `NonEmptyString`, `PositiveInt`, `Percentage`
- `ValueOfValidationException` for validation failures
