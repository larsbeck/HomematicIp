namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.TEMPERATURE_SENSOR_2_EXTERNAL_DELTA_CHANNEL)]
    public class TemperatureSensor2ExternalDeltaChannel : DeviceSabotageChannelBase
    {
        public double? TemperatureExternalOne { get; set; }
        public double? TemperatureExternalTwo { get; set; }
        public double? TemperatureExternalDelta { get; set; }
    }
}