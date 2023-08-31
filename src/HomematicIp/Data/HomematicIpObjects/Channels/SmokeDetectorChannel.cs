using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.SMOKE_DETECTOR_CHANNEL)]

    public class SmokeDetectorChannel : DeviceSabotageChannelBase
    {
        public SmokeDetectorAlarmType? SmokeDetectorAlarmType { get; set; }
    }
}