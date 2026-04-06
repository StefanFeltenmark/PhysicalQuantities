using DimensionAndSort;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GreenOptimizer.DimensionAndSort
{
    /// <summary>
    /// Compact Newtonsoft.Json converter for <see cref="QuantityBase"/> and all subtypes.
    ///
    /// Registered units are serialized as:
    ///   { "v": 1.0, "u": "MegaWatt" }
    ///
    /// Unregistered units fall back to storing the SI value and raw unit definition:
    ///   { "si": 3600000000.0, "d": [2,1,-2,0,0,0,0], "s": 3600000000.0, "o": 0.0, "p": 10 }
    ///
    /// Register the converter once on your JsonSerializerSettings:
    ///   settings.Converters.Add(new QuantityJsonConverter());
    /// </summary>
    public class QuantityJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) =>
            typeof(QuantityBase).IsAssignableFrom(objectType);

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value is not QuantityBase q)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteStartObject();

            string? unitName = UnitRegistry.TryGetName(q.Unit);
            if (unitName != null)
            {
                writer.WritePropertyName("v");
                writer.WriteValue(q.Value);
                writer.WritePropertyName("u");
                writer.WriteValue(unitName);
            }
            else
            {
                // Fallback: store SI value + raw unit definition
                writer.WritePropertyName("si");
                writer.WriteValue(q.ValueInSIUnits);

                if (q.Unit?._dimensions != null)
                {
                    writer.WritePropertyName("d");
                    writer.WriteStartArray();
                    foreach (Unit.DimensionUnit dim in q.Unit._dimensions)
                        writer.WriteValue(dim.Exponent);
                    writer.WriteEndArray();

                    writer.WritePropertyName("s");
                    writer.WriteValue(q.Unit.Scale);
                    writer.WritePropertyName("o");
                    writer.WriteValue(q.Unit.Offset);
                    writer.WritePropertyName("p");
                    writer.WriteValue((int)q.Unit.PrefixIndex);
                }
            }

            writer.WriteEndObject();
        }

        public override object? ReadJson(
            JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);

            if (jo["u"] != null)
            {
                // Compact format
                double value = jo["v"]!.Value<double>();
                Unit? unit = UnitRegistry.TryGet(jo["u"]!.Value<string>());
                return new Quantity(value, unit);
            }
            else
            {
                // Fallback format
                double siValue = jo["si"]!.Value<double>();
                Unit? unit = null;

                if (jo["d"] is JArray dims && dims.Count == 7)
                {
                    double scale = jo["s"]?.Value<double>() ?? 1.0;
                    double offset = jo["o"]?.Value<double>() ?? 0.0;
                    int prefix = jo["p"]?.Value<int>() ?? (int)Unit.SI_PrefixEnum.unity;

                    // Construct unit from raw dimensions; scale already encodes the prefix factor
                    unit = new Unit(
                        dims[0].Value<int>(), dims[1].Value<int>(), dims[2].Value<int>(),
                        dims[3].Value<int>(), dims[4].Value<int>(), dims[5].Value<int>(),
                        dims[6].Value<int>());
                    unit.Scale = scale;
                    unit.Offset = offset;
                    unit.PrefixIndex = (Unit.SI_PrefixEnum)prefix;
                }

                // Build a Quantity from SI value directly
                Quantity result = new Quantity(0, unit);
                result.ValueInSIUnits = siValue;
                return result;
            }
        }
    }
}
