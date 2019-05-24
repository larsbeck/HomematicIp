namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.DEVICE_PERMANENT_FULL_RX)]
    public class DevicePermanentFullRx : AbstractDeviceBase
    {
        public bool? PermanentFullRx { get; set; }
    }
}