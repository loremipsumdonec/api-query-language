namespace ApiQueryLanguage.LanguageV1.Functions
{
    public class MaxAggregateFunction
        : AggregateFunction
    {
        public MaxAggregateFunction()
        {
        }

        public MaxAggregateFunction(string propertyId)
            : base(propertyId)
        {
        }

        public MaxAggregateFunction(string propertyId, string name)
            : base(propertyId, name)
        {
        }

        public override string Func { get; } = "max";
    }
}
