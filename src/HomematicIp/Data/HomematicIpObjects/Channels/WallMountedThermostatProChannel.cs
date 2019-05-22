using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.WALL_MOUNTED_THERMOSTAT_PRO_CHANNEL)]
    public class WallMountedThermostatProChannel : DeviceSabotageChannelBase
    {
        public double? TemperatureOffset { get; set; }
        public double? ActualTemperature { get; set; }
        public double? Humidity { get; set; }
        public double? VaporAmount { get; set; }
        public ClimateControlDisplay Display { get; set; }
    }
}