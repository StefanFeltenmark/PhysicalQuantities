namespace PhysicalQuantities
{
    public partial class Unit
    {
        public struct DimensionUnit
        {
            #region fields
            private int _exponent;
            private SI_Prefix _SI_prefix;
            private Scaling _scaling;
            #endregion

            public int Exponent
            {
                get { return _exponent; }
                set { _exponent = value; }
            }

            public Scaling Scaling
            {
                get { return _scaling; }
                set { _scaling = value; }
            }

            public SI_Prefix SI_prefix
            {
                get { return _SI_prefix; }
                set { _SI_prefix = value; }
            }

            public DimensionUnit(DimensionUnit u)
            {
                _exponent = u.Exponent;
                _SI_prefix = u.SI_prefix;
                _scaling = u.Scaling;
            }

            public void CopyTo(DimensionUnit u)
            {
                u.Exponent = _exponent;
                u._SI_prefix = _SI_prefix;
                u._scaling = _scaling;
            }

            public DimensionUnit(int exponent, Scaling scale, SI_Prefix prefix)
            {
                _exponent = exponent;
                _SI_prefix = prefix;
                _scaling = scale;
            }

            public DimensionUnit()
            {
                _exponent = 0;
                _SI_prefix = SI_Prefix.unity;
                _scaling = new Scaling();
            }

            public override string ToString()
            {
                return _prefixes[(int)_SI_prefix].Name;
            }
        }
    }
}
