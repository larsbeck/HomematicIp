using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.CARBON_DIOXIDE_SENSOR_CHANNEL)]
    public class CarbonDioxideSensorChannel : DeviceSabotageChannelBase
    {
        public double? ActualTemperature { get; set; }
        public double? Humidity { get; set; }
        public double? VaporAmount { get; set; }
        [JsonProperty(PropertyName = "carbonDioxideVisualisationEnabled")]
        public bool? IsCarbonDioxideVisualisationEnabled { get; set; }
        public double? CarbonDioxideConcentration { get; set; }
    }
}