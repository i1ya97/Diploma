using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma.Models
{
    public class AttributeDto
    {
        public int Id { get; set; }
        public string Desc { get; set; } = string.Empty;
        public int PrecedentId { get; set; }
        public int ClusterId { get; set; }
        public int AttributeTypeId { get; set; }
    }
}
