using ApiQueryLanguage.GraphV1;

namespace ApiQueryLanguageTests.SUT.GraphV1
{
    public class SimpleGraphBuilderTests
    {
        [Theory]
        [InlineData("Lorem", "Lorem")]
        [InlineData("Lorem(donec)", "Lorem")]
        public void GetNodeId_WhenValidSegment_ReturnExpectedId(string segment, string expected)
        {
            string nodeId = SimpleGraphBuilder<object>.GetNodeId(segment);

            Assert.Equal(expected, nodeId);
        }

        [Theory]
        [InlineData("--", true)]
        [InlineData("->", true)]
        [InlineData("a", false)]
        public void IsEdge_WhenValidSegment_ReturnsExpected(string segment, bool expected)
        {
            var (actual, _, _) = SimpleGraphBuilder<object>.IsEdge(segment);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetSegments_WithValidSection_ReturnExpected()
        {
            List<string> expected = new()
            {
                "a",
                "--",
                "b",
                "--",
                "c",
                "--",
                "d"
            };

            var segments = SimpleGraphBuilder<object>.GetSegments(string.Join(" ", expected));
            Assert.Equal(expected, segments);
        }

        [Fact]
        public void GetSegments_WithRepeat_ReturnExpected()
        {
            var segments = SimpleGraphBuilder<object>.GetSegments("a -- b,c,d,e");
            Assert.Equal(new string[] { "a", "--", "b", "c", "d", "e" }, segments);
        }

        [Fact]
        public void Load_WithMultipleNodes_GraphHasExpectedNodes()
        {
            var graph = new SimpleGraphBuilder<object>()
                .Load("a, b, c, d")
                .Get();

            Assert.NotNull(graph.Nodes.Find(n => n.Id == "a"));
            Assert.NotNull(graph.Nodes.Find(n => n.Id == "b"));
            Assert.NotNull(graph.Nodes.Find(n => n.Id == "c"));
            Assert.NotNull(graph.Nodes.Find(n => n.Id == "d"));
        }

        [Fact]
        public void Load_WithAndEdge_EdgeBetweenTwoNodes()
        {
            var graph = new SimpleGraphBuilder<object>()
                .Load("a -- b")
                .Get();

            Assert.NotNull(graph.Nodes.Find(n => n.Id == "a"));
            Assert.NotNull(graph.Nodes.Find(n => n.Id == "b"));
        }
    }
}
