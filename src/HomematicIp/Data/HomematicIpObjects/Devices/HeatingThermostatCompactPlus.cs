namespace HomematicIp.Data.HomematicIpObjects.Devices
{
    [EnumMap(Enums.DeviceType.HEATING_THERMOSTAT_COMPACT_PLUS)]
    public class HeatingThermostatCompactPlus : Device
    {
        public bool? AutomaticValveAdaptionNeeded { get; set; }
        public bool? ManuallyUpdateForced { get; set; }
    }
}