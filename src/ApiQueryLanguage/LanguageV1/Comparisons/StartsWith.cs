namespace ApiQueryLanguage.LanguageV1.Comparisons
{
    public sealed class StartsWith
        : ComparisonWithValues
    {
        public StartsWith()
        {
        }

        public StartsWith(string propertyId, object value)
            : base(propertyId, value)
        {
        }
    }
}
