using ApiQueryLanguage.LanguageV1;
using ApiQueryLanguage.LanguageV1.Comparisons;

namespace ApiQueryLanguageTests.SUT.LanguageV1
{
    [Trait("type", "unit")]
    public class FilterFactoryTests
    {
        [Theory]
        [InlineData("eq()", typeof(Equal))]
        [InlineData("gt()", typeof(GreaterThan))]
        [InlineData("gte()", typeof(GreaterThanOrEqualTo))]
        [InlineData("lt()", typeof(LessThan))]
        [InlineData("lte()", typeof(LessThanOrEqualTo))]
        [InlineData("between()", typeof(Between))]
        [InlineData("startswith()", typeof(StartsWith))]
        [InlineData("and()", typeof(Conjunction))]
        [InlineData("or()", typeof(Disjunction))]
        public void GetType_WithValidSegment_ReturnsExpectedType(string segment, Type expected)
        {
            var type = FilterFactory.GetType(segment);

            Assert.Equal(expected, type);
        }

        [Theory]
        [InlineData(typeof(Conjunction))]
        [InlineData(typeof(Disjunction))]
        public void IsFilter_WithValidFilterType_IsAFilter(Type type)
        {
            Assert.True(FilterFactory.IsFilter(type));
        }

        [Theory]
        [InlineData(typeof(Equal))]
        [InlineData(typeof(GreaterThan))]
        [InlineData(typeof(GreaterThanOrEqualTo))]
        [InlineData(typeof(LessThan))]
        [InlineData(typeof(LessThanOrEqualTo))]
        [InlineData(typeof(Between))]
        [InlineData(typeof(StartsWith))]
        public void IsFilter_WithComparisonType_IsNotAFilter(Type type)
        {
            Assert.False(FilterFactory.IsFilter(type));
        }

        [Theory]
        [InlineData("eq(Person.name, \"lorem\")", "eq")]
        [InlineData("startsWith(Person.name, \"lorem\")", "startsWith")]
        [InlineData("and(eq(Person.name, \"lorem\"))", "and")]
        public void GetName_WithValidSegment_ReturnsExpectedName(string segment, string expectedName)
        {
            string name = FilterFactory.GetName(segment);

            Assert.Equal(expectedName, name);
        }

        [Theory]
        [InlineData("eq(),lte(),gte()", "eq();lte();gte()")]
        [InlineData("eq(Person.name, a, b, c),lte(),gte()", "eq(Person.name, a, b, c);lte();gte()")]
        [InlineData("or(eq(),lte(),gte())", "or(eq(),lte(),gte())")]
        public void GetSegments_WithValidQueryString_ReturnExpectedSegments(string queryString, string expectedSegments)
        {
            var segments = FilterFactory.GetSegments(queryString);

            Assert.Equal(expectedSegments, string.Join(";", segments));
        }

        [Fact]
        public void GetFilterParameters_WithValidSegment_ReturnExpectedString()
        {
            string segment = "or(eq(),lte(),gte())";
            string expected = "eq(),lte(),gte()";

            string actual = FilterFactory.GetFilterParameters(segment);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Create_WithOnlyComparisons_FilterIsConjunction()
        {
            var factory = new FilterFactory();
            var filter = factory.Create("eq(Person.name, \"Lorem\"),gte(Person.age, 34)");

            Assert.IsType<Conjunction>(filter);
        }

        [Fact]
        public void Create_WithAnd_FilterIsConjunction()
        {
            var factory = new FilterFactory();
            var filter = factory.Create("and(eq(Person.name, \"Lorem\"),gte(Person.age, 34))");

            Assert.IsType<Conjunction>(filter);
        }

        [Fact]
        public void Create_WithOr_FilterIsDisjunction()
        {
            var factory = new FilterFactory();
            var filter = factory.Create("or(eq(Person.name, \"Lorem\"),gte(Person.age, 34))");

            Assert.IsType<Disjunction>(filter);
        }
    }
}
