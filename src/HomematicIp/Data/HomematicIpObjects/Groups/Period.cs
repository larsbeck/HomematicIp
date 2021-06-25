using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    public class Period : HomematicIpObjectBase
    {
        //e.g 17:00
        [JsonProperty("starttime")]
        public string Starttime { get; set; }

        //e.g 22:00
        [JsonProperty("endtime")]
        public string Endtime { get; set; }

        [JsonProperty("value")]
        public long Value { get; set; }
    }
}
