using HomematicIp.Data.Enums;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(FunctionalChannelType.CODE_PROTECTED_PRIMARY_ACTION_CHANNEL)]
    public class CodeProtectedPrimaryActionChannel : DeviceSabotageChannelBase
    {
        public WindowState? WindowState { get; set; }
        public int EventDelay { get; set; }
        
        [JsonProperty(PropertyName = "authorized")]
        public bool IsAuthorized { get; set; }
        //e.g. NOT_CUSTOMISABLE
        public string ActionParameter { get; set; }
        [JsonProperty(PropertyName = "actionCodeConfigured")]
        public bool IsActionCodeConfigured { get; set; }
        //e.g. CODE_INPUT_FOR_GROUP_AND_ALARM_ZONE
        public string ChannelRole { get; set; }
    }
}
