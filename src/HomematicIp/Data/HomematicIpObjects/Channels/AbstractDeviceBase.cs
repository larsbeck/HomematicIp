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
        public bool? DeviceOverloaded { get; set; }
        public bool? CoProUpdateFailure { get; set; }
        public bool? CoProFaulty { get; set; }
        public bool? CoProRestartNeeded { get; set; }
        public bool? DeviceUndervoltage { get; set; }
        public bool? DeviceOverheated { get; set; }
        public bool? TemperatureOutOfRange { get; set; }

        public dynamic SupportedOptionalFeatures { get; set; }
    }
}