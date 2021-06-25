using HomematicIp.Data.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    [EnumMap(GroupType.META)]
    public class MetaGroup : Group
    {
        [JsonIgnore]
        public override GroupType GroupType { get; set; }
        public List<string> Groups { get; set; }
        public bool? Sabotage { get; set; }
        public bool? ConfigPending { get; set; }
        public bool? IncorrectPositioned { get; set; }

    }
}