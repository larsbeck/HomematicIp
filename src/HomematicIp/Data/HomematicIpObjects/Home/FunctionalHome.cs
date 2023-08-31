using Newtonsoft.Json;
using System.Collections.Generic;

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