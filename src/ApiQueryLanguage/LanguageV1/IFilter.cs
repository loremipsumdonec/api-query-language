namespace ApiQueryLanguage.LanguageV1
{
    public interface IFilter
    {
        public IEnumerable<ISet> Sets { get; }
    }
}
