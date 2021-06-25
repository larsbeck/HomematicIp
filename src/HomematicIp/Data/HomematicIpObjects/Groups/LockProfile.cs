using HomematicIp.Data.Enums;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    [EnumMap(GroupType.LOCK_PROFILE)]
    public class LockProfile : Group
    {
        public string ProfileId { get; set; }
    }
}
