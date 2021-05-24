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
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }



        private static void AddTypesToDb()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (!db.AttributeTypes.Any())
                {
                    AttributeType symptom = new AttributeType { Type = "Symptoms" };
                    AttributeType consequence = new AttributeType { Type = "Consequences" };
                    AttributeType losses = new AttributeType { Type = "Losses" };
                    AttributeType vulnerabilitiy = new AttributeType { Type = "Vulnerabilities" };
                    AttributeType countermeasures = new AttributeType { Type = "Countermeasures" };
                    db.AttributeTypes.Add(symptom);
                    db.AttributeTypes.Add(consequence);
                    db.AttributeTypes.Add(vulnerabilitiy);
                    db.AttributeTypes.Add(countermeasures);
                    db.AttributeTypes.Add(losses);
                    db.SaveChanges();
                }
            }
        }

        private static List<JsonPrecedent> ParseJson()
        {
            List<JsonPrecedent> precedents;
            using (StreamReader file = System.IO.File.OpenText(@"C:\Users\ASUS\Desktop\диплом\result.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                precedents = (List<JsonPrecedent>)serializer.Deserialize(file, typeof(IEnumerable<JsonPrecedent>));
            }
            return precedents;
        }

        private static void AddData(List<JsonPrecedent> jsonPrecedents)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                for (int i = 1; i <= jsonPrecedents.ToArray().Length; i++)
                {
                    var prec = jsonPrecedents[i - 1];
                    string desc = prec.Symptom + prec.Consequence + prec.Vulnerabilitiy + prec.Countermeasures + prec.Losses;

                    if (db.Precedents.FirstOrDefault(x => x.Id == i) == null)
                    {
                        db.Precedents.Add(new Precedent());
                        db.SaveChanges();
                    }


                    if (!String.IsNullOrEmpty(prec.Symptom))
                    {
                        var cluster = db.Clusters.FirstOrDefault(x
                            => x.Num == prec.Symptom_cluster_id
                            && x.AttributeTypeId == 1);

                        if (cluster == null)
                        {
                            db.Clusters.Add(new Cluster { AttributeTypeId = 1, Num = prec.Symptom_cluster_id });
                            db.SaveChanges();
                            cluster = db.Clusters.OrderBy(x => x.Id).Last();
                        };
                        if (db.Attributes.FirstOrDefault(x =>
                            x.Desc == prec.Symptom
                            && x.PrecedentId == i
                            && x.ClusterId == cluster.Id
                            && x.AttributeTypeId == cluster.AttributeTypeId) == null)
                        {
                            db.Attributes.Add(new Diploma.Models.Attribute
                            {
                                Desc = prec.Symptom.Replace("\n\t\t\t\t\t\t\t", " "),
                                PrecedentId = i,
                                ClusterId = cluster.Id,
                                AttributeTypeId = cluster.AttributeTypeId
                            });
                            db.SaveChanges();
                        }

                    }

                    if (!String.IsNullOrEmpty(prec.Consequence))
                    {
                        var cluster = db.Clusters.FirstOrDefault(x
                            => x.Num == prec.Consequence_cluster_id
                            && x.AttributeTypeId == 2);

                        if (cluster == null)
                        {
                            db.Clusters.Add(new Cluster { AttributeTypeId = 2, Num = prec.Consequence_cluster_id });
                            db.SaveChanges();
                            cluster = db.Clusters.OrderBy(x => x.Id).Last();
                        }
                        if (db.Attributes.FirstOrDefault(x
                            => x.Desc == prec.Consequence
                            && x.PrecedentId == i
                            && x.ClusterId == cluster.Id
                            && x.AttributeTypeId == cluster.AttributeTypeId) == null)
                        {
                            db.Attributes.Add(new Diploma.Models.Attribute
                            {
                                Desc = prec.Consequence.Replace("\n\t\t\t\t\t\t\t", " "),
                                PrecedentId = i,
                                ClusterId = cluster.Id,
                                AttributeTypeId = cluster.AttributeTypeId
                            });
                            db.SaveChanges();
                        }

                    }

                    if (!String.IsNullOrEmpty(prec.Vulnerabilitiy))
                    {
                        var cluster = db.Clusters.FirstOrDefault(x
                            => x.Num == prec.Vulnerabilitiy_cluster_id
                            && x.AttributeTypeId == 3);

                        if (cluster == null)
                        {
                            db.Clusters.Add(new Cluster { AttributeTypeId = 3, Num = prec.Vulnerabilitiy_cluster_id });
                            db.SaveChanges();
                            cluster = db.Clusters.OrderBy(x => x.Id).Last();
                        }
                        if (db.Attributes.FirstOrDefault(x
                            => x.Desc == prec.Vulnerabilitiy
                            && x.PrecedentId == i
                            && x.ClusterId == cluster.Id
                            && x.AttributeTypeId == cluster.AttributeTypeId) == null)
                        {
                            db.Attributes.Add(new Diploma.Models.Attribute
                            {
                                Desc = prec.Vulnerabilitiy.Replace("\n\t\t\t\t\t\t\t", " "),
                                PrecedentId = i,
                                ClusterId = cluster.Id,
                                AttributeTypeId = cluster.AttributeTypeId
                            });
                            db.SaveChanges();
                        }

                    }

                    if (!String.IsNullOrEmpty(prec.Countermeasures))
                    {
                        var cluster = db.Clusters.FirstOrDefault(x
                            => x.Num == prec.Countermeasures_cluster_id
                            && x.AttributeTypeId == 4);

                        if (cluster == null)
                        {
                            db.Clusters.Add(new Cluster { AttributeTypeId = 4, Num = prec.Countermeasures_cluster_id });
                            db.SaveChanges();
                            cluster = db.Clusters.OrderBy(x => x.Id).Last();
                        }
                        if (db.Attributes.FirstOrDefault(x
                            => x.Desc == prec.Countermeasures
                            && x.PrecedentId == i
                            && x.ClusterId == cluster.Id
                            && x.AttributeTypeId == cluster.AttributeTypeId) == null)
                        {
                            db.Attributes.Add(new Diploma.Models.Attribute
                            {
                                Desc = prec.Countermeasures.Replace("\n\t\t\t\t\t\t\t", " "),
                                PrecedentId = i,
                                ClusterId = cluster.Id,
                                AttributeTypeId = cluster.AttributeTypeId
                            });
                            db.SaveChanges();
                        }

                    }

                    if (!String.IsNullOrEmpty(prec.Losses))
                    {
                        var cluster = db.Clusters.FirstOrDefault(x
                            => x.Num == prec.Losses_cluster_id
                            && x.AttributeTypeId == 5);

                        if (cluster == null)
                        {
                            db.Clusters.Add(new Cluster { AttributeTypeId = 5, Num = prec.Losses_cluster_id });
                            db.SaveChanges();
                            cluster = db.Clusters.OrderBy(x => x.Id).Last();
                        }
                        if (db.Attributes.FirstOrDefault(x
                            => x.Desc == prec.Losses
                            && x.PrecedentId == i
                            && x.ClusterId == cluster.Id
                            && x.AttributeTypeId == cluster.AttributeTypeId) == null)
                        {
                            db.Attributes.Add(new Diploma.Models.Attribute
                            {
                                Desc = prec.Losses.Replace("\n\t\t\t\t\t\t\t", " "),
                                PrecedentId = i,
                                ClusterId = cluster.Id,
                                AttributeTypeId = cluster.AttributeTypeId
                            });
                            db.SaveChanges();
                        }

                    }


                }
            }
        }

        private static void AddTags()
        {

            List<IGrouping<int, Diploma.Models.Attribute>> clusters;
            using (ApplicationContext db = new ApplicationContext())
            {
                clusters = db.Attributes.ToList().GroupBy(x => x.ClusterId).ToList();
            }

            foreach (var clust in clusters)
            {
                var attributes = clust.Select(x => x.Desc).ToList();
                var document = JsonConvert.SerializeObject(attributes);
                System.IO.File.WriteAllText("cluster.json", document);

                var psi = new ProcessStartInfo();
                psi.FileName = @"C:\Python38\python.exe";
                var script = @"C:\Users\ASUS\source\repos\JsonParser\LDA.py";

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

                using (ApplicationContext db = new ApplicationContext())
                {
                    var value = db.Clusters.FirstOrDefault(x => x.Id == clust.Key);
                    value.Tags = result;
                    db.Clusters.Update(value);
                    db.SaveChanges();
                }
                System.IO.File.Delete("cluster.json");
            }
        }

        private static void MakeDictionary() 
        {
            var dictionary = new List<string>();
            using (ApplicationContext db = new ApplicationContext())
            {
                dictionary = db.Attributes.Select(x => x.Desc).ToList();
            }

            var document = JsonConvert.SerializeObject(dictionary);
            System.IO.File.WriteAllText("dictionary.json", document);

            var psi = new ProcessStartInfo();
            psi.FileName = @"C:\Python38\python.exe";
            var script = @"C:\Users\ASUS\source\repos\Diploma\dictionary.py";

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
        }

        private static void Vectors() 
        {
            var clusters = new List<Cluster>();
            using (ApplicationContext db = new ApplicationContext())
            {
                clusters = db.Clusters.Where(x => !string.IsNullOrEmpty(x.Tags)).ToList();
            }

            foreach (var cluster in clusters)
            {
                //var document = JsonConvert.SerializeObject(tag);
                System.IO.File.WriteAllText("tags.json", cluster.Tags);

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

                var keyWords = JsonConvert.DeserializeObject<string[]>(cluster.Tags);
                var vectors = JsonConvert.DeserializeObject<double[][]>(result);

                var tagVectors = new List<TagVector>();
                for (var i = 0; i < vectors.Length; i++) 
                {
                    var tagVector = new TagVector
                    {
                        Tag = keyWords[i],
                        Vector = vectors[i]
                    };
                    tagVectors.Add(tagVector);
                }
                
                using (ApplicationContext db = new ApplicationContext()) 
                {
                    var clust = db.Clusters.FirstOrDefault(x => x.Id == cluster.Id);
                    clust.Tags = JsonConvert.SerializeObject(tagVectors);
                    db.Clusters.Update(clust);
                    db.SaveChanges();
                }
                
                System.IO.File.Delete("tags.json");
            }

        }
    }
}
