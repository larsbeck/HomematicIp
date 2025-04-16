using Newtonsoft.Json;
using System.Collections.Generic;

//sample JSON:
//{
//	"activationProblems": [],
//	"deviceActivationProblems": {
//		"3014F711A0000AD5699C66C8": ["SABOTAGE"],
//		"3014F711A000109709AAA8CC": ["WINDOW_NOT_CLOSED"],
//		"3014F711A000109709AAA8D3": ["LOW_BAT", "UNREACH"]
//	}
//}
namespace HomematicIp.Data.HomematicIpObjects
{
    public class ZonesActivationResult
    {
        [JsonProperty("activationProblems")]
        public string[] ActivationProblems { get; set; }

        [JsonProperty("channelActivationProblems")]
        public Dictionary<string, string[]> ChannelActivationProblems { get; set; }

        [JsonProperty("deviceActivationProblems")]
        public Dictionary<string, string[]> DeviceActivationProblems { get; set; }
    }
}
