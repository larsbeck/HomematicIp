using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum HeatingLoadType
    {
        LOAD_BALANCING,
        LOAD_COLLECTION
    }
}