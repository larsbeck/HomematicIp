namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.WALL_MOUNTED_THERMOSTAT_WITHOUT_DISPLAY_CHANNEL)]
    public class WallMountedThermostatWithoutDisplayChannel : ThermostatChannelBase
    {
        public double? ActualTemperature { get; set; }
        public double? Humidity { get; set; }
        public double? VaporAmount { get; set; }
        public double? SetPointTemperature { get; set; }
    }
}