using HomematicIp.Data.Enums;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(FunctionalChannelType.ENERGY_SENSORS_INTERFACE_CHANNEL)]
    public class EnergySensorsInterfaceChannel : DeviceSabotageChannelBase
    {
        [JsonProperty("impulsesPerKWH")]
        public int? ImpulsesPerKwh { get; set; }
        public double? GasVolumePerImpulse { get; set; }
        public double? CurrentPowerConsumption { get; set; }
        public double? CurrentGasFlow { get; set; }
        public double? GasVolume { get; set; }
        public double? EnergyCounterOne { get; set; }
        public double? EnergyCounterTwo { get; set; }
        public double? EnergyCounterThree { get; set; }
        public string EnergyCounterOneType { get; set; }
        public string EnergyCounterTwoType { get; set; }
        public string EnergyCounterThreeType { get; set; }
    }
}
