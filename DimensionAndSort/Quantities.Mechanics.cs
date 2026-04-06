using DimensionAndSort;

namespace GreenOptimizer.DimensionAndSort
{
    public class Length : QuantityBase
    {
        public Length(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, Units.Metre, prefix) { }
        public Length(double val, Unit? unit, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, unit, prefix) { }
        public Length()
        {

        }
        public static implicit operator Length(double val) { return new Length(val); }
        public static implicit operator Length(Quantity mq)
        {
            if (mq.Unit.SameDimension(Units.Metre))
            {
                return new Length(mq.Value, mq.Unit, mq.PrefixIndex);
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static Length operator +(Length q1, Length q2)
        {
            return new Length(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Length operator -(Length q1, Length q2)
        {
            return new Length(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Length operator -(Length q1)
        {
            return new Length(-q1.Value, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new Length(Value, _unit, _prefixIndex);
        }
    }

    public class Mass : QuantityBase
    {
        protected static Kilogram? _kilo = Units.Kilogram;

        public Mass(double val) : base(val, _kilo) { }
        public Mass(double val, Unit? unit) : base(val, unit) { }
        public Mass(double val, Unit? unit, Unit.SI_PrefixEnum prefix) : base(val, unit, prefix) { }

        public Mass()
        {

        }

        public static implicit operator Mass(double val) { return new Mass(val); }
        public static implicit operator Mass(Quantity mq)
        {
            if (mq.Unit.SameDimension(_kilo))
            {
                return new Mass(mq.Value, mq.Unit, mq.PrefixIndex);
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static Mass operator +(Mass q1, Mass q2)
        {
            return new Mass(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Mass operator -(Mass q1, Mass q2)
        {
            return new Mass(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new Mass(Value, _unit, _prefixIndex);
        }
    }

    public class Area : QuantityBase
    {
        protected static SquareMetre? _squareMetreUnit = new SquareMetre();

        public Area(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, new SquareMetre(), prefix) { }
        public Area(double val, Unit? unit, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, unit, prefix) { }

        public Area() : base(0, new SquareMetre(), Unit.SI_PrefixEnum.unity)
        {

        }

        public static implicit operator Area(double val) { return new Area(val); }
        public static implicit operator Area(Quantity mq)
        {
            if (mq.Unit.SameDimension(_squareMetreUnit))
            {
                return new Area(mq.Value, mq.Unit, mq.PrefixIndex);
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static Area operator +(Area q1, Area q2)
        {
            return new Area(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Area operator -(Area q1, Area q2)
        {
            return new Area(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new Area(Value, _unit, _prefixIndex);
        }
    }

    public class Volume : QuantityBase
    {
        private static QubicMetre? _qubicMetreUnit = Units.QubicMetre;
        public Volume(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, _qubicMetreUnit, prefix) { }
        public Volume(double val, Unit? unit, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, unit, prefix) { }

        public Volume()
        {

        }
        public static implicit operator Volume(double val) { return new Volume(val); } // NB lose prefix!
        public static implicit operator Volume(Quantity mq)
        {
            if (mq.Unit.SameDimension(_qubicMetreUnit))
            {
                return new Volume(mq.Value, mq.Unit, mq.PrefixIndex);
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static Volume operator +(Volume q1, Volume q2)
        {
            return new Volume(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits), q1.Unit, q1.PrefixIndex);
        }

        public static Volume operator -(Volume q1, Volume q2)
        {
            return new Volume(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits), q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new Volume(Value, _unit, _prefixIndex);
        }
    }

    public class VolumeFlow : QuantityBase
    {
        private QubicMetrePerSecond _qubicMetrePerSecondUnit = new QubicMetrePerSecond();

        public VolumeFlow(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, new QubicMetrePerSecond(), prefix) { }
        public VolumeFlow(double val, Unit? unit, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, unit, prefix) { }

        public VolumeFlow()
        {

        }
        public static implicit operator VolumeFlow(double val) { return new VolumeFlow(val); }
        public static implicit operator VolumeFlow(Quantity mq)
        {
            if (mq.Unit.SameDimension(new QubicMetrePerSecond()))
            {
                return new VolumeFlow(mq.Value, mq.Unit, mq.PrefixIndex);
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static VolumeFlow operator +(VolumeFlow q1, VolumeFlow q2)
        {
            return new VolumeFlow(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static VolumeFlow operator -(VolumeFlow q1, VolumeFlow q2)
        {
            return new VolumeFlow(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new VolumeFlow(Value, _unit, _prefixIndex);
        }
    }

    public class MassFlow : QuantityBase
    {
        protected static KilogramPerSecond? _kilogramPerSecondUnit = Units.KilogramPerSecond;
        public MassFlow(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, _kilogramPerSecondUnit, prefix) { }
        public MassFlow(double val, Unit? unit, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, unit, prefix) { }
        public static implicit operator MassFlow(double val) { return new MassFlow(val); }
        public static implicit operator MassFlow(Quantity mq)
        {
            if (mq.Unit.SameDimension(_kilogramPerSecondUnit))
            {
                return new MassFlow(mq.Value, mq.Unit, mq.PrefixIndex);
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static MassFlow operator +(MassFlow q1, MassFlow q2)
        {
            return new MassFlow(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static MassFlow operator -(MassFlow q1, MassFlow q2)
        {
            return new MassFlow(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new MassFlow(Value, _unit, _prefixIndex);
        }
    }

    public class Speed : QuantityBase
    {
        private static Unit? _metrePerSecond = new Unit(1, 0, -1, 0, 0, 0, 0);
        public Speed(double val) : base(val, _metrePerSecond) { }
        public Speed(double val, Unit? unit) : base(val, unit) { }
        public Speed(double val, Unit? unit, Unit.SI_PrefixEnum prefix) : base(val, unit, prefix) { }
        public static implicit operator Speed(double val) { return new Speed(val); }
        public static implicit operator Speed(Quantity mq)
        {
            if (mq.Unit.SameDimension(_metrePerSecond))
            {
                return new Speed(mq.Value, mq.Unit, mq.PrefixIndex);
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static Speed operator +(Speed q1, Speed q2)
        {
            return new Speed(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Speed operator -(Speed q1, Speed q2)
        {
            return new Speed(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new Speed(Value, _unit, _prefixIndex);
        }
    }

    public class Acceleration : QuantityBase
    {
        private static Unit? _metrePerSecond2 = new Unit(1, 0, -2, 0, 0, 0, 0);
        public Acceleration(double val) : base(val, _metrePerSecond2) { }
        public Acceleration(double val, Unit? unit) : base(val, unit) { }
        public Acceleration(double val, Unit? unit, Unit.SI_PrefixEnum prefix) : base(val, unit, prefix) { }
        public static implicit operator Acceleration(double val) { return new Acceleration(val); }
        public static implicit operator Acceleration(Quantity mq)
        {
            if (mq.Unit.SameDimension(_metrePerSecond2))
            {
                return new Acceleration(mq.Value, mq.Unit, mq.PrefixIndex);
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static Acceleration operator +(Acceleration q1, Acceleration q2)
        {
            return new Acceleration(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Acceleration operator -(Acceleration q1, Acceleration q2)
        {
            return new Acceleration(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new Acceleration(Value, _unit, _prefixIndex);
        }
    }

    public class Force : QuantityBase
    {
        static Newton? _newton = Units.Newton;
        public Force(double val) : base(val, _newton) { }
        public Force(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, _newton, prefix) { }
        public Force(double val, Unit? u, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, u, prefix) { }
        public static implicit operator Force(double val) { return new Force(val); }
        public static implicit operator Force(Quantity mq)
        {
            if (mq.Unit.SameDimension(_newton))
            {
                if (mq.Unit == _newton)
                {
                    return new Force(mq.Value, _newton, mq.PrefixIndex);
                }
                else
                {
                    Force f = new Force(mq.Value, mq.Unit, mq.PrefixIndex);
                    f.SetUnit(_newton);
                    return f;
                }
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static Force operator +(Force q1, Force q2)
        {
            return new Force(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Force operator -(Force q1, Force q2)
        {
            return new Force(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new Force(Value, _unit, _prefixIndex);
        }
    }

    public class Pressure : QuantityBase
    {
        static Pascal? _pascal = Units.Pascal;
        public Pressure(Pressure p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public Pressure(double val) : base(val, _pascal) { }
        public Pressure(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, _pascal, prefix) { }
        public Pressure(double val, Unit? u, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, u, prefix) { }
        public static implicit operator Pressure(double val) { return new Pressure(val); }
        public static implicit operator Pressure(Quantity mq)
        {
            if (mq.Unit.SameDimension(_pascal))
            {
                if (mq.Unit == _pascal)
                {
                    return new Pressure(mq.Value, _pascal, mq.PrefixIndex);
                }
                else
                {
                    Pressure p = new Pressure(mq.Value, mq.Unit, mq.PrefixIndex);
                    p.SetUnit(_pascal);
                    return p;
                }
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static Pressure operator +(Pressure q1, Pressure q2)
        {
            return new Pressure(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Pressure operator -(Pressure q1, Pressure q2)
        {
            return new Pressure(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new Pressure(Value, _unit, _prefixIndex);
        }
    }

    public class Density : QuantityBase
    {
        static Unit? _densityUnit = new Unit(-3, 1, 0, 0, 0, 0, 0);
        public Density(Density p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public Density(double val) : base(val, _densityUnit) { }
        public Density(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, _densityUnit, prefix) { }
        public Density(double val, Unit? u, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, u, prefix) { }
        public static implicit operator Density(double val) { return new Density(val); }
        public static implicit operator Density(Quantity mq)
        {
            if (mq.Unit.SameDimension(_densityUnit))
            {
                return new Density(mq.Value, mq.Unit, mq.PrefixIndex);
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static Density operator +(Density q1, Density q2)
        {
            return new Density(q1.Unit.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Density operator -(Density q1, Density q2)
        {
            return new Density(q1.Unit.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new Density(Value, _unit, _prefixIndex);
        }
    }
}
