using ApiQueryLanguage.GraphV1;

namespace ApiQueryLanguageTests.SUT.GraphV1
{
    [Trait("type", "unit")]
    public class GraphPathQueryTests
    {
        [Fact]
        public void GivenUndirectedGraph_WhenTwoEdges_FindPath()
        {
            var graph = new SimpleGraphBuilder<object>()
                .Load("a -- b -- c")
                .Get();

            var paths = graph.Where(n => n.Id == "a")
                .PathTo(n => n.Id == "c");

            Assert.Single(paths);
            Assert.Equal(2, paths.First().Count());
        }

        [Fact]
        public void GivenDirectedGraph_WhenRespectDirection_DoesNotFindPath()
        {
            var graph = new SimpleGraphBuilder<object>()
                .Load("a -> b <- c")
                .Get();

            var paths = graph.Where(n => n.Id == "a")
                .RespectDirection()
                .PathTo(n => n.Id == "c");

            Assert.Empty(paths);
        }

        [Fact]
        public void GivenDirectedGraph_WhenRespectDirection_FindPath()
        {
            var graph = new SimpleGraphBuilder<object>()
                .Load("a -> b -> c")
                .Get();

            var paths = graph.Where(n => n.Id == "a")
                .RespectDirection()
                .PathTo(n => n.Id == "c");

            Assert.Single(paths);
            Assert.Equal(2, paths.First().Count());
        }

        [Fact]
        public void GivenDirectedGraph_WhenDoesNotRespectDirection_FindPath()
        {
            var graph = new SimpleGraphBuilder<object>()
                .Load("a -> b <- c")
                .Get();

            var paths = graph.Where(n => n.Id == "a")
                .PathTo(n => n.Id == "c");

            Assert.Single(paths);
            Assert.Equal(2, paths.First().Count());
        }

        [Fact]
        [Trait("importance", "required")]
        public void GivenDirectedGraph_WhenMultiplePathsRespectDirection_FindCorrectPath()
        {
            var graph = new SimpleGraphBuilder<object>()
                .Load("a -> b -> c")
                .Load("a -> d <- c")
                .Get();

            var paths = graph.Where(n => n.Id == "a")
                .RespectDirection()
                .PathTo(n => n.Id == "c");

            Assert.Single(paths);
            Assert.Equal(2, paths.First().Count());
        }

        [Fact]
        public void GivenDirectedGraph_WhenMultiplePathsDoesNotRespectDirection_FindAllPaths()
        {
            var graph = new SimpleGraphBuilder<object>()
                .Load("a -> b -> c")
                .Load("a -> d <- c")
                .Get();

            var paths = graph.Where(n => n.Id == "a")
                .PathTo(n => n.Id == "c");

            Assert.Equal(2, paths.Count());
        }

        [Fact]
        public void GivenUnDirectedGraph_WhenMultiplePathsDoesNotRespectDirection_FindAllPaths()
        {
            var graph = new SimpleGraphBuilder<object>()
                .Load("a -- b -- c")
                .Load("a -- d -- c")
                .Get();

            var paths = graph.Where(n => n.Id == "a")
                .PathTo(n => n.Id == "c");

            Assert.Equal(2, paths.Count());
        }
    }
}
