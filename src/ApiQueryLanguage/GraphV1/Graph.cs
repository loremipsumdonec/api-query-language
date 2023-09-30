using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiQueryLanguage.GraphV1
{
    public class Graph<T>
    {
        public Graph()
        {
            Nodes = new List<Node<T>>();
            Edges = new List<Edge>();
        }

        public Graph(Graph<T> graph)
            : this()
        {
            foreach (var node in graph.Nodes)
            {
                Add(node.Id, node.Label, node.Data);
            }

            foreach (var edge in graph.Edges)
            {
                AddEdge(
                    new Edge(edge.SourceId, edge.TargetId)
                    {
                        Directed = edge.Directed,
                        Kind = edge.Kind,
                        Weight = edge.Weight
                    }
                );
            }
        }

        public List<Node<T>> Nodes { get; }

        public List<Edge> Edges { get; }

        public void Add(string id, string label = null, T data = default)
        {
            Add(new Node<T>(id, label, data));
        }

        public void Add(Node<T> node)
        {
            var exists = Nodes.FirstOrDefault(n => n.Id == node.Id);

            if (exists != null)
            {
                Nodes.Remove(exists);
            }

            Nodes.Add(node);
        }

        public void AddEdge(string sourceId, string targetId, bool createNodesIfMissing = true)
        {
            if (createNodesIfMissing && !Exists(sourceId))
            {
                Add(new Node<T>(sourceId));
            }

            if (createNodesIfMissing && !Exists(targetId))
            {
                Add(new Node<T>(targetId));
            }

            AddEdge(
                new Edge(sourceId, targetId) { Directed = false }
            );
        }

        public void AddEdge(Edge edge)
        {
            Edges.Add(edge);
        }

        public bool Exists(string nodeId)
        {
            return Nodes.Find(n => n.Id == nodeId) != null;
        }
    }
}
