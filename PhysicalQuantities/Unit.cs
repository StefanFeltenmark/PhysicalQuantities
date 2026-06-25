using System.Text;
using Newtonsoft.Json;

namespace PhysicalQuantities
{

    public class Scalings
    {
        // length
        public static Unit.Scaling metre = new("m");
        public static Unit.Scaling foot = new("ft", 0.3048);
        public static Unit.Scaling inch = new("in", 0.0254);
        public static Unit.Scaling mile = new("mi", 1609.344);

        // weight
        public static Unit.Scaling kilogram = new("kg");
        public static Unit.Scaling ton = new("ton", 1000);

        // time
        public static Unit.Scaling second = new("s");
        public static Unit.Scaling hour = new("h", 3600);
        public static Unit.Scaling minute = new("min", 60);
        public static Unit.Scaling dayAndNight = new("d", 86400);
        public static Unit.Scaling week = new("w", 604800);

        public static Unit.Scaling ampere = new("A");
        public static Unit.Scaling kelvin = new("K");
        public static Unit.Scaling candela = new("Ca");
        public static Unit.Scaling mole = new("M");
    }

    public class StandardScalings
    {
        public static Unit.Scaling metre = new("m");
        public static Unit.Scaling kilogram = new("kg");
        public static Unit.Scaling second = new("s");
        public static Unit.Scaling ampere = new("A");
        public static Unit.Scaling kelvin = new("K");
        public static Unit.Scaling candela = new("Ca");
        public static Unit.Scaling mole = new("M");
    }


    public partial class Unit : IEquatable<Unit>, IUnit
    {
        private static SIprefix[] _prefixes = new SIprefix[21] {
                new SIprefix((int) SI_Prefix.yocto,"y","yocto"),
                new SIprefix(-21,"z", "zepto"),
                new SIprefix(-18,"a","atto"),
                new SIprefix(-15,"f","femto"),
                new SIprefix(-12,"p","pico"),
                new SIprefix(-9,"n","nano"),
                new SIprefix(-6,"mu","micro"),
                new SIprefix(-3,"m","milli"),
                new SIprefix(-2,"c","centi"),
                new SIprefix(-1,"d","deci"),
                new SIprefix(0,"",""),
                new SIprefix(1,"da","deca"),
                new SIprefix(2,"h","hecto"),
                new SIprefix(3,"k","kilo"),
                new SIprefix(6,"M","mega"),
                new SIprefix(9,"G","giga"),
                new SIprefix(12,"T","tera"),
                new SIprefix(15,"P","peta"),
                new SIprefix(18,"E","exa"),
                new SIprefix(21,"Z","zetta"),
                new SIprefix(24,"Y","yotta")};

        private static Unit[] _baseUnits = [new Metre(), new Kilogram(), new Second(), new Ampere(), new Kelvin(), new Candela(), new Mole()
        ];
        private static Unit[] _derivedUnits = [new Hertz(), new Newton(), new Pascal(), new Joule(), new Watt(), new Coulomb(), new Volt(), new Farad(), new Ohm(), new Siemens(), new Weber(), new Tesla(), new Henry(), new Lux(), new Katal()
        ];

        public enum BaseUnitEnum { metre, kilogram, second, ampere, kelvin, candela, mole }

        public enum DerivedUnitEnum { hertz, newton, pascal, joule, watt, coulomb, volt, farad, ohm, siemens, weber, tesla, henry, lux, katal }

        public enum SI_Prefix { yocto, zepto, atto, femto, pico, nano, micro, milli, centi, deci, unity, deca, hecto, kilo, mega, giga, tera, peta, exa, zetta, yotta };

        #region memberVariables

        public DimensionUnit[]? _dimensions; // = new Unit.DimensionUnit[7];
        protected double _scale;  // To SI-units
        protected double _offset;  // To SI-units

        protected SI_Prefix _prefixIndex;
        #endregion

        public double Offset
        {
            get { return _offset; }
            set { _offset = value; }
        }

        public static SIprefix[] Prefixes
        {
            get { return Unit._prefixes; }
            set { Unit._prefixes = value; }
        }

        public double Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        public Unit()
        {

        }

        protected void SetScaling(BaseUnitEnum baseunit, Scaling s)
        {
            if (_dimensions != null) _dimensions[(int)baseunit].Scaling = s;
            RecomputeScale();
        }


        private void RecomputeScale()
        {
            if (_dimensions == null) return;
            double f = 1.0;
            for (int i = 0; i < 7; ++i)
            {
                f = f * Math.Pow(_dimensions[i].Scaling.Factor * _prefixes[(int)_dimensions[i].SI_prefix].Factor, _dimensions[i].Exponent);
            }
            _scale = f; // look at this: we may have a scale that comes from some other unit change, like for mmHg or Watthour. This will overwrite that value!
        }


        [JsonIgnore]
        public SIprefix Prefix
        {
            get { return _prefixes[(int)_prefixIndex]; }
        }

        public SI_Prefix PrefixIndex
        {
            get { return _prefixIndex; }
            set { _prefixIndex = value; }
        }

        public Unit(int exp_metre, int exp_kilogram, int exp_second, int exp_ampere, int exp_kelvin, int exp_candela, int exp_mole, double scale = 1.0, double offset = 0.0, SI_Prefix prefix = SI_Prefix.unity)
        {
            _dimensions = new DimensionUnit[7];
            _dimensions[(int)Unit.BaseUnitEnum.metre] = new Unit.DimensionUnit(exp_metre, Scalings.metre, SI_Prefix.unity);
            _dimensions[(int)Unit.BaseUnitEnum.kilogram] = new Unit.DimensionUnit(exp_kilogram, Scalings.kilogram, SI_Prefix.unity);
            _dimensions[(int)Unit.BaseUnitEnum.second] = new Unit.DimensionUnit(exp_second, Scalings.second, SI_Prefix.unity);
            _dimensions[(int)Unit.BaseUnitEnum.ampere] = new Unit.DimensionUnit(exp_ampere, Scalings.ampere, SI_Prefix.unity);
            _dimensions[(int)Unit.BaseUnitEnum.kelvin] = new Unit.DimensionUnit(exp_kelvin, Scalings.kelvin, SI_Prefix.unity);
            _dimensions[(int)Unit.BaseUnitEnum.candela] = new Unit.DimensionUnit(exp_candela, Scalings.candela, SI_Prefix.unity);
            _dimensions[(int)Unit.BaseUnitEnum.mole] = new Unit.DimensionUnit(exp_mole, Scalings.mole, SI_Prefix.unity);

            if (scale <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(scale), scale, "Scale must be positive");
            }

            _scale = scale;
            _offset = offset;
            _prefixIndex = prefix;
            _scale *= Prefix.Factor;
        }


        public double FromSIUnit(double val)
        {
            return (val - _offset) / _scale;
        }


        public static Unit operator *(Unit? q1, Unit? q2)
        {
            Unit u = new Unit(q1!._dimensions![(int)BaseUnitEnum.metre].Exponent + q2!._dimensions![(int)BaseUnitEnum.metre].Exponent,
                                q1._dimensions[(int)BaseUnitEnum.kilogram].Exponent + q2._dimensions[(int)BaseUnitEnum.kilogram].Exponent,
                                q1._dimensions[(int)BaseUnitEnum.second].Exponent + q2._dimensions[(int)BaseUnitEnum.second].Exponent,
                                q1._dimensions[(int)BaseUnitEnum.ampere].Exponent + q2._dimensions[(int)BaseUnitEnum.ampere].Exponent,
                                q1._dimensions[(int)BaseUnitEnum.kelvin].Exponent + q2._dimensions[(int)BaseUnitEnum.kelvin].Exponent,
                                q1._dimensions[(int)BaseUnitEnum.candela].Exponent + q2._dimensions[(int)BaseUnitEnum.candela].Exponent,
                                q1._dimensions[(int)BaseUnitEnum.mole].Exponent + q2._dimensions[(int)BaseUnitEnum.mole].Exponent);


            u.Scale = q1.Scale * q2.Scale;

            return u;
        }

        public static Unit operator /(Unit? q1, Unit? q2)
        {
            Unit u = new Unit(q1!._dimensions![(int)BaseUnitEnum.metre].Exponent - q2!._dimensions![(int)BaseUnitEnum.metre].Exponent,
                                q1._dimensions[(int)BaseUnitEnum.kilogram].Exponent - q2._dimensions[(int)BaseUnitEnum.kilogram].Exponent,
                                q1._dimensions[(int)BaseUnitEnum.second].Exponent - q2._dimensions[(int)BaseUnitEnum.second].Exponent,
                                q1._dimensions[(int)BaseUnitEnum.ampere].Exponent - q2._dimensions[(int)BaseUnitEnum.ampere].Exponent,
                                q1._dimensions[(int)BaseUnitEnum.kelvin].Exponent - q2._dimensions[(int)BaseUnitEnum.kelvin].Exponent,
                                q1._dimensions[(int)BaseUnitEnum.candela].Exponent - q2._dimensions[(int)BaseUnitEnum.candela].Exponent,
                                q1._dimensions[(int)BaseUnitEnum.mole].Exponent - q2._dimensions[(int)BaseUnitEnum.mole].Exponent);
            u.Scale = q1.Scale / q2.Scale;
            return u;
        }

        /// <summary>
        /// Raises this unit to the integer power <paramref name="n"/>, multiplying every
        /// dimension exponent by n (e.g. metre.Pow(3) is cubic metre). The resulting scale
        /// is the unit's scale raised to the same power.
        /// </summary>
        public Unit Pow(int n)
        {
            var u = new Unit(_dimensions![(int)BaseUnitEnum.metre].Exponent * n,
                             _dimensions[(int)BaseUnitEnum.kilogram].Exponent * n,
                             _dimensions[(int)BaseUnitEnum.second].Exponent * n,
                             _dimensions[(int)BaseUnitEnum.ampere].Exponent * n,
                             _dimensions[(int)BaseUnitEnum.kelvin].Exponent * n,
                             _dimensions[(int)BaseUnitEnum.candela].Exponent * n,
                             _dimensions[(int)BaseUnitEnum.mole].Exponent * n);
            u.Scale = Math.Pow(Scale, n);
            return u;
        }

        public static bool operator ==(Unit? u1, Unit? u2)
        {
            if (u1 is null && u2 is null) return true;
            if (u1 is null && u2 is not null) return false;
            if (u1 is not null && u2 is null) return false;

            return u1!.Equals(u2);
        }

        public static bool operator !=(Unit? u1, Unit? u2)
        {
            if (u1 is null && u2 is null)
            {
                return false;
            }
            if (u1 is null ^ u2 is null)
            {
                return true;
            }
            else
            {
                return !u1!.Equals(u2);
            }
        }

        public static bool IsBaseUnit(Unit u)
        {
            return _baseUnits.Contains(u, new UnitEqComp());
        }


        public static Unit? AsBaseUnit(Unit? u)
        {
            return _baseUnits.FirstOrDefault(cu => cu.Equals(u));
        }

        public static Unit? AsDerivedUnit(Unit? u)
        {
            Unit? match = _derivedUnits.FirstOrDefault(cu => cu.SameDimension(u));

            if (match == null) return null;

            // Clone before mutating: the entries in _derivedUnits are shared singletons,
            // so writing Scale/PrefixIndex onto the match would corrupt the global instance
            // for every later caller.
            Unit du = match.Clone()!;
            du.Scale = u!.Scale;
            du.PrefixIndex = u._prefixIndex;

            return du;
        }

        public static bool IsDerivedUnit(Unit u)
        {
            return _derivedUnits.Contains(u, new UnitEqComp());
        }

        public class UnitEqComp : IEqualityComparer<Unit>
        {
            public UnitEqComp()
            {

            }

