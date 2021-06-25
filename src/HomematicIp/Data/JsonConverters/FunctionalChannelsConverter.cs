using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Channels;

namespace HomematicIp.Data.JsonConverters
{
    public class FunctionalChannelsConverter : AbstractListConverter<Channel, FunctionalChannelType>
    {
        public FunctionalChannelsConverter(string typeDefiningProperty = "functionalChannelType") : base(typeDefiningProperty)
        {
        }
    }
}