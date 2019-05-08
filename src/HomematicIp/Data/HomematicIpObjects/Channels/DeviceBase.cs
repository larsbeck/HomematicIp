using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.DEVICE_BASE)]
    public class DeviceBase : DeviceSabotageChannelBase
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