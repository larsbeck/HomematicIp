﻿using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.MOTION_DETECTION_CHANNEL)]
    public class MotionDetectionChannel : DeviceSabotageChannelBase
    {
        public bool? MotionDetected { get; set; }
        public dynamic Illumination { get; set; }
        public dynamic CurrentIllumination { get; set; }
        public double? NumberOfBrightnessMeasurements { get; set; }
        public MotionDetectionSendInterval MotionDetectionSendInterval { get; set; }
        public bool? MotionBufferActive { get; set; }
        // "motionSensorZones": "PYRO_1_3",
        // "motionSensorZoneSensitivityMap": {
        //     "PYRO_3": 70,
        //     "PYRO_2": 70,
        //     "PYRO_1": 70
        // }
    }
}