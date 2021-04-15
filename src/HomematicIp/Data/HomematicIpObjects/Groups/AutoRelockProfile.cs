using HomematicIp.Data.Enums;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    [EnumMap(GroupType.AUTO_RELOCK_PROFILE)]
    public class AutoRelockProfile : Group
    {
        public string ProfileId { get; set; }

        [JsonProperty(PropertyName = "active")]
        public bool? IsActive { get; set; }

        public double? AutoRelockDelay { get; set; }
    }
}
