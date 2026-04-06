using PhysicalQuantities;
using ProtoBuf;

namespace PhysicalQuantities
{
    /// <summary>
    /// Compact protobuf-net DTO for <see cref="QuantityBase"/> and all subtypes.
    ///
    /// Registered units are stored as value + unit name (e.g. "MegaWatt").
    /// Unregistered units fall back to value-in-SI-units + raw unit definition.
    ///
    /// Usage:
    ///   // Serialize
    ///   var dto = QuantityDto.From(myPower);
    ///   Serializer.Serialize(stream, dto);
    ///
    ///   // Deserialize
    ///   var dto = Serializer.Deserialize&lt;QuantityDto&gt;(stream);
    ///   Power p = (Power)dto.ToQuantity();
    ///
    /// To serialize QuantityBase directly (e.g. inside another proto contract), register
    /// the surrogate once at startup:
    ///   QuantityDto.RegisterSurrogate();
    /// </summary>
    [ProtoContract]
    public class QuantityDto
    {
        /// <summary>Display value in the named unit (populated when <see cref="UnitName"/> is set).</summary>
        [ProtoMember(1)] public double Value { get; set; }

        /// <summary>Registry name from <see cref="UnitRegistry"/> (e.g. "MegaWatt"), or null for fallback.</summary>
        [ProtoMember(2)] public string? UnitName { get; set; }

        // Fallback fields — used when the unit is not in UnitRegistry.
        /// <summary>Value in SI units (fallback).</summary>
        [ProtoMember(3)] public double SIValue { get; set; }
        /// <summary>The 7 SI dimension exponents (fallback).</summary>
        [ProtoMember(4)] public int[]? Dimensions { get; set; }
        /// <summary>Unit scale factor relative to SI (fallback).</summary>
        [ProtoMember(5)] public double Scale { get; set; }
        /// <summary>Unit offset relative to SI (fallback, e.g. 273.15 for Celsius).</summary>
        [ProtoMember(6)] public double Offset { get; set; }
        /// <summary>SI prefix index (fallback).</summary>
        [ProtoMember(7)] public int PrefixIndex { get; set; }

        /// <summary>Converts a <see cref="QuantityBase"/> to its DTO representation.</summary>
        public static QuantityDto From(QuantityBase q)
        {
            string? name = UnitRegistry.TryGetName(q.Unit);
            if (name != null)
            {
                return new QuantityDto { Value = q.Value, UnitName = name };
            }

            // Fallback
            var dto = new QuantityDto { SIValue = q.ValueInSIUnits };
            if (q.Unit?._dimensions != null)
            {
                dto.Dimensions = q.Unit._dimensions.Select(d => d.Exponent).ToArray();
                dto.Scale = q.Unit.Scale;
                dto.Offset = q.Unit.Offset;
                dto.PrefixIndex = (int)q.Unit.PrefixIndex;
            }
            return dto;
        }

        /// <summary>
        /// Reconstructs a <see cref="Quantity"/> from this DTO.
        /// Cast to a typed subclass using the existing implicit operators, e.g.:
        ///   Power p = (Power)dto.ToQuantity();
        /// </summary>
        public Quantity ToQuantity()
        {
            if (UnitName != null)
            {
                Unit? unit = UnitRegistry.TryGet(UnitName);
                return new Quantity(Value, unit);
            }

            // Fallback
            Unit? fallbackUnit = null;
            if (Dimensions is { Length: 7 })
            {
                fallbackUnit = new Unit(
                    Dimensions[0], Dimensions[1], Dimensions[2],
                    Dimensions[3], Dimensions[4], Dimensions[5], Dimensions[6]);
                fallbackUnit.Scale = Scale;
                fallbackUnit.Offset = Offset;
                fallbackUnit.PrefixIndex = (Unit.SI_Prefix)PrefixIndex;
            }

            var result = new Quantity(0, fallbackUnit);
            result.ValueInSIUnits = SIValue;
            return result;
        }

        /// <summary>
        /// Registers <see cref="QuantityDto"/> as the protobuf-net surrogate for
        /// <see cref="QuantityBase"/>, enabling transparent serialization of
        /// <c>QuantityBase</c> fields inside other proto contracts.
        /// Call once at application startup.
        /// </summary>
        public static void RegisterSurrogate()
        {
            ProtoBuf.Meta.RuntimeTypeModel.Default
                .Add(typeof(QuantityBase), false)
                .SetSurrogate(typeof(QuantityDto));
        }

        public static implicit operator QuantityDto(QuantityBase q) => From(q);
        public static implicit operator Quantity(QuantityDto dto) => dto.ToQuantity();
    }
}
