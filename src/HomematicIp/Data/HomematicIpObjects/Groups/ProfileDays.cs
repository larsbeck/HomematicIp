using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    public class ProfileDays : HomematicIpObjectBase
    {
        [JsonProperty("THURSDAY")]
        public Day Thursday { get; set; }

        [JsonProperty("TUESDAY")]
        public Day Tuesday { get; set; }

        [JsonProperty("MONDAY")]
        public Day Monday { get; set; }

        [JsonProperty("WEDNESDAY")]
        public Day Wednesday { get; set; }

        [JsonProperty("SUNDAY")]
        public Day Sunday { get; set; }

        [JsonProperty("FRIDAY")]
        public Day Friday { get; set; }

        [JsonProperty("SATURDAY")]
        public Day Saturday { get; set; }
    }
}
