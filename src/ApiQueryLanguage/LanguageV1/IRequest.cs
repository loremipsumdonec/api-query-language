namespace ApiQueryLanguage.LanguageV1
{
    public interface IRequest
    {
        public IEnumerable<IOrderByProperty> OrderBy { get; set; }

        public IEnumerable<IProperty> Properties { get; }

        public IFilter? Filter { get; }

        public int Offset { get; }

        public int Fetch { get; }
    }
}
