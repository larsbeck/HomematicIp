using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Devices;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    [EnumMap(GroupType.HUMIDITY_WARNING_RULE_GROUP)]
    public class HumidityWarningRuleGroup : RuleGroup
    {
        public double? HumidityUpperThreshold { get; set; }
        public double? HumidityLowerThreshold { get; set; }

        [JsonProperty(PropertyName = "humidityValidationResult")]
        public virtual HumidityValidationType? HumidityValidationResult { get; set; }

        [JsonProperty(PropertyName = "ventilationRecommended")]
        public bool? IsVentilationecommended { get; set; }

        public OutdoorClimateSensor OutdoorClimateSensor { get; set; }
    }
}
