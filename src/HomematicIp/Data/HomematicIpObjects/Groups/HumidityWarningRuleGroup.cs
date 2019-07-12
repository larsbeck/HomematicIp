using System;
using HomematicIp.Data.Enums;
using HomematicIp.Data.JsonConverters;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    [EnumMap(GroupType.HUMIDITY_WARNING_RULE_GROUP)]
    public class HumidityWarningRuleGroup : Group
    {
        [JsonProperty(PropertyName = "triggered")]
        public bool? IsTriggered { get; set; }
        [JsonProperty(PropertyName = "enabled")]
        public bool? IsEnabled { get; set; }

        public double? HumidityUpperThreshold { get; set; }
        public double? HumidityLowerThreshold { get; set; }

        [JsonProperty(PropertyName = "humidityValidationResult")]
        public virtual HumidityValidationType? HumidityValidationResult { get; set; }

        [JsonProperty(PropertyName = "validationRecommended")]
        public bool? IsValidationRecommended { get; set; }

        public dynamic OutdoorClimateSensor { get; set; }
        [JsonConverter(typeof(TimespanConverter))]
        public TimeSpan? LastExecutionTimestamp { get; set; }
    }
}
