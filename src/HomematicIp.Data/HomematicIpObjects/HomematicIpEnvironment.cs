using System.Collections.Generic;
using HomematicIp.Data.HomematicIpObjects.Clients;
using HomematicIp.Data.HomematicIpObjects.Devices;
using HomematicIp.Data.HomematicIpObjects.Groups;
using HomematicIp.Data.JsonConverters;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects
{
    public class HomematicIpEnvironment
    {
        public Home.Home Home { get; set; }

        [JsonConverter(typeof(GroupsConverter), "type")]
        public List<Group> Groups { get; set; } = new List<Group>();

        [JsonConverter(typeof(DevicesConverter), "type")]
        public List<Device> Devices { get; set; } = new List<Device>();
        [JsonConverter(typeof(ClientsConverter), "clientType")]
        public List<Client> Clients { get; set; }=new List<Client>();
    }
}