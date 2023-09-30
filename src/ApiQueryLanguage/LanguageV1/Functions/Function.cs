namespace ApiQueryLanguage.LanguageV1.Functions
{
    public abstract class Function
        : Property
    {
        protected Function()
        {
        }

        protected Function(string propertyId)
            : base(propertyId)
        {
        }

        protected Function(string propertyId, string name)
            : base(propertyId, name)
        {
        }

        public virtual string? Func { get; }
    }
}
