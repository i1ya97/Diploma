using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diploma.Data;
using Diploma.Models;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Diploma.Controllers
{
    [ApiController]
    [Route("api/main")]
    public class MainController : ControllerBase
    {
        public MainController() { }

        [HttpPost("get-clusters")]
        public async Task<List<ClusterDto>> GetClusters([FromBody] Filter filter)
        {
            var result = await Clustering(filter);
            return result;
        }

        [HttpPost("get-attributes")]
        public async Task<List<AttributeDto>> GetAttributes([FromBody]int[] ids)
        {
            var attributes = new List<Diploma.Models.Attribute>();
            using (ApplicationContext db = new ApplicationContext())
            {
                attributes = db.Attributes.Where(x => ids.Contains(x.Id)).ToList();
            }

            var attributesDtos = new List<AttributeDto>();

            attributes.ForEach(x =>
            {
                var dto = new AttributeDto
                {
                    Id = x.Id,
                    Desc = x.Desc,
                    PrecedentId = x.PrecedentId,
                    AttributeTypeId = x.AttributeTypeId,
                    ClusterId = x.ClusterId                  
                };
                attributesDtos.Add(dto);
            });

            return attributesDtos;
        }

        [HttpGet("get-precedent/{precedentId}")]
        public async Task<PrecedentDto> GetPrecedent(int precedentId)
        {
            var attributes = new List<Diploma.Models.Attribute>();
            using (ApplicationContext db = new ApplicationContext())
            {
                attributes = db.Attributes.Where(x => x.PrecedentId == precedentId).ToList();
            }

            var precedentDto = new PrecedentDto
            {
                Id = precedentId,
                FactDescs = new List<DescType>()
            };

            attributes.ForEach(x =>
            {
                var factDesc = new DescType
                {
                    TypeId = x.AttributeTypeId,
                    Desc = x.Desc
                };
                precedentDto.FactDescs.Add(factDesc);
            });

            precedentDto.FactDescs.ForEach(x =>
            {
                string type = "";
                using (ApplicationContext db = new ApplicationContext()) 
                {
                    type = db.AttributeTypes.Find(x.TypeId).Type;
                };
                x.Type = type;
            });

            return precedentDto;
        }

        private double Distance(string[] left, string[] right) 
        {
            double count = 0;
            foreach (var l in left) 
            {
                if (right.Contains(l)) 
                {
                    count++;
                }               
            }

            double distance = 1.0 - ((1.0 / (double)left.Length) * count);
            return distance;
        }

        private async Task<List<ClusterDto>> Clustering(Filter filter) 
        {
            List<Cluster> clusters;
            using (ApplicationContext db = new ApplicationContext()) 
            {
                clusters = db.Clusters.Where(x => x.AttributeTypeId == filter.AttributeType).Where(x => !string.IsNullOrEmpty(x.Tags)).ToList();
            }

            List<ClusterDto> clusterDtos = new List<ClusterDto>();

            clusters.ForEach(x =>
            {
                var dto = new ClusterDto
                {
                    Id = x.Id,
                    Num = x.Num,
                    AttributeTypeId = x.AttributeTypeId,
                    Tags = JsonConvert.DeserializeObject<List<TagVector>>(x.Tags)
                };

                clusterDtos.Add(dto);
            });

            clusterDtos.ForEach(c =>
            {
                c.Distance = Distance(filter.Tags, c.Tags.Select(x => x.Tag).ToArray());
            });

            clusterDtos = clusterDtos.Where(x => x.Distance < 0.75).ToList();

            var clusterIds = clusterDtos.Select(x => x.Id);

            var attributes = new List<Diploma.Models.Attribute>();
            using (ApplicationContext db = new ApplicationContext()) 
            {
                attributes = db.Attributes.Where(x => clusterIds.Contains(x.ClusterId)).ToList();
            };

            var groupAttributes = attributes.GroupBy(x => x.ClusterId);

            foreach (var group in groupAttributes) 
            {
                clusterDtos.FirstOrDefault(x => x.Id == group.Key).AttributeIds = group.Select(x => x.Id).ToList();
            }

            clusterDtos = clusterDtos.OrderBy(x => x.Distance).ToList();

            return clusterDtos;
        }


        private List<TagVector> Vectorization(string[] tags) 
        {
            var jsonTags = JsonConvert.SerializeObject(tags);
            System.IO.File.WriteAllText("tags.json", jsonTags);

            var psi = new ProcessStartInfo();

            psi.FileName = @"C:\Python38\python.exe";
            var script = @"C:\Users\ASUS\source\repos\Diploma\vectorization.py";

            psi.Arguments = $"\"{script}\"";

            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            var errors = "";
            var result = "";

            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                result = process.StandardOutput.ReadToEnd();
            }


            var vectors = JsonConvert.DeserializeObject<double[][]>(result);

            var tagVectors = new List<TagVector>();
            for (var i = 0; i < vectors.Length; i++)
            {
                var tagVector = new TagVector
                {
                    Tag = tags[i],
                    Vector = vectors[i]
                };
                tagVectors.Add(tagVector);
            }
            System.IO.File.Delete("tags.json");

            return tagVectors;
        }
    }



    public class Filter
    {
        public int AttributeType { get; set; }
        public string[] Tags { get; set; }
    }
}
