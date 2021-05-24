using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Diploma.Models
{
    public class JsonPrecedent
    {
        [JsonProperty("name")]
        public string Name { get; set; } = "";

        [JsonProperty("symptom")]
        public string Symptom { get; set; } = "";

        [JsonProperty("symptom_cluster_id")]
        public int Symptom_cluster_id { get; set; }


        [JsonProperty("consequence")]
        public string Consequence { get; set; } = "";

        [JsonProperty("consequence_cluster_id")]
        public int Consequence_cluster_id { get; set; }


        [JsonProperty("losses")]
        public string Losses { get; set; } = "";

        [JsonProperty("losses_cluster_id")]
        public int Losses_cluster_id { get; set; }


        [JsonProperty("vulnerabilitiy")]
        public string Vulnerabilitiy { get; set; } = "";

        [JsonProperty("vulnerabilitiy_cluster_id")]
        public int Vulnerabilitiy_cluster_id { get; set; }


        [JsonProperty("countermeasures")]
        public string Countermeasures { get; set; } = "";

        [JsonProperty("countermeasures_cluster_id")]
        public int Countermeasures_cluster_id { get; set; }


    }
}
