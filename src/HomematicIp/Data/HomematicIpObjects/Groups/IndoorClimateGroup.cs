using HomematicIp.Data.Enums;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    [EnumMap(GroupType.INDOOR_CLIMATE)]
    public class IndoorClimateGroup : SecurityGroup
    {
        [JsonProperty(PropertyName = "processing")]
        public bool? IsProcessing { get; set; }
        public dynamic VentilationState { get; set; }
        public dynamic VentilationLevel { get; set; }
    }
}
