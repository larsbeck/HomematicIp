namespace HomematicIp.Data.HomematicIpObjects.Devices
{
    [EnumMap(Enums.DeviceType.HEATING_THERMOSTAT_EVO)]
    public class HeatingThermostatEvo : Device
    {
        public bool? AutomaticValveAdaptionNeeded { get; set; }
    }
}