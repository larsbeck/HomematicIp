using HomematicIp.Data.Enums;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(FunctionalChannelType.MULTI_MODE_INPUT_DIMMER_CHANNEL)]
    public class MultiModeInputDimmerChannel : Channel
    {
        [JsonProperty(PropertyName = "dimLevel")]
        public double? DimLevel { get; set; }
        [JsonProperty(PropertyName = "on")]
        public bool? IsOn { get; set; }

        public string ProfileMode { get; set; }
        public string UserDesiredProfileMode { get; set; }

        public MultiModeInputMode? MultiModeInputMode { get; set; }
        public BinaryBehaviorType? BinaryBehaviorType { get; set; }
    }
}
