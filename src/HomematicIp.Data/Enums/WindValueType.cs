using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum WindValueType
    {
        CURRENT_VALUE,
        MIN_VALUE,
        MAX_VALUE,
        AVERAGE_VALUE
    }
}