namespace ApiQueryLanguage.LanguageV1.Comparisons
{
    public sealed class GreaterThan
        : ComparisonWithValues
    {
        public GreaterThan()
        {
        }

        public GreaterThan(string propertyId, object value)
            : base(propertyId, value)
        {
        }
    }
}
