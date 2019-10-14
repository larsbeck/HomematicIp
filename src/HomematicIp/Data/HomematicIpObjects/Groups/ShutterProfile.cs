using HomematicIp.Data.Enums;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    [EnumMap(GroupType.SHUTTER_PROFILE)]
    public class ShutterProfile : Group
    {
        public double? ShutterLevel { get; set; }
        public double? SlatsLevel { get; set; }

        public double? PrimaryShadingLevel { get; set; }
        public ShadingStateType PrimaryShadingStateType { get; set; }
        public double? SecondaryShadingLevel { get; set; }
        public ShadingStateType SecondaryShadingStateType { get; set; }

        [JsonProperty(PropertyName = "processing")]
        public bool? IsProcessing { get; set; }
        public string ProfileMode { get; set; }
        public string ProfileId { get; set; }
    }
}