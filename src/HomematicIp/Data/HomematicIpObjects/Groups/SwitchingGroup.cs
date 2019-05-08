using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    [EnumMap(GroupType.SWITCHING)]

    public class SwitchingGroup : LinkedSwitchingGroup
    {
        public dynamic ShutterLevel { get; set; }
        public dynamic SlatsLevel { get; set; }
        public bool? Processing { get; set; }
    }
}