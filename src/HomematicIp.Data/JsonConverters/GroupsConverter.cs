using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Groups;
using HomematicIp.Data.HomematicIpObjects.Home;

namespace HomematicIp.Data.JsonConverters
{
    public class GroupsConverter : AbstractListConverter<Group,GroupType>
    {
        public GroupsConverter(string typeDefiningProperty="type") : base(typeDefiningProperty)
        {
        }
    }
}