using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects.Clients
{
    [EnumMap(ClientType.APP)]
    public class AppClient : Client
    {
        public string ClientId { get; set; }
    }
}