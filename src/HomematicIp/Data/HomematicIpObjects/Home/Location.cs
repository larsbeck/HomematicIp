using HomematicIp.Data.JsonConverters;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Home
{
    public class Location
    {
        public string City { get; set; }
        [JsonConverter(typeof(DoubleConverter))]
        public double? Latitude { get; set; }
        [JsonConverter(typeof(DoubleConverter))]
        public double? Longitude { get; set; }
    }
}