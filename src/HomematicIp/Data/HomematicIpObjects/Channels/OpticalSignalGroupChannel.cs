using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.OPTICAL_SIGNAL_GROUP_CHANNEL)]
    public class OpticalSignalGroupChannel : AbstractDeviceBaseChannel
    {
        public double? DimLevel { get; set; }
        public string ProfileMode { get; set; }
        public string UserDesiredProfileMode { get; set; }
        [JsonProperty(PropertyName = "on")]
        public bool? IsOn { get; set; }
        public string SimpleRGBColorState { get; set; }
        [JsonProperty(PropertyName = "deviceOverloaded")]
        public bool? IsDeviceOverloaded { get; set; }
        [JsonProperty(PropertyName = "coProFaulty")]
        public bool? IsCoProFaulty { get; set; }
        [JsonProperty(PropertyName = "coProRestartNeeded")]
        public bool? IsCoProRestartNeeded { get; set; }
        [JsonProperty(PropertyName = "deviceOverheated")]
        public bool? IsDeviceOverheated { get; set; }
        public string OpticalSignalBehaviour { get; set; }
    }
}
