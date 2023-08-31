using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum HeatingFailureValidationResult
    {
        NO_HEATING_FAILURE,
        HEATING_FAILURE_WARNING,
        HEATING_FAILURE_ALARM
    }
}
