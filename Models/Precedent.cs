using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diploma.Models
{
    public class Precedent
    {
        public int Id { get; set; }
        public List<Attribute> Attributes { get; set; } = new List<Attribute>();

    }
}
