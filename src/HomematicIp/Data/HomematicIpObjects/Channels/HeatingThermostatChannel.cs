using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.HEATING_THERMOSTAT_CHANNEL)]
    public class HeatingThermostatChannel : ThermostatChannelBase
    {
        public double? ValvePosition { get; set; }
        public double? SetPointTemperature { get; set; }
        public double? ValveActualTemperature { get; set; }
        public ValveState? ValveState { get; set; }
    }
}