using System.Linq;
using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Channels;
using Newtonsoft.Json;

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

        //https://github.com/iobroker-community-adapters/ioBroker.hmip/blob/be548cf299c54bef0cc0fcc94231a949a7e9736c/main.js#L1423
        [JsonProperty(PropertyName = "windowOpen")]
        public bool? IsWindowOpened { get; set; }
    }
}