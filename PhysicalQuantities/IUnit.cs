namespace PhysicalQuantities
{
    public interface IUnit
    {
        double Scale { get; }
        double Offset { get; }
        Unit.SI_Prefix PrefixIndex { get; init; }
        bool SameDimension(Unit? other);
        double FromSIUnit(double val);
        Unit? Clone();
    }
}
