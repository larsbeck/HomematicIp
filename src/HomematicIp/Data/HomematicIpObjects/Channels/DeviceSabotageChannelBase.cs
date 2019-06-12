using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    public abstract class DeviceSabotageChannelBase : Channel
    {
        public FunctionalChannelType FunctionalChannelType { get; set; }
        public string Label { get; set; }
    }
}