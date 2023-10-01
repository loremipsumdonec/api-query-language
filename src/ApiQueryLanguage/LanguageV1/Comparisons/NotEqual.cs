namespace ApiQueryLanguage.LanguageV1.Comparisons
{
    public sealed class NotEqual
        : ComparisonWithValues
    {
        public NotEqual()
        {
        }

        public NotEqual(string propertyId)
            : base(propertyId)
        {
        }

        public NotEqual(string propertyId, object value)
            : base(propertyId, value)
        {
        }

        public NotEqual(string propertyId, IEnumerable<object> values)
            : base(propertyId, values)
        {
        }
    }
}
