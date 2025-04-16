using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Channels;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    public class Group : HomematicIpObjectBase
    {
        [JsonProperty(PropertyName = "lowBat")]
        public bool? HasLowBattery { get; set; }
        public string MetaGroupId { get; set; }
        [JsonProperty(PropertyName = "type")]
        public virtual GroupType GroupType { get; set; }
        [JsonProperty(PropertyName = "unreach")]
        public bool? IsUnreachable { get; set; }
        [JsonProperty(PropertyName = "dutyCycle")]
        public bool? IsDutyCycle { get; set; }
        public List<Channel> Channels { get; set; } = new List<Channel>();
    }
}