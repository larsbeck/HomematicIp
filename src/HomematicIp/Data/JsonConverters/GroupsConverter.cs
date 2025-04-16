using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Groups;

namespace HomematicIp.Data.JsonConverters
{
    public class GroupsConverter : AbstractListConverter<Group, GroupType>
    {
        public GroupsConverter(string typeDefiningProperty = "type") : base(typeDefiningProperty)
        {
        }
    }
}