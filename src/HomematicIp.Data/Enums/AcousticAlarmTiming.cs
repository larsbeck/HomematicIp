using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AcousticAlarmTiming
    {
        PERMANENT,
        THREE_MINUTES,
        SIX_MINUTES,
        ONCE_PER_MINUTE
    }
}