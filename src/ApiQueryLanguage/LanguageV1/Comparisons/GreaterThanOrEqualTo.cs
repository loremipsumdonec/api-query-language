using System.ComponentModel.DataAnnotations;

namespace ApiQueryLanguage.LanguageV1.Comparisons
{
    [Display(ShortName = "gte")]
    public sealed class GreaterThanOrEqualTo
        : ComparisonWithValues
    {
        public GreaterThanOrEqualTo()
        {
        }

        public GreaterThanOrEqualTo(string propertyId, object value)
            : base(propertyId, value)
        {
        }
    }
}
