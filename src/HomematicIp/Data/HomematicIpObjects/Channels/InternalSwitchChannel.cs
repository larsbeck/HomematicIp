namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.INTERNAL_SWITCH_CHANNEL)]
    public class InternalSwitchChannel : DeviceSabotageChannelBase
    {
        public bool InternalSwitchOutputEnabled { get; set; }
        public string HeatingValveType { get; set; }
        public double? ValveProtectionDuration { get; set; }
        public double? ValveProtectionSwitchingInterval { get; set; }
        public double? FrostProtectionTemperature { get; set; }
    }
}
