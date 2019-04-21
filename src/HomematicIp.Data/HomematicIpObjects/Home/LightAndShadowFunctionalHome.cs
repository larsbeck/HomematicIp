using System.Collections.Generic;
using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects.Home
{
    [EnumMap(FunctionalHomeType.LIGHT_AND_SHADOW)]
    public class LightAndShadowFunctionalHome : FunctionalHome
    {
        public List<string> ExtendedLinkedSwitchingGroups { get; set; }=new List<string>();
        public List<string> ExtendedLinkedShutterGroups { get; set; } = new List<string>();
        public List<string> SwitchingProfileGroups { get; set; } = new List<string>();
        public List<string> ShutterProfileGroups { get; set; } = new List<string>();
    }
}