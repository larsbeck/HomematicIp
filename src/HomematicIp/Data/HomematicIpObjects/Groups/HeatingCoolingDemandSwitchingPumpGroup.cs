using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    [EnumMap(GroupType.HEATING_COOLING_DEMAND_PUMP)]

    public class HeatingCoolingDemandSwitchingPumpGroup : HeatingChangeOverGroup
    {
        public double PumpLeadTime { get; set; }
        public double PumpFollowUpTime { get; set; }
        public double PumpProtectionDuration { get; set; }
        public double PumpProtectionSwitchingInterval { get; set; }
        public dynamic HeatDemand { get; set; }
    }
}