using System.ComponentModel.DataAnnotations;

namespace ApiQueryLanguage.LanguageV1.Comparisons
{
    [Display(ShortName = "lte")]
    public sealed class LessThanOrEqualTo
        : ComparisonWithValues
    {
        public LessThanOrEqualTo()
        {
        }

        public LessThanOrEqualTo(string propertyId, object value)
            : base(propertyId, value)
        {
        }
    }
}
