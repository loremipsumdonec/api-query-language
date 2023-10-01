namespace ApiQueryLanguage.LanguageV1.Comparisons
{
    public sealed class LessThan
        : ComparisonWithValues
    {
        public LessThan()
        {
        }

        public LessThan(string propertyId, object value)
            : base(propertyId, value)
        {
        }
    }
}
