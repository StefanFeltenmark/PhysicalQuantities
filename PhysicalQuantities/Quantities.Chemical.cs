using PhysicalQuantities;

namespace PhysicalQuantities
{
    public class LuminousIntensity : Quantity<LuminousIntensity>
    {
        static Unit? _candela = Units.Candela;
        public LuminousIntensity(LuminousIntensity p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public LuminousIntensity(double val) : base(val, _candela) { }
        public LuminousIntensity(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _candela, prefix) { }
        public LuminousIntensity(double val, Unit? u, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, u, prefix) { }
        public LuminousIntensity() { }

        protected override Unit CanonicalUnit => _candela!;
        protected override ConversionMode Conversion => ConversionMode.NormalizeToCanonical;

        public static implicit operator LuminousIntensity(double val) => FromValue(val);
        public static implicit operator LuminousIntensity(Quantity mq) => FromQuantity(mq);
    }

    public class CatalyticActivity : Quantity<CatalyticActivity>
    {
        static Unit? _katal = Units.Katal;
        public CatalyticActivity(CatalyticActivity p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public CatalyticActivity(double val) : base(val, _katal) { }
        public CatalyticActivity(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _katal, prefix) { }
        public CatalyticActivity(double val, Unit? u, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, u, prefix) { }
        public CatalyticActivity() { }

        protected override Unit CanonicalUnit => _katal!;
        protected override ConversionMode Conversion => ConversionMode.NormalizeToCanonical;

        public static implicit operator CatalyticActivity(double val) => FromValue(val);
        public static implicit operator CatalyticActivity(Quantity mq) => FromQuantity(mq);
    }

    public class AmountOfSubstance : Quantity<AmountOfSubstance>
    {
        static Unit? _mole = Units.Mole;
        public AmountOfSubstance(AmountOfSubstance p) : base(p.Value, p.Unit, p.PrefixIndex) { }
        public AmountOfSubstance(double val) : base(val, _mole) { }
        public AmountOfSubstance(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _mole, prefix) { }
        public AmountOfSubstance(double val, Unit? u, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, u, prefix) { }
        public AmountOfSubstance() { }

        protected override Unit CanonicalUnit => _mole!;
        protected override ConversionMode Conversion => ConversionMode.NormalizeToCanonical;

        public static implicit operator AmountOfSubstance(double val) => FromValue(val);
        public static implicit operator AmountOfSubstance(Quantity mq) => FromQuantity(mq);
    }
}
