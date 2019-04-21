using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    [EnumMap(GroupType.SECURITY)]
    public class SecurityGroup : SecurityGroupBase
    {
        public bool? MoistureDetected { get; set; }
        public bool? WaterlevelDetected { get; set; }
        public bool? PowerMainsFailure { get; set; }
        public SmokeDetectorAlarmType? SmokeDetectorAlarmType { get; set; }
    }
}