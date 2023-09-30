using System.ComponentModel.DataAnnotations;

namespace ApiQueryLanguage.LanguageV1.Comparisons
{
    [Display(ShortName = "contains")]
    public sealed class Contains
        : ComparisonWithValues
    {
        public Contains()
        {
        }

        public Contains(string propertyId, object value)
            : base(propertyId, value)
        {
        }
    }
}
