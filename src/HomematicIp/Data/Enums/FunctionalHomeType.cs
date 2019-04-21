using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FunctionalHomeType
    {
        INDOOR_CLIMATE,
        LIGHT_AND_SHADOW,
        SECURITY_AND_ALARM,
        WEATHER_AND_ENVIRONMENT
    }
}