using System.ComponentModel.DataAnnotations;

namespace ApiQueryLanguage.LanguageV1.Comparisons
{
    [Display(ShortName = "gt")]
    public sealed class GreaterThan
        : ComparisonWithValues
    {
        public GreaterThan()
        {
        }

        public GreaterThan(string propertyId, object value)
            : base(propertyId, value)
        {
        }
    }
}
