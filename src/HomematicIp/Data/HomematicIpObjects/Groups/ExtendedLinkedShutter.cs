using HomematicIp.Data.Enums;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    [EnumMap(GroupType.EXTENDED_LINKED_SHUTTER)]
    public class ExtendedLinkedShutter : Group
    {
        public double? ShutterLevel { get; set; }
        public double? SlatsLevel { get; set; }

        public double? PrimaryShadingLevel { get; set; }
        public ShadingStateType? PrimaryShadingStateType { get; set; }
        public double? SecondaryShadingLevel { get; set; }
        public ShadingStateType? SecondaryShadingStateType { get; set; }

        public double? TopShutterLevel { get; set; }
        public double? BottomShutterLevel { get; set; }
        public double? TopSlatsLevel { get; set; }
        public double? BottomSlatsLevel { get; set; }

        public dynamic SensorSpecificParameters { get; set; }

        [JsonProperty(PropertyName = "processing")]
        public bool? IsProcessing { get; set; }

        public GroupVisibility? GroupVisibility { get; set; }
    }
}
