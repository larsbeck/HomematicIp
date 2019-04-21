using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SecuritySwitchingGroup
    {
        SIREN,
        PANIC,
        ALARM,
        COMING_HOME,
        BACKUP_ALARM_SIREN
    }
}