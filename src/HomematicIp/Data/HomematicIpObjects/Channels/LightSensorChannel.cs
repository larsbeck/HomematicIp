namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.LIGHT_SENSOR_CHANNEL)]
    public class LightSensorChannel : DeviceSabotageChannelBase
    {
        public double? CurrentIllumination { get; set; }
        public double? AverageIllumination { get; set; }
        public double? LowestIllumination { get; set; }
        public double? HighestIllumination { get; set; }
    }
}