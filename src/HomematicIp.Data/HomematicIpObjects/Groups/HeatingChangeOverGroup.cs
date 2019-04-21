using HomematicIp.Data.Enums;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    [EnumMap(GroupType.HEATING_CHANGEOVER)]
    public class HeatingChangeOverGroup : Group
    {
        [JsonProperty(PropertyName = "on")]
        public bool? IsOn { get; set; }
    }
}