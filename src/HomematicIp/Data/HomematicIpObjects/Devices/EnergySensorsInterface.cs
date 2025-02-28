using System.Collections.Generic;

namespace HomematicIp.Data.HomematicIpObjects.Devices
{
    [EnumMap(Enums.DeviceType.ENERGY_SENSORS_INTERFACE)]
    public class EnergySensorsInterface : Device
    {
        /// <summary>
        /// "measuredAttributes": {
        ///     "1": {
        ///         "currentGasFlow": true,
        ///         "gasVolume": true
        ///     }
        /// }
        /// </summary>
        public Dictionary<string, MeasuredAttribute> MeasuredAttributes { get; set; }
    }
    
    public class MeasuredAttribute
    {
        public bool CurrentGasFlow { get; set; }
        public bool GasVolume { get; set; }
    }
}
