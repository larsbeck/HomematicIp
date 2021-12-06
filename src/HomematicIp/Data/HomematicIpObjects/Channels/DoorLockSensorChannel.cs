using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(FunctionalChannelType.DOOR_LOCK_SENSOR_CHANNEL)]
    public class DoorLockSensorChannel : DeviceSabotageChannelBase
    {
        public string LockState { get; set; }
        public string DoorLockDirection { get; set; }
        public string DoorLockNeutralPosition { get; set; }
        public int? DoorLockTurns { get; set; }
    }
}
