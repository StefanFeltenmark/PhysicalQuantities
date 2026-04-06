using DimensionAndSort;

namespace GreenOptimizer.DimensionAndSort
{
    public class LuminousIntensity : QuantityBase
    {
        static Unit? _candela = Units.Candela;
        public LuminousIntensity(LuminousIntensity p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public LuminousIntensity(double val) : base(val, _candela) { }
        public LuminousIntensity(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, _candela, prefix) { }
        public LuminousIntensity(double val, Unit? u, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, u, prefix) { }
        public static implicit operator LuminousIntensity(double val) { return new LuminousIntensity(val); }
        public static implicit operator LuminousIntensity(Quantity mq)
        {
            if (mq.Unit!.SameDimension(_candela))
            {
                if (mq.Unit! ==_candela)
                {
                    return new LuminousIntensity(mq.Value, _candela, mq.PrefixIndex);
                }
                else
                {
                    LuminousIntensity p = new LuminousIntensity(mq.Value, mq.Unit, mq.PrefixIndex);
                    p.SetUnit(_candela);
                    return p;
                }
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static LuminousIntensity operator +(LuminousIntensity q1, LuminousIntensity q2)
        {
            return new LuminousIntensity(q1.Unit!.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static LuminousIntensity operator -(LuminousIntensity q1, LuminousIntensity q2)
        {
            return new LuminousIntensity(q1.Unit!.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new LuminousIntensity(this);
        }
    }

    public class CatalyticActivity : QuantityBase
    {
        static Unit? _katal = Units.Katal;
        public CatalyticActivity(CatalyticActivity p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public CatalyticActivity(double val) : base(val, _katal) { }
        public CatalyticActivity(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, _katal, prefix) { }
        public CatalyticActivity(double val, Unit? u, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, u, prefix) { }
        public static implicit operator CatalyticActivity(double val) { return new CatalyticActivity(val); }
        public static implicit operator CatalyticActivity(Quantity mq)
        {
            if (mq.Unit!.SameDimension(_katal))
            {
                if (mq.Unit! ==_katal)
                {
                    return new CatalyticActivity(mq.Value, _katal, mq.PrefixIndex);
                }
                else
                {
                    CatalyticActivity p = new CatalyticActivity(mq.Value, mq.Unit, mq.PrefixIndex);
                    p.SetUnit(_katal);
                    return p;
                }
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static CatalyticActivity operator +(CatalyticActivity q1, CatalyticActivity q2)
        {
            return new CatalyticActivity(q1.Unit!.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static CatalyticActivity operator -(CatalyticActivity q1, CatalyticActivity q2)
        {
            return new CatalyticActivity(q1.Unit!.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new CatalyticActivity(this);
        }
    }

    public class AmountOfSubstance : QuantityBase
    {
        static Unit? _mole = Units.Mole;
        public AmountOfSubstance(AmountOfSubstance p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public AmountOfSubstance(double val) : base(val, _mole) { }
        public AmountOfSubstance(double val, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, _mole, prefix) { }
        public AmountOfSubstance(double val, Unit? u, Unit.SI_PrefixEnum prefix = Unit.SI_PrefixEnum.unity) : base(val, u, prefix) { }
        public static implicit operator AmountOfSubstance(double val) { return new AmountOfSubstance(val); }
        public static implicit operator AmountOfSubstance(Quantity mq)
        {
            if (mq.Unit!.SameDimension(_mole))
            {
                if (mq.Unit! ==_mole)
                {
                    return new AmountOfSubstance(mq.Value, _mole, mq.PrefixIndex);
                }
                else
                {
                    AmountOfSubstance p = new AmountOfSubstance(mq.Value, mq.Unit, mq.PrefixIndex);
                    p.SetUnit(_mole);
                    return p;
                }
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }
        public static AmountOfSubstance operator +(AmountOfSubstance q1, AmountOfSubstance q2)
        {
            return new AmountOfSubstance(q1.Unit!.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public static AmountOfSubstance operator -(AmountOfSubstance q1, AmountOfSubstance q2)
        {
            return new AmountOfSubstance(q1.Unit!.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits) / q1.prefix.Factor, q1.Unit, q1.PrefixIndex);
        }

        public override QuantityBase Clone()
        {
            return new AmountOfSubstance(this);
        }
    }
}
