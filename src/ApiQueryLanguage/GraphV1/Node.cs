using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiQueryLanguage.GraphV1
{
    public class Node<T>
    {
        public Node()
        {
        }

        public Node(string id, string label)
        {
            Id = id;
            Label = label;
            Data = Activator.CreateInstance<T>();
        }

        public Node(string id)
            : this(id, "node")
        {
            Id = id;
        }

        public Node(string id, string label, T data)
        {
            Id = id;
            Label = label;
            Data = data;
        }

        public string Id { get; set; }

        public string Label { get; set; }

        public T Data { get; set; }
    }
}
