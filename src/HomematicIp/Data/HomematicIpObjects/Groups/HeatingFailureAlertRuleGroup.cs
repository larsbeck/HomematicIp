using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    [EnumMap(GroupType.HEATING_FAILURE_ALERT_RULE_GROUP)]
    public class HeatingFailureAlertRuleGroup : RuleGroup
    {
        public double? CheckInterval { get; set; }
        public double? ValidationTimeout { get; set; }
        public HeatingFailureValidationResult? HeatingFailureValidationResult { get; set; }
    }
}
