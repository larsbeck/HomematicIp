using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Groups;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Rules
{
    [EnumMap(GroupType.NO_HEAT_DEMAND_RULE)]
    public class NoHeatDemandRule : Group
    {
        [JsonProperty(PropertyName = "triggered")]
        public bool? IsTriggered { get; set; }
        public double? OnTime { get; set; }
        public double? ValvePositionUpperThreshold { get; set; }
        public double? ValvePositionLowerThreshold { get; set; }
        public double? EmergencyProfileOnTime { get; set; }
        public double? EmergencyProfileOffTime { get; set; }
        [JsonProperty(PropertyName = "emergencyProfileSupported")]
        public bool? IsEmergencyProfileSupported { get; set; }
        public double? CentralChannelIndex { get; set; }

        [JsonProperty(PropertyName = "noHeatDemand")]
        public bool? IsNoHeatDemand { get; set; }

    }
}
