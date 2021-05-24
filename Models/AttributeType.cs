using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diploma.Models
{
    public class AttributeType
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }
}
