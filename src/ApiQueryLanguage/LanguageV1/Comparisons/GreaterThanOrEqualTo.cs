namespace ApiQueryLanguage.LanguageV1.Comparisons
{
    public sealed class GreaterThanOrEqualTo
        : ComparisonWithValues
    {
        public GreaterThanOrEqualTo()
        {
        }

        public GreaterThanOrEqualTo(string propertyId, object value)
            : base(propertyId, value)
        {
        }
    }
}
