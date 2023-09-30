using ApiQueryLanguage.LanguageV1.Comparisons;

namespace ApiQueryLanguage.LanguageV1
{
    internal static class ComparisonFactory
    {
        private const char CollectionSeperator = ',';

        public static Comparison CreateComparison(Type comparisonType, string segment)
        {
            if (comparisonType is null)
            {
                throw new ArgumentNullException(nameof(comparisonType));
            }

            if (string.IsNullOrEmpty(segment))
            {
                throw new ArgumentNullException(nameof(segment));
            }

            var comparison = Activator.CreateInstance(comparisonType) as Comparison
                ?? throw new ArgumentException("Comparison type is not a Comparison", nameof(comparisonType));

            comparison.PropertyId = GetPropertyId(segment);
            comparison.FromPropertyId = GetFromPropertyId(segment);

            if (comparison is ComparisonWithValues withValues)
            {
                foreach (string value in GetValues(segment))
                {
                    withValues.Add(value);
                }
            }

            return comparison;
        }

        public static string GetPropertyId(string segment)
        {
            int start = segment.IndexOf("(") + 1;

            if (HasFromPropertyId(segment))
            {
                return segment[start..segment.IndexOf("<")].Trim();
            }

            if (segment.IndexOf(",") > -1)
            {
                return segment[start..segment.IndexOf(",")].Trim();
            }

            return segment[start..segment.IndexOf(")")].Trim();
        }

        public static IEnumerable<string> GetValues(string segment)
        {
            if (segment.IndexOf(",") < 0)
            {
                return new List<string>();
            }

            return segment.Substring(segment.IndexOf(","), segment.Length - segment.IndexOf(",") - 1)
                .AdvancedSplit(CollectionSeperator, options: StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        }

        public static string GetFromPropertyId(string segment)
        {
            if (HasFromPropertyId(segment))
            {
                segment = segment[(segment.IndexOf('<') + 1)..];
                return segment[..segment.IndexOf('>')];
            }

            return string.Empty;
        }

        public static bool HasFromPropertyId(string segment)
        {
            return segment.Contains('>');
        }
    }
}
