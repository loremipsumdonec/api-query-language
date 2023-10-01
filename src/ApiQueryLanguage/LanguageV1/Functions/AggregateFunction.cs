namespace ApiQueryLanguage.LanguageV1.Functions
{
    public abstract class AggregateFunction
        : Function
    {
        protected AggregateFunction()
        {
        }

        protected AggregateFunction(string propertyId)
            : base(propertyId)
        {
        }

        protected AggregateFunction(string propertyId, string name)
            : base(propertyId, name)
        {
        }

        public override string Type { get; } = "aggregate";
    }
}
