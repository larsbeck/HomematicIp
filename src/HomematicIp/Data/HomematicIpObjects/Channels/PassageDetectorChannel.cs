using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(FunctionalChannelType.PASSAGE_DETECTOR_CHANNEL)]
    public class PassageDetectorChannel : DeviceSabotageChannelBase
    {
        public double? LeftCounter { get; set; }
        public double? RightCounter { get; set; }
        public double? LeftRightCounterDelta { get; set; }
        public double? PassageSensorSensitivity { get; set; }
        public double? PassageTimeout { get; set; }
        public double? PassageBlindtime { get; set; }
        public PassageDirection PassageDirection { get; set; }
    }
}