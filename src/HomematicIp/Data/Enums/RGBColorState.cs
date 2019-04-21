using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RGBColorState
    {
        BLACK,
        BLUE,
        GREEN,
        TURQUOISE,
        RED,
        PURPLE,
        YELLOW,
        WHITE
    }
}