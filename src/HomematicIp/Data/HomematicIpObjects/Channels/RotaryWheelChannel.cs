namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.ROTARY_WHEEL_CHANNEL)]
    public class RotaryWheelChannel : Channel
    {
        //"rotationDirection": "CLOCK_WISE" we could use enum in the future..
        public string RotationDirection { get; set; }
        public double? EventDelay { get; set; }
    }
}
