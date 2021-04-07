using HomematicIp.Data.JsonConverters;
using Newtonsoft.Json;
using System;

namespace HomematicIp.Data.HomematicIpObjects
{
    public abstract class HomematicIpObjectBase : IHaveARawJsonProperty
    {
        public string RawJson { get; set; }
        public string Id { get; set; }
        public string HomeId { get; set; }
        public string Label { get; set; }

        [JsonConverter(typeof(TimespanConverter))]
        public TimeSpan? LastStatusUpdate { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
