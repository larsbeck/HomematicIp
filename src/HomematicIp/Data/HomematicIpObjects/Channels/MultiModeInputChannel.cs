using System.Linq;
using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Channels;

namespace HomematicIp.Data.HomematicIpObjects.Devices
{
    [EnumMap(FunctionalChannelType.MULTI_MODE_INPUT_CHANNEL)]
    public class MultiModeInputChannel : SabotageDevice
    {
        private ShutterContactChannel ShutterContactChannel =>
            FunctionalChannels?.OfType<ShutterContactChannel>().FirstOrDefault();

        public WindowState? WindowState => ShutterContactChannel?.WindowState;
        public MultiModeInputMode? MultiModeInputMode { get; set; }
        public BinaryBehaviorType? BinaryBehaviorType { get; set; }
    }
}