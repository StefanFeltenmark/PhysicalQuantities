using System;
using PhysicalQuantities;
using Xunit;

namespace UnitTests
{
    // ── Custom unit (defined the usual way) ────────────────────────────────────
    // A furlong is 201.168 m. Note the scale is configured in the constructor body
    // exactly as the built-in units do (Bar, MegaWatt, …). This still compiles even
    // though Unit.Scale is now init-only, because init setters are permitted inside
    // a (derived) constructor. External code can no longer write `furlong.Scale = …`.
    public class Furlong : Unit
    {
        public Furlong() : base(1, 0, 0, 0, 0, 0, 0) { Scale = 201.168; }
        public override string ToString() => "fur";
    }

    // ── Custom quantity (genuinely new dimension: N/m) ─────────────────────────
    // Spring stiffness = force per length. Not a built-in quantity. Extends the
    // CRTP base, so it gets +/-/comparison/clone for free and only declares its
    // constructors, canonical unit, and the two (non-inheritable) implicit operators.
    public class Stiffness : Quantity<Stiffness>
    {
        private static readonly Unit _newtonPerMetre = new Unit(0, 1, -2, 0, 0, 0, 0);

        public Stiffness(double val, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, _newtonPerMetre, prefix) { }
        public Stiffness(double val, Unit? unit, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity) : base(val, unit, prefix) { }
        public Stiffness() { }

        protected override Unit CanonicalUnit => _newtonPerMetre;

        public static implicit operator Stiffness(double val) => FromValue(val);
        public static implicit operator Stiffness(Quantity mq) => FromQuantity(mq);
    }

    public class ExtensibilityTests
    {
        [Fact]
        public void CustomUnitPlugsIntoBuiltInQuantity()
        {
            // A user-defined Unit works inside a built-in typed quantity.
            var d = new Length(2, new Furlong());
            Assert.True(Math.Abs(d.ValueInSIUnits - 2 * 201.168) < 1e-6);
        }

        [Fact]
        public void CustomQuantityGetsArithmeticFromBase()
        {
            var k = new Stiffness(100) + new Stiffness(50);
            Assert.IsType<Stiffness>(k);
            Assert.True(Math.Abs(k.ValueInSIUnits - 150.0) < 1e-9);

            // Unary minus and comparison are inherited too.
            Assert.True((-k).ValueInSIUnits < new Stiffness(0).ValueInSIUnits);
        }

        [Fact]
        public void CustomQuantityParticipatesInDimensionalAnalysis()
        {
            // Force / Length has dimension N/m, so it converts into the custom Stiffness.
            Stiffness s = new Force(100) / new Length(2);
            Assert.True(Math.Abs(s.ValueInSIUnits - 50.0) < 1e-9);
        }

        [Fact]
        public void IncompatibleDimensionStillThrowsForCustomQuantity()
        {
            Assert.Throws<IncompatibleUnits>(() =>
            {
                Stiffness bad = (Quantity)(new Mass(1) * new DimensionlessQuantity(1));
                return bad;
            });
        }
    }
}
