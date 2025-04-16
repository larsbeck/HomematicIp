using HomematicIp.Data.Enums;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(FunctionalChannelType.UNIVERSAL_LIGHT_CHANNEL)]
    public class UniversalLightChannel : DeviceSabotageChannelBase
    {
        [JsonProperty(PropertyName = "on")]
        public bool? IsOn { get; set; }
        public dynamic ColorTemperature { get; set; }
        public double? Hue { get; set; }
        public double? SaturationLevel { get; set; }
        public double? DimLevel { get; set; }
        public double? HardwareColorTemperatureColdWhite { get; set; }
        public double? HardwareColorTemperatureWarmWhite { get; set; }
        [JsonProperty(PropertyName = "dim2WarmActive")]
        public bool? IsDim2WarmActive { get; set; }
        [JsonProperty(PropertyName = "humanCentricLightActive")]
        public bool? IsHumanCentricLightActive { get; set; }
        public int? LightSceneId { get; set; }
        [JsonProperty(PropertyName = "channelActive")]
        public bool? IsChannelActive { get; set; }
        [JsonProperty(PropertyName = "connectedDeviceUnreach")]
        public bool? IsConnectedDeviceUnreach { get; set; }
        [JsonProperty(PropertyName = "controlGearFailure")]
        public bool? IsControlGearFailure { get; set; }
        [JsonProperty(PropertyName = "lampFailure")]
        public bool? IsLampFailure { get; set; }
        [JsonProperty(PropertyName = "limitFailure")]
        public bool? IsLimitFailure { get; set; }
        public double? OnMinLevel { get; set; }
        public double? MinimalColorTemperature { get; set; }
        public double? MaximumColorTemperature { get; set; }

    }
}
