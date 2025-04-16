using HomematicIp.Data.Enums;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    [EnumMap(GroupType.LINKED_RAIN_PROTECTION)]
    public class LinkedRainProtection : Group
    {
        public double? ShutterLevel { get; set; }
        public double? SlatsLevel { get; set; }

        public double? PrimaryShadingLevel { get; set; }
        public ShadingStateType? PrimaryShadingStateType { get; set; }
        public double? SecondaryShadingLevel { get; set; }
        public ShadingStateType? SecondaryShadingStateType { get; set; }

        [JsonProperty(PropertyName = "processing")]
        public bool? IsProcessing { get; set; }

        public double? TopShutterLevel { get; set; }
        public double? TopSlatsLevel { get; set; }
        public GroupVisibility? GroupVisibility { get; set; }
    }
}
