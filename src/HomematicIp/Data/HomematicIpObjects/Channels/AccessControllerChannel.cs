namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.ACCESS_CONTROLLER_CHANNEL)]
    public class AccessControllerChannel : AbstractDeviceBaseChannel
	{
		public string BusConfigMismatch { get; set; }
		public string PowerShortCircuit { get; set; }
		public string ShortCircuitDataLine { get; set; }
		public double? SignalBrightness { get; set; }
		public int? AccessPointPriority { get; set; }
		public double? DutyCycleLevel { get; set; }
		public double? CarrierSenseLevel { get; set; }
	}
}
