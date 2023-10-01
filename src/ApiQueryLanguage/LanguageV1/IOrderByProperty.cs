namespace ApiQueryLanguage.LanguageV1
{
    public interface IOrderByProperty
    {
        public string PropertyId { get; }

        public SortingDirections Direction { get; }
    }
}
