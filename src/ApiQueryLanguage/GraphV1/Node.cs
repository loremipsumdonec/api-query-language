namespace ApiQueryLanguage.GraphV1
{
    public class Node<T>
    {

        public Node(string id)
            : this(id, "node")
        {
        }

        public Node(string id, string label)
        {
            Id = id;
            Label = label;
            Data = Activator.CreateInstance<T>();
        }

        public Node(string id, string? label, T? data)
        {
            Id = id;
            Label = label;
            Data = data;
        }

        public string Id { get; }

        public string? Label { get; set; }

        public T? Data { get; set; }
    }
}
