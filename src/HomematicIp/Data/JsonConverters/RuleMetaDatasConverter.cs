using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Rules;

namespace HomematicIp.Data.JsonConverters
{
    public class RuleMetaDatasConverter : AbstractListConverter<RuleMetaData, RuleType>
    {
        public RuleMetaDatasConverter(string typeDefiningProperty = "type") : base(typeDefiningProperty)
        {
        }
    }
}
