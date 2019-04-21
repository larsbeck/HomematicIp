using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects.Home
{
    public class Weather
    {
        public double Temperature { get; set; }
        public WeatherCondition WeatherCondition { get; set; }
        public WeatherDayTime WeatherDayTime { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; }
        public double WindDirection { get; set; }
        public double VaporAmount { get; set; }
    }
}