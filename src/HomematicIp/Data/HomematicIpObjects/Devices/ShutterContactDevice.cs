using System.Linq;
using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Channels;

namespace HomematicIp.Data.HomematicIpObjects.Devices
{
    [EnumMap(DeviceType.SHUTTER_CONTACT, DeviceType.SHUTTER_CONTACT_MAGNETIC)]
    public class ShutterContactDevice : SabotageDevice
    {
        private ShutterContactChannel ShutterContactChannel =>
            FunctionalChannels?.OfType<ShutterContactChannel>().FirstOrDefault();

        public WindowState? WindowState => ShutterContactChannel?.WindowState;
        public int? EventDelay => ShutterContactChannel?.EventDelay;
    }
}