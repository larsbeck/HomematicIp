using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Devices;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.SMOKE_DETECTOR_CHANNEL)]

    public class SmokeDetectorChannel : DeviceSabotageChannelBase
    {
        public SmokeDetectorAlarmType? SmokeDetectorAlarmType { get; set; }
    }
}