using System.Collections.Generic;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Home
{
    public abstract class FunctionalHome : IHaveARawJsonProperty
    {
        public List<string> FunctionalGroups { get; set; } = new List<string>();
        public bool Active { get; set; }
        [JsonProperty(PropertyName = "solution")]
        public string Name { get; set; }
        public string RawJson { get; set; }
    }
}