using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AbsenceType
    {
        /// <summary>
        /// Automatic mode
        /// </summary>
        NOT_ABSENT,
        /// <summary>
        /// Eco mode with a duration based on date time or <see cref="EcoDuration" />
        /// </summary>
        PERIOD,
        /// <summary>
        /// Eco mode without a duration which lasts infinite
        /// </summary>
        PERMANENT,
        /// <summary>
        /// Vacation mode with a date time for the end
        /// </summary>
        VACATION,
        /// <summary>
        /// Same as <see cref="VACATION" />
        /// </summary>
        PARTY
    }
}