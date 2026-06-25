using PhysicalQuantities;

namespace PhysicalQuantities
{
    public class Time : Quantity<Time>
    {
        public static TimeUnit? _second = Units.Second;
        public Time(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _second, prefix) { }
        public Time(double val, Unit? unit, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, unit, prefix) { }
        public Time() { }

        protected override Unit CanonicalUnit => _second!;

        public static implicit operator Time(double val) => FromValue(val);
        public static implicit operator Time(Quantity mq) => FromQuantity(mq);
    }

    public class DimensionlessQuantity : QuantityBase, IComparable<DimensionlessQuantity>
    {
        protected static Dimensionless? _dimless = Units.Dimensionless;
        public DimensionlessQuantity(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, new Dimensionless(), prefix) { }
        public static implicit operator DimensionlessQuantity(double val) { return new DimensionlessQuantity(val); }
        public static implicit operator DimensionlessQuantity(Quantity mq)
        {
            if (mq.Unit!.SameDimension(_dimless))
            {
                return new DimensionlessQuantity(mq.Value, mq.PrefixIndex);
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }

        public int CompareTo(DimensionlessQuantity? other) => ValueInSIUnits.CompareTo(other?.ValueInSIUnits);

        public override QuantityBase Clone()
        {
            return new DimensionlessQuantity(Value, _prefixIndex);
        }
    }

    public class Frequency : Quantity<Frequency>
    {
        static Hertz? _hertz = Units.Hertz;
        public Frequency(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _hertz, prefix) { }
        public Frequency(double val, Unit? unit, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, unit, prefix) { }
        public Frequency() { }

        protected override Unit CanonicalUnit => _hertz!;

        public static implicit operator Frequency(double val) => FromValue(val);
        public static implicit operator Frequency(Quantity mq) => FromQuantity(mq);
    }

    public class Percentage : QuantityBase, IComparable<Percentage>
    {
        protected static Percent? _perc = Units.Percent;
        public Percentage(double val) : base(val, Units.Percent, Unit.SI_Prefix.unity, "%") { }

        public static implicit operator Percentage(double val) { return new Percentage(val); }
        public static implicit operator Percentage(Quantity mq)
        {
            if (mq.Unit!.SameDimension(_perc))
            {
                return new Percentage(mq.Value);
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }

        public int CompareTo(Percentage? other) => ValueInSIUnits.CompareTo(other?.ValueInSIUnits);

        public override QuantityBase Clone()
        {
            return new Percentage(Value);
        }
    }
}
