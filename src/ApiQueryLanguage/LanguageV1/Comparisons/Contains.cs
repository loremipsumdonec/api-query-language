namespace ApiQueryLanguage.LanguageV1.Comparisons
{
    public sealed class Contains
        : ComparisonWithValues
    {
        public Contains()
        {
        }

        public Contains(string propertyId, object value)
            : base(propertyId, value)
        {
        }
    }
}
