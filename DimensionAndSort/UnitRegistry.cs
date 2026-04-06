using System.Reflection;

namespace GreenOptimizer.DimensionAndSort
{
    /// <summary>
    /// Maps well-known unit names (matching the field names on <see cref="Units"/>) to their
    /// singleton instances, enabling compact serialization: store a name instead of a full
    /// 7-dimensional unit definition.
    /// </summary>
    public static class UnitRegistry
    {
        private static readonly Dictionary<string, Unit> _byName =
            new(StringComparer.Ordinal);

        static UnitRegistry()
        {
            foreach (FieldInfo field in typeof(Units).GetFields(
                BindingFlags.Public | BindingFlags.Static))
            {
                if (!typeof(Unit).IsAssignableFrom(field.FieldType)) continue;
                if (field.GetValue(null) is Unit unit && !_byName.ContainsKey(field.Name))
                    _byName[field.Name] = unit;
            }
        }

        /// <summary>Returns the unit registered under <paramref name="name"/>, or null.</summary>
        public static Unit? TryGet(string? name)
        {
            if (name == null) return null;
            return _byName.TryGetValue(name, out Unit? u) ? u : null;
        }

        /// <summary>
        /// Returns the registry name for <paramref name="unit"/> (e.g. "MegaWatt"), or null
        /// if the unit is not registered.
        /// </summary>
        public static string? TryGetName(Unit? unit)
        {
            if (unit == null) return null;
            foreach (KeyValuePair<string, Unit> kvp in _byName)
                if (kvp.Value.Equals(unit)) return kvp.Key;
            return null;
        }

        /// <summary>Registers a custom unit under <paramref name="name"/>.</summary>
        public static void Register(string name, Unit unit)
        {
            _byName[name] = unit;
        }

        /// <summary>All registered (name, unit) pairs.</summary>
        public static IEnumerable<KeyValuePair<string, Unit>> All => _byName;
    }
}
