using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.WATER_SENSOR_CHANNEL)]
    public class WaterSensorChannel : Channel
    {
        [JsonProperty(PropertyName = "waterlevelDetected")]
        public bool? IsWaterLevelDetected { get; set; }
        [JsonProperty(PropertyName = "moistureDetected")]
        public bool? IsMoistureDetected { get; set; }
        //WATER_MOISTURE_DETECTION
        public string AcousticWaterAlarmTrigger { get; set; }
        //WATER_MOISTURE_DETECTION
        public string SirenWaterAlarmTrigger { get; set; }
        //WATER_MOISTURE_DETECTION
        public string InAppWaterAlarmTrigger { get; set; }
        //FREQUENCY_RISING
        public string AcousticAlarmSignal { get; set; }
        //THREE_MINUTES
        public string AcousticAlarmTiming { get; set; }
    }
}
