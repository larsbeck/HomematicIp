using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Groups;
using HomematicIp.Data.JsonConverters;
using Newtonsoft.Json;
using System;

namespace HomematicIp.Data.HomematicIpObjects.Rules
{
    [EnumMap(GroupType.OPEN_DOOR_NOTIFICATION_RULE_GROUP)]
    public class OpenDoorNotificationRuleGroup : Group
    {
        [JsonProperty(PropertyName = "triggered")]
        public bool? IsTriggered { get; set; }
        public WindowState? WindowState { get; set; }
        public LockState? LockState { get; set; }
        public string MotorState { get; set; }
        [JsonConverter(typeof(TimespanConverter))]
        public TimeSpan? LastExecutionTimestamp { get; set; }
    }
}

