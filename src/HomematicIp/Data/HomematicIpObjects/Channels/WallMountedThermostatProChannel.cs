using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.WALL_MOUNTED_THERMOSTAT_PRO_CHANNEL)]
    public class WallMountedThermostatProChannel : ThermostatChannelBase
    {
        public double? ActualTemperature { get; set; }
        public double? Humidity { get; set; }
        public double? VaporAmount { get; set; }
        public double? SetPointTemperature { get; set; }
        public ClimateControlDisplay Display { get; set; }
    }
}