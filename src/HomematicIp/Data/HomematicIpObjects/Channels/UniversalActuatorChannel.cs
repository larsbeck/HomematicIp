using HomematicIp.Data.Enums;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(FunctionalChannelType.UNIVERSAL_ACTUATOR_CHANNEL)]
    public class UniversalActuatorChannel : DeviceSabotageChannelBase
    {
        [JsonProperty(PropertyName = "on")]
        public bool? IsOn { get; set; }
        [JsonProperty(PropertyName = "ventilationPositionSupported")]
        public bool? IsVentilationPositionSupported { get; set; }
        
        public double? DimLevel { get; set; }
        public string ProfileMode { get; set; }
        public string UserDesiredProfileMode { get; set; }
        public dynamic VentilationState { get; set; }
        public double? VentilationLevel { get; set; }

    }
}
