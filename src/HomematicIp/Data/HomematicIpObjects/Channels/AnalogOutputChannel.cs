namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.ANALOG_OUTPUT_CHANNEL)]
    public class AnalogOutputChannel : DeviceSabotageChannelBase
    {
        public double? AnalogOutputLevel { get; set; }
    }
}
