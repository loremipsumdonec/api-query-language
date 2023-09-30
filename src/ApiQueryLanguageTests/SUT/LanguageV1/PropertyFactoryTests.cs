using ApiQueryLanguage.LanguageV1;

namespace ApiQueryLanguageTests.SUT.LanguageV1
{
    public class PropertyFactoryTests
    {
        [Theory]
        [InlineData("Person.Name", "Person.Name")]
        [InlineData("Person.Name[A name]", "Person.Name")]
        [InlineData("SUM(Person.Name)", "Person.Name")]
        [InlineData("SUM(Person.Name)[A name]", "Person.Name")]
        [InlineData("CalendarEntry.year<Person.dateOfBirth>", "CalendarEntry.year")]
        public void GetPropertyId_ExpectedPropertyId(string segment, string expectedPropertyId)
        {
            string path = PropertyFactory.GetPropertyId(segment);

            Assert.Equal(expectedPropertyId, path);
        }

        [Theory]
        [InlineData("Person.Name", "")]
        [InlineData("Person.Name[A long name]", "A long name")]
        [InlineData("SUM(Person.Name)[Namn]", "Namn")]
        public void GetName_ExpectedName(string segment, string expectedName)
        {
            string name = PropertyFactory.GetName(segment);

            Assert.Equal(expectedName, name);
        }

        [Theory]
        [InlineData("Person.Name", "")]
        [InlineData("SUM(Person.Name)", "SUM")]
        [InlineData("SUM(Person.Name)[Namn]", "SUM")]
        [InlineData("LOREM(Person.Name)[Namn]", "LOREM")]
        public void GetFunction_ExpectedFunction(string segment, string expectedFunction)
        {
            string name = PropertyFactory.GetFunction(segment);

            Assert.Equal(expectedFunction, name);
        }

        [Theory]
        [InlineData("Person.Name<Pet.Owner>", "Pet.Owner")]
        [InlineData("SUM(Person.Name<Pet.Owner>)", "Pet.Owner")]
        [InlineData("SUM(Person.Name<Pet.Owner>)[Namn]", "Pet.Owner")]
        [InlineData("Person.Name", "")]
        public void GetFromPropertyId_ExpectedFromPropertyId(string segment, string expectedFromPropertyId)
        {
            string fromPropertyId = PropertyFactory.GetFromPropertyId(segment);

            Assert.Equal(expectedFromPropertyId, fromPropertyId);
        }
    }
}
