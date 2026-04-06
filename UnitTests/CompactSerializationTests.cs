using System.Collections.Generic;
using System.IO;
using Xunit;
using DimensionAndSort;
using GreenOptimizer.DimensionAndSort;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProtoBuf;

namespace UnitTests
{
    public class CompactSerializationTests
    {
        // -------------------------------------------------------------------------
        // UnitRegistry
        // -------------------------------------------------------------------------

        [Fact]
        public void Registry_KnownUnitsAreReachableByName()
        {
            Assert.NotNull(UnitRegistry.TryGet("MegaWatt"));
            Assert.NotNull(UnitRegistry.TryGet("WattHour"));
            Assert.NotNull(UnitRegistry.TryGet("MegaWattHour"));
            Assert.NotNull(UnitRegistry.TryGet("Metre"));
            Assert.NotNull(UnitRegistry.TryGet("Joule"));
        }

        [Fact]
        public void Registry_UnknownNameReturnsNull()
        {
            Assert.Null(UnitRegistry.TryGet("NonExistentUnit"));
        }

        [Fact]
        public void Registry_UnitInstanceHasName()
        {
            Assert.Equal("MegaWatt", UnitRegistry.TryGetName(Units.MegaWatt));
            Assert.Equal("Metre",    UnitRegistry.TryGetName(Units.Metre));
            Assert.Equal("Joule",    UnitRegistry.TryGetName(Units.Joule));
        }

        [Fact]
        public void Registry_UnregisteredUnitHasNoName()
        {
            // J/kg = m²/s² (specific energy) — not a named entry in Units
            var derived = new Joule() / new Kilogram();
            Assert.Null(UnitRegistry.TryGetName(derived));
        }

        [Fact]
        public void Registry_CustomUnitCanBeRegistered()
        {
            var myUnit = new MegaWatt();
            UnitRegistry.Register("CustomMW", myUnit);
            Assert.NotNull(UnitRegistry.TryGet("CustomMW"));
        }

        // -------------------------------------------------------------------------
        // JSON — compact format (registered units)
        // -------------------------------------------------------------------------

        [Fact]
        public void Json_RegisteredUnit_RoundTrip()
        {
            Power p = new Power(42.5, Units.MegaWatt);
            Quantity result = CompactJsonRoundTrip(p);
            Assert.Equal(p.ValueInSIUnits, result.ValueInSIUnits, precision: 6);
        }

        [Fact]
        public void Json_RegisteredUnit_OutputIsCompact()
        {
            Power p = new Power(1.0, Units.MegaWatt);
            string json = Serialize(p);
            JObject jo = JObject.Parse(json);

            Assert.True(jo.ContainsKey("v"), "should have 'v' (value)");
            Assert.True(jo.ContainsKey("u"), "should have 'u' (unit name)");
            Assert.Equal("MegaWatt", jo["u"]!.Value<string>());
            Assert.False(jo.ContainsKey("si"), "should not have SI fallback fields");
        }

        [Fact]
        public void Json_RegisteredUnit_ListRoundTrip()
        {
            var powers = new List<Power>
            {
                new Power(100, Units.MegaWatt),
                new Power(200, Units.MegaWatt),
                new Power(50,  Units.Watt),
            };

            string json = JsonConvert.SerializeObject(powers, ConverterSettings());
            var result = JsonConvert.DeserializeObject<List<Quantity>>(json, ConverterSettings())!;

            Assert.Equal(powers.Count, result.Count);
            for (int i = 0; i < powers.Count; i++)
                Assert.Equal(powers[i].ValueInSIUnits, result[i].ValueInSIUnits, precision: 6);
        }

        [Fact]
        public void Json_Energy_DifferentUnits_RoundTrip()
        {
            var energies = new List<Energy>
            {
                new Energy(1.0,   Units.MegaWattHour),
                new Energy(3600,  Units.WattHour),
                new Energy(1000,  Units.Joule),
            };

            string json = JsonConvert.SerializeObject(energies, ConverterSettings());
            var result = JsonConvert.DeserializeObject<List<Quantity>>(json, ConverterSettings())!;

            for (int i = 0; i < energies.Count; i++)
                Assert.Equal(energies[i].ValueInSIUnits, result[i].ValueInSIUnits, precision: 3);
        }

        // -------------------------------------------------------------------------
        // JSON — fallback format (unregistered / derived units)
        // -------------------------------------------------------------------------

        [Fact]
        public void Json_UnregisteredUnit_FallbackRoundTrip()
        {
            // J/kg = m²/s² — not a named entry in Units, triggers fallback path
            var derivedUnit = new Joule() / new Kilogram();
            var q = new SpecificEnergy(500, derivedUnit);

            string json = Serialize(q);
            JObject jo = JObject.Parse(json);

            Assert.True(jo.ContainsKey("si"),  "fallback should have 'si'");
            Assert.True(jo.ContainsKey("d"),   "fallback should have 'd' (dimensions)");
            Assert.False(jo.ContainsKey("u"),  "fallback should not have 'u'");

            Quantity result = Deserialize(json);
            Assert.Equal(q.ValueInSIUnits, result.ValueInSIUnits, precision: 6);
        }

        // -------------------------------------------------------------------------
        // Protobuf — QuantityDto
        // -------------------------------------------------------------------------

        [Fact]
        public void Protobuf_RegisteredUnit_RoundTrip()
        {
            Power p = new Power(42.5, Units.MegaWatt);

            QuantityDto dto = QuantityDto.From(p);
            Assert.Equal("MegaWatt", dto.UnitName);
            Assert.Null(dto.Dimensions);

            Quantity result = (Quantity)ProtobufRoundTrip(dto);
            Assert.Equal(p.ValueInSIUnits, result.ValueInSIUnits, precision: 6);
        }

        [Fact]
        public void Protobuf_RegisteredUnit_ListRoundTrip()
        {
            var powers = new List<Power>
            {
                new Power(100, Units.MegaWatt),
                new Power(200, Units.MegaWatt),
                new Power(0.5, Units.MegaWatt),
            };

            var dtos = powers.ConvertAll(QuantityDto.From);
            using var ms = new MemoryStream();
            Serializer.Serialize(ms, dtos);

            ms.Position = 0;
            var results = Serializer.Deserialize<List<QuantityDto>>(ms);

            Assert.Equal(powers.Count, results.Count);
            for (int i = 0; i < powers.Count; i++)
                Assert.Equal(powers[i].ValueInSIUnits, results[i].ToQuantity().ValueInSIUnits, precision: 6);
        }

        [Fact]
        public void Protobuf_UnregisteredUnit_FallbackRoundTrip()
        {
            // J/kg = m²/s² — not a named entry in Units, triggers fallback path
            var derivedUnit = new Joule() / new Kilogram();
            var q = new SpecificEnergy(500, derivedUnit);

            QuantityDto dto = QuantityDto.From(q);
            Assert.Null(dto.UnitName);
            Assert.NotNull(dto.Dimensions);

            Quantity result = (Quantity)ProtobufRoundTrip(dto);
            Assert.Equal(q.ValueInSIUnits, result.ValueInSIUnits, precision: 6);
        }

        [Fact]
        public void Protobuf_ImplicitConversion_RoundTrip()
        {
            Energy e = new Energy(0.25, Units.MegaWattHour);

            QuantityDto dto = e;                // implicit operator
            Quantity result = dto;              // implicit operator

            Assert.Equal(e.ValueInSIUnits, result.ValueInSIUnits, precision: 6);
        }

        // -------------------------------------------------------------------------
        // JSON — prefix preservation
        // -------------------------------------------------------------------------

        [Fact]
        public void Json_PrefixedUnit_RoundTripPreservesValue()
        {
            // 1.5 MW stored as Watt + mega prefix — value must survive round-trip
            var p = new Power(1.5, Unit.SI_PrefixEnum.mega);
            Quantity result = CompactJsonRoundTrip(p);
            Assert.Equal(p.ValueInSIUnits, result.ValueInSIUnits, precision: 6);
        }

        [Fact]
        public void Json_PrefixedUnit_OutputIncludesPField()
        {
            var p = new Power(1.5, Unit.SI_PrefixEnum.mega);
            string json = Serialize(p);
            JObject jo = JObject.Parse(json);
            Assert.True(jo.ContainsKey("p"), "compact format should include 'p' when prefix != unity");
            Assert.Equal((int)Unit.SI_PrefixEnum.mega, jo["p"]!.Value<int>());
        }

        [Fact]
        public void Json_UnityPrefix_NoPField()
        {
            var p = new Power(1500, Units.Watt);
            string json = Serialize(p);
            JObject jo = JObject.Parse(json);
            Assert.False(jo.ContainsKey("p"), "unity prefix should not write 'p'");
        }

        // -------------------------------------------------------------------------
        // New quantity types — Frequency and ElectricCharge
        // -------------------------------------------------------------------------

        [Fact]
        public void Registry_HertzAndCoulombAreRegistered()
        {
            Assert.NotNull(UnitRegistry.TryGet("Hertz"));
            Assert.NotNull(UnitRegistry.TryGet("Coulomb"));
        }

        [Fact]
        public void Json_Frequency_RoundTrip()
        {
            var f = new Frequency(50, Units.Hertz);
            Quantity result = CompactJsonRoundTrip(f);
            Assert.Equal(f.ValueInSIUnits, result.ValueInSIUnits, precision: 6);
        }

        [Fact]
        public void Json_ElectricCharge_RoundTrip()
        {
            var q = new ElectricCharge(3600, Units.Coulomb); // 1 Ah
            Quantity result = CompactJsonRoundTrip(q);
            Assert.Equal(q.ValueInSIUnits, result.ValueInSIUnits, precision: 6);
        }

        // -------------------------------------------------------------------------
        // Helpers
        // -------------------------------------------------------------------------

        private static JsonSerializerSettings ConverterSettings() =>
            new JsonSerializerSettings
            {
                Converters = { new QuantityJsonConverter() },
                Formatting = Formatting.None
            };

        private static string Serialize(QuantityBase q) =>
            JsonConvert.SerializeObject(q, ConverterSettings());

        private static Quantity Deserialize(string json) =>
            JsonConvert.DeserializeObject<Quantity>(json, ConverterSettings())!;

        private static Quantity CompactJsonRoundTrip(QuantityBase q) =>
            Deserialize(Serialize(q));

        private static Quantity ProtobufRoundTrip(QuantityDto dto)
        {
            using var ms = new MemoryStream();
            Serializer.Serialize(ms, dto);
            ms.Position = 0;
            return Serializer.Deserialize<QuantityDto>(ms).ToQuantity();
        }
    }
}
