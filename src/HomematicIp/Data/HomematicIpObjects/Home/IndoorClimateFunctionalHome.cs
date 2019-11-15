using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects.Home
{
    [EnumMap(FunctionalHomeType.INDOOR_CLIMATE)]
    public class IndoorClimateFunctionalHome : FunctionalHome
    {
        public AbsenceType AbsenceType { get; set; }
        /// <summary>
        /// For now this is a string until we see an actual example for the string representation of time/date. Then this will probably become a DateTime.
        /// </summary>
        public string AbsenceEndTime { get; set; }

        public FloorHeatingSpecificGroups FloorHeatingSpecificGroups { get; set; }
        public double? EcoTemperature { get; set; }
        public bool CoolingEnabled { get; set; }
        public EcoDuration EcoDuration { get; set; }
        public bool OptimumStartStopEnabled { get; set; }
        public dynamic DeviceChannelSpecificFunction { get; set; }
    }
}