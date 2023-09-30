namespace ApiQueryLanguage.LanguageV1
{
    public interface IProperty
    {
        public string? PropertyId { get; }

        public string? FromPropertyId { get; set; }

        public string? Name { get; set; }

        public bool Hidden { get; }

        public string Type { get; }
    }
}
