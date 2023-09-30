using System.ComponentModel.DataAnnotations;

namespace ApiQueryLanguage.LanguageV1.Comparisons
{
    [Display(ShortName = "endsWith")]
    public sealed class EndsWith
        : ComparisonWithValues
    {
        public EndsWith()
        {
        }

        public EndsWith(string propertyId, object value)
            : base(propertyId, value)
        {
        }
    }
}
