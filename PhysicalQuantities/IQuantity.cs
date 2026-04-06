using PhysicalQuantities;

namespace PhysicalQuantities
{
    public interface IQuantity
    {
        double Value { get; }
        double ValueInSIUnits { get; set; }
        Unit? Unit { get; set; }
        Unit.SI_Prefix PrefixIndex { get; set; }
        void SetUnit(Unit? newUnit);
        QuantityBase Clone();
    }
}
