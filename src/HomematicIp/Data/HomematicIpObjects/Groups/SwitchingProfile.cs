using HomematicIp.Data.Enums;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    [EnumMap(GroupType.SWITCHING_PROFILE)]
    public class SwitchingProfile : Group
    {
        public double? DimLevel { get; set; }
        public string ProfileMode { get; set; }
        public string ProfileId { get; set; }
        [JsonProperty(PropertyName = "on")]
        public bool? IsOn { get; set; }
    }
}