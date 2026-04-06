# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Build the solution
dotnet build DimensionAndSort.sln

# Run all tests
dotnet test DimensionAndSort.sln

# Run tests for a specific project
dotnet test UnitTests/UnitTests.csproj

# Run a single test class or method
dotnet test UnitTests/UnitTests.csproj --filter "FullyQualifiedName~PhysicalUnitsTest"
dotnet test UnitTests/UnitTests.csproj --filter "FullyQualifiedName~PhysicalUnitsTest.SomeTestMethod"
```

## Architecture

This is a C# class library (.NET 8.0) for representing physical quantities with type-safe unit conversions. Three build targets exist: `DimensionAndSort` (.NET 8.0), `DimensionAndSort48` (.NET Framework 4.8), and `DimensionAndSortLight` (lightweight .NET 4.8 variant).

### Core Design

All quantities store their internal value **in SI units** (`ValueInSIUnits`). The user-facing `Value` property converts to/from the chosen unit using scale and offset. This means unit conversions are always lossless round-trips through SI.

**Dimensional representation**: A `Unit` holds a `_dimensions[7]` array of `DimensionUnit` structs — one exponent per SI base dimension (metre, kilogram, second, ampere, kelvin, candela, mole). Derived units (Joule, Watt, Newton, etc.) are defined by their exponent combinations. Multiplying/dividing quantities derives new units automatically; passing incompatible units throws `IncompatibleUnits`.

### Key Files

| File | Role |
|------|------|
| `Unit.cs` | Core unit system: SI base units, derived units, dimensional analysis, SI prefixes (21 levels, yokto–yotta) |
| `Quantity.cs` | Generic `Quantity` class with arithmetic/comparison operator overloads and unit conversion |
| `StandardQuantities.cs` | ~40+ typed quantity classes (`Length`, `Mass`, `Energy`, `Power`, etc.) with constructors and operators |
| `StandardUnits.cs` | Static `Units` class — predefined unit instances (metres, feet, seconds, joules, etc.) |
| `Monetary.cs` | `Currency`, `MonetaryAmount`, `PriceUnit`/`UnitPrice` — currencies keyed to EUR exchange rates |
| `Constants.cs` | Physical constants (gas constant, Planck, gravitational constant, speed of light) |
| `Exceptions.cs` | `IncompatibleUnits` exception thrown on dimension mismatch |

### Adding a New Unit or Quantity

1. Define the `Unit` subclass in `Unit.cs` (set `_dimensions` exponents and scale factor).
2. Add a typed quantity class to `StandardQuantities.cs` following the existing pattern.
3. Register a static instance in `StandardUnits.cs` under the relevant region.
4. Add tests in `UnitTests/PhysicalUnitsTest.cs`.

### Monetary System

`Currency` extends `Unit`. Exchange rates to EUR are set on each `Currency` instance. `MonetaryAmount` wraps a `Quantity` for currency-aware arithmetic. `PriceUnit` represents price-per-physical-unit (e.g. EUR/MWh).

### Serialization

JSON serialization uses Newtonsoft.Json and preserves unit metadata. Tests are in `UnitTests/SerializationTests.cs`.
