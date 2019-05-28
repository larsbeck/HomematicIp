using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    public abstract class MeasuringChannelBase : DeviceSabotageChannelBase
    {
        public string ProfileMode { get; set; }
        public string UserDesiredProfileMode { get; set; }
        [JsonProperty(PropertyName = "on")]
        public bool? IsOn { get; set; }
    }
}