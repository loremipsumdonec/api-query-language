namespace ApiQueryLanguage.GraphV1
{
    public class Edge
    {
        public Edge(string sourceId, string targetId)
        {
            SourceId = sourceId;
            TargetId = targetId;
        }

        public bool Directed { get; set; }

        public string Id => $"{SourceId}_{TargetId}";

        public string SourceId { get; }

        public string TargetId { get; }

        public string Kind { get; set; } = "default";

        public double Weight { get; set; }
    }
}
