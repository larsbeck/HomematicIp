using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.WEATHER_SENSOR_PLUS_CHANNEL)]
    public class WeatherSensorPlusChannel : Channel
    {
        public double? ActualTemperature { get; set; }
        public double? Humidity { get; set; }
        public double? VaporAmount { get; set; }
        public double? Illumination { get; set; }
        public double? WindSpeed { get; set; }
        [JsonProperty(PropertyName = "sunshine")]
        public bool? IsSunshine { get; set; }
        [JsonProperty(PropertyName = "storm")]
        public bool? IsStorm { get; set; }
        public double? TotalSunshineDuration { get; set; }
        public double? TodaySunshineDuration { get; set; }
        public double? YesterdaySunshineDuration { get; set; }
        public double? IlluminationThresholdSunshine { get; set; }
        //AVERAGE_VALUE
        public string WindValueType { get; set; }
        [JsonProperty(PropertyName = "raining")]
        public bool? IsRaining { get; set; }
        public double? TotalRainCounter { get; set; }
        public double? TodayRainCounter { get; set; }
        public double? YesterdayRainCounter { get; set; }
    }
}
