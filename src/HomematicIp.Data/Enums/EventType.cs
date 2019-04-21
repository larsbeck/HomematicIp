using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EventType
    {
        SECURITY_JOURNAL_CHANGED,
        GROUP_ADDED,
        GROUP_REMOVED,
        DEVICE_REMOVED,
        DEVICE_CHANGED,
        DEVICE_ADDED,
        CLIENT_REMOVED,
        CLIENT_CHANGED,
        CLIENT_ADDED,
        HOME_CHANGED,
        GROUP_CHANGED
    }
}