namespace ApiQueryLanguage.LanguageV1.Functions
{
    public class SumAggregateFunction
        : AggregateFunction
    {
        public SumAggregateFunction()
        {
        }

        public SumAggregateFunction(string propertyId)
            : base(propertyId)
        {
        }

        public SumAggregateFunction(string propertyId, string name)
            : base(propertyId, name)
        {
        }

        public override string Func { get; } = "sum";
    }
}
