using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma.Models
{
    public class ClusterDto
    {
        public int Id { get; set; }
        public int Num { get; set; }
        public int AttributeTypeId { get; set; }
        public List<TagVector> Tags { get; set; } = new List<TagVector>();
        public double Distance { get; set; }
        public List<int> AttributeIds { get; set; } = new List<int>();
    }
}
