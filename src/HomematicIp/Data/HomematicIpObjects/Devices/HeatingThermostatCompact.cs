namespace HomematicIp.Data.HomematicIpObjects.Devices
{
    [EnumMap(Enums.DeviceType.HEATING_THERMOSTAT_COMPACT)]
    public class HeatingThermostatCompact : Device
    {
        public bool? AutomaticValveAdaptionNeeded { get; set; }
    }
}