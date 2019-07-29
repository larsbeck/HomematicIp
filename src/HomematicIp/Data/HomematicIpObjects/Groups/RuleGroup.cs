using System;
using HomematicIp.Data.JsonConverters;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    public class RuleGroup : Group
    {
        [JsonProperty(PropertyName = "triggered")]
        public bool? IsTriggered { get; set; }
        [JsonProperty(PropertyName = "enabled")]
        public bool? IsEnabled { get; set; }
        [JsonConverter(typeof(TimespanConverter))]
        public TimeSpan? LastExecutionTimestamp { get; set; }
    }
}
