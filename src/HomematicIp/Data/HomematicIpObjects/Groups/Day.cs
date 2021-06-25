using Newtonsoft.Json;
using System.Collections.Generic;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    public class Day : HomematicIpObjectBase
    {
        [JsonProperty("periods")]
        public List<Period> Periods { get; set; }

        [JsonProperty("baseValue")]
        public long BaseValue { get; set; }
    }
}
