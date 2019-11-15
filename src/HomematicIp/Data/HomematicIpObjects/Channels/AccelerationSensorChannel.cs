using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.ACCELERATION_SENSOR_CHANNEL)]
    public class AccelerationSensorChannel : AbstractDeviceBaseChannel
    {
        //ANY_MOTION
        public string AccelerationSensorMode { get; set; }
        [JsonProperty(PropertyName = "accelerationSensorTriggered")]
        public bool IsAccelerationSensorTriggered { get; set; }
        //SENSOR_RANGE_2G
        public string AccelerationSensorSensitivity { get; set; }
        public int AccelerationSensorTriggerAngle { get; set; }
        //SOUND_SHORT_SHORT
        public string NotificationSoundTypeHighToLow { get; set; }
        //SOUND_SHORT
        public string NotificationSoundTypeLowToHigh { get; set; }
        public double? AccelerationSensorEventFilterPeriod { get; set; }
        //HORIZONTAL
        public string AccelerationSensorNeutralPosition { get; set; }
    }
}
