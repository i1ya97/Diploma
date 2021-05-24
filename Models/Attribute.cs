using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diploma.Models
{
    public class Attribute
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public Precedent Precedent { get; set; }
        public int PrecedentId { get; set; }
        public Cluster Cluster { get; set; }
        public int ClusterId { get; set; }
        public AttributeType AttributeType { get; set; }
        public int AttributeTypeId { get; set; }
    }
}
