using System.Collections;

namespace ApiQueryLanguage.GraphV1
{
    public class GraphPathQuery<T>
        : IEnumerable<IEnumerable<Edge>>
    {
        private readonly int _hops;
        private readonly bool _respectDirection;
        private readonly Graph<T> _graph;
        private readonly IEnumerable<Node<T>> _nodes;
        private readonly List<List<Edge>>? _paths;

        internal GraphPathQuery(Graph<T> graph)
        {
            _graph = graph;
            _nodes = graph.Nodes;
        }

        internal GraphPathQuery(IEnumerable<Node<T>> nodes, Graph<T> graph, int hops = 0, bool respectDirection = false)
        {
            _graph = graph;
            _nodes = nodes;
            _hops = hops;
            _respectDirection = respectDirection;
        }

        internal GraphPathQuery(IEnumerable<Node<T>> nodes, List<List<Edge>> paths, Graph<T> graph)
        {
            _graph = graph;
            _nodes = nodes;
            _paths = paths;
        }

        internal GraphPathQuery<T> PathTo(Func<Node<T>, bool> filter)
        {
            var paths = new List<List<Edge>>();

            if (_paths == null)
            {
                foreach (var root in _nodes)
                {
                    TraverseTo(root, filter, new List<Edge>(), new List<string>(), paths, 1);
                }
            }
            else
            {
                foreach (var path in _paths)
                {
                    var edge = path[path.Count - 1];
                    var root = _graph.Nodes.First(n => n.Id == edge.TargetId);
                    TraverseTo(root, filter, path, new List<string>(), paths, 1);
                }
            }

            return new GraphPathQuery<T>(_nodes, paths, _graph);
        }

        public IEnumerator<IEnumerable<Edge>> GetEnumerator() => _paths.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private bool TraverseTo(
            Node<T> node,
            Func<Node<T>, bool> filter,
            List<Edge> path,
            List<string> visited,
            List<List<Edge>> paths,
            int currentHops
        )
        {
            if (filter.Invoke(node))
            {
                return true;
            }

            if (_hops > 0 && _hops < currentHops)
            {
                return false;
            }

            visited.Add(node.Id);

            foreach (var edge in _graph.Edges.Where(e =>
                _respectDirection ? e.SourceId == node.Id : e.SourceId == node.Id || e.TargetId == node.Id))
            {
                string targetId = edge.SourceId == node.Id ? edge.TargetId : edge.SourceId;

                if (!visited.Contains(targetId))
                {
                    path.Add(edge);

                    if (TraverseTo(_graph.Nodes.First(n => n.Id == targetId), filter, path, visited, paths, currentHops + 1))
                    {
                        paths.Add(new List<Edge>(path));
                    }

                    path.Remove(edge);
                }
            }

            visited.Remove(node.Id);
            return false;
        }
    }
}
