using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Groups;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Rules
{
    [EnumMap(GroupType.SHUTTER_WIND_PROTECTION_RULE)]
    public class ShutterWindProtectionRule : Group
    {
        public double? ShutterLevel { get; set; }
        public double? SlatsLevel { get; set; }

        public double? PrimaryShadingLevel { get; set; }
        public ShadingStateType? PrimaryShadingStateType { get; set; }
        public double? SecondaryShadingLevel { get; set; }
        public ShadingStateType? SecondaryShadingStateType { get; set; }

        public double? TargetShutterLevel { get; set; }
        public double? WindSpeedThreshold { get; set; }

        [JsonProperty(PropertyName = "processing")]
        public bool? IsProcessing { get; set; }
        [JsonProperty(PropertyName = "triggered")]
        public bool? IsTriggered { get; set; }
    }
}