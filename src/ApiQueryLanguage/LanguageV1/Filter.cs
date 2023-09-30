namespace ApiQueryLanguage.LanguageV1
{
    public abstract class Filter
        : ISet
    {
        public IEnumerable<ISet> Sets { get; } = new List<ISet>();

        public void Add(ISet set)
        {
            if (set != null)
            {
                ((List<ISet>)Sets).Add(set);
            }
        }
    }
}
