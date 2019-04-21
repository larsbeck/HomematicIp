using HomematicIp.Data.Enums;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Home
{
    public class SecuritySwitchingGroups
    {
        [JsonProperty(nameof(SecuritySwitchingGroup.SIREN))]
        public string SirenId { get; set; }
        [JsonProperty(nameof(SecuritySwitchingGroup.PANIC))]
        public string PanicId { get; set; }
        [JsonProperty(nameof(SecuritySwitchingGroup.ALARM))]
        public string AlarmId { get; set; }
        [JsonProperty(nameof(SecuritySwitchingGroup.COMING_HOME))]
        public string ComingHomeId { get; set; }
        [JsonProperty(nameof(SecuritySwitchingGroup.BACKUP_ALARM_SIREN))]
        public string BackupAlarmSiren { get; set; }
    }
}