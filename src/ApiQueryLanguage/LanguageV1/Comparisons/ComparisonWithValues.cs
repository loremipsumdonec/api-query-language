namespace ApiQueryLanguage.LanguageV1.Comparisons
{
    public abstract class ComparisonWithValues
        : Comparison
    {
        protected ComparisonWithValues()
        {
        }

        protected ComparisonWithValues(string propertyId)
        {
            PropertyId = propertyId;
        }

        protected ComparisonWithValues(string propertyId, object value)
        {
            PropertyId = propertyId;
            Add(value);
        }

        protected ComparisonWithValues(string propertyId, IEnumerable<object> values)
        {
            PropertyId = propertyId;
            Add(values);
        }

        public object? Value => Values.FirstOrDefault();

        public List<object> Values { get; set; } = new();

        public void Add(object value)
        {
            if (value != null)
            {
                Values.Add(value);
            }
        }

        public void AddValues(IEnumerable<object> values)
        {
            Values.AddRange(values);
        }
    }
}
