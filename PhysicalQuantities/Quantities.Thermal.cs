using PhysicalQuantities;

namespace PhysicalQuantities
{
    public class Temperature : QuantityBase, IComparable<Temperature>
    {
        protected static Kelvin? _kelvin = Units.Kelvin;
        public Temperature(double val) : base(val, _kelvin) { }
        public Temperature(double val, Unit.SI_Prefix prefix) : base(val, _kelvin, prefix) { }
        public Temperature(double val, Unit? unit, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, unit, prefix) { }
        public static implicit operator Temperature(double val)
        {
            return new Temperature(val);
        }
        public static implicit operator Temperature(Quantity mq)
        {
            if (mq.Unit!.SameDimension(_kelvin))
            {
                if (mq.Unit == _kelvin)
                {
                    return new Temperature(mq.Value, _kelvin, mq.PrefixIndex);
                }
                else
                {
                    Temperature t = new Temperature(mq.Value, mq.Unit, mq.PrefixIndex);
                    t.SetUnit(_kelvin);
                    return t;
                }
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static Temperature operator +(Temperature q1, Temperature q2)
        {
            return new Temperature(q1.Unit!.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Temperature operator -(Temperature q1, Temperature q2)
        {
            return new Temperature(q1.Unit!.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public int CompareTo(Temperature? other) => ValueInSIUnits.CompareTo(other?.ValueInSIUnits);

        public override QuantityBase Clone()
        {
            return new Temperature(Value, _unit, _prefixIndex);
        }
    }
}
