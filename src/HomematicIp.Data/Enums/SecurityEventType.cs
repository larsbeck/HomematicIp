using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SecurityEventType
    {
        SENSOR_EVENT,
        ACCESS_POINT_DISCONNECTED,
        ACCESS_POINT_CONNECTED,
        ACTIVATION_CHANGED,
        SILENCE_CHANGED,
        SABOTAGE,
        MOISTURE_DETECTION_EVENT,
        SMOKE_ALARM,
        EXTERNAL_TRIGGERED,
        OFFLINE_ALARM,
        WATER_DETECTION_EVENT,
        MAINS_FAILURE_EVENT,
        OFFLINE_WATER_DETECTION_EVENT
    }
}