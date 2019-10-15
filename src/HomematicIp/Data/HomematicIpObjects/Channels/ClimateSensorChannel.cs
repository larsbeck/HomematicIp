namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.CLIMATE_SENSOR_CHANNEL)]
    public class ClimateSensorChannel : ThermostatChannelBase
    {
        public double? ActualTemperature { get; set; }
        public double? Humidity { get; set; }
        public double? VaporAmount { get; set; }
    }
}