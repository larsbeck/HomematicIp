using System.Collections.Generic;
using HomematicIp.Data.Enums;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    [EnumMap(GroupType.ACCESS_AUTHORIZATION_PROFILE)]
    public class AccessAuthorizationProfile : Group
    {
        public List<string> ClientIds { get; set; } = new List<string>();
        public string ProfileId { get; set; }

        public GroupVisibility? GroupVisibility { get; set; }

        [JsonProperty(PropertyName = "authorizationPinAssigned")]
        public bool? IsAuthorizationPinAssigned { get; set; }

        [JsonProperty(PropertyName = "authorized")]
        public bool? IsAuthorized { get; set; }

        [JsonProperty(PropertyName = "active")]
        public bool? IsActive { get; set; }
    }
}
