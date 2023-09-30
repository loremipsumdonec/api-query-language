namespace ApiQueryLanguage.LanguageV1.Sorting
{
    public sealed class OrderByProperty
    {
        public OrderByProperty()
        {
        }

        public OrderByProperty(string propertyId, SortingDirections direction)
        {
            PropertyId = propertyId;
            Direction = direction;
        }

        public string? PropertyId { get; set; }

        public SortingDirections Direction { get; set; }
    }
}
