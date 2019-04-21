using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects.Home
{
    [EnumMap(FunctionalHomeType.SECURITY_AND_ALARM)]
    public class SecurityAndAlarmFunctionalHome : FunctionalHome
    {
        public string AlarmEventTimestamp { get; set; }
        public string AlarmEventDeviceId { get; set; }
        public string AlarmEventTriggerId { get; set; }
        public string AlarmEventDeviceChannel { get; set; }
        public string AlarmSecurityJournalEntryType { get; set; }
        public bool AlarmActive { get; set; }
        public SecurityZones SecurityZones { get; set; }
        public SecuritySwitchingGroups SecuritySwitchingGroups { get; set; }
        public double ZoneActivationDelay { get; set; }
        public bool IntrusionAlertThroughSmokeDetectors { get; set; }
        public SecurityZoneActivationMode SecurityZoneActivationMode { get; set; }
        public bool ActivationInProgress { get; set; }
    }
}