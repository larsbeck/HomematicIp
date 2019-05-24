using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Devices;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(FunctionalChannelType.SHUTTER_CONTACT_CHANNEL)]
    public class ShutterContactChannel : DeviceSabotageChannelBase
    {
        public WindowState? WindowState { get; set; }
        public int EventDelay { get; set; }
    }
}
