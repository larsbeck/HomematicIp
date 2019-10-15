using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(FunctionalChannelType.CONTACT_INTERFACE_CHANNEL)]
    public class ContactInterfaceChannel : DeviceSabotageChannelBase
    {
        public WindowState? WindowState { get; set; }
        public int EventDelay { get; set; }
    }
}
