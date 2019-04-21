using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MultiModeInputMode
    {
        KEY_BEHAVIOR,
        SWITCH_BEHAVIOR,
        BINARY_BEHAVIOR
    }
}