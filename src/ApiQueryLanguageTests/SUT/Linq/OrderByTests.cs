using ApiQueryLanguage.LanguageV1;
using ApiQueryLanguage.Linq;

namespace ApiQueryLanguageTests.SUT.Linq
{
    [Trait("type", "unit")]
    public class OrderByTests
    {
        internal enum EyeColors
        {
            Blue,
            Green,
            Brown
        }

        internal class Person
        {
            public string Name { get; set; } = IpsumGenerator.GenerateName();

            public int Age { get; set; } = IpsumGenerator.Random(10, 80);

            public DateTime DateOfBirth { get; set; } = IpsumGenerator.RandomDate(1950, 2013);

            public EyeColors EyeColor { get; set; } = new List<EyeColors>() { EyeColors.Brown, EyeColors.Blue, EyeColors.Green }.PickRandom();
        }

        private static (IQueryable<Person>, IQueryable<Person>) CreatePersons(int total = 500)
        {
            List<Person> persons = new();

            for (int i = 0; i < total; i++)
            {
                persons.Add(new Person());
            }

            var shuffled = persons.Shuffle().ToList();

            Assert.NotEqual(persons, shuffled);

            return (shuffled.AsQueryable(), new List<Person>(shuffled).AsQueryable());
        }

        [Fact]
        public void ApplyOn_WithStringProperyAscendingOrder_ExpectedOrder()
        {
            var (expected, source) = CreatePersons();
            Assert.Equal(expected.ToList(), source.ToList());

            var orderBy = new List<OrderByProperty>()
            {
                new OrderByProperty("Person.name", SortingDirections.Ascending)
            };

            var builder = new OrderBy<Person>(orderBy);
            var actual = builder.ApplyOn(source);

            Assert.Equal(
                expected.OrderBy(p => p.Name).ToList(),
                actual.ToList()
            );
        }

        [Fact]
        public void ApplyOn_WithStringProperyDescendingOrder_ExpectedOrder()
        {
            var (expected, source) = CreatePersons();
            Assert.Equal(expected.ToList(), source.ToList());

            var orderBy = new List<OrderByProperty>()
            {
                new OrderByProperty("Person.name", SortingDirections.Descending)
            };

            var builder = new OrderBy<Person>(orderBy);
            var actual = builder.ApplyOn(source);

            Assert.Equal(
                expected.OrderByDescending(p => p.Name).ToList(),
                actual.ToList()
            );
        }

        [Fact]
        public void ApplyOn_WithStringProperyNoneOrder_ExpectedOrder()
        {
            var (expected, source) = CreatePersons();
            Assert.Equal(expected.ToList(), source.ToList());

            var orderBy = new List<OrderByProperty>()
            {
                new OrderByProperty("Person.name", SortingDirections.None)
            };

            var builder = new OrderBy<Person>(orderBy);
            var actual = builder.ApplyOn(source);

            Assert.Equal(
                expected.ToList(),
                actual.ToList()
            );
        }

        [Fact]
        public void ApplyOn_WithIntProperyAscendingOrder_ExpectedOrder()
        {
            var (expected, source) = CreatePersons();
            Assert.Equal(expected.ToList(), source.ToList());

            var orderBy = new List<OrderByProperty>()
            {
                new OrderByProperty("Person.age", SortingDirections.Ascending)
            };

            var builder = new OrderBy<Person>(orderBy);
            var actual = builder.ApplyOn(source);

            Assert.Equal(
                expected.OrderBy(p => p.Age).ToList(),
                actual.ToList()
            );
        }

        [Fact]
        public void ApplyOn_WithIntProperyDescendingOrder_ExpectedOrder()
        {
            var (expected, source) = CreatePersons();
            Assert.Equal(expected.ToList(), source.ToList());

            var orderBy = new List<OrderByProperty>()
            {
                new OrderByProperty("Person.age", SortingDirections.Descending)
            };

            var builder = new OrderBy<Person>(orderBy);
            var actual = builder.ApplyOn(source);

            Assert.Equal(expected.OrderByDescending(p => p.Age).ToList(), actual.ToList());
        }

        [Fact]
        public void ApplyOn_WithIntProperyNoneOrder_ExpectedOrder()
        {
            var (expected, source) = CreatePersons();
            Assert.Equal(expected.ToList(), source.ToList());

            var orderBy = new List<OrderByProperty>()
            {
                new OrderByProperty("Person.age", SortingDirections.None)
            };

            var builder = new OrderBy<Person>(orderBy);
            var actual = builder.ApplyOn(source);

            Assert.Equal(
                expected.ToList(),
                actual.ToList()
            );
        }

        [Fact]
        public void ApplyOn_WithDateTimeProperyAscendingOrder_ExpectedOrder()
        {
            var (expected, source) = CreatePersons();
            Assert.Equal(expected.ToList(), source.ToList());

            var orderBy = new List<OrderByProperty>()
            {
                new OrderByProperty("Person.dateOfBirth", SortingDirections.Ascending)
            };

            var builder = new OrderBy<Person>(orderBy);
            var actual = builder.ApplyOn(source);

            Assert.Equal(
                expected.OrderBy(p => p.DateOfBirth).ToList(),
                actual.ToList()
            );
        }

        [Fact]
        public void ApplyOn_WithDateTimeProperyDescendingOrder_ExpectedOrder()
        {
            var (expected, source) = CreatePersons();
            Assert.Equal(expected.ToList(), source.ToList());

            var orderBy = new List<OrderByProperty>()
            {
                new OrderByProperty("Person.dateOfBirth", SortingDirections.Descending)
            };

            var builder = new OrderBy<Person>(orderBy);
            var actual = builder.ApplyOn(source);

            Assert.Equal(
                expected.OrderByDescending(p => p.DateOfBirth).ToList(),
                actual.ToList()
            );
        }

        [Fact]
        public void ApplyOn_WithDateTimeProperyNoneOrder_ExpectedOrder()
        {
            var (expected, source) = CreatePersons();
            Assert.Equal(expected.ToList(), source.ToList());

            var orderBy = new List<OrderByProperty>()
            {
                new OrderByProperty("Person.dateOfBirth", SortingDirections.None)
            };

            var builder = new OrderBy<Person>(orderBy);
            var actual = builder.ApplyOn(source);

            Assert.Equal(
                expected.ToList(),
                actual.ToList()
            );
        }

        [Fact]
        public void ApplyOn_WithMultipleOrderByAscending_ExpectedOrder()
        {
            var (expected, source) = CreatePersons(10);
            Assert.Equal(expected.ToList(), source.ToList());

            var orderBy = new List<OrderByProperty>()
            {
                new OrderByProperty("Person.eyeColor", SortingDirections.Ascending),
                new OrderByProperty("Person.age", SortingDirections.Ascending)
            };

            var builder = new OrderBy<Person>(orderBy);
            var actual = builder.ApplyOn(source);

            Assert.Equal(
                expected.OrderBy(p => p.EyeColor).ThenBy(p => p.Age).ToList(),
                actual.ToList()
            );
        }

        [Fact]
        public void ApplyOn_WithMultipleOrderByDescending_ExpectedOrder()
        {
            var (expected, source) = CreatePersons(10);
            Assert.Equal(expected.ToList(), source.ToList());

            var orderBy = new List<OrderByProperty>()
            {
                new OrderByProperty("Person.eyeColor", SortingDirections.Ascending),
                new OrderByProperty("Person.age", SortingDirections.Descending)
            };

            var builder = new OrderBy<Person>(orderBy);
            var actual = builder.ApplyOn(source);

            Assert.Equal(
                expected.OrderBy(p => p.EyeColor).ThenByDescending(p => p.Age).ToList(),
                actual.ToList()
            );
        }

        [Fact]
        public void ApplyOn_WithMultipleOrderByNone_ExpectedOrder()
        {
            var (expected, source) = CreatePersons(10);
            Assert.Equal(expected.ToList(), source.ToList());

            var orderBy = new List<OrderByProperty>()
            {
                new OrderByProperty("Person.eyeColor", SortingDirections.Ascending),
                new OrderByProperty("Person.age", SortingDirections.None)
            };

            var builder = new OrderBy<Person>(orderBy);
            var actual = builder.ApplyOn(source);

            Assert.Equal(
                expected.OrderBy(p => p.EyeColor).ToList(),
                actual.ToList()
            );
        }
    }
}
