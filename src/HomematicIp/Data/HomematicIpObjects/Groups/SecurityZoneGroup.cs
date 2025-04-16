using HomematicIp.Data.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    [EnumMap(GroupType.SECURITY_ZONE)]

    public class SecurityZoneGroup : SecurityGroupBase
    {
        [JsonProperty(PropertyName = "active")]
        public bool IsActive { get; set; }
        [JsonProperty(PropertyName = "silent")]
        public bool IsSilent { get; set; }

        public List<string> IgnorableDevices { get; set; } = new List<string>();
        public string ZoneAssignmentIndex { get; set; }
        public bool? ConfigPending { get; set; }

    }
}