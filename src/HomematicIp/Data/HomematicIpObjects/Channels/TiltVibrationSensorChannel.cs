using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(FunctionalChannelType.TILT_VIBRATION_SENSOR_CHANNEL)]
    public class TiltVibrationSensorChannel : DeviceSabotageChannelBase
    {
        //"accelerationSensorMode": "ANY_MOTION",
        //"accelerationSensorTriggered": false,
        //"accelerationSensorSensitivity": "SENSOR_RANGE_4G",
        //"accelerationSensorTriggerAngle": 20,
        //"accelerationSensorEventFilterPeriod": 3.0,
        //"accelerationSensorNeutralPosition": "HORIZONTAL"
        public string AccelerationSensorMode { get; set; }
        public bool? AccelerationSensorTriggered { get; set; }
        public string AccelerationSensorSensitivity { get; set; }
        public double? AccelerationSensorTriggerAngle { get; set; }
        public double? AccelerationSensorEventFilterPeriod { get; set; }
        public string AccelerationSensorNeutralPosition { get; set; }
    }
}