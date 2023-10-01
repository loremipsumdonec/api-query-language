namespace ApiQueryLanguageTests
{
    public static class EnumerableExtension
    {
        public static T PickRandom<T>(this IEnumerable<T> source)
        {
            return source.PickRandom(1).Single();
        }

        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int min, int max)
        {
            Random random = new();
            int count = random.Next(min, max);

            return source.Shuffle().Take(count);
        }

        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
        {
            return source.Shuffle().Take(count);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(_ => Guid.NewGuid());
        }

        public static double? Median<TProp, TValue>(this IEnumerable<TProp> source, Func<TProp, TValue> selector)
        {
            return source.Select(selector).Median();
        }

        public static double? Median<T>(this IEnumerable<T> source)
        {
            if (Nullable.GetUnderlyingType(typeof(T)) != null)
            {
                source = source.Where(x => x != null);
            }

            int count = source.Count();

            if (count == 0)
            {
                return null;
            }

            source = source.OrderBy(n => n);

            int midpoint = count / 2;

            if (count % 2 == 0)
            {
                return (Convert.ToDouble(source.ElementAt(midpoint - 1)) + Convert.ToDouble(source.ElementAt(midpoint))) / 2.0;
            }

            return Convert.ToDouble(source.ElementAt(midpoint));
        }
    }
}
