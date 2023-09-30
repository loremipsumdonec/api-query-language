using System.ComponentModel.DataAnnotations;

namespace ApiQueryLanguage.LanguageV1.Comparisons
{
    [Display(ShortName = "lt")]
    public sealed class LessThan
        : ComparisonWithValues
    {
        public LessThan()
        {
        }

        public LessThan(string propertyId, object value)
            : base(propertyId, value)
        {
        }
    }
}
