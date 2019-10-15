using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(FunctionalChannelType.MOTION_DETECTION_CHANNEL)]
    public class PresenceDetectionChannel : DeviceSabotageChannelBase
    {
        public bool? PresenceDetected { get; set; }
        public double? Illumination { get; set; }
        public double? CurrentIllumination { get; set; }
        public double? NumberOfBrightnessMeasurements { get; set; }
        public MotionDetectionSendInterval MotionDetectionSendInterval { get; set; }
        public bool? MotionBufferActive { get; set; }
    }
}