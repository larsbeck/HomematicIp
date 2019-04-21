using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BinaryBehaviorType
    {
        NORMALLY_CLOSE,
        NORMALLY_OPEN
    }
}