using HomematicIp.Data.Enums;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(FunctionalChannelType.IMPULSE_OUTPUT_CHANNEL)]
    public class ImpulseOutputChannel : Channel
    {
        [JsonProperty(PropertyName = "processing")]
        public bool? IsProcessing { get; set; }
        public double? ImpulseDuration { get; set; }
    }
}
