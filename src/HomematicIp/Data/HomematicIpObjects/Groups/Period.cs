using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    public class Period : HomematicIpObjectBase
    {
        //e.g 17:00
        [JsonProperty("starttime")]
        public string StartTime { get; set; }

        //e.g 22:00
        [JsonProperty("endtime")]
        public string EndTime { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("endtimeAsMinutesOfDay")]
        public int EndTimeAsMinutesOfDay { get; set; }

        [JsonProperty("starttimeAsMinutesOfDay")]
        public int StartTimeAsMinutesOfDay { get; set; }
    }
}
