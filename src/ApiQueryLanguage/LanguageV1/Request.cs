namespace ApiQueryLanguage.LanguageV1
{
    internal sealed class Request
        : IRequest
    {
        public int Fetch { get; set; }

        public int Offset { get; set; }

        public IEnumerable<IProperty> Properties { get; set; } = new List<IProperty>();

        public IEnumerable<IOrderByProperty> OrderBy { get; set; } = new List<IOrderByProperty>();

        public IFilter? Filter { get; set; }

        public void Add(IProperty property)
        {
            ((List<IProperty>)Properties).Add(property);
        }

        public void Add(IOrderByProperty orderByProperty)
        {
            ((List<IOrderByProperty>)OrderBy).Add(orderByProperty);
        }
    }
}
