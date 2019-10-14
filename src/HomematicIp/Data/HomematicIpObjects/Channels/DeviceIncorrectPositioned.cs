using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.DEVICE_INCORRECT_POSITIONED)]
    public class DeviceIncorrectPositioned : AbstractDeviceBaseChannel
    {
        [JsonProperty(PropertyName = "incorrectPositioned")]
        public bool IsIncorrectPositioned { get; set; }
    }
}
