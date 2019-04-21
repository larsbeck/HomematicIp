using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DeviceUpdateStrategy
    {
        MANUALLY,
        AUTOMATICALLY_IF_POSSIBLE
    }
}