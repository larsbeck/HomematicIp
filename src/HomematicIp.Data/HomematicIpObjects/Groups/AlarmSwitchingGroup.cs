using HomematicIp.Data.Enums;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    [EnumMap(GroupType.ALARM_SWITCHING, GroupType.SECURITY_BACKUP_ALARM_SWITCHING)]
    public class AlarmSwitchingGroup : LinkedSwitchingGroup
    {
        public double OnTime { get; set; }
        public AcousticAlarmSignal? SignalAcoustic { get; set; }
        public OpticalAlarmSignal? SignalOptical { get; set; }
        public SmokeDetectorAlarmType? SmokeDetectorAlarmType { get; set; }
        [JsonProperty(PropertyName = "acousticFeedbackEnabled")]
        public bool IsAcousticFeedbackEnabled { get; set; }
    }
}