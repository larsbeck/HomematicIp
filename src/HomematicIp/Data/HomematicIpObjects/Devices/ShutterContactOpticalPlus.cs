using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Channels;
using System.Linq;

namespace HomematicIp.Data.HomematicIpObjects.Devices
{
    [EnumMap(DeviceType.SHUTTER_CONTACT_OPTICAL_PLUS)]
    public class ShutterContactOpticalPlus : SabotageDevice
    {
        private ShutterContactChannel ShutterContactChannel =>
            FunctionalChannels?.OfType<ShutterContactChannel>().FirstOrDefault();

        public WindowState? WindowState => ShutterContactChannel?.WindowState;
        public int? EventDelay => ShutterContactChannel?.EventDelay;
    }
}