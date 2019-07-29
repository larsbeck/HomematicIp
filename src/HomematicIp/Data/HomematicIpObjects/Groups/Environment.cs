using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    [EnumMap(Enums.GroupType.ENVIRONMENT)]
    public class Environment : Group
    {
        public double? ActualTemperature { get; set; }
        public double? Humidity { get; set; }
        public double? Illumination { get; set; }
        public double? WindSpeed { get; set; }
        [JsonProperty(PropertyName = "raining")]
        public bool IsRaining { get; set; }
    }
}
