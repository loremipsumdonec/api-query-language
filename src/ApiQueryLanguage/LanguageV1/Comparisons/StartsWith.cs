using System.ComponentModel.DataAnnotations;

namespace ApiQueryLanguage.LanguageV1.Comparisons
{
    [Display(ShortName = "startsWith")]
    public sealed class StartsWith
        : ComparisonWithValues
    {
        public StartsWith()
        {
        }

        public StartsWith(string propertyId, object value)
            : base(propertyId, value)
        {
        }
    }
}
