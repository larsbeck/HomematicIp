namespace HomematicIp.Data.HomematicIpObjects.Devices
{
    [EnumMap(Enums.DeviceType.HEATING_THERMOSTAT, Enums.DeviceType.HEATING_THERMOSTAT_COMPACT)]
    public class HeatingThermostat:Device
    {
        public bool? AutomaticValveAdaptionNeeded { get; set; }
    }
}