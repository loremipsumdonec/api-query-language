using ApiQueryLanguage.LanguageV1;
using ApiQueryLanguage.LanguageV1.Comparisons;

namespace ApiQueryLanguageTests.SUT.LanguageV1
{
    public class ComparisonFactoryTests
    {
        [Theory]
        [InlineData("eq(Person.name)", typeof(Equal))]
        [InlineData("gt(Person.name)", typeof(GreaterThan))]
        [InlineData("gte(Person.name)", typeof(GreaterThanOrEqualTo))]
        [InlineData("lt(Person.name)", typeof(LessThan))]
        [InlineData("lte(Person.name)", typeof(LessThanOrEqualTo))]
        [InlineData("between(Person.name)", typeof(Between))]
        [InlineData("startswith(Person.name)", typeof(StartsWith))]
        [Trait("importance", "required")]
        public void CreateComparison_WithValidInput_HasPropertyId(string segment, Type type)
        {
            var comparison = ComparisonFactory.CreateComparison(type, segment);

            Assert.NotNull(comparison);
            Assert.Equal("Person.name", comparison.PropertyId);
        }

        [Theory]
        [InlineData("eq(Person.name, \"lorem\")", "Person.name")]
        [InlineData("eq(Person.name , \"lorem\")", "Person.name")]
        [InlineData("distinct(Person.name)", "Person.name")]
        [InlineData("eq(Person.name<Lorem.donec> , \"lorem\")", "Person.name")]
        [Trait("importance", "required")]
        public void GetPropertyId_ExpectedPropertyId(string segment, string expectedPropertyId)
        {
            string propertyId = ComparisonFactory.GetPropertyId(segment);

            Assert.Equal(expectedPropertyId, propertyId);
        }

        [Theory]
        [InlineData("eq(Person.name<Lorem.donec> , \"lorem\")", "Lorem.donec")]
        [Trait("importance", "required")]
        public void GetFromPropertyId_ExpectedFromPropertyId(string segment, string expectedFromPropertyId)
        {
            string propertyId = ComparisonFactory.GetFromPropertyId(segment);

            Assert.Equal(expectedFromPropertyId, propertyId);
        }

        [Theory]
        [InlineData("eq(Person.name, lorem, donec)", "lorem;donec")]
        [InlineData("eq(Person.name, \"lorem, ipsum\", donec)", "lorem, ipsum;donec")]
        [Trait("importance", "required")]
        public void GetValues_ExpectedValues(string segment, string expectedValues)
        {
            var values = ComparisonFactory.GetValues(segment);

            Assert.Equal(expectedValues, string.Join(";", values));
        }
    }
}
