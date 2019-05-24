namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.SWITCH_MEASURING_CHANNEL)]
    public class SwitchMeasuringChannel : MeasuringChannelBase
    {
        public double? EnergyCounter { get; set; }
        public double? CurrentPowerConsumption { get; set; }
    }
}