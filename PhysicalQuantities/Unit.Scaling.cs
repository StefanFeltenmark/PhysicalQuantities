namespace PhysicalQuantities
{
    public partial class Unit
    {
        public struct Scaling : IEquatable<Scaling>
        {
            private double _factor; // say 60 for a minute, 3600 for hourly equivalent
            private string _symbol; // "HE", "SqFt"

            public double Factor
            {
                get { return _factor; }
                set { _factor = value; }
            }

            public string Symbol
            {
                get { return _symbol; }
                set { _symbol = value; }
            }

            public Scaling(string symbol, double factor = 1.0)
            {
                _symbol = symbol;
                _factor = factor;
            }

            public Scaling()
            {
                _symbol = "";
                _factor = 1.0;
            }

            public bool Equals(Scaling other)
            {
                return (Math.Abs(_factor - other.Factor) < 1e-6) && (_symbol.Equals(other.Symbol));
            }

            public override string ToString()
            {
                return _symbol;
            }
        }
    }
}
