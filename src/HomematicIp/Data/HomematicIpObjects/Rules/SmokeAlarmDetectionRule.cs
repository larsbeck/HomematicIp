using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Groups;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Rules
{
    [EnumMap(GroupType.SMOKE_ALARM_DETECTION_RULE)]
    public class SmokeAlarmDetectionRule : Group
    {
        public double? ShutterLevel { get; set; }
        public double? SlatsLevel { get; set; }

        public double? PrimaryShadingLevel { get; set; }
        public ShadingStateType PrimaryShadingStateType { get; set; }
        public double? SecondaryShadingLevel { get; set; }
        public ShadingStateType SecondaryShadingStateType { get; set; }

        [JsonProperty(PropertyName = "processing")]
        public bool IsProcessing { get; set; }

        public SmokeDetectorAlarmType? SmokeDetectorAlarmType { get; set; }
        public string ProfileMode { get; set; }
    }
}