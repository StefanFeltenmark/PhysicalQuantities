namespace GreenOptimizer.DimensionAndSort
{
    public interface IUnit
    {
        double Scale { get; }
        double Offset { get; }
        Unit.SI_PrefixEnum PrefixIndex { get; set; }
        bool SameDimension(Unit? other);
        double FromSIUnit(double val);
        Unit? Clone();
    }
}
