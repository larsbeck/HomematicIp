using HomematicIp.Data.Enums;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    //"lockState": "LOCKED",
    //"motorState": "STOPPED",
    //"doorLockDirection": "LEFT",
    //"doorLockNeutralPosition": "HORIZONTAL",
    //"doorLockTurns": 1,
    //"doorHandleType": "LEVER_HANDLE",
    //"autoRelockDelay": 300.0
    //-- for now we handle the possible enums as string until we know alle the enum
    [EnumMap(FunctionalChannelType.DOOR_LOCK_CHANNEL)]
    public class DoorLockChannel : DeviceSabotageChannelBase
    {
        public string LockState { get; set; }
        public string MotorState { get; set; }
        public string DoorLockDirection { get; set; }
        public string DoorLockNeutralPosition { get; set; }
        public int? DoorLockTurns { get; set; }
        public string DoorHandleType { get; set; }
        public double? AutoRelockDelay { get; set; }

        [JsonProperty(PropertyName = "autoRelockEnabled")]
        public bool? IsAutoRelockEnabled { get; set; }
    }
}
