namespace PhysicalQuantities
{
    /// <summary>
    /// Generic, strongly-typed base for linear (offset-free) physical quantities.
    /// Centralises the arithmetic, comparison, conversion and cloning logic that used to
    /// be copy-pasted across every typed quantity. The curiously-recurring type parameter
    /// <typeparamref name="TSelf"/> lets the inherited operators stay strongly typed
    /// (e.g. <c>Length + Length</c> returns <c>Length</c>, not the untyped <see cref="Quantity"/>).
    ///
    /// Affine quantities that carry an offset (e.g. <see cref="Temperature"/>) do NOT derive
    /// from this base: their add/subtract semantics differ and remain hand-written on
    /// <see cref="QuantityBase"/>.
    /// </summary>
    public abstract class Quantity<TSelf> : QuantityBase, IComparable<TSelf>
        where TSelf : Quantity<TSelf>, new()
    {
        /// <summary>
        /// Controls how an untyped <see cref="Quantity"/> is re-expressed when converted to
        /// this typed quantity. Mirrors the three hand-written conversion shapes that existed
        /// before the refactor.
        /// </summary>
        protected enum ConversionMode
        {
            /// <summary>Keep the source quantity's unit and prefix verbatim.</summary>
            KeepSourceUnit,

            /// <summary>Keep the source unit, but substitute the canonical unit instance when
            /// the source unit is dimensionally and scale-wise equal to it (preserves the
            /// nicer ToString of the canonical unit).</summary>
            CanonicalInstanceWhenEqual,

            /// <summary>Re-express the value in the canonical unit unless the source unit is
            /// already equal to it.</summary>
            NormalizeToCanonical
        }

        protected Quantity() { }

        protected Quantity(double value, Unit? unit, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity)
            : base(value, unit, prefix) { }

        /// <summary>The unit a bare <see cref="double"/> is interpreted in (e.g. metre for Length).</summary>
        protected abstract Unit CanonicalUnit { get; }

        /// <summary>How <see cref="FromQuantity"/> re-expresses a converted value. Defaults to
        /// keeping the source unit.</summary>
        protected virtual ConversionMode Conversion => ConversionMode.KeepSourceUnit;

        // ── Conversion helpers (called from each type's implicit operators) ─────────────

        /// <summary>Builds a typed quantity from a bare value expressed in the canonical unit.</summary>
        protected static TSelf FromValue(double value)
        {
            var q = new TSelf();
            Unit u = q.CanonicalUnit;
            q.Unit = u;
            q.PrefixIndex = Unit.SI_Prefix.unity;
            q.ValueInSIUnits = u.Scale * value + u.Offset;
            return q;
        }

        /// <summary>Builds a typed quantity from an untyped <see cref="Quantity"/>, enforcing the
        /// dimension and applying this type's <see cref="Conversion"/> policy.</summary>
        protected static TSelf FromQuantity(Quantity mq)
        {
            var q = new TSelf();
            Unit canonical = q.CanonicalUnit;
            if (!mq.Unit!.SameDimension(canonical)) throw new IncompatibleUnits();

            bool equalsCanonical = mq.Unit == canonical; // value equality (dimension + scale)

            q.ValueInSIUnits = mq.ValueInSIUnits;
            q.PrefixIndex = mq.PrefixIndex;

            if (q.Conversion == ConversionMode.NormalizeToCanonical)
            {
                if (equalsCanonical)
                {
                    q.Unit = canonical;
                }
                else
                {
                    q.Unit = mq.Unit;
                    q.SetUnit(canonical); // -> canonical unit, prefix unity, SI value unchanged
                }
            }
            else if (q.Conversion == ConversionMode.CanonicalInstanceWhenEqual && equalsCanonical)
            {
                q.Unit = canonical;
            }
            else
            {
                q.Unit = mq.Unit;
            }

            return q;
        }

        // ── Arithmetic (returns the concrete type) ──────────────────────────────────────

        public static TSelf operator +(Quantity<TSelf> a, Quantity<TSelf> b)
        {
            if (!a._unit!.SameDimension(b._unit)) throw new IncompatibleUnits();
            return WithUnitOf(a, a.ValueInSIUnits + b.ValueInSIUnits);
        }

        public static TSelf operator -(Quantity<TSelf> a, Quantity<TSelf> b)
        {
            if (!a._unit!.SameDimension(b._unit)) throw new IncompatibleUnits();
            return WithUnitOf(a, a.ValueInSIUnits - b.ValueInSIUnits);
        }

        public static TSelf operator -(Quantity<TSelf> a)
        {
            return WithUnitOf(a, -a.ValueInSIUnits);
        }

        /// <summary>Creates a new <typeparamref name="TSelf"/> carrying the unit/prefix of
        /// <paramref name="proto"/> and the given SI value.</summary>
        private static TSelf WithUnitOf(Quantity<TSelf> proto, double valueInSIUnits)
        {
            var q = new TSelf();
            q.Unit = proto._unit;
            q.ValueInSIUnits = valueInSIUnits;
            q.PrefixIndex = proto._prefixIndex;
            return q;
        }

        public int CompareTo(TSelf? other) => ValueInSIUnits.CompareTo(other?.ValueInSIUnits);

        public override QuantityBase Clone() => WithUnitOf(this, ValueInSIUnits);
    }
}
