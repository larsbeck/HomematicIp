using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum HomeUpdateState
    {
        UP_TO_DATE,
        UPDATE_AVAILABLE,
        PERFORM_UPDATE_SENT,
        PERFORMING_UPDATE
    }
}