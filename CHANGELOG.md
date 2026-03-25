# Changelog

## 0.1.5 (2026-03-24)

- Sync .csproj description with README

## 0.1.4 (2026-03-22)

- Add dates to changelog entries

## 0.1.3 (2026-03-17)

- Rename Install section to Installation in README per package guide

## 0.1.2 (2026-03-16)

- Add badges, Development section to README
- Add GenerateDocumentationFile, RepositoryType, PackageReadmeFile to .csproj

## 0.1.0 (2026-03-15)

- Initial release
- Abstract `ValueOf<TValue, TSelf>` base class with equality, comparison, and conversions
- `ValueOfJsonConverterFactory` for System.Text.Json serialization
- `ValueOfAttribute` marker attribute for future source generator support
- Built-in types: `NonEmptyString`, `PositiveInt`, `Percentage`
- `ValueOfValidationException` for validation failures
