namespace ApiQueryLanguage.LanguageV1.Comparisons
{
    public sealed class EndsWith
        : ComparisonWithValues
    {
        public EndsWith()
        {
        }

        public EndsWith(string propertyId, object value)
            : base(propertyId, value)
        {
        }
    }
}
