using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DoorState
    {
        OPEN,
        CLOSED,
        TILTED,
        VENTILATION_POSITION,
        POSITION_UNKNOWN
    }
}
