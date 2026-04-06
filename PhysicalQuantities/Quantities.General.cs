using PhysicalQuantities;

namespace PhysicalQuantities
{
    public class Time : QuantityBase, IComparable<Time>
    {
        public static TimeUnit? _second = Units.Second;
        public Time(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _second, prefix) { }
        public Time(double val, Unit? unit, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, unit, prefix) { }
        public Time()
        {

        }
        public static implicit operator Time(double val) { return new Time(val, _second); }
        public static implicit operator Time(Quantity mq)
        {
            if (mq.Unit!.SameDimension(_second))
            {
                return new Time(mq.Value, mq.Unit, mq.PrefixIndex);
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static Time operator +(Time q1, Time q2)
        {
            return new Time(q1.Unit!.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Time operator -(Time q1, Time q2)
        {
            return new Time(q1.Unit!.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public int CompareTo(Time? other) => ValueInSIUnits.CompareTo(other?.ValueInSIUnits);

        public override QuantityBase Clone()
        {
            return new Time(Value, _unit, _prefixIndex);
        }
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

    public class Frequency : QuantityBase, IComparable<Frequency>
    {
        static Hertz? _hertz = Units.Hertz;
        public Frequency(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _hertz, prefix) { }
        public Frequency(double val, Unit? unit, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, unit, prefix) { }
        public static implicit operator Frequency(double val) { return new Frequency(val); }
        public static implicit operator Frequency(Quantity mq)
        {
            if (mq.Unit!.SameDimension(_hertz))
                return new Frequency(mq.Value, mq.Unit, mq.PrefixIndex);
            throw new IncompatibleUnits();
        }
        public static Frequency operator +(Frequency q1, Frequency q2)
            => new Frequency(q1.Unit!.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        public static Frequency operator -(Frequency q1, Frequency q2)
            => new Frequency(q1.Unit!.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        public int CompareTo(Frequency? other) => ValueInSIUnits.CompareTo(other?.ValueInSIUnits);
        public override QuantityBase Clone() => new Frequency(Value, _unit, _prefixIndex);
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
