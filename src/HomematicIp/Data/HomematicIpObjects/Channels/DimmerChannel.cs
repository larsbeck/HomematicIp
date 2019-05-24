namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.DIMMER_CHANNEL)]
    public class DimmerChannel : MeasuringChannelBase
    {
        public double? DimLevel { get; set; }
    }
}