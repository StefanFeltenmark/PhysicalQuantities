using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using PhysicalQuantities;

namespace PhysicalQuantities
{
    public class Quantity : QuantityBase
    {
        public Quantity(double val, Unit? unit, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity, string symbol = "") : base(val, unit, prefix, symbol)
        {

        }

        public Quantity(Quantity q) : base(q.Value, q.Unit, q.PrefixIndex)
        {

        }

    }

    public class QuantityBase : IEquatable<QuantityBase>, IComparable<QuantityBase>, IQuantity
    {
        #region members
        protected Unit? _unit;
        protected double _valueInSIUnits;
        protected Unit.SI_Prefix _prefixIndex = Unit.SI_Prefix.unity;
        #endregion

        public bool Equals(QuantityBase? other)
        {
            if (other == null) return false;
            bool ok1 = _unit?.Equals(other._unit) ?? other._unit == null;
          //  bool ok3 = _prefixIndex == other._prefixIndex;
            bool ok2 = Math.Abs(ValueInSIUnits - other.ValueInSIUnits) < 1e-9;
            return ok1 && ok2; //&& ok3;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((QuantityBase)obj);
        }

        public override int GetHashCode()
        {
            // Equals compares the value with a tolerance (and ignores the display prefix),
            // so neither the value nor the prefix can participate in the hash without
            // breaking the equals/hashcode contract. Hash on the unit only.
            return _unit != null ? _unit.GetHashCode() : 0;
        }

        public static bool operator ==(QuantityBase? q1, QuantityBase? q2)
        {
            if (q1 is null && q2 is null) return true;
            if (q1 is null || q2 is null) return false;
            return q1.Equals(q2);
        }

        public static bool operator !=(QuantityBase? q1, QuantityBase? q2) => !(q1 == q2);

        public Unit? Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        public Unit.SI_Prefix PrefixIndex
        {
            get { return _prefixIndex; }
            set { _prefixIndex = value; }
        }

        [JsonIgnore]
        public Unit.SIprefix prefix
        {
            get { return Unit.Prefixes[(int)_prefixIndex]; }
        }

        public double ValueInSIUnits
        {
           // get { return prefix.Factor * (_unit.Scale * _value + _unit.Offset); }
            get { return _valueInSIUnits;}
            set { _valueInSIUnits = value; }
        }

        public double ToSIUnit(double value, Unit? unit, Unit.SI_Prefix prefixIndex = Unit.SI_Prefix.unity)
        {
            return Unit.Prefixes[(int) prefixIndex].Factor * (_unit!.Scale * value + _unit.Offset);
        }

        [JsonIgnore]
        public virtual double Value
        {
            get { return (_valueInSIUnits/prefix.Factor - _unit!.Offset)/_unit.Scale; }
           
        }

        
        public QuantityBase(double value, Unit? unit, Unit.SI_Prefix prefix = Unit.SI_Prefix.unity, string symbol = "")
        {
            _unit = unit;
            _valueInSIUnits = ToSIUnit(value, unit, prefix);
            _prefixIndex = prefix;
        }

        public QuantityBase()
        {
            _unit = new Unit();
        }

        public QuantityBase ConvertToDerivedUnit()
        {
            SetUnit(this.Unit.ToDerivedUnit());
            return this;
        }

        public void SetUnit(Unit? newUnit)
        {
            if (_unit!.SameDimension(newUnit))
            {
                _unit = newUnit;
                _prefixIndex = Unit.SI_Prefix.unity;
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }

        public bool TrySetUnit(Unit? newUnit)
        {
            if (newUnit!.SameDimension(_unit))
            {
                _unit = newUnit;
                _prefixIndex = Unit.SI_Prefix.unity;
                return true;
            }
            else
            {
                return false;
            }
        
        }

        public Quantity ConvertToUnit(Unit? newUnit)
        {
            Quantity q;
            if (newUnit!.SameDimension(_unit))
            {
                double value = newUnit.FromSIUnit(ValueInSIUnits);
                Unit? unit = newUnit;
                Unit.SI_Prefix prefixIndex = Unit.SI_Prefix.unity;
                q = new Quantity(value, unit, prefixIndex);
            }
            else
            {
                throw new IncompatibleUnits();
            }
            return q;
        }

        public static Quantity operator *(QuantityBase q1, double f)
        {
            return new Quantity(q1.Value * f, q1.Unit, q1.PrefixIndex);
        }

        public static Quantity operator *(double f, QuantityBase q1)
        {
            return new Quantity(q1.Value * f, q1.Unit, q1.PrefixIndex);
        }

        public static Quantity operator /(QuantityBase q1, double f)
        {
            return new Quantity(q1.Value / f, q1.Unit, q1.PrefixIndex);
        }

        public static Quantity operator /(double f, QuantityBase q1)
        {
            // Dividing a scalar by a quantity inverts the unit: f / (x·U) has dimension U^-1.
            Unit invUnit = new Dimensionless() / q1.Unit;
            double val = (f / q1.ValueInSIUnits) / invUnit.Scale;
            return new Quantity(val, invUnit);
        }

        public static Quantity operator +(QuantityBase q1, QuantityBase q2)
        {
            if (q1._unit!.SameDimension(q2._unit))
            {
                return new Quantity(q1.Unit!.FromSIUnit(q1.ValueInSIUnits + q2.ValueInSIUnits), q1.Unit);
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }

        public static Quantity operator -(QuantityBase q1, QuantityBase q2)
        {
            if (q1._unit!.SameDimension(q2._unit))
            {
                return new Quantity(q1.Unit!.FromSIUnit(q1.ValueInSIUnits - q2.ValueInSIUnits), q1.Unit);
            }
            else
            {
                throw new IncompatibleUnits();
            }
        }

        public static Quantity operator *(QuantityBase q1, QuantityBase q2)
        {
            Unit? prodUnit = q1.Unit * q2.Unit;

            double val = (q1.ValueInSIUnits * q2.ValueInSIUnits) / prodUnit.Scale;

            return new Quantity(val, prodUnit);
        }

        public static Quantity operator /(QuantityBase q1, QuantityBase q2)
        {
            Unit? divUnit = q1.Unit / q2.Unit;

            double divVal = (q1.ValueInSIUnits / q2.ValueInSIUnits) / divUnit.Scale;

            return new Quantity(divVal, divUnit);
        }

        public static bool operator <=(QuantityBase q1, QuantityBase q2)
        {
            if (!q1._unit!.SameDimension(q2._unit)) throw new IncompatibleUnits();
            return q1.ValueInSIUnits <= q2.ValueInSIUnits;
        }

        public static bool operator >=(QuantityBase q1, QuantityBase q2)
        {
            if (!q1._unit!.SameDimension(q2._unit)) throw new IncompatibleUnits();
            return q1.ValueInSIUnits >= q2.ValueInSIUnits;
        }

        public static bool operator <(QuantityBase q1, QuantityBase q2)
        {
            if (!q1._unit!.SameDimension(q2._unit)) throw new IncompatibleUnits();
            return q1.ValueInSIUnits < q2.ValueInSIUnits;
        }

        public static bool operator >(QuantityBase q1, QuantityBase q2)
        {
            if (!q1._unit!.SameDimension(q2._unit)) throw new IncompatibleUnits();
            return q1.ValueInSIUnits > q2.ValueInSIUnits;
        }



        public static Quantity Pow(QuantityBase q1, int n)
        {
            Unit u = q1.Unit!.Pow(n);
            double siValue = Math.Pow(q1.ValueInSIUnits, (double)n);
            return new Quantity(u.FromSIUnit(siValue), u);
        }

        /// <summary>
        /// Find prefix that gives a value as close to 1 a possible
        /// </summary>
        public QuantityBase AdjustPrefix()
        {
            double val = Value;
            double minval = Math.Abs(val - 1); 
            Unit.SI_Prefix minind = _prefixIndex;
            foreach (Unit.SI_Prefix sIprefix in Enum.GetValues(typeof(Unit.SI_Prefix)))
            {
                Unit.SIprefix  pref = Unit.Prefixes[(int) sIprefix];
                double newValue =  Math.Abs(val*prefix.Factor / pref.Factor - 1);
                if (newValue < minval)
                {
                    minval = newValue;
                    minind = sIprefix;
                }
            }

            if (minind != _prefixIndex)
            {
                SetPrefix(minind);
            }

            return this;
        }

        public void SetPrefix(Unit.SI_Prefix newprefix)
        {
            //this. *= prefix.Factor / Unit.Prefixes[(int)newprefix].Factor;
            _prefixIndex = newprefix;
        }

        public override string ToString()
        {
            string str = "";

            string unitString = " "  + _unit;


            if (_prefixIndex != Unit.SI_Prefix.unity)
            {
                unitString = " " + prefix.Symbol + _unit;
            }
            

            if (Value >= 0.01)
            {
                str = Value.ToString("0.00") + unitString;
            }
            else
            {
                str = Value.ToString("e2") + unitString;
            }

            return str;
        }


        public virtual QuantityBase Clone()
        {
            return new QuantityBase(Value, _unit, _prefixIndex);
        }



        #region IComparable<QuantityBase> Members

        public int CompareTo(QuantityBase? other)
        {
            if (other == null) return 1;
            if (!_unit!.SameDimension(other._unit)) throw new IncompatibleUnits();
            return ValueInSIUnits.CompareTo(other.ValueInSIUnits);
        }

        #endregion
    }
}
