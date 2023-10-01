using ApiQueryLanguage.LanguageV1.Functions;

namespace ApiQueryLanguage.LanguageV1
{
    public static class PropertyFactory
    {
        private const char CollectionSeperator = ',';

        public static IEnumerable<IProperty> CreateProperties(string? segment)
        {
            if (string.IsNullOrEmpty(segment))
            {
                yield break;
            }

            foreach (string current in CreateSegments(segment))
            {
                string path = GetPropertyId(current);

                if (string.IsNullOrEmpty(path))
                {
                    continue;
                }

                yield return Create(current);
            }
        }

        private static IProperty Create(string segment)
        {
            if (HasFunction(segment))
            {
                return CreateFunction(segment);
            }

            return CreateProperty(segment);
        }

        public static Function CreateFunction(string segment)
        {
            var functionType = GetFunctionType(segment);
            var function = Activator.CreateInstance(functionType) as Function
                ?? throw new NullReferenceException();

            function.Name = GetName(segment);
            function.PropertyId = GetPropertyId(segment);

            return function;
        }

        public static Type GetFunctionType(string segment)
        {
            string functionName = GetFunction(segment);

            return functionName.ToLower() switch
            {
                "sum" => typeof(SumAggregateFunction),
                "max" => typeof(MaxAggregateFunction),
                "min" => typeof(MinAggregateFunction),
                "avg" => typeof(AvgAggregateFunction),
                "count" => typeof(CountAggregateFunction),
                _ => throw new ArgumentException("Not supported function"),
            };
        }

        public static IProperty CreateProperty(string segment)
        {
            return new Property()
            {
                Name = GetName(segment),
                PropertyId = GetPropertyId(segment),
                FromPropertyId = GetFromPropertyId(segment)
            };
        }

        public static string GetFunction(string segment)
        {
            if (HasFunction(segment))
            {
                return segment[..segment.IndexOf("(")];
            }

            return string.Empty;
        }

        public static string GetName(string segment)
        {
            if (HasName(segment))
            {
                segment = segment[(segment.IndexOf("[") + 1)..];
                return segment[..segment.IndexOf("]")];
            }

            return string.Empty;
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

        public static IEnumerable<string> GetParameters(string segment)
        {
            int indexOf = segment.IndexOf('(');
            int lastIndexOf = segment.LastIndexOf(')');
            return segment
                .Substring(startIndex: indexOf + 1, length: lastIndexOf - indexOf - 1)
                .Split(CollectionSeperator, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        }

        public static bool HasName(string segment)
        {
            return segment.EndsWith(']');
        }

        public static bool HasFromPropertyId(string segment)
        {
            return segment.Contains('>');
        }

        public static string GetPropertyId(string segment)
        {
            if (HasFunction(segment))
            {
                segment = segment[..segment.IndexOf(")")];

                int start = segment.IndexOf('(') + 1;
                int end = segment.Length - start;

                return segment.Substring(
                    start,
                    end
                );
            }

            if (HasFromPropertyId(segment))
            {
                return segment[..segment.IndexOf("<")];
            }

            if (HasName(segment))
            {
                return segment[..segment.IndexOf("[")];
            }

            return segment;
        }

        public static bool HasFunction(string segment)
        {
            return segment.Contains('(');
        }

        private static IEnumerable<string> CreateSegments(string queryString)
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
    }
}
