using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma.Models
{
    public class PrecedentDto
    {
        public int Id { get; set; }
        public List<DescType> FactDescs { get; set; } = new List<DescType>();
        public List<DescType>? ForecastDescs { get; set; } = new List<DescType>();
    }

    public class DescType 
    {
        public int TypeId { get; set; } 
        public string Type { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty;
    }
}
