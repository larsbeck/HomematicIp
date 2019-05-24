namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.DEVICE_PERMANENT_FULL_RX)]
    public class DevicePermanentFullRx : AbstractDeviceBaseChannel
    {
        public bool? PermanentFullRx { get; set; }
    }
}