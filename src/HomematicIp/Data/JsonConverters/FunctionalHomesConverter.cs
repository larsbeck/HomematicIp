using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Home;

namespace HomematicIp.Data.JsonConverters
{
    public class FunctionalHomesConverter : AbstractListConverter<FunctionalHome, FunctionalHomeType>
    {
        public FunctionalHomesConverter(string typeDefiningProperty = "solution") : base(typeDefiningProperty)
        {
        }
    }
}