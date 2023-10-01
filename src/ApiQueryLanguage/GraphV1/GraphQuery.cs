using System.Collections;

namespace ApiQueryLanguage.GraphV1
{
    public class GraphQuery<T>
        : IEnumerable<Node<T>>
    {
        private readonly Graph<T> _graph;
        private readonly IEnumerable<Node<T>> _nodes;
        private readonly int _hops = 0;
        private readonly bool _respectDirection = false;

        public GraphQuery(Graph<T> graph)
        {
            _graph = graph;
            _nodes = graph.Nodes.Where(_ => true);
        }

        public GraphQuery(IEnumerable<Node<T>> nodes, Graph<T> graph)
        {
            _graph = graph;
            _nodes = nodes;
        }

        public GraphQuery(IEnumerable<Node<T>> nodes, Graph<T> graph, int hops, bool respectDirection)
        {
            _graph = graph;
            _nodes = nodes;
            _hops = hops;
            _respectDirection = respectDirection;
        }

        public GraphQuery<T> Where(Func<Node<T>, bool> selector)
        {
            return new GraphQuery<T>(
                _nodes.Where(selector),
                _graph,
                _hops,
                _respectDirection
            );
        }

        public GraphQuery<T> RespectDirection()
        {
            return new GraphQuery<T>(
                _nodes,
                _graph,
                _hops,
                true
            );
        }

        public GraphQuery<T> Hops(int hops)
        {
            if (hops == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(hops), "Hops must be greater than zero");
            }

            return new GraphQuery<T>(
                _nodes,
                _graph,
                hops,
                _respectDirection
            );
        }

        public GraphPathQuery<T> PathTo(Func<Node<T>, bool> filter)
        {
            var q = new GraphPathQuery<T>(this, _graph, _hops, _respectDirection);
            return q.PathTo(filter);
        }

        public IEnumerator<Node<T>> GetEnumerator()
        {
            return _nodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _nodes.GetEnumerator();
        }
    }
}
