namespace ApiQueryLanguage.LanguageV1.Comparisons
{
    public sealed class LessThanOrEqualTo
        : ComparisonWithValues
    {
        public LessThanOrEqualTo()
        {
        }

        public LessThanOrEqualTo(string propertyId, object value)
            : base(propertyId, value)
        {
        }
    }
}
