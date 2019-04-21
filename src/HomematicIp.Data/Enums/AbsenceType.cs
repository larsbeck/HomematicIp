using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AbsenceType
    {
        NOT_ABSENT,
        PERIOD,
        PERMANENT,
        VACATION,
        PARTY
    }
}