namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.ANALOG_ROOM_CONTROL_CHANNEL)]
    public class AnalogRoomControlChannel : DeviceBaseChannel
    {
        public double? ActualTemperature { get; set; }
        public double? TemperatureOffset { get; set; }
        public double? SetPointTemperature { get; set; }
    }
}