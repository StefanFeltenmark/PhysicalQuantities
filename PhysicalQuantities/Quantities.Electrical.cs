using PhysicalQuantities;

namespace PhysicalQuantities
{
    public class Current : Quantity<Current>
    {
        static Unit? _ampere = Units.Ampere;
        public Current(Current p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public Current(double val) : base(val, _ampere) { }
        public Current(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _ampere, prefix) { }
        public Current(double val, Unit? u, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, u, prefix) { }
        public Current() { }

        protected override Unit CanonicalUnit => _ampere!;
        protected override ConversionMode Conversion => ConversionMode.NormalizeToCanonical;

        public static implicit operator Current(double val) => FromValue(val);
        public static implicit operator Current(Quantity mq) => FromQuantity(mq);
    }

    public class Voltage : Quantity<Voltage>
    {
        static Unit? _volt = Units.Volt;
        public Voltage(Voltage p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public Voltage(double val) : base(val, _volt) { }
        public Voltage(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _volt, prefix) { }
        public Voltage(double val, Unit? u, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, u, prefix) { }
        public Voltage() { }

        protected override Unit CanonicalUnit => _volt!;
        protected override ConversionMode Conversion => ConversionMode.NormalizeToCanonical;

        public static implicit operator Voltage(double val) => FromValue(val);
        public static implicit operator Voltage(Quantity mq) => FromQuantity(mq);
    }

    public class Resistance : Quantity<Resistance>
    {
        static Unit? _ohm = Units.Ohm;
        public Resistance(Resistance p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public Resistance(double val) : base(val, _ohm) { }
        public Resistance(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _ohm, prefix) { }
        public Resistance(double val, Unit? u, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, u, prefix) { }
        public Resistance() { }

        protected override Unit CanonicalUnit => _ohm!;
        protected override ConversionMode Conversion => ConversionMode.NormalizeToCanonical;

        public static implicit operator Resistance(double val) => FromValue(val);
        public static implicit operator Resistance(Quantity mq) => FromQuantity(mq);
    }

    public class Capacitance : Quantity<Capacitance>
    {
        static Unit? _farad = Units.Farad;
        public Capacitance(Capacitance p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public Capacitance(double val) : base(val, _farad) { }
        public Capacitance(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _farad, prefix) { }
        public Capacitance(double val, Unit? u, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, u, prefix) { }
        public Capacitance() { }

        protected override Unit CanonicalUnit => _farad!;
        protected override ConversionMode Conversion => ConversionMode.NormalizeToCanonical;

        public static implicit operator Capacitance(double val) => FromValue(val);
        public static implicit operator Capacitance(Quantity mq) => FromQuantity(mq);
    }

    public class MagneticFluxIntensity : Quantity<MagneticFluxIntensity>
    {
        static Unit? _amperePerMetre = Units.AmperePerMetre;
        public MagneticFluxIntensity(MagneticFluxIntensity p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public MagneticFluxIntensity(double val) : base(val, _amperePerMetre) { }
        public MagneticFluxIntensity(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _amperePerMetre, prefix) { }
        public MagneticFluxIntensity(double val, Unit? u, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, u, prefix) { }
        public MagneticFluxIntensity() { }

        protected override Unit CanonicalUnit => _amperePerMetre!;
        protected override ConversionMode Conversion => ConversionMode.NormalizeToCanonical;

        public static implicit operator MagneticFluxIntensity(double val) => FromValue(val);
        public static implicit operator MagneticFluxIntensity(Quantity mq) => FromQuantity(mq);
    }

    public class MagneticFluxDensity : Quantity<MagneticFluxDensity>
    {
        static Unit? _tesla = Units.Tesla;
        public MagneticFluxDensity(MagneticFluxDensity p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public MagneticFluxDensity(double val) : base(val, _tesla) { }
        public MagneticFluxDensity(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _tesla, prefix) { }
        public MagneticFluxDensity(double val, Unit? u, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, u, prefix) { }
        public MagneticFluxDensity() { }

        protected override Unit CanonicalUnit => _tesla!;
        protected override ConversionMode Conversion => ConversionMode.NormalizeToCanonical;

        public static implicit operator MagneticFluxDensity(double val) => FromValue(val);
        public static implicit operator MagneticFluxDensity(Quantity mq) => FromQuantity(mq);
    }

    public class MagneticFlux : Quantity<MagneticFlux>
    {
        static Unit? _weber = Units.Weber;
        public MagneticFlux(MagneticFlux p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public MagneticFlux(double val) : base(val, _weber) { }
        public MagneticFlux(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _weber, prefix) { }
        public MagneticFlux(double val, Unit? u, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, u, prefix) { }
        public MagneticFlux() { }

        protected override Unit CanonicalUnit => _weber!;
        protected override ConversionMode Conversion => ConversionMode.NormalizeToCanonical;

        public static implicit operator MagneticFlux(double val) => FromValue(val);
        public static implicit operator MagneticFlux(Quantity mq) => FromQuantity(mq);
    }

    public class Inductance : Quantity<Inductance>
    {
        static Unit? _henry = Units.Henry;
        public Inductance(Inductance p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public Inductance(double val) : base(val, _henry) { }
        public Inductance(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _henry, prefix) { }
        public Inductance(double val, Unit? u, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, u, prefix) { }
        public Inductance() { }

        protected override Unit CanonicalUnit => _henry!;
        protected override ConversionMode Conversion => ConversionMode.NormalizeToCanonical;

        public static implicit operator Inductance(double val) => FromValue(val);
        public static implicit operator Inductance(Quantity mq) => FromQuantity(mq);
    }

    public class ElectricCharge : Quantity<ElectricCharge>
    {
        static Unit? _coulomb = Units.Coulomb;
        public ElectricCharge(double val) : base(val, _coulomb) { }
        public ElectricCharge(double val, Unit.SI_Prefix prefix) : base(val, _coulomb, prefix) { }
        public ElectricCharge(double val, Unit? u, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, u, prefix) { }
        public ElectricCharge() { }

        protected override Unit CanonicalUnit => _coulomb!;

        public static implicit operator ElectricCharge(double val) => FromValue(val);
        public static implicit operator ElectricCharge(Quantity mq) => FromQuantity(mq);
    }
}
