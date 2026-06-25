using PhysicalQuantities;

namespace PhysicalQuantities
{
    public class Energy : Quantity<Energy>
    {
        private static Joule? _jouleUnit = new Joule();
        public Energy(Energy e) : base(e.Value, e.Unit, e.PrefixIndex) { }
        public Energy(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _jouleUnit, prefix) { }
        public Energy(double val, Unit? u, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, u, prefix) { }
        public Energy() { }

        protected override Unit CanonicalUnit => _jouleUnit!;
        protected override ConversionMode Conversion => ConversionMode.CanonicalInstanceWhenEqual;

        public static implicit operator Energy(double val) => FromValue(val);
        public static implicit operator Energy(Quantity mq) => FromQuantity(mq);
    }

    public class EnergyEquivalent : Quantity<EnergyEquivalent>
    {
        private static Unit? _energyPerVolume = new Joule() / new QubicMetre();

        public EnergyEquivalent(EnergyEquivalent e) : base(e.Value, e.Unit, e.PrefixIndex) { }
        public EnergyEquivalent() { }
        public EnergyEquivalent(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _energyPerVolume, prefix) { }
        public EnergyEquivalent(double val, Unit? u, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, u, prefix) { }

        protected override Unit CanonicalUnit => _energyPerVolume!;
        protected override ConversionMode Conversion => ConversionMode.CanonicalInstanceWhenEqual;

        public static implicit operator EnergyEquivalent(double val) => FromValue(val);
        public static implicit operator EnergyEquivalent(Quantity mq) => FromQuantity(mq);
    }

    public class HeatingValue : Quantity<HeatingValue>
    {
        private static Joule _energyUnit = new Joule(Unit.SI_Prefix.kilo);
        private static Kilogram _weightUnit = new Kilogram();
        private static Unit? _heatingValueUnit = _energyUnit / _weightUnit;
        public HeatingValue(HeatingValue e) : base(e.Value, e.Unit, e.PrefixIndex) { }
        public HeatingValue(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _heatingValueUnit, prefix) { }
        public HeatingValue(double val, Unit? u, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, u, prefix) { }
        public HeatingValue() { }

        protected override Unit CanonicalUnit => _heatingValueUnit!;
        protected override ConversionMode Conversion => ConversionMode.CanonicalInstanceWhenEqual;

        public static implicit operator HeatingValue(double val) => FromValue(val);
        public static implicit operator HeatingValue(Quantity mq) => FromQuantity(mq);
    }

    public class SpecificEnergy : Quantity<SpecificEnergy>
    {
        private static Joule _energyUnit = new Joule();
        private static Kilogram _weightUnit = new Kilogram();
        private static Unit? _specificEnergyUnit = _energyUnit / _weightUnit;

        public SpecificEnergy() { }
        public SpecificEnergy(SpecificEnergy e) : base(e.Value, e.Unit, e.PrefixIndex) { }
        public SpecificEnergy(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _specificEnergyUnit, prefix) { }
        public SpecificEnergy(double val, Unit? u, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, u, prefix) { }

        protected override Unit CanonicalUnit => _specificEnergyUnit!;
        protected override ConversionMode Conversion => ConversionMode.CanonicalInstanceWhenEqual;

        public static implicit operator SpecificEnergy(double val) => FromValue(val);
        public static implicit operator SpecificEnergy(Quantity mq) => FromQuantity(mq);
    }

    public class PowerRampRate : Quantity<PowerRampRate>
    {
        static Unit? _wattsPerSecond = Units.Watt / Units.Second;

        public PowerRampRate(PowerRampRate p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public PowerRampRate(double val) : base(val, _wattsPerSecond) { }
        public PowerRampRate(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _wattsPerSecond, prefix) { }
        public PowerRampRate(double val, Unit? u, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, u, prefix) { }
        public PowerRampRate() { }

        protected override Unit CanonicalUnit => _wattsPerSecond!;
        protected override ConversionMode Conversion => ConversionMode.NormalizeToCanonical;

        public static implicit operator PowerRampRate(double val) => FromValue(val);
        public static implicit operator PowerRampRate(Quantity mq) => FromQuantity(mq);
    }

    public class Power : Quantity<Power>
    {
        static Watt? _watt = new Watt();
        public Power(Power p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public Power(double val = 0.0) : base(val, _watt) { }
        public Power(double val = 0.0, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _watt, prefix) { }
        public Power(double val = 0.0, Unit? u = null, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, u, prefix) { }
        public Power() { }

        protected override Unit CanonicalUnit => _watt!;
        protected override ConversionMode Conversion => ConversionMode.NormalizeToCanonical;

        public static implicit operator Power(double val) => FromValue(val);
        public static implicit operator Power(Quantity mq) => FromQuantity(mq);
    }
}
