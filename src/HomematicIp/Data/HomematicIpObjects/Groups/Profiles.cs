using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    public class Profiles : HomematicIpObjectBase
    {
        [JsonProperty("PROFILE_1")]
        public Profile Profile1 { get; set; } = new Profile();
        [JsonProperty("PROFILE_2")]
        public Profile Profile2 { get; set; } = new Profile();
        [JsonProperty("PROFILE_3")]
        public Profile Profile3 { get; set; } = new Profile();
        [JsonProperty("PROFILE_4")]
        public Profile Profile4 { get; set; } = new Profile();
        [JsonProperty("PROFILE_5")]
        public Profile Profile5 { get; set; } = new Profile();
        [JsonProperty("PROFILE_6")]
        public Profile Profile6 { get; set; } = new Profile();
    }
}
