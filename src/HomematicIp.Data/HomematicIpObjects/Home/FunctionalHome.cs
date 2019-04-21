using System.Collections.Generic;

namespace HomematicIp.Data.HomematicIpObjects.Home
{
    public abstract class FunctionalHome: IHaveARawJsonProperty
    {
        public List<string> FunctionalGroups { get; set; }
        public bool Active { get; set; }
        public string RawJson { get; set; }
    }
}