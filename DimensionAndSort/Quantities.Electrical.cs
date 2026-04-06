using DimensionAndSort;

namespace GreenOptimizer.DimensionAndSort
{
    public class Current : QuantityBase
    {
        static Unit? _ampere = Units.Ampere;
        public Current(Current p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public Current(double val) : base(val, _ampere) { }
        public Current(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, _ampere, prefix) { }
        public Current(double val, Unit? u, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, u, prefix) { }
        public static implicit operator Current(double val) { return new Current(val); }
        public static implicit operator Current(Quantity mq)
        {
            if (mq.Unit!.SameDimension(_ampere))
            {
                if (mq.Unit == _ampere)
                {
                    return new Current(mq.Value, _ampere, mq.PrefixIndex);
                }
                else
                {
                    Current i = new Current(mq.Value, mq.Unit, mq.PrefixIndex);
                    i.SetUnit(_ampere);
                    return i;
                }
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static Current operator +(Current q1, Current q2)
        {
            return new Current(q1.Unit!.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Current operator -(Current q1, Current q2)
        {
            return new Current(q1.Unit!.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new Current(this);
        }
    }

    public class Voltage : QuantityBase
    {
        static Unit? _volt = Units.Volt;
        public Voltage(Voltage p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public Voltage(double val) : base(val, _volt) { }
        public Voltage(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, _volt, prefix) { }
        public Voltage(double val, Unit? u, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, u, prefix) { }
        public static implicit operator Voltage(double val) { return new Voltage(val); }
        public static implicit operator Voltage(Quantity mq)
        {
            if (mq.Unit!.SameDimension(_volt))
            {
                if (mq.Unit == _volt)
                {
                    return new Voltage(mq.Value, _volt, mq.PrefixIndex);
                }
                else
                {
                    Voltage v = new Voltage(mq.Value, mq.Unit, mq.PrefixIndex);
                    v.SetUnit(_volt);
                    return v;
                }
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static Voltage operator +(Voltage q1, Voltage q2)
        {
            return new Voltage(q1.Unit!.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Voltage operator -(Voltage q1, Voltage q2)
        {
            return new Voltage(q1.Unit!.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new Voltage(this);
        }
    }

    public class Resistance : QuantityBase
    {
        static Unit? _ohm = Units.Ohm;
        public Resistance(Resistance p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public Resistance(double val) : base(val, _ohm) { }
        public Resistance(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, _ohm, prefix) { }
        public Resistance(double val, Unit? u, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, u, prefix) { }
        public static implicit operator Resistance(double val) { return new Resistance(val); }
        public static implicit operator Resistance(Quantity mq)
        {
            if (mq.Unit!.SameDimension(_ohm))
            {
                if (mq.Unit == _ohm)
                {
                    return new Resistance(mq.Value, _ohm, mq.PrefixIndex);
                }
                else
                {
                    Resistance r = new Resistance(mq.Value, mq.Unit, mq.PrefixIndex);
                    r.SetUnit(_ohm);
                    return r;
                }
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static Resistance operator +(Resistance q1, Resistance q2)
        {
            return new Resistance(q1.Unit!.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Resistance operator -(Resistance q1, Resistance q2)
        {
            return new Resistance(q1.Unit!.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new Resistance(this);
        }
    }

    public class Capacitance : QuantityBase
    {
        static Unit? _farad = Units.Farad;
        public Capacitance(Capacitance p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public Capacitance(double val) : base(val, _farad) { }
        public Capacitance(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, _farad, prefix) { }
        public Capacitance(double val, Unit? u, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, u, prefix) { }
        public static implicit operator Capacitance(double val) { return new Capacitance(val); }
        public static implicit operator Capacitance(Quantity mq)
        {
            if (mq.Unit!.SameDimension(_farad))
            {
                if (mq.Unit == _farad)
                {
                    return new Capacitance(mq.Value, _farad, mq.PrefixIndex);
                }
                else
                {
                    Capacitance p = new Capacitance(mq.Value, mq.Unit, mq.PrefixIndex);
                    p.SetUnit(_farad);
                    return p;
                }
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static Capacitance operator +(Capacitance q1, Capacitance q2)
        {
            return new Capacitance(q1.Unit!.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Capacitance operator -(Capacitance q1, Capacitance q2)
        {
            return new Capacitance(q1.Unit!.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new Capacitance(this);
        }
    }

    public class MagneticFluxIntensity : QuantityBase
    {
        static Unit? _siemens = Units.Siemens;
        public MagneticFluxIntensity(MagneticFluxIntensity p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public MagneticFluxIntensity(double val) : base(val, _siemens) { }
        public MagneticFluxIntensity(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, _siemens, prefix) { }
        public MagneticFluxIntensity(double val, Unit? u, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, u, prefix) { }
        public static implicit operator MagneticFluxIntensity(double val) { return new MagneticFluxIntensity(val); }
        public static implicit operator MagneticFluxIntensity(Quantity mq)
        {
            if (mq.Unit!.SameDimension(_siemens))
            {
                if (mq.Unit == _siemens)
                {
                    return new MagneticFluxIntensity(mq.Value, _siemens, mq.PrefixIndex);
                }
                else
                {
                    MagneticFluxIntensity p = new MagneticFluxIntensity(mq.Value, mq.Unit, mq.PrefixIndex);
                    p.SetUnit(_siemens);
                    return p;
                }
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static MagneticFluxIntensity operator +(MagneticFluxIntensity q1, MagneticFluxIntensity q2)
        {
            return new MagneticFluxIntensity(q1.Unit!.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static MagneticFluxIntensity operator -(MagneticFluxIntensity q1, MagneticFluxIntensity q2)
        {
            return new MagneticFluxIntensity(q1.Unit!.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new MagneticFluxIntensity(this);
        }
    }

    public class MagneticFluxDensity : QuantityBase
    {
        static Unit? _tesla = Units.Tesla;
        public MagneticFluxDensity(MagneticFluxDensity p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public MagneticFluxDensity(double val) : base(val, _tesla) { }
        public MagneticFluxDensity(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, _tesla, prefix) { }
        public MagneticFluxDensity(double val, Unit? u, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, u, prefix) { }
        public static implicit operator MagneticFluxDensity(double val) { return new MagneticFluxDensity(val); }
        public static implicit operator MagneticFluxDensity(Quantity mq)
        {
            if (mq.Unit!.SameDimension(_tesla))
            {
                if (mq.Unit == _tesla)
                {
                    return new MagneticFluxDensity(mq.Value, _tesla, mq.PrefixIndex);
                }
                else
                {
                    MagneticFluxDensity p = new MagneticFluxDensity(mq.Value, mq.Unit, mq.PrefixIndex);
                    p.SetUnit(_tesla);
                    return p;
                }
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static MagneticFluxDensity operator +(MagneticFluxDensity q1, MagneticFluxDensity q2)
        {
            return new MagneticFluxDensity(q1.Unit!.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static MagneticFluxDensity operator -(MagneticFluxDensity q1, MagneticFluxDensity q2)
        {
            return new MagneticFluxDensity(q1.Unit!.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new MagneticFluxDensity(this);
        }
    }

    public class MagneticFlux : QuantityBase
    {
        static Unit? _weber = Units.Weber;
        public MagneticFlux(MagneticFlux p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public MagneticFlux(double val) : base(val, _weber) { }
        public MagneticFlux(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, _weber, prefix) { }
        public MagneticFlux(double val, Unit? u, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, u, prefix) { }
        public static implicit operator MagneticFlux(double val) { return new MagneticFlux(val); }
        public static implicit operator MagneticFlux(Quantity mq)
        {
            if (mq.Unit!.SameDimension(_weber))
            {
                if (mq.Unit == _weber)
                {
                    return new MagneticFlux(mq.Value, _weber, mq.PrefixIndex);
                }
                else
                {
                    MagneticFlux p = new MagneticFlux(mq.Value, mq.Unit, mq.PrefixIndex);
                    p.SetUnit(_weber);
                    return p;
                }
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static MagneticFlux operator +(MagneticFlux q1, MagneticFlux q2)
        {
            return new MagneticFlux(q1.Unit!.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static MagneticFlux operator -(MagneticFlux q1, MagneticFlux q2)
        {
            return new MagneticFlux(q1.Unit!.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new MagneticFlux(this);
        }
    }

    public class Inductance : QuantityBase
    {
        static Unit? _henry = Units.Henry;
        public Inductance(Inductance p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public Inductance(double val) : base(val, _henry) { }
        public Inductance(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, _henry, prefix) { }
        public Inductance(double val, Unit? u, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, u, prefix) { }
        public static implicit operator Inductance(double val) { return new Inductance(val); }
        public static implicit operator Inductance(Quantity mq)
        {
            if (mq.Unit!.SameDimension(_henry))
            {
                if (mq.Unit == _henry)
                {
                    return new Inductance(mq.Value, _henry, mq.PrefixIndex);
                }
                else
                {
                    Inductance p = new Inductance(mq.Value, mq.Unit, mq.PrefixIndex);
                    p.SetUnit(_henry);
                    return p;
                }
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static Inductance operator +(Inductance q1, Inductance q2)
        {
            return new Inductance(q1.Unit!.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Inductance operator -(Inductance q1, Inductance q2)
        {
            return new Inductance(q1.Unit!.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new Inductance(this);
        }
    }
}
