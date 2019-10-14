namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.ROTARY_HANDLE_CHANNEL)]
    public class RotaryHandleChannel : Channel
    {
        public string WindowState { get; set; }
        public double? EventDelay { get; set; }
    }
}
