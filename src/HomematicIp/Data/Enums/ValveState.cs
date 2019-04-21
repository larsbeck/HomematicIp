using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ValveState
    {
        STATE_NOT_AVAILABLE,
        RUN_TO_START,
        WAIT_FOR_ADAPTION,
        ADAPTION_IN_PROGRESS,
        ADAPTION_DONE,
        TOO_TIGHT,
        ADJUSTMENT_TOO_BIG,
        ADJUSTMENT_TOO_SMALL,
        ERROR_POSITION
    }
}