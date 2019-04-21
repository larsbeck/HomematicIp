using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MotionDetectionSendInterval
    {
        SECONDS_30,
        SECONDS_60,
        SECONDS_120,
        SECONDS_240,
        SECONDS_480
    }
}