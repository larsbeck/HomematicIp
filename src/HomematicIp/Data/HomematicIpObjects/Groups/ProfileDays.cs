using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    public class ProfileDays : HomematicIpObjectBase
    {
        [JsonProperty("THURSDAY")]
        public Day Thursday { get; set; } = new Day();

        [JsonProperty("TUESDAY")]
        public Day Tuesday { get; set; } = new Day();

        [JsonProperty("MONDAY")]
        public Day Monday { get; set; } = new Day();

        [JsonProperty("WEDNESDAY")]
        public Day Wednesday { get; set; } = new Day();

        [JsonProperty("SUNDAY")]
        public Day Sunday { get; set; } = new Day();

        [JsonProperty("FRIDAY")]
        public Day Friday { get; set; } = new Day();

        [JsonProperty("SATURDAY")]
        public Day Saturday { get; set; } = new Day();
    }
}
