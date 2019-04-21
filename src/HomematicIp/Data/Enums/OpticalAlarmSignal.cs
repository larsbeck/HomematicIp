using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OpticalAlarmSignal
    {
        DISABLE_OPTICAL_SIGNAL,
        BLINKING_ALTERNATELY_REPEATING,
        BLINKING_BOTH_REPEATING,
        DOUBLE_FLASHING_REPEATING,
        FLASHING_BOTH_REPEATING,
        CONFIRMATION_SIGNAL_0,
        CONFIRMATION_SIGNAL_1,
        CONFIRMATION_SIGNAL_2
    }
}