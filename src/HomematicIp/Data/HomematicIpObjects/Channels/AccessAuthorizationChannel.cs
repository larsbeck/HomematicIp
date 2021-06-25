using HomematicIp.Data.Enums;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(FunctionalChannelType.ACCESS_AUTHORIZATION_CHANNEL)]
    public class AccessAuthorizationChannel : DeviceSabotageChannelBase
    {
        [JsonProperty(PropertyName = "authorized")]
        public bool? IsAuthorized { get; set; }

    }
}
