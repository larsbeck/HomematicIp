using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum WeatherCondition
    {
        CLEAR,
        LIGHT_CLOUDY,
        CLOUDY,
        CLOUDY_WITH_RAIN,
        CLOUDY_WITH_SNOW_RAIN,
        HEAVILY_CLOUDY,
        HEAVILY_CLOUDY_WITH_RAIN,
        HEAVILY_CLOUDY_WITH_STRONG_RAIN,
        HEAVILY_CLOUDY_WITH_SNOW,
        HEAVILY_CLOUDY_WITH_SNOW_RAIN,
        HEAVILY_CLOUDY_WITH_THUNDER,
        HEAVILY_CLOUDY_WITH_RAIN_AND_THUNDER,
        FOGGY,
        STRONG_WIND,
        UNKNOWN
    }
}