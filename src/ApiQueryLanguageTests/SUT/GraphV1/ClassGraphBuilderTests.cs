using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiQueryLanguage.GraphV1;

namespace ApiQueryLanguageTests.SUT.GraphV1
{
    public class ClassGraphBuilderTests
    {
        [Fact]
        public void GetIdentityPropertyId_WhenUsingStandardNaming_ReturnExpectedProperty()
        {
            var property = ClassGraphBuilder.GetIdentityPropertyId(typeof(CompanyWithStandardNaming));
            Assert.Equal(nameof(CompanyWithStandardNaming.CompanyWithStandardNamingId), property.Name);
        }

        [Fact]
        public void GetIdentityPropertyId_WhenUsingKeyAttribute_ReturnExpectedProperty()
        {
            var property = ClassGraphBuilder.GetIdentityPropertyId(typeof(CompanyWithKeyAttribute));
            Assert.Equal(nameof(CompanyWithKeyAttribute.NotStandardNamingForId), property.Name);
        }

        [Fact]
        public void GetIdentityPropertyId_WhenMissingAIdentityProperty_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(
                () => ClassGraphBuilder.GetIdentityPropertyId(typeof(CompanyMissingAValidIdentityProperty))
            );
        }

        [Fact]
        public void GetForeignIdentityProperty_WhenUsingStandardNaming_ReturnExpectedProperty()
        {
            var through = typeof(Company).GetProperty(nameof(Company.Orders));

            var property = ClassGraphBuilder.GetForeignPropertyId(
                forType: typeof(Company),
                onType: typeof(OrderUsingStandardNaming),
                through
            );

            Assert.Equal(typeof(OrderUsingStandardNaming), property.DeclaringType);
            Assert.Equal(nameof(OrderUsingStandardNaming.CompanyId), property.Name);
        }

        [Fact]
        public void GetForeignIdentityProperty_WhenUsingForeignKeyAttribute_ReturnExpectedProperty()
        {
            var through = typeof(CompanyWhenUsingForeignKey).GetProperty(nameof(CompanyWhenUsingForeignKey.Orders));

            var property = ClassGraphBuilder.GetForeignPropertyId(
                forType: typeof(CompanyWhenUsingForeignKey),
                onType: typeof(OrderUsingForeignKey),
                through
            );

            Assert.Equal(typeof(OrderUsingForeignKey), property.DeclaringType);
            Assert.Equal(nameof(OrderUsingForeignKey.NotUsingAStandardNamingForForeignKey), property.Name);
        }

        [Fact]
        public void GetForeignIdentityProperty_WhenUsingForeignKeyAttributeWithInvalidName_ThrowsException()
        {
            var through = typeof(CompanyWhenUsingInvalidForeignKey).GetProperty(nameof(CompanyWhenUsingInvalidForeignKey.Orders));

            Assert.Throws<ArgumentNullException>(() =>
                ClassGraphBuilder.GetForeignPropertyId(
                    forType: typeof(CompanyWhenUsingInvalidForeignKey),
                    onType: typeof(OrderUsingForeignKey),
                    through)
            );
        }

        [Fact]
        public void GetForeignIdentityProperty_WhenRelationsHasForeignKeyProperty_ReturnsExpectedProperty()
        {
            var through = typeof(CompanyWhenForeignKeyOnRelation).GetProperty(nameof(CompanyWhenForeignKeyOnRelation.Relations));

            var property = ClassGraphBuilder.GetForeignPropertyId(
                forType: typeof(CompanyRelation),
                onType: typeof(CompanyWhenForeignKeyOnRelation),
                through
            );

            Assert.Equal(typeof(CompanyRelation), property.DeclaringType);
            Assert.Equal(nameof(CompanyRelation.CompanyWhenForeignKeyOnRelationId), property.Name);
        }

        [Theory]
        [InlineData(typeof(CompanyWithStandardNaming))]
        [InlineData(typeof(Company))]
        public void Build_WithSimpleClass_EdgesIsNotDirected(Type type)
        {
            var graph = new ClassGraphBuilder(type).Build();

            Assert.Empty(graph.Edges.Where(e => e.Directed));
        }

        [Fact]
        public void Build_WithClassThatHasAManyRelationship_AddedClassInRelationship()
        {
            var graph = new ClassGraphBuilder(typeof(Company)).Build();

            Assert.NotNull(graph.Edges.Find(n => n.TargetId == "OrderUsingStandardNaming" && n.SourceId == "Company.orders"));
            Assert.NotEmpty(graph.Nodes.Where(n => n.Id.StartsWith(nameof(OrderUsingStandardNaming))));
        }

        [Fact]
        public void Build_WithClassThatHasAManyRelationship_IEnumerablePropertyAddedInGraph()
        {
            var graph = new ClassGraphBuilder(typeof(Company)).Build();

            var property = typeof(Company).GetProperty(nameof(Company.Orders));
            string nodeId = ClassGraphBuilder.CreateNodeId(property);

            Assert.NotNull(graph.Nodes.Find(n => n.Id == nodeId));
        }

        [Fact]
        public void Build_WithClassHasOneRelationship_AddedClassInRelationship()
        {
            var graph = new ClassGraphBuilder(typeof(OrderUsingStandardNaming)).Build();

            Assert.NotEmpty(graph.Nodes.Where(n => n.Id.StartsWith(nameof(Company))));
            Assert.NotNull(graph.Edges.Find(n => n.TargetId == "Company" && n.SourceId == "OrderUsingStandardNaming.company"));
        }

        [Fact]
        public void Build_WithClassHasOneRelationship_PropertyWithClassAddedInGraph()
        {
            var graph = new ClassGraphBuilder(typeof(OrderUsingStandardNaming)).Build();

            var property = typeof(OrderUsingStandardNaming).GetProperty(nameof(OrderUsingStandardNaming.Company));
            string nodeId = ClassGraphBuilder.CreateNodeId(property);

            Assert.NotNull(graph.Nodes.Find(n => n.Id == nodeId));
        }

        [Fact]
        public void Build_WithASimpleClass_HasExpectedNodes()
        {
            var graph = new ClassGraphBuilder(typeof(SimpleClass)).Build();

            Assert.Equal(4, graph.Nodes.Count);
            Assert.NotNull(graph.Nodes.Find(n => n.Id == "SimpleClass"));
            Assert.NotNull(graph.Nodes.Find(n => n.Id == "SimpleClass.simpleClassId"));
            Assert.NotNull(graph.Nodes.Find(n => n.Id == "SimpleClass.name"));
            Assert.NotNull(graph.Nodes.Find(n => n.Id == "SimpleClass.age"));
        }

        [Fact]
        public void Build_WithASimpleClass_NodeHasClassType()
        {
            var graph = new ClassGraphBuilder(typeof(SimpleClass)).Build();

            Assert.NotNull(graph.Nodes.Find(n => n.Id == "SimpleClass" && n.Data!.Equals(typeof(SimpleClass))));
        }

        [Fact]
        public void Build_WithASimpleClass_HasExpectedEdges()
        {
            var graph = new ClassGraphBuilder(typeof(SimpleClass)).Build();

            Assert.Equal(3, graph.Edges.Count);
            Assert.NotNull(graph.Edges.Find(e => e.SourceId == "SimpleClass.simpleClassId" && e.TargetId == "SimpleClass"));
            Assert.NotNull(graph.Edges.Find(e => e.SourceId == "SimpleClass.name" && e.TargetId == "SimpleClass"));
            Assert.NotNull(graph.Edges.Find(e => e.SourceId == "SimpleClass.age" && e.TargetId == "SimpleClass"));
        }

        [Fact]
        public void Build_WithASimpleClass_NodeHasPropertyInfo()
        {
            var propertyInfo = typeof(SimpleClass).GetProperty("Name");
            var graph = new ClassGraphBuilder(typeof(SimpleClass)).Build();

            Assert.NotNull(graph.Nodes.Find(n => n.Id == "SimpleClass.name" && n.Data!.Equals(propertyInfo)));
        }

        [Fact]
        public void CreateNodeId_WithType_ReturnExpected()
        {
            string expected = ClassGraphBuilder.CreateNodeId(typeof(Customer));
            Assert.Equal("Customer", expected);
        }

        [Fact]
        public void CreateNodeId_WithPropertyInfo_ReturnExpected()
        {
            var propertyInfo = typeof(Customer).GetProperty("Name");

            string expected = ClassGraphBuilder.CreateNodeId(propertyInfo);
            Assert.Equal("Customer.name", expected);
        }
    }

    public class CompanyWithStandardNaming
    {
        public long CompanyWithStandardNamingId { get; set; }

        public string Name { get; set; } = string.Empty;
    }

    public class CompanyWithKeyAttribute
    {
        [Key]
        public long NotStandardNamingForId { get; set; }

        public string Name { get; set; } = string.Empty;
    }

    public class CompanyMissingAValidIdentityProperty
    {
        public string Name { get; set; } = string.Empty;
    }

    public class Company
    {
        public int CompanyId { get; set; }

        public IEnumerable<OrderUsingStandardNaming>? Orders { get; set; }
    }

    public class OrderUsingStandardNaming
    {
        public int OrderUsingStandardNamingId { get; set; }

        public int CompanyId { get; set; }

        public Company? Company { get; set; }
    }

    public class CompanyWhenUsingForeignKey
    {
        [ForeignKey(nameof(OrderUsingForeignKey.NotUsingAStandardNamingForForeignKey))]
        public IEnumerable<OrderUsingForeignKey>? Orders { get; set; }
    }

    public class OrderUsingForeignKey
    {
        public long NotUsingAStandardNamingForForeignKey { get; set; }
    }

    public class CompanyWhenUsingInvalidForeignKey
    {
        [ForeignKey("ThisDoesNotExistsOnOrderUsingForeignKey")]
        public IEnumerable<OrderUsingForeignKey>? Orders { get; set; }
    }

    public class CompanyWhenForeignKeyOnRelation
    {
        public int CompanyId { get; set; }

        public IEnumerable<CompanyRelation>? Relations { get; set; }
    }

    public class CompanyRelation
    {
        public int CompanyRelationId { get; set; }

        public int CompanyWhenForeignKeyOnRelationId { get; set; }
    }

    public class SimpleClass
    {
        public int SimpleClassId { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }
    }

    public class Customer
    {
        public int CustomerId { get; set; }

        public string Name { get; set; } = string.Empty; 
    }
}
