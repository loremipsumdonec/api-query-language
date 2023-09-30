﻿using ApiQueryLanguage.LanguageV1;
using ApiQueryLanguage.LanguageV1.Sorting;

namespace ApiQueryLanguageTests.SUT.LanguageV1
{
    public class SortingFactoryTests
    {

        [Fact]
        public void CreateOrderBy_WithValidInput_HasExpected()
        {
            var orderBy = SortingFactory.CreateOrderBy("Person.name_DESC, Person.age_ASC");

            Assert.Equal(2, orderBy.Count());
        }

        [Fact]
        public void CreatePropertyOrderBy_WithValidInput_HasExpected()
        {
            var orderBy = SortingFactory.CreatePropertyOrderBy("Person.name_DESC");

            Assert.Equal("Person.name", orderBy.PropertyId);
            Assert.Equal(SortingDirections.Descending, orderBy.Direction);
        }

        [Theory]
        [InlineData("Person.name_ASC,Person.age_DESC", "Person.name_ASC;Person.age_DESC")]
        [InlineData("Person.name_ASC, Person.age_DESC", "Person.name_ASC;Person.age_DESC")]
        [InlineData("Person.name_ASC,", "Person.name_ASC")]
        public void GetSegments_ExpectedSegments(string queryString, string expectedSegments)
        {
            var segments = SortingFactory.GetSegments(queryString);

            Assert.Equal(expectedSegments, string.Join(";", segments));
        }

        [Theory]
        [InlineData("Person.name_ASC", "Person.name")]
        [InlineData("Person.name", "Person.name")]
        public void GetPropertyId_ExpectedPropertyId(string segment, string expectedPropertyId)
        {
            string path = SortingFactory.GetPropertyId(segment);

            Assert.Equal(expectedPropertyId, path);
        }

        [Theory]
        [InlineData("Person.Name_ASC", SortingDirections.Ascending)]
        [InlineData("Person.Name_asc", SortingDirections.Ascending)]
        [InlineData("Person.Name", SortingDirections.None)]
        [InlineData("Person.Name_DESC", SortingDirections.Descending)]
        [InlineData("Person.Name_desc", SortingDirections.Descending)]
        public void GetOrderBy_ExpectedOrderByDirection(string segment, SortingDirections expectedDirection)
        {
            var direction = SortingFactory.GetDirection(segment);

            Assert.Equal(expectedDirection, direction);
        }
    }
}
