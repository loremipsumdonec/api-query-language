namespace ApiQueryLanguage.LanguageV1.Comparisons
{
    public interface IComparisonFactory
    {
        public Comparison CreateComparison(string segment);
    }
}
