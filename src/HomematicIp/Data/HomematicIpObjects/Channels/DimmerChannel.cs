using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.DIMMER_CHANNEL)]
    public class DimmerChannel : DeviceSabotageChannelBase
    {
        public string ProfileMode { get; set; }
        public string UserDesiredProfileMode { get; set; }
        public double? DimLevel { get; set; }
        [JsonProperty(PropertyName = "on")]
        public bool? IsOn { get; set; }
    }
}