
namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.ACCESS_CONTROLLER_WIRED_CHANNEL)]
    public class AccessControllerWiredChannel : AbstractDeviceBaseChannel
    {
        public string BusConfigMismatch { get; set; }
        public string PowerShortCircuit { get; set; }
        public string ShortCircuitDataLine { get; set; }
        //"busMode": "STUBS",
        public string BusMode { get; set; }
        public int? PowerSupplyCurrent { get; set; }
        public double? SignalBrightness { get; set; }
        public int? AccessPointPriority { get; set; }
    }
}