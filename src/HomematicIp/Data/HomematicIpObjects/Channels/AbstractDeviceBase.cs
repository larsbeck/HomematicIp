using HomematicIp.Data.HomematicIpObjects.Devices;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    public abstract class AbstractDeviceBaseChannel : DeviceSabotageChannelBase
    {
        public int? RssiDeviceValue { get; set; }
        public int? RssiPeerValue { get; set; }
        public bool? ConfigPending { get; set; }
        public bool? DutyCycle { get; set; }
        public bool? Sabotage { get; set; }
        [JsonProperty(PropertyName = "lowBat")]
        public bool? HasLowBattery { get; set; }
        [JsonProperty(PropertyName = "unreach")]
        public bool? IsUnreachable { get; set; }
        public bool RouterModuleEnabled { get; set; }
        public bool RouterModuleSupported { get; set; }
    }
}