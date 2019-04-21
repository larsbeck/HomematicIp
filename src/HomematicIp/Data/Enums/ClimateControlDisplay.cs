using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ClimateControlDisplay
    {
        ACTUAL,
        SETPOINT,
        ACTUAL_HUMIDITY
    }
}