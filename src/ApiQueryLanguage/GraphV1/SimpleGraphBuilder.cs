using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiQueryLanguage.GraphV1
{
    public class SimpleGraphBuilder<T>
    {
        private readonly Graph<T> _graph = new();

        public SimpleGraphBuilder<T> Load(string section)
        {
            string latestNodeId = string.Empty;
            string latestNodeIdBeforeEdge = string.Empty;
            bool edgeIsDirected = false;
            bool edgeIsReverted = false;

            foreach (var segment in GetSegments(section))
            {
                var edge = IsEdge(segment);

                if (edge.IsEdge)
                {
                    edgeIsDirected = edge.Directed;
                    edgeIsReverted = edge.Reverted;
                    latestNodeIdBeforeEdge = latestNodeId;
                    continue;
                }

                latestNodeId = AddNode(segment);

                if (!string.IsNullOrEmpty(latestNodeIdBeforeEdge))
                {
                    if (edgeIsReverted)
                    {
                        AddEdge(latestNodeId, latestNodeIdBeforeEdge, edgeIsDirected);
                        continue;
                    }

                    AddEdge(latestNodeIdBeforeEdge, latestNodeId, edgeIsDirected);
                }
            }

            return this;
        }

        public Graph<T> Get()
        {
            return _graph;
        }

        public static string GetNodeId(string segment)
        {
            if (segment.Contains('('))
            {
                return segment[..segment.IndexOf('(')];
            }

            return segment;
        }

        public static (bool IsEdge, bool Directed, bool Reverted) IsEdge(string segment)
        {
            return (
                segment.Equals("--") || segment.Equals("->") || segment.Equals("<-"),
                segment.Contains('<') || segment.Contains('>'),
                segment.Contains('<')
            );
        }


        public static IEnumerable<string> GetSegments(string section)
        {
            if (string.IsNullOrEmpty(section))
            {
                yield break;
            }

            foreach (string part in section.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                foreach (string segment in part.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    yield return segment;
                }
            }
        }

        private string AddNode(string segment)
        {
            string nodeId = GetNodeId(segment);

            if (!_graph.Exists(nodeId))
            {
                _graph.Add(nodeId);
            }

            return nodeId;
        }

        private void AddEdge(string fromNodeId, string toNodeId, bool directed)
        {
            _graph.AddEdge(fromNodeId, toNodeId, directed);
        }
    }
}
