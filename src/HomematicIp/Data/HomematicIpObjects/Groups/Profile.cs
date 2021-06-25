using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    //   "PROFILE_6": {
    //	"profileId": "ec747aa7-c83d-475c-b565-7e43d5f321ce",
    //	"groupId": "5ba14748-1b83-4bd6-aff0-2b11f8b5c361",
    //	"index": "PROFILE_6",
    //	"name": "",
    //	"visible": true,
    //	"enabled": false
    //}
    public class Profile : HomematicIpObjectBase
    {
        public string ProfileId { get; set; }
        public string GroupId { get; set; }
        public string Index { get; set; }
        public string Name { get; set; }
        [JsonProperty(PropertyName = "visible")]
        public bool? IsVisible { get; set; }
        [JsonProperty(PropertyName = "enabled")]
        public bool? IsEnabled { get; set; }

        public ProfileDays ProfileDays { get; set; } = new ProfileDays();
    }
}
