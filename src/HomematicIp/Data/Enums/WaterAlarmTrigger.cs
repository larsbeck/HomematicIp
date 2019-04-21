using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum WaterAlarmTrigger
    {
        NO_ALARM,
        MOISTURE_DETECTION,
        WATER_DETECTION,
        WATER_MOISTURE_DETECTION
    }
}