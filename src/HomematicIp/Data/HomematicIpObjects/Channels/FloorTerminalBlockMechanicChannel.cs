using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.FLOOR_TERMINAL_BLOCK_MECHANIC_CHANNEL)]
    public class FloorTerminalBlockMechanicChannel : DeviceSabotageChannelBase
    {
        public ValveState? ValveState { get; set; }
    }
}