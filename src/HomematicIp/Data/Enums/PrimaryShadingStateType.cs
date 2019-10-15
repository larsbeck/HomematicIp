using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ShadingStateType
    {
        POSITION_USED,
        NOT_EXISTENT,
        TILT_USED
    }
}
