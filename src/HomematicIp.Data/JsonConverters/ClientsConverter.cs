using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Clients;

namespace HomematicIp.Data.JsonConverters
{
    public class ClientsConverter : AbstractListConverter<Client, ClientType>
    {
        public ClientsConverter(string typeDefiningProperty = "clientType") : base(typeDefiningProperty)
        {
        }
    }
}