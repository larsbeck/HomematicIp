using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    [EnumMap(GroupType.HEATING_COOLING_DEMAND_BOILER)]
    public class HeatingCoolingDemandBoilerGroup : HeatingChangeOverGroup
    {
        public double? BoilerLeadTime { get; set; }
        public double? BoilerFollowUpTime { get; set; }
        public dynamic HeatDemand { get; set; }
    }
}