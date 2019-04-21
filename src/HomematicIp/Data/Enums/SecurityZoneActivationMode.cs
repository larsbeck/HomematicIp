using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SecurityZoneActivationMode
    {
        ACTIVATION_WITH_DEVICE_IGNORELIST,
        ACTIVATION_IF_ALL_IN_VALID_STATE
    }
}