namespace ApiQueryLanguage.LanguageV1.Comparisons
{
    public abstract class Comparison
        : ISet
    {
        public string PropertyId { get; set; } = string.Empty;

        public string? FromPropertyId { get; set; }
    }
}

