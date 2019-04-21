using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AcousticAlarmSignal
    {
        DISABLE_ACOUSTIC_SIGNAL,
        FREQUENCY_RISING,
        FREQUENCY_FALLING,
        FREQUENCY_RISING_AND_FALLING,
        FREQUENCY_ALTERNATING_LOW_HIGH,
        FREQUENCY_ALTERNATING_LOW_MID_HIGH,
        FREQUENCY_HIGHON_OFF,
        FREQUENCY_HIGHON_LONGOFF,
        FREQUENCY_LOWON_OFF_HIGHON_OFF,
        FREQUENCY_LOWON_LONGOFF_HIGHON_LONGOFF,
        LOW_BATTERY,
        DISARMED,
        INTERNALLY_ARMED,
        EXTERNALLY_ARMED,
        DELAYED_INTERNALLY_ARMED,
        DELAYED_EXTERNALLY_ARMED,
        EVENT,
        ERROR
    }
}