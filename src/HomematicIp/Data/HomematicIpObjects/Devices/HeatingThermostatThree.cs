namespace HomematicIp.Data.HomematicIpObjects.Devices
{
    [EnumMap(Enums.DeviceType.HEATING_THERMOSTAT_THREE)]
    public class HeatingThermostatThree : Device
    {
        public bool? AutomaticValveAdaptionNeeded { get; set; }
     
        // "measuredAttributes": {
        //     "1": {
        //         "setPointTemperature": true
        //     }
        // },
    }
}