using System.ComponentModel.DataAnnotations;

namespace ApiQueryLanguage.LanguageV1.Comparisons
{
    [Display(ShortName = "eq")]
    public sealed class Equal
        : ComparisonWithValues
    {
        public Equal()
        {
        }

        public Equal(string propertyId)
            : base(propertyId)
        {
        }

        public Equal(string propertyId, object value)
            : base(propertyId, value)
        {
        }
    }
}
