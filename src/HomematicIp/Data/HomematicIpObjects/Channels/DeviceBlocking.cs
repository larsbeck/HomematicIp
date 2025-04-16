using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.DEVICE_BLOCKING)]
    public class DeviceBlocking : AbstractDeviceBaseChannel
    {
        [JsonProperty(PropertyName = "blockedSabotage")]
        public bool? IsBlockedSabotage { get; set; }
        [JsonProperty(PropertyName = "blockedWrongCodePermanently")]
        public bool? IsBlockedWrongCodePermanently { get; set; }
        [JsonProperty(PropertyName = "blockedWrongCodeTemporarily")]
        public bool? IsBlockedWrongCodeTemporarily { get; set; }
        public int? BlockingPermanentAttempts { get; set; }
        public int? BlockingTemporaryAttempts { get; set; }
    }
}