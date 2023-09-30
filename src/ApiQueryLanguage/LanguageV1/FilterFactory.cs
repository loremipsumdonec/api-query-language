using ApiQueryLanguage.LanguageV1.Comparisons;

namespace ApiQueryLanguage.LanguageV1
{
    internal sealed class FilterFactory
        : IFilterFactory
    {
        private const char CollectionSeperator = ',';

        public Filter Create(string segment)
        {
            Filter root = new Conjunction();
            Load(segment, root);

            if (root.Sets.Count() == 1 && root.Sets.First() is Filter filter)
            {
                return filter;
            }

            return root;
        }

        public void Load(string segment, Filter filter)
        {
            foreach (string s in GetSegments(segment))
            {
                var type = GetType(s);

                if (IsFilter(type))
                {
                    var current = Activator.CreateInstance(type) as Filter
                        ?? throw new NullReferenceException();

                    filter.Add(current);

                    Load(GetFilterParameters(s), current);

                    continue;
                }

                var comparison = ComparisonFactory.CreateComparison(type, s);
                filter.Add(comparison);
            }
        }

        public static string GetName(string segment)
        {
            return segment[..segment.IndexOf("(")];
        }

        public static bool IsFilter(Type type)
        {
            return type.IsAssignableTo(typeof(Filter));
        }

        public static Type GetType(string segment)
        {
            string functionName = GetName(segment);

            return functionName.ToLower() switch
            {
                "eq" => typeof(Equal),
                "neq" => typeof(NotEqual),
                "gt" => typeof(GreaterThan),
                "gte" => typeof(GreaterThanOrEqualTo),
                "lt" => typeof(LessThan),
                "lte" => typeof(LessThanOrEqualTo),
                "between" => typeof(Between),
                "startswith" => typeof(StartsWith),
                "and" => typeof(Conjunction),
                "or" => typeof(Disjunction),
                "contains" => typeof(Contains),
                _ => throw new NotImplementedException()
            };
        }

        public static IEnumerable<string> GetSegments(string queryString)
        {
            foreach (string value in queryString.AdvancedSplit(
                CollectionSeperator,
                quote: '(',
                endQuote: ')',
                keepQuote: true,
                options: StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
            ))
            {
                yield return value.Trim();
            }
        }

        public static string GetFilterParameters(string segment)
        {
            int indexOf = segment.IndexOf('(');
            return segment.Substring(startIndex: indexOf + 1, length: segment.Length - indexOf - 2);
        }
    }
}
