using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiQueryLanguage.GraphV1
{
    public class Edge
    {
        public Edge()
        {
        }

        public Edge(string sourceId, string targetId)
        {
            SourceId = sourceId;
            TargetId = targetId;
        }

        public Edge(string sourceId, string targetId, string kind)
        {
            SourceId = sourceId;
            TargetId = targetId;
            Kind = kind;
        }

        public bool Directed { get; set; }

        public string Id
        {
            get
            {
                return $"{SourceId}_{TargetId}";
            }
        }

        public string SourceId { get; set; }

        public string TargetId { get; set; }

        public string Kind { get; set; }

        public double Weight { get; set; }
    }
}