            #region IEqualityComparer<Unit> Members

            public bool Equals(Unit? x, Unit? y)
            {
                return x!.Equals(y);
            }

            public int GetHashCode(Unit obj)
            {
                return obj._dimensions!.Select(s => s.Exponent).Sum();
            }

            #endregion
        }

        #region IEquatable<Unit> Members
        public bool SameDimension(Unit? other)
        {
            if(other == null) return false;

            bool equals = true;
            for (int i = 0; i <= 6; ++i)
            {
                if (_dimensions![i].Exponent != other._dimensions![i].Exponent)
                {
                    equals = false;
                    break;
                }
            }
            return equals;
        }

        public bool SameScaling(Unit other)
        {
            bool equals = true;
            for (int i = 0; i <= 6; ++i)
            {
                if (!_dimensions![i].Scaling.Equals(other._dimensions![i].Scaling))
                {
                    equals = false;
                    break;
                }
            }
            return equals;
        }

        public bool Equals(Unit? other)
        {
            bool equals = true;
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            Unit p = other as Unit;
            if ((System.Object)p == null)
            {
                return false;
            }

            equals = SameDimension(other) && SameScaling(other) && (Math.Abs(other.Scale - Scale) < 1e-6);

            return equals;
        }

        public override bool Equals(object? other_obj)
        {
            bool equals = true;

            if (ReferenceEquals(other_obj, null)) return false;

            if (ReferenceEquals(other_obj, this)) return true;

            if(typeof(Unit) != other_obj.GetType()) return false;

            @equals = this.Equals((Unit)other_obj);

            return equals;
        }

        public override int GetHashCode()
        {
            // Must be consistent with Equals (value-based). Equals requires SameDimension,
            // so hashing the dimension exponents alone guarantees equal units hash equally.
            // Scale/scaling are intentionally excluded: Equals compares them with a tolerance,
            // which cannot be reflected in a hash without breaking the equals/hashcode contract.
            if (_dimensions == null) return 0;
            var hash = new HashCode();
            for (int i = 0; i < 7; ++i)
            {
                hash.Add(_dimensions[i].Exponent);
            }
            return hash.ToHashCode();
        }

        #endregion

        public virtual Unit? Clone()
        {
            return new Unit(_dimensions![0].Exponent, _dimensions[1].Exponent, _dimensions[2].Exponent, _dimensions[3].Exponent, _dimensions[4].Exponent, _dimensions[5].Exponent, _dimensions[6].Exponent, _scale, _offset, _prefixIndex);
        }

        public override string ToString()
        {
            if(_dimensions == null) return String.Empty;


            StringBuilder sbTaljare = new StringBuilder();
            for (int i = 0; i <= 6; ++i)
            {
                if (_dimensions[i].Exponent > 0)
                {
                    sbTaljare.Append(Unit.Prefixes[(int)_dimensions[i].SI_prefix].Symbol);
                    if (_dimensions[i].Exponent == 1)
                    {
                        sbTaljare.Append(_dimensions[i].Scaling);
                    }
                    else
                    {
                        sbTaljare.Append(_dimensions[i].Scaling + _dimensions[i].Exponent.ToString());
                    }
                }
            }

            StringBuilder sbNamnare = new StringBuilder();
            for (int i = 0; i <= 6; ++i)
            {
                if (_dimensions[i].Exponent < 0)
                {
                    sbNamnare.Append(Unit.Prefixes[(int)_dimensions[i].SI_prefix].Symbol);
                    if (_dimensions[i].Exponent == -1)
                    {
                        sbNamnare.Append(_dimensions[i].Scaling);
                    }
                    else
                    {
                        sbNamnare.Append(_dimensions[i].Scaling + (-_dimensions[i].Exponent).ToString());
                    }
                }
            }
            string str = Prefix + ((sbTaljare.Length > 0) ? sbTaljare.ToString() : "1") + ((sbNamnare.Length > 0) ? ("/" + sbNamnare) : "");

            return str;
        }
    }
}
