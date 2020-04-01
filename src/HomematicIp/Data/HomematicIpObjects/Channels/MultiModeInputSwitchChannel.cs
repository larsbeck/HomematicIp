using HomematicIp.Data.Enums;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(FunctionalChannelType.MULTI_MODE_INPUT_SWITCH_CHANNEL)]
    public class MultiModeInputSwitchChannel : Channel
    {
        [JsonProperty(PropertyName = "on")]
        public bool? IsOn { get; set; }
        public string ProfileMode { get; set; }
        public string UserDesiredProfileMode { get; set; }
        public MultiModeInputMode? MultiModeInputMode { get; set; }
        public BinaryBehaviorType? BinaryBehaviorType { get; set; }
    }
}
