using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    [EnumMap(GroupType.EXTENDED_LINKED_SWITCHING)]
    public class ExtendedLinkedSwitchingGroup : LinkedSwitchingGroup
    {
        public double OnTime { get; set; }
        public double OnLevel { get; set; }
        public dynamic SensorSpecificParameters { get; set; }
    }
}