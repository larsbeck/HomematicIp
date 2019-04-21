using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SmokeDetectorAlarmType
    {
        IDLE_OFF,
        PRIMARY_ALARM,
        INTRUSION_ALARM,
        SECONDARY_ALARM
    }
}