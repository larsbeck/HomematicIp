using System.Collections.Generic;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Rules
{
    public class RuleMetaData : HomematicIpObjectBase
    {
        [JsonProperty(PropertyName = "active")]
        public bool IsActive { get; set; }
        public List<string> RuleErrorCategories { get; set; } = new List<string>();
    }
}
