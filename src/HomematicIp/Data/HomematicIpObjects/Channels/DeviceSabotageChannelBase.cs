using System.Collections.Generic;
using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.SINGLE_KEY_CHANNEL)]

    public abstract class DeviceSabotageChannelBase : Channel
    {
        public int GroupIndex { get; set; }
        public int Index { get; set; }
        public FunctionalChannelType FunctionalChannelType { get; set; }
        public List<string> Groups { get; set; }=new List<string>();
        public string Label { get; set; }
    }
}