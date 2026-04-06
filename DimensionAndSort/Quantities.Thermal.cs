using DimensionAndSort;

namespace GreenOptimizer.DimensionAndSort
{
    public class Temperature : QuantityBase
    {
        protected static Kelvin? _kelvin = Units.Kelvin;
        public Temperature(double val) : base(val, _kelvin) { }
        public Temperature(double val, Unit? unit) : base(val, unit) { }
        public Temperature(double val, Unit? unit, Unit.SI_PrefixEnum prefix) : base(val, unit, prefix) { }
        public static implicit operator Temperature(double val)
        {
            return new Temperature(val);
        }
        public static implicit operator Temperature(Quantity mq)
        {
            if (mq.Unit.SameDimension(_kelvin))
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
            return new Temperature(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Temperature operator -(Temperature q1, Temperature q2)
        {
            return new Temperature(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new Temperature(Value, _unit, _prefixIndex);
        }
    }
}
