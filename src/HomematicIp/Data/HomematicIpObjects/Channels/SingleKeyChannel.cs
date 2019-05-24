using HomematicIp.Data.HomematicIpObjects.Devices;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.SINGLE_KEY_CHANNEL)]
    public class SingleKeyChannel : DeviceSabotageChannelBase { }
}