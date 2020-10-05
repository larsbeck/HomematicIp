using HomematicIp.Data.Enums;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(FunctionalChannelType.RAIN_DETECTION_CHANNEL)]
    public class RainDetectionChannel : DeviceSabotageChannelBase
    {
        [JsonProperty(PropertyName = "raining")]
        public bool IsRaining { get; set; }
        public double? RainSensorSensitivity { get; set; }
    }
}