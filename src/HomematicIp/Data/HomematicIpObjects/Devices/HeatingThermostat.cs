namespace HomematicIp.Data.HomematicIpObjects.Devices
{
    [EnumMap(Enums.DeviceType.HEATING_THERMOSTAT)]
    public class HeatingThermostat:Device
    {
        public bool? AutomaticValveAdaptionNeeded { get; set; }
    }
}