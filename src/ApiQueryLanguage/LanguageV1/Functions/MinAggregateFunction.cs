namespace ApiQueryLanguage.LanguageV1.Functions
{
    public class MinAggregateFunction
        : AggregateFunction
    {
        public MinAggregateFunction()
        {
        }

        public MinAggregateFunction(string propertyId)
            : base(propertyId)
        {
        }

        public MinAggregateFunction(string propertyId, string name)
            : base(propertyId, name)
        {
        }

        public override string Func { get; } = "min";
    }
}
