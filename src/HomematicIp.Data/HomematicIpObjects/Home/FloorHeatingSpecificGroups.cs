using HomematicIp.Data.Enums;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Home
{
    public class FloorHeatingSpecificGroups
    {
        [JsonProperty(nameof(GroupType.HEATING_COOLING_DEMAND_PUMP))]
        public string CoolingDemandPump { get; set; }
        [JsonProperty(nameof(GroupType.HEATING_TEMPERATURE_LIMITER))]
        public string TemperatureLimiter { get; set; }
        [JsonProperty(nameof(GroupType.HEATING_EXTERNAL_CLOCK))]
        public string ExternalClock { get; set; }
        [JsonProperty(nameof(GroupType.HEATING_DEHUMIDIFIER))]
        public string Dehumidifier { get; set; }
        [JsonProperty(nameof(GroupType.HEATING_CHANGEOVER))]
        public string Changeover { get; set; }
        [JsonProperty(nameof(GroupType.HEATING_COOLING_DEMAND_BOILER))]
        public string CoolingDemandBoiler { get; set; }
        [JsonProperty(nameof(GroupType.HEATING_HUMIDITY_LIMITER))]
        public string HumidityLimiter { get; set; }
    }
}