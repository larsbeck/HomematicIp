using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    [EnumMap(GroupType.LINKED_SWITCHING)]

    public class LinkedSwitchingGroup : HeatingChangeOverGroup
    {
        public dynamic DimLevel { get; set; }

    }
}