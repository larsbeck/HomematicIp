namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.DEVICE_BASE_FLOOR_HEATING)]
    public class DeviceBaseFloorHeating : AbstractDeviceBaseChannel
    {
        public double? ValveProtectionDuration { get; set; }
        public double? ValveProtectionSwitchingInterval { get; set; }
        public double? FrostProtectionTemperature { get; set; }
        public double? CoolingEmergencyValue { get; set; }
        public double? HeatingEmergencyValue { get; set; }
        public double? MinimumFloorHeatingValvePosition { get; set; }
        public bool? PulseWidthModulationAtLowFloorHeatingValvePositionEnabled { get; set; }
    }
}