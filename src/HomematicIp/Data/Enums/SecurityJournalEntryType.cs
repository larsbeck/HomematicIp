using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SecurityJournalEntryType
    {
        SENSOR_EVENT,
        SABOTAGE
    }
}
