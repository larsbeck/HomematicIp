using Newtonsoft.Json;
using System.Collections.Generic;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    public class Day : HomematicIpObjectBase
    {
        [JsonProperty("dayOfWeek")]
        public string DayOfWeek { get; set; }

        [JsonProperty("periods")]
        public List<Period> Periods { get; set; } = new List<Period>();

        [JsonProperty("baseValue")]
        public double BaseValue { get; set; }
    }
}
