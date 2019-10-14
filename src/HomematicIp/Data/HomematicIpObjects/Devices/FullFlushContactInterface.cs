using System.Linq;
using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Channels;

namespace HomematicIp.Data.HomematicIpObjects.Devices
{
    [EnumMap(DeviceType.FULL_FLUSH_CONTACT_INTERFACE)]
    public class FullFlushContactInterface : SabotageDevice
    {
        private ShutterContactChannel ShutterContactChannel =>
            FunctionalChannels?.OfType<ShutterContactChannel>().FirstOrDefault();

        public WindowState? WindowState => ShutterContactChannel?.WindowState;
        public MultiModeInputMode? MultiModeInputMode { get; set; }
        public BinaryBehaviorType? BinaryBehaviorType { get; set; }
    }
}