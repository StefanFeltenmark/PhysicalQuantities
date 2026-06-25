using PhysicalQuantities;

namespace PhysicalQuantities
{
    public class Length : Quantity<Length>
    {
        public Length(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, Units.Metre, prefix) { }
        public Length(double val, Unit? unit, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, unit, prefix) { }
        public Length() { }

        protected override Unit CanonicalUnit => Units.Metre!;

        public static implicit operator Length(double val) => FromValue(val);
        public static implicit operator Length(Quantity mq) => FromQuantity(mq);
    }

    public class Mass : Quantity<Mass>
    {
        protected static Kilogram? _kilo = Units.Kilogram;

        public Mass(double val) : base(val, _kilo) { }
        public Mass(double val, Unit.SI_Prefix prefix) : base(val, _kilo, prefix) { }
        public Mass(double val, Unit? unit, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, unit, prefix) { }
        public Mass() { }

        protected override Unit CanonicalUnit => _kilo!;

        public static implicit operator Mass(double val) => FromValue(val);
        public static implicit operator Mass(Quantity mq) => FromQuantity(mq);
    }

    public class Area : Quantity<Area>
    {
        protected static SquareMetre? _squareMetreUnit = new SquareMetre();

        public Area(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, new SquareMetre(), prefix) { }
        public Area(double val, Unit? unit, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, unit, prefix) { }
        public Area() : base(0, new SquareMetre(), Unit.SI_Prefix.unity) { }

        protected override Unit CanonicalUnit => _squareMetreUnit!;

        public static implicit operator Area(double val) => FromValue(val);
        public static implicit operator Area(Quantity mq) => FromQuantity(mq);
    }

    public class Volume : Quantity<Volume>
    {
        private static QubicMetre? _qubicMetreUnit = Units.QubicMetre;
        public Volume(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _qubicMetreUnit, prefix) { }
        public Volume(double val, Unit? unit, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, unit, prefix) { }
        public Volume() { }

        protected override Unit CanonicalUnit => _qubicMetreUnit!;

        public static implicit operator Volume(double val) => FromValue(val);
        public static implicit operator Volume(Quantity mq) => FromQuantity(mq);
    }

    public class VolumeFlow : Quantity<VolumeFlow>
    {
        public VolumeFlow(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, new QubicMetrePerSecond(), prefix) { }
        public VolumeFlow(double val, Unit? unit, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, unit, prefix) { }
        public VolumeFlow() { }

        protected override Unit CanonicalUnit => new QubicMetrePerSecond();

        public static implicit operator VolumeFlow(double val) => FromValue(val);
        public static implicit operator VolumeFlow(Quantity mq) => FromQuantity(mq);
    }

    public class MassFlow : Quantity<MassFlow>
    {
        protected static KilogramPerSecond? _kilogramPerSecondUnit = Units.KilogramPerSecond;
        public MassFlow(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _kilogramPerSecondUnit, prefix) { }
        public MassFlow(double val, Unit? unit, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, unit, prefix) { }
        public MassFlow() { }

        protected override Unit CanonicalUnit => _kilogramPerSecondUnit!;

        public static implicit operator MassFlow(double val) => FromValue(val);
        public static implicit operator MassFlow(Quantity mq) => FromQuantity(mq);
    }

    public class Speed : Quantity<Speed>
    {
        private static Unit? _metrePerSecond = new Unit(1, 0, -1, 0, 0, 0, 0);
        public Speed(double val) : base(val, _metrePerSecond) { }
        public Speed(double val, Unit.SI_Prefix prefix) : base(val, _metrePerSecond, prefix) { }
        public Speed(double val, Unit? unit, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, unit, prefix) { }
        public Speed() { }

        protected override Unit CanonicalUnit => _metrePerSecond!;

        public static implicit operator Speed(double val) => FromValue(val);
        public static implicit operator Speed(Quantity mq) => FromQuantity(mq);
    }

    public class Acceleration : Quantity<Acceleration>
    {
        private static Unit? _metrePerSecond2 = new Unit(1, 0, -2, 0, 0, 0, 0);
        public Acceleration(double val) : base(val, _metrePerSecond2) { }
        public Acceleration(double val, Unit.SI_Prefix prefix) : base(val, _metrePerSecond2, prefix) { }
        public Acceleration(double val, Unit? unit, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, unit, prefix) { }
        public Acceleration() { }

        protected override Unit CanonicalUnit => _metrePerSecond2!;

        public static implicit operator Acceleration(double val) => FromValue(val);
        public static implicit operator Acceleration(Quantity mq) => FromQuantity(mq);
    }

    public class Force : Quantity<Force>
    {
        static Newton? _newton = Units.Newton;
        public Force(double val) : base(val, _newton) { }
        public Force(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _newton, prefix) { }
        public Force(double val, Unit? u, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, u, prefix) { }
        public Force() { }

        protected override Unit CanonicalUnit => _newton!;
        protected override ConversionMode Conversion => ConversionMode.NormalizeToCanonical;

        public static implicit operator Force(double val) => FromValue(val);
        public static implicit operator Force(Quantity mq) => FromQuantity(mq);
    }

    public class Pressure : Quantity<Pressure>
    {
        static Pascal? _pascal = Units.Pascal;
        public Pressure(Pressure p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public Pressure(double val) : base(val, _pascal) { }
        public Pressure(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _pascal, prefix) { }
        public Pressure(double val, Unit? u, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, u, prefix) { }
        public Pressure() { }

        protected override Unit CanonicalUnit => _pascal!;
        protected override ConversionMode Conversion => ConversionMode.NormalizeToCanonical;

        public static implicit operator Pressure(double val) => FromValue(val);
        public static implicit operator Pressure(Quantity mq) => FromQuantity(mq);
    }

    public class Density : Quantity<Density>
    {
        static Unit? _densityUnit = new Unit(-3, 1, 0, 0, 0, 0, 0);
        public Density(Density p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public Density(double val) : base(val, _densityUnit) { }
        public Density(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _densityUnit, prefix) { }
        public Density(double val, Unit? u, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, u, prefix) { }
        public Density() { }

        protected override Unit CanonicalUnit => _densityUnit!;

        public static implicit operator Density(double val) => FromValue(val);
        public static implicit operator Density(Quantity mq) => FromQuantity(mq);
    }
}
