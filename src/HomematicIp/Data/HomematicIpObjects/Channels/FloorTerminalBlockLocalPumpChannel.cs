namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.FLOOR_TERMINAL_BLOCK_LOCAL_PUMP_CHANNEL)]
    public class FloorTerminalBlockLocalPumpChannel : DeviceSabotageChannelBase
    {
        public double? PumpLeadTime { get; set; }
        public double? PumpFollowUpTime { get; set; }
        public double? PumpProtectionDuration { get; set; }
        public double? PumpProtectionSwitchingInterval { get; set; }

    }
}