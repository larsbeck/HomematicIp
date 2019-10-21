using HomematicIp.Data.Enums;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(FunctionalChannelType.DOOR_CHANNEL)]
    public class DoorChannel : DeviceSabotageChannelBase
    {
        public DoorState? DoorState { get; set; }
        [JsonProperty(PropertyName = "processing")]
        public bool? IsProcessing { get; set; }
        [JsonProperty(PropertyName = "on")]
        public bool? IsOn { get; set; }
        [JsonProperty(PropertyName = "ventilationPositionSupported")]
        public bool? IsVentilationPositionSupported { get; set; }

    }
}
