# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Build the solution
dotnet build DimensionAndSort.sln

# Run all tests (net8.0 and net48 simultaneously)
dotnet test

# Run with warnings-as-errors (keep this clean)
dotnet test -warnaserror

# Run tests for a specific project
dotnet test UnitTests/UnitTests.csproj

# Run a single test class or method
dotnet test UnitTests/UnitTests.csproj --filter "FullyQualifiedName~PhysicalUnitsTest"
dotnet test UnitTests/UnitTests.csproj --filter "FullyQualifiedName~PhysicalUnitsTest.SomeTestMethod"
```

## Architecture

This is a C# class library targeting **net8.0, net10.0, and net48** from a single SDK-style project (`DimensionAndSort/DimensionAndSort.csproj`). `<LangVersion>latest</LangVersion>` enables modern C# on all targets. `<Nullable>enable</Nullable>` is active — keep the build warning-free.

### Core Design

All quantities store their internal value **in SI units** (`ValueInSIUnits`). The user-facing `Value` property converts back using scale and offset. Unit conversions are lossless round-trips through SI.

**Dimensional representation**: A `Unit` holds a `_dimensions[7]` array of `DimensionUnit` structs — one exponent per SI base dimension (metre, kilogram, second, ampere, kelvin, candela, mole). Derived units (Joule, Watt, Newton, etc.) are defined by their exponent combinations. Multiplying/dividing quantities derives new units automatically; passing incompatible units throws `IncompatibleUnits`.

### Project Structure

| Project | Role |
|---------|------|
| `DimensionAndSort/` | Main library (net8.0 + net10.0 + net48) |
| `DimensionAndSort.Protobuf/` | Optional protobuf-net serialization helpers |
| `UnitTests/` | xUnit tests (net8.0 + net48) |

### Key Files in `DimensionAndSort/`

| File | Role |
|------|------|
| `Unit.cs` | Core unit system: SI base units, derived units, dimensional analysis, operators |
| `Unit.SIPrefix.cs` | `Unit.SIprefix` nested class (partial) |
| `Unit.Scaling.cs` | `Unit.Scaling` struct (partial) |
| `Unit.DimensionUnit.cs` | `Unit.DimensionUnit` struct (partial) |
| `Quantity.cs` | `QuantityBase` + `Quantity`: arithmetic/comparison operators, `ValueInSIUnits`, `SetUnit`, `AdjustPrefix` |
| `IUnit.cs` / `IQuantity.cs` | Interfaces implemented by `Unit` / `QuantityBase` |
| `Quantities.General.cs` | `Time`, `DimensionlessQuantity`, `Percentage` |
| `Quantities.Mechanics.cs` | `Length`, `Mass`, `Area`, `Volume`, `VolumeFlow`, `MassFlow`, `Speed`, `Acceleration`, `Force`, `Pressure`, `Density` |
| `Quantities.Thermal.cs` | `Temperature` |
| `Quantities.Energy.cs` | `Energy`, `EnergyEquivalent`, `HeatingValue`, `SpecificEnergy`, `PowerRampRate`, `Power` |
| `Quantities.Electrical.cs` | `Current`, `Voltage`, `Resistance`, `Capacitance`, `MagneticFlux*`, `Inductance` |
| `Quantities.Chemical.cs` | `LuminousIntensity`, `CatalyticActivity`, `AmountOfSubstance` |
| `StandardUnits.cs` | Static `Units` class — predefined unit instances (metres, joules, watts, etc.) |
| `UnitRegistry.cs` | Name↔Unit lookup built via reflection on `typeof(Units)` fields; used for compact serialization |
| `QuantityJsonConverter.cs` | Newtonsoft.Json converter: compact `{"v":…,"u":"Watt"}` or fallback `{"si":…,"d":[…],…}` |
| `Monetary.cs` | `Currency`, `MonetaryAmount`, `PriceUnit`/`UnitPrice` — currencies keyed to EUR exchange rates |
| `Constants.cs` | Physical constants (gas constant, Planck, gravitational constant, speed of light) |
| `Exceptions.cs` | `IncompatibleUnits` exception thrown on dimension mismatch |

### Adding a New Unit or Quantity

1. Define the `Unit` subclass in the appropriate `Unit*.cs` or `StandardUnits.cs` file (set `_dimensions` exponents and scale factor).
2. Add a typed quantity class to the appropriate `Quantities.*.cs` file, following the existing pattern.
3. Register a static instance in `StandardUnits.cs`.
4. Add tests in `UnitTests/PhysicalUnitsTest.cs`.

### Monetary System

`Currency` extends `Unit` with a dimensionless unit (all exponents 0). Exchange rates to EUR are set on each `Currency` instance. `MonetaryAmount` stores values in EUR internally. `PriceUnit` represents price-per-physical-unit (e.g. EUR/MWh).

### Compact Serialization

`QuantityJsonConverter` (Newtonsoft.Json) writes compact JSON when the unit is found in `UnitRegistry` (`{"v": 1.0, "u": "MegaWatt"}`), and a fallback format with full dimension array otherwise. `DimensionAndSort.Protobuf/QuantityDto.cs` provides the same for protobuf-net v3 via `RuntimeTypeModel.Default.SetSurrogate()`. Tests are in `UnitTests/CompactSerializationTests.cs`.
