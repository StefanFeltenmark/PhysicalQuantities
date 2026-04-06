
# PhysicalQuantities

PhysicalQuantities is a C# library for representing physical quantities (force, energy, length) and their units (m, s, W, J) with type-safe dimensional analysis.

## Installation

```
dotnet add package PhysicalQuantities
```

## Features

- Strongly typed quantities: `Length`, `Mass`, `Force`, `Energy`, `Power`, `Voltage`, `Temperature`, and many more
- Full arithmetic with automatic dimensional analysis — multiplying a `Mass` by an `Acceleration` gives a `Force`
- SI prefix support (milli, kilo, mega, giga, …)
- Unit conversion with `SetUnit()` / `ConvertToUnit()`
- `AdjustPrefix()` for human-readable output
- Compact JSON serialization via Newtonsoft.Json

## Usage

### Creating quantities

```csharp
var distance = new Length(500, Unit.SI_Prefix.milli); // 500 mm
var mass     = new Mass(75);                           // 75 kg
var power    = new Power(1.5, Units.MegaWatt);         // 1.5 MW
```

### Arithmetic and dimensional analysis

```csharp
var force    = new Mass(10) * new Acceleration(9.81);  // → Force in N
var pressure = force / new Area(2);                    // → Pressure in Pa
var speed    = new Length(100) / new Time(9.58);       // → Speed in m/s
```

Incompatible unit combinations throw `IncompatibleUnits` at runtime.

### Unit conversion

```csharp
var energy = new Energy(1.0, Units.KiloWattHour);
energy.SetUnit(Units.Joule);
Console.WriteLine(energy); // 3 600 000 J

var converted = energy.ConvertToUnit(Units.KiloWattHour); // non-destructive
```

### Prefix adjustment

```csharp
var power = new Power(1_500_000); // 1 500 000 W
power.AdjustPrefix();
Console.WriteLine(power);         // 1.50 MW
```

### JSON serialization

```csharp
var settings = new JsonSerializerSettings
{
    Converters = { new QuantityJsonConverter() }
};

var power = new Power(42.5, Units.MegaWatt);
string json = JsonConvert.SerializeObject(power, settings);
// {"v":42.5,"u":"MegaWatt"}

var restored = JsonConvert.DeserializeObject<Power>(json, settings);
```

## Supported quantity types

| Category | Types |
|----------|-------|
| Mechanics | `Length`, `Mass`, `Area`, `Volume`, `Speed`, `Acceleration`, `Force`, `Pressure`, `Density`, `MassFlow`, `VolumeFlow` |
| Energy | `Energy`, `Power`, `SpecificEnergy`, `HeatingValue`, `EnergyEquivalent`, `PowerRampRate` |
| Electrical | `Current`, `Voltage`, `Resistance`, `Capacitance`, `Inductance`, `MagneticFlux`, `MagneticFluxDensity` |
| Thermal | `Temperature` |
| Chemical | `AmountOfSubstance`, `CatalyticActivity`, `LuminousIntensity` |
| Other | `Time`, `Frequency`, `ElectricCharge`, `DimensionlessQuantity`, `Percentage` |

## Monetary amounts and pricing

`MonetaryAmount` stores values internally in EUR. Other currencies convert via an exchange rate you set.

```csharp
// Set exchange rates
Currencies.USDollar.ExchangeRateToEur  = 0.92;
Currencies.SwedishCrown.ExchangeRateToEur = 0.088;

// Create amounts in different currencies
var priceEur = new MonetaryAmount(100, Currencies.Euro);
var priceUsd = new MonetaryAmount(108, Currencies.USDollar);

var total = priceEur + priceUsd; // added in EUR internally
```

`UnitPrice` represents a price per physical unit (e.g. EUR/MWh) and supports conversion between currencies and units:

```csharp
var eurPerMWh = new UnitPrice(50, new PriceUnit(Currencies.Euro, Units.MegaWattHour));
var usdPerMWh = eurPerMWh.ConvertToUnit(new PriceUnit(Currencies.USDollar, Units.MegaWattHour));
```

Available currencies: `Euro`, `USDollar`, `SwedishCrown`, `NorwegianCrown`, `TurkishLira`. Additional currencies can be created with `new Currency("GBP") { ExchangeRateToEur = 1.17 }`.

## License

MIT License — Copyright 2023-2026 Stefan Feltenmark, stefan@feltenmark.se
