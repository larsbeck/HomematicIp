using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Groups;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Rules
{
    [EnumMap(GroupType.LOCK_OUT_PROTECTION_RULE)]
    public class LockOutProtectionRule : Group
    {
        [JsonProperty(PropertyName = "triggered")]
        public bool? IsTriggered { get; set; }
        public WindowState? WindowState { get; set; }
        public string ProfileMode { get; set; }

    }
}
