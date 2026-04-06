using PhysicalQuantities;

namespace PhysicalQuantities
{
    public class Energy : QuantityBase, IComparable<Energy>
    {
        private static Joule? _jouleUnit = new Joule();
        public Energy(Energy e) : base(e.Value, e.Unit, e.PrefixIndex) { }
        public Energy(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _jouleUnit, prefix) { }
        public Energy(double val, Unit? u, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, u, prefix) { }
        public static implicit operator Energy(double val) { return new Energy(val); }

        public Energy()
        {

        }
        public static implicit operator Energy(Quantity mq)
        {
            if (mq.Unit!.SameDimension(_jouleUnit))
            {
                if (mq.Unit == _jouleUnit)
                {
                    return new Energy(mq.Value, _jouleUnit, mq.PrefixIndex);
                }
                else
                {
                    Energy e = new Energy(mq.Value, mq.Unit, mq.PrefixIndex);
                    return e;
                }
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static Energy operator +(Energy q1, Energy q2)
        {
            return new Energy(q1.Unit!.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Energy operator -(Energy q1, Energy q2)
        {
            return new Energy(q1.Unit!.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public int CompareTo(Energy? other) => ValueInSIUnits.CompareTo(other?.ValueInSIUnits);

        public override QuantityBase Clone()
        {
            return new Energy(Value, _unit, _prefixIndex);
        }
    }

    public class EnergyEquivalent : QuantityBase, IComparable<EnergyEquivalent>
    {
        private static Unit? _energyPerVolume = new Joule() / new QubicMetre();

        public EnergyEquivalent(EnergyEquivalent e) : base(e.Value, e.Unit, e.PrefixIndex) { }
        public EnergyEquivalent() { }
        public EnergyEquivalent(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _energyPerVolume, prefix) { }
        public EnergyEquivalent(double val, Unit? u, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, u, prefix) { }
        public static implicit operator EnergyEquivalent(double val) { return new EnergyEquivalent(val); }
        public static implicit operator EnergyEquivalent(Quantity mq)
        {
            if (mq.Unit!.SameDimension(_energyPerVolume))
            {
                if (mq.Unit == _energyPerVolume)
                {
                    return new EnergyEquivalent(mq.Value, _energyPerVolume, mq.PrefixIndex);
                }
                else
                {
                    EnergyEquivalent e = new EnergyEquivalent(mq.Value, mq.Unit, mq.PrefixIndex);
                    return e;
                }
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static EnergyEquivalent operator +(EnergyEquivalent q1, EnergyEquivalent q2)
        {
            return new EnergyEquivalent(q1.Unit!.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static EnergyEquivalent operator -(EnergyEquivalent q1, EnergyEquivalent q2)
        {
            return new EnergyEquivalent(q1.Unit!.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static EnergyEquivalent operator -(EnergyEquivalent q1)
        {
            return new EnergyEquivalent(q1.Unit!.FromSIUnit(-q1.ValueInSIUnits), q1.Unit, q1.PrefixIndex);
        }

        public int CompareTo(EnergyEquivalent? other) => ValueInSIUnits.CompareTo(other?.ValueInSIUnits);

        public override QuantityBase Clone()
        {
            return new EnergyEquivalent(Value, _unit, _prefixIndex);
        }
    }

    public class HeatingValue : QuantityBase, IComparable<HeatingValue>
    {
        private static Joule _energyUnit = new Joule(Unit.SI_Prefix.kilo);
        private static Kilogram _weightUnit = new Kilogram();
        private static Unit? _heatingValueUnit = _energyUnit / _weightUnit;
        public HeatingValue(HeatingValue e) : base(e.Value, e.Unit, e.PrefixIndex) { }
        public HeatingValue(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _heatingValueUnit, prefix) { }
        public HeatingValue(double val, Unit? u, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, u, prefix) { }
        public static implicit operator HeatingValue(double val) { return new HeatingValue(val); }
        public static implicit operator HeatingValue(Quantity mq)
        {
            if (mq.Unit!.SameDimension(_heatingValueUnit))
            {
                if (mq.Unit == _heatingValueUnit)
                {
                    return new HeatingValue(mq.Value, _heatingValueUnit, mq.PrefixIndex);
                }
                else
                {
                    HeatingValue e = new HeatingValue(mq.Value, mq.Unit, mq.PrefixIndex);
                    return e;
                }
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static HeatingValue operator +(HeatingValue q1, HeatingValue q2)
        {
            return new HeatingValue(q1.Unit!.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static HeatingValue operator -(HeatingValue q1, HeatingValue q2)
        {
            return new HeatingValue(q1.Unit!.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public int CompareTo(HeatingValue? other) => ValueInSIUnits.CompareTo(other?.ValueInSIUnits);

        public override QuantityBase Clone()
        {
            return new HeatingValue(Value, _unit, _prefixIndex);
        }
    }

    public class SpecificEnergy : QuantityBase, IComparable<SpecificEnergy>
    {
        private static Joule _energyUnit = new Joule();
        private static Kilogram _weightUnit = new Kilogram();
        private static Unit? _specificEnergyUnit = _energyUnit / _weightUnit;

        public SpecificEnergy()
        {

        }
        public SpecificEnergy(SpecificEnergy e) : base(e.Value, e.Unit, e.PrefixIndex) { }
        public SpecificEnergy(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _specificEnergyUnit, prefix) { }
        public SpecificEnergy(double val, Unit? u, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, u, prefix) { }
        public static implicit operator SpecificEnergy(double val) { return new SpecificEnergy(val); }
        public static implicit operator SpecificEnergy(Quantity mq)
        {
            if (mq.Unit!.SameDimension(_specificEnergyUnit))
            {
                if (mq.Unit == _specificEnergyUnit)
                {
                    return new SpecificEnergy(mq.Value, _specificEnergyUnit, mq.PrefixIndex);
                }
                else
                {
                    SpecificEnergy e = new SpecificEnergy(mq.Value, mq.Unit, mq.PrefixIndex);
                    return e;
                }
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static SpecificEnergy operator +(SpecificEnergy q1, SpecificEnergy q2)
        {
            return new SpecificEnergy(q1.Unit!.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static SpecificEnergy operator -(SpecificEnergy q1, SpecificEnergy q2)
        {
            return new SpecificEnergy(q1.Unit!.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public int CompareTo(SpecificEnergy? other) => ValueInSIUnits.CompareTo(other?.ValueInSIUnits);

        public override QuantityBase Clone()
        {
            return new SpecificEnergy(Value, _unit, _prefixIndex);
        }
    }

    public class PowerRampRate : QuantityBase, IComparable<PowerRampRate>
    {
        static Unit? _wattsPerSecond = Units.Watt / Units.Second;

        public PowerRampRate(PowerRampRate p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public PowerRampRate(double val) : base(val, _wattsPerSecond) { }
        public PowerRampRate(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _wattsPerSecond, prefix) { }
        public PowerRampRate(double val, Unit? u, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, u, prefix) { }
        public static implicit operator PowerRampRate(double val) { return new PowerRampRate(val); }
        public static implicit operator PowerRampRate(Quantity mq)
        {
            if (mq.Unit!.SameDimension(_wattsPerSecond))
            {
                if (mq.Unit == _wattsPerSecond)
                {
                    return new PowerRampRate(mq.Value, _wattsPerSecond, mq.PrefixIndex);
                }
                else
                {
                    PowerRampRate p = new PowerRampRate(mq.Value, mq.Unit, mq.PrefixIndex);
                    p.SetUnit(_wattsPerSecond);
                    return p;
                }
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static PowerRampRate operator +(PowerRampRate q1, PowerRampRate q2)
        {
            return new PowerRampRate(q1.Unit!.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static PowerRampRate operator -(PowerRampRate q1, PowerRampRate q2)
        {
            return new PowerRampRate(q1.Unit!.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public int CompareTo(PowerRampRate? other) => ValueInSIUnits.CompareTo(other?.ValueInSIUnits);

        public override QuantityBase Clone()
        {
            return new PowerRampRate(this);
        }
    }

    public class Power : QuantityBase, IComparable<Power>
    {
        static Watt? _watt = new Watt();
        public Power(Power p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public Power(double val = 0.0) : base(val, _watt) { }
        public Power(double val = 0.0, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _watt, prefix) { }
        public Power(double val = 0.0, Unit? u = null, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, u, prefix) { }

        public Power()
        {

        }
        public static implicit operator Power(double val) { return new Power(val); }
        public static implicit operator Power(Quantity mq)
        {
            if (mq.Unit!.SameDimension(_watt))
            {
                if (mq.Unit == _watt)
                {
                    return new Power(mq.Value, _watt, mq.PrefixIndex);
                }
                else
                {
                    Power p = new Power(mq.Value, mq.Unit, mq.PrefixIndex);
                    p.SetUnit(_watt);
                    return p;
                }
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static Power operator +(Power q1, Power q2)
        {
            return new Power(q1.Unit!.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static Power operator -(Power q1, Power q2)
        {
            return new Power(q1.Unit!.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public int CompareTo(Power? other) => ValueInSIUnits.CompareTo(other?.ValueInSIUnits);

        public override QuantityBase Clone()
        {
            return new Power(this);
        }
    }
}
