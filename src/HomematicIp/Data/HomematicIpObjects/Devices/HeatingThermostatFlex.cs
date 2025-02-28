namespace HomematicIp.Data.HomematicIpObjects.Devices
{
    [EnumMap(Enums.DeviceType.HEATING_THERMOSTAT_FLEX)]
    public class HeatingThermostatFlex : Device
    {
        public bool? AutomaticValveAdaptionNeeded { get; set; }
    }
}