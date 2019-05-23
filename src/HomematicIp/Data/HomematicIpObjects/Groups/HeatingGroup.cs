using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    [EnumMap(GroupType.HEATING)]
    public class HeatingGroup : Group
    {
        public double? WindowOpenTemperature { get; set; }
        public double? SetPointTemperature { get; set; }
        public double? MinTemperature { get; set; }
        public double? MaxTemperature { get; set; }
        public WindowState? WindowState { get; set; }
        public dynamic Cooling { get; set; }
        public bool PartMode { get; set; }
        public ClimateControlMode ControlMode { get; set; }
        public dynamic Profiles { get; set; }
        public string ActiveProfile { get; set; }
        public bool BoostMode { get; set; }
        public double? BoostDuration { get; set; }
        public double? ActualTemperature { get; set; }
        public double? Humidity { get; set; }
        public bool CoolingAllowed { get; set; }
        public bool CoolingIgnored { get; set; }
        public bool EcoAllowed { get; set; }
        public bool EcoIgnored { get; set; }
        public bool Controllable { get; set; }
        public string FloorHeatingMode { get; set; }
        public bool HumidityLimitEnabled { get; set; }
        public double? HumidityLimitValue { get; set; }
        public bool ExternalClockEnabled { get; set; }
        public double? ExternalClockHeatingTemperature { get; set; }
        public double? ExternalClockCoolingTemperature { get; set; }
        public string ValvePosition { get; set; }
        public bool? Sabotage { get; set; }
        public bool? ValveSilentModeSupported { get; set; }
        public bool? ValveSilentModeEnabled { get; set; }
        public string LastSetPointReachedTimestamp { get; set; }
        public string LastSetPointUpdatedTimestamp { get; set; }
        public bool? HeatingFailureSupported { get; set; }

    }
}