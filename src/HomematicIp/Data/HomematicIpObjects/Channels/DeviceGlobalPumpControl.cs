using HomematicIp.Data.Enums;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(FunctionalChannelType.DEVICE_GLOBAL_PUMP_CONTROL)]
    public class DeviceGlobalPumpControl : AbstractDeviceBaseChannel
    {
        public double? ValveProtectionDuration { get; set; }
        public double? ValveProtectionSwitchingInterval { get; set; }
        public double? FrostProtectionTemperature { get; set; }
        public double? CoolingEmergencyValue { get; set; }
        public double? HeatingEmergencyValue { get; set; }
        public double? MinimumFloorHeatingValvePosition { get; set; }

        [JsonProperty(PropertyName = "pulseWidthModulationAtLowFloorHeatingValvePositionEnabled")]
        public bool IsPulseWidthModulationAtLowFloorHeatingValvePositionEnabled { get; set; }
        [JsonProperty(PropertyName = "globalPumpControl")]
        public bool IsGlobalPumpControl { get; set; }
        public HeatingValveType HeatingValveType { get; set; }
        public HeatingValveType HeatingLoadType { get; set; }
    }
}
