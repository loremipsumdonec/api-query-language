namespace ApiQueryLanguage.LanguageV1.Functions
{
    public class CountAggregateFunction
        : AggregateFunction
    {
        public CountAggregateFunction()
        {
        }

        public CountAggregateFunction(string propertyId)
            : base(propertyId)
        {
        }

        public CountAggregateFunction(string propertyId, string name)
            : base(propertyId, name)
        {
        }

        public bool AllRows { get; set; }

        public override string Func { get; } = "count";
    }
}
