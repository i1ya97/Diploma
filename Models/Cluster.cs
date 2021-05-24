using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diploma.Models
{
    public class Cluster
    {
        public int Id { get; set; }
        public int Num { get; set; }
        public AttributeType AttributeType { get; set; }
        public int AttributeTypeId { get; set; }
        public string Tags { get; set; }
        public List<Attribute> Attributes { get; set; }
    }
}
