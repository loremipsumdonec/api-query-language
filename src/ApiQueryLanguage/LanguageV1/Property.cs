namespace ApiQueryLanguage.LanguageV1
{
    public class Property
        : IProperty
    {
        public Property()
        {
        }

        public Property(string propertyId)
        {
            PropertyId = propertyId;
        }

        public Property(string propertyId, string name)
        {
            PropertyId = propertyId;
            Name = name;
        }

        public Property(string propertyId, bool hidden)
        {
            PropertyId = propertyId;
            Hidden = hidden;
        }

        public string? PropertyId { get; set; }

        public string? FromPropertyId { get; set; }

        public string? Name { get; set; }

        public bool Hidden { get; set; }

        public virtual string Type { get; } = "property";
    }
}
