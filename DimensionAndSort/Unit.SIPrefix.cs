namespace GreenOptimizer.DimensionAndSort
{
    public partial class Unit
    {
        public class SIprefix : IEquatable<SIprefix>
        {
            #region memberfields
            private string _symbol;
            private string _name;
            private double _factor;
            #endregion

            public string Symbol
            {
                get { return _symbol; }
                set { _symbol = value; }
            }

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            public double Factor
            {
                get { return _factor; }
                set { _factor = value; }
            }

            public SIprefix(int exp, string symb, string name)
            {
                _symbol = symb;
                _name = name;
                _factor = Math.Pow(10, exp);
            }

            public SIprefix()
            {
                _symbol = "nosymbol";
                _name = "noname";
                _factor = 1.0;
            }

            public bool Equals(SIprefix? other)
            {
                bool equal = false;
                if (other != null)
                {
                    equal = Math.Abs(_factor - other.Factor) < 1.0e-6;
                    equal = equal && _symbol.Equals(other.Symbol);
                    equal = equal && _name.Equals(other.Name);
                }

                return equal;
            }

            public override string ToString()
            {
                return _symbol;
            }
        }
    }
}
