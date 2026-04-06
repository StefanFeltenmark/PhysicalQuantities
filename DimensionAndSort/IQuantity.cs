using DimensionAndSort;

namespace GreenOptimizer.DimensionAndSort
{
    public interface IQuantity
    {
        double Value { get; }
        double ValueInSIUnits { get; set; }
        Unit? Unit { get; set; }
        Unit.SI_PrefixEnum PrefixIndex { get; set; }
        void SetUnit(Unit? newUnit);
        QuantityBase Clone();
    }
}
