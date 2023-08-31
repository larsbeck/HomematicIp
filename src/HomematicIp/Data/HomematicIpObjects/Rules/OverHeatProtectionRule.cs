using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Groups;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Rules
{
    [EnumMap(GroupType.OVER_HEAT_PROTECTION_RULE)]
    public class OverHeatProtectionRule : Group
    {
        public double? ShutterLevel { get; set; }
        public double? SlatsLevel { get; set; }

        public double? PrimaryShadingLevel { get; set; }
        public ShadingStateType? PrimaryShadingStateType { get; set; }
        public double? SecondaryShadingLevel { get; set; }
        public ShadingStateType? SecondaryShadingStateType { get; set; }

        public double? TargetShutterLevel { get; set; }

        [JsonProperty(PropertyName = "processing")]
        public bool? IsProcessing { get; set; }
        [JsonProperty(PropertyName = "triggered")]
        public bool? IsTriggered { get; set; }

        public double? ActualTemperature { get; set; }
        public double? SetPointTemperature { get; set; }
        public double? TemperatureUpperThreshold { get; set; }
        public double? TemperatureLowerThreshold { get; set; }
        [JsonProperty(PropertyName = "startSunrise")]
        public bool? ShouldStartOnSunrise { get; set; }
        [JsonProperty(PropertyName = "endSunset")]
        public bool? ShouldEndOnSunset { get; set; }
        public double? StartHour { get; set; }
        public double? StartMinute { get; set; }
        public double? EndHour { get; set; }
        public double? EndMinute { get; set; }
    }
}
