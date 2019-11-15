using System;
using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Channels;
using HomematicIp.Data.JsonConverters;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Home
{
    [EnumMap(FunctionalHomeType.SECURITY_AND_ALARM)]
    public class SecurityAndAlarmFunctionalHome : FunctionalHome
    {
        [JsonConverter(typeof(TimespanConverter))]
        public TimeSpan? AlarmEventTimestamp { get; set; }
        public string AlarmEventDeviceId { get; set; }
        public string AlarmEventTriggerId { get; set; }
        public AlarmEventDeviceChannel AlarmEventDeviceChannel { get; set; }
        public SecurityJournalEntryType? AlarmSecurityJournalEntryType { get; set; }
        public bool AlarmActive { get; set; }
        [JsonConverter(typeof(TimespanConverter))]
        public TimeSpan? SafetyAlarmEventTimestamp { get; set; }
        public SafetyAlarmEventDeviceChannel SafetyAlarmEventDeviceChannel { get; set; }
        public SecurityJournalEntryType? SafetyAlarmSecurityJournalEntryType { get; set; }
        public bool SafetyAlarmActive { get; set; }
        [JsonConverter(typeof(TimespanConverter))]
        public TimeSpan? IntrusionAlarmEventTimestamp { get; set; }
        public string IntrusionAlarmEventTriggerId { get; set; }
        public IntrusionAlarmEventDeviceChannel IntrusionAlarmEventDeviceChannel { get; set; }
        public SecurityJournalEntryType? IntrusionAlarmSecurityJournalEntryType { get; set; }
        public bool IntrusionAlarmActive { get; set; }
        public SecurityZones SecurityZones { get; set; }
        public SecuritySwitchingGroups SecuritySwitchingGroups { get; set; }
        public double? ZoneActivationDelay { get; set; }
        public bool IntrusionAlertThroughSmokeDetectors { get; set; }
        public SecurityZoneActivationMode? SecurityZoneActivationMode { get; set; }
        public bool ActivationInProgress { get; set; }
    }
}