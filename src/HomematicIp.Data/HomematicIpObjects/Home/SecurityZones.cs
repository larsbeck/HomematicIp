using HomematicIp.Data.Enums;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Home
{
    public class SecurityZones
    {
        [JsonProperty(nameof(SecurityZone.EXTERNAL))]
        public string ExternalId { get; set; }
        [JsonProperty(nameof(SecurityZone.INTERNAL))]
        public string InternalId { get; set; }
    }
}