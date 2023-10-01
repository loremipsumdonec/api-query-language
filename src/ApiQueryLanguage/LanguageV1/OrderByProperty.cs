namespace ApiQueryLanguage.LanguageV1
{
    internal sealed class OrderByProperty
        : IOrderByProperty
    {
        public OrderByProperty()
        {
        }

        public OrderByProperty(string propertyId, SortingDirections direction)
        {
            PropertyId = propertyId;
            Direction = direction;
        }

        public string PropertyId { get; set; } = string.Empty;

        public SortingDirections Direction { get; set; }
    }
}
