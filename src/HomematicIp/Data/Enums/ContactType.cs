using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ContactType
    {
        NORMALLY_CLOSE,
        NORMALLY_OPEN,
        WINDOW_DOOR_CONTACT
    }
}