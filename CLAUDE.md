# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Build the solution
dotnet build PhysicalQuantities.sln

# Build with warnings-as-errors
dotnet build PhysicalQuantities.sln -warnaserror

# Pack the NuGet (also produced on Release build via GeneratePackageOnBuild)
dotnet pack PhysicalQuantities/PhysicalQuantities.csproj -c Release

# Run all tests for the solution (net8.0 + net10.0)
dotnet test --solution PhysicalQuantities.sln

# Run tests for a specific project
dotnet test --project UnitTests/UnitTests.csproj

# Run a single test class or method (xunit.v3 runner flags)
dotnet test --project UnitTests/UnitTests.csproj -- --xunit-class UnitTests.PhysicalUnitsTest
dotnet test --project UnitTests/UnitTests.csproj -- --xunit-method UnitTests.PhysicalUnitsTest.TestLength1
```

**Test stack**: xUnit v3 + Microsoft.Testing.Platform (MTP). `Microsoft.NET.Test.Sdk` (VSTest) is intentionally **not** referenced — VSTest is no longer supported by `dotnet test` on the .NET 10 SDK. The opt-in is via `global.json` at the repo root:

```json
{ "test": { "runner": "Microsoft.Testing.Platform" } }
```

The `UnitTests` project is built as `<OutputType>Exe</OutputType>` with `<UseMicrosoftTestingPlatformRunner>true</UseMicrosoftTestingPlatformRunner>` because MTP test projects are standalone executables. Under MTP, some legacy `dotnet test` flags (e.g. `--nologo`) are not recognized and will be forwarded to the runner where they cause "0 tests ran, error: 2" — drop them. Use `dotnet build … -c Release` first if you want a release-mode test run.

## Architecture

This is a C# class library targeting **net8.0 and net10.0** from a single SDK-style project (`PhysicalQuantities/PhysicalQuantities.csproj`). All code lives in the `PhysicalQuantities` namespace. `<LangVersion>latest</LangVersion>` enables modern C# on all targets. `<Nullable>enable</Nullable>` is active — keep the build warning-free.

### Core Design

All quantities store their internal value **in SI units** (`ValueInSIUnits`). The user-facing `Value` property converts back using scale and offset. Unit conversions are lossless round-trips through SI.

**Dimensional representation**: A `Unit` holds a `_dimensions[7]` array of `DimensionUnit` structs — one exponent per SI base dimension (metre, kilogram, second, ampere, kelvin, candela, mole). Derived units (Joule, Watt, Newton, etc.) are defined by their exponent combinations. Multiplying/dividing quantities derives new units automatically; passing incompatible units throws `IncompatibleUnits`.

**SI prefixes**: The prefix enum is `Unit.SI_Prefix` (renamed from `SI_PrefixEnum` in the legacy `DimensionAndSort` library). Prefixes (`milli`, `kilo`, `mega`, …) are independent of the unit's own scale.

**Typed quantities vs. `Quantity`**: `QuantityBase` arithmetic operators return the untyped `Quantity`. Each typed quantity (`Length`, `Power`, …) defines `implicit operator T(Quantity)` that re-validates dimension at runtime and throws `IncompatibleUnits` on mismatch. So `Mass m * Acceleration a` produces a `Quantity` that's implicitly assignable to `Force` — both representations are part of the public API.

### Project Structure

| Project | Role |
|---------|------|
| `PhysicalQuantities/` | Main library (net8.0 + net10.0) |
| `PhysicalQuantities.Protobuf/` | Optional protobuf-net serialization helpers |
| `UnitTests/` | xUnit tests (net8.0 + net10.0) |

### Key Files in `PhysicalQuantities/`

| File | Role |
|------|------|
| `Unit.cs` | Core unit system: SI base units, derived units, dimensional analysis, operators |
| `Unit.SIPrefix.cs` | `Unit.SIprefix` nested class (partial) |
| `Unit.Scaling.cs` | `Unit.Scaling` struct (partial) |
| `Unit.DimensionUnit.cs` | `Unit.DimensionUnit` struct (partial) |
| `Quantity.cs` | `QuantityBase` + `Quantity`: arithmetic/comparison operators, `ValueInSIUnits`, `SetUnit`, `AdjustPrefix` |
| `IUnit.cs` / `IQuantity.cs` | Interfaces implemented by `Unit` / `QuantityBase` |
| `Quantities.General.cs` | `Time`, `DimensionlessQuantity`, `Frequency`, `Percentage` |
| `Quantities.Mechanics.cs` | `Length`, `Mass`, `Area`, `Volume`, `VolumeFlow`, `MassFlow`, `Speed`, `Acceleration`, `Force`, `Pressure`, `Density` |
| `Quantities.Thermal.cs` | `Temperature` |
| `Quantities.Energy.cs` | `Energy`, `EnergyEquivalent`, `HeatingValue`, `SpecificEnergy`, `PowerRampRate`, `Power` |
| `Quantities.Electrical.cs` | `Current`, `Voltage`, `Resistance`, `Capacitance`, `Inductance`, `MagneticFlux`, `MagneticFluxDensity`, `MagneticFluxIntensity`, `ElectricCharge` |
| `Quantities.Chemical.cs` | `LuminousIntensity`, `CatalyticActivity`, `AmountOfSubstance` |
| `StandardUnits.cs` | Static `Units` class — predefined unit instances and all concrete `Unit` subclasses (`Metre`, `Joule`, `MegaWatt`, `Bar`, `mmHg`, …) |
| `UnitRegistry.cs` | Name↔Unit lookup built via reflection over `public static Unit`-typed fields **directly on `Units`** (nested classes like `Units.Lengths` are skipped). Used for compact serialization |
| `QuantityJsonConverter.cs` | Newtonsoft.Json converter: compact `{"v":…,"u":"Watt"}`, currency `{"v":…,"c":"USD"}`, or fallback `{"si":…,"d":[…],…}` |
| `Monetary.cs` | `Currency` (and subclasses `Euro`/`USDollar`/…), `MonetaryAmount`, `PriceUnit`/`UnitPrice` — currencies keyed to EUR exchange rates |
| `Extensions.cs` | Extension methods on `Unit`/`Quantity`: `ToBaseUnit`, `ToDerivedUnit`, `ToUnit`, `ToPrefix` |
| `Constants.cs` | Physical constants (gas constant, Planck, gravitational constant, speed of light) |
| `Exceptions.cs` | `IncompatibleUnits` exception thrown on dimension mismatch |

### Adding a New Unit or Quantity

1. Define the `Unit` subclass in `StandardUnits.cs` (`_dimensions` exponents and scale factor). Currency subclasses go in `Monetary.cs`. The `Unit.SIPrefix.cs` / `Unit.Scaling.cs` / `Unit.DimensionUnit.cs` files are partials of `Unit` itself, not where new units live.
2. Add a typed quantity class to the appropriate `Quantities.*.cs` file, following the existing pattern: typed `+`/`-` operators, `implicit operator T(double)`, and `implicit operator T(Quantity)` with a `SameDimension` runtime check.
3. Register a `public static` field on `Units` *directly* — `UnitRegistry` reflection skips nested classes like `Units.Lengths`, so a unit registered there silently falls back to the verbose JSON shape on serialization.
4. Add tests in `UnitTests/PhysicalUnitsTest.cs`.

### Monetary System

`Currency` extends `Unit` with a dimensionless unit (all exponents 0). Exchange rates to EUR are set on each `Currency` instance. `MonetaryAmount` stores values in EUR internally. `PriceUnit` represents price-per-physical-unit (e.g. EUR/MWh).

### Compact Serialization

`QuantityJsonConverter` (Newtonsoft.Json) emits three shapes:

- Registered unit: `{"v": 1.0, "u": "MegaWatt"}` (display value + registry name).
- Currency: `{"v": …, "c": "USD"}` where `v` is the SI value (i.e. EUR) and `c` is the currency code — distinct key from the standard `"u"`.
- Unregistered fallback: `{"si": …, "d": […], "s": …, "o": …, "p": …}` with the full 7-dimension exponent array, scale, offset, and prefix index.

`PhysicalQuantities.Protobuf/QuantityDto.cs` provides the equivalent for protobuf-net v3 via `RuntimeTypeModel.Default.SetSurrogate()` (call `QuantityDto.RegisterSurrogate()` once at startup to use it on raw `QuantityBase` fields). Tests are in `UnitTests/CompactSerializationTests.cs`.

### Pitfalls

- **Scale × prefix × offset.** Custom units that bake a non-default scale into their constructor (e.g. `MegaWattHour`, `Bar`, `mmHg`) collide with `Unit.RecomputeScale()`, which rebuilds `_scale` from per-dimension scalings + prefixes alone — overwriting any externally set value. Recent bugs in this area: `80ec2e0` (MegaWattHour scale), `da48bf1` (Weber dimensions), `1696df0` (ramp-rate). When adding such a unit, double-check the constructor's final `_scale` and `_offset`.
- **Affine units** (`Celsius`, `Farenheit`) carry `_offset != 0`. The library uses the same `Temperature` type for both absolute temperatures and temperature differences and does not distinguish them, so `Temperature(20°C) + Temperature(20°C)` returns a value but the caller is responsible for whether it makes sense.

### Legacy folders (ignore)

These directories from the pre-rename layout are still on disk but are **not** part of `PhysicalQuantities.sln` and are no longer maintained — don't edit them, don't reference them from new code, and don't include them in build/test commands:

`DimensionAndSort/`, `DimensioAndSortLight/` (sic), `DimensionAndSort48/`, `DimensionAndSortCT/`, `UnitTests48/`, `UnitTestsLight/`, `DimensionAndSort.sln.DotSettings.user`, `packages/`.

The repository root directory is also still named `DimensionAndSort` even though the package is `PhysicalQuantities`. The `README.md` at the root is the package readme for `PhysicalQuantities` and is included in the NuGet — it's the authoritative public-facing documentation; keep it in sync with API changes.
