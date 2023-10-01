namespace ApiQueryLanguage.LanguageV1.Functions
{
    public class AvgAggregateFunction
        : AggregateFunction
    {
        public AvgAggregateFunction()
        {
        }

        public AvgAggregateFunction(string propertyId)
            : base(propertyId)
        {
        }

        public AvgAggregateFunction(string propertyId, string name)
            : base(propertyId, name)
        {
        }

        public override string Func { get; } = "avg";
    }
}
