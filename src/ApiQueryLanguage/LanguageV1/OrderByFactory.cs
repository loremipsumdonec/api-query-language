namespace ApiQueryLanguage.LanguageV1
{
    public static class OrderByFactory
    {
        private const char CollectionSeperator = ',';
        private const char Seperator = '_';

        public static IEnumerable<IOrderByProperty> CreateOrderBy(string? segment)
        {
            if (string.IsNullOrEmpty(segment))
            {
                yield break;
            }

            foreach (string current in GetSegments(segment))
            {
                yield return CreatePropertyOrderBy(current);
            }
        }

        public static IOrderByProperty CreatePropertyOrderBy(string segment)
        {
            return new OrderByProperty()
            {
                PropertyId = GetPropertyId(segment),
                Direction = GetDirection(segment),
            };
        }

        public static string GetPropertyId(string segment)
        {
            if (segment.IndexOf("_") > -1)
            {
                return segment[..segment.IndexOf("_")];
            }

            return segment;
        }

        public static SortingDirections GetDirection(string segment)
        {
            if (segment.IndexOf(Seperator) > -1)
            {
                string direction = segment[(segment.IndexOf(Seperator) + 1)..];

                switch (direction.ToUpperInvariant())
                {
                    case "ASC":
                        return SortingDirections.Ascending;
                    case "DESC":
                        return SortingDirections.Descending;
                }
            }

            return SortingDirections.None;
        }

        public static IEnumerable<string> GetSegments(string queryString)
        {
            foreach (string value in queryString.Split(
                new char[] { CollectionSeperator }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            )
            {
                yield return value.Trim();
            }
        }
    }
}
