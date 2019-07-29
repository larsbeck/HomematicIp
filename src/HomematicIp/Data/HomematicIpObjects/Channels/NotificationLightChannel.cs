using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.NOTIFICATION_LIGHT_CHANNEL)]
    public class NotificationLightChannel : AbstractDeviceBaseChannel
    {
        public double? DimLevel { get; set; }
        public string ProfileMode { get; set; }
        public string UserDesiredProfileMode { get; set; }
        [JsonProperty(PropertyName = "on")]
        public bool? IsOn { get; set; }
        public string SimpleRGBColorState { get; set; }
    }
}
