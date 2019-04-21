using System.Collections.Generic;

namespace HomematicIp.Data.HomematicIpObjects.Home
{
    public class VoiceControlSettings
    {
        public string Language { get; set; }
        public List<string> AllowedActiveSecurityZoneIds { get; set; } = new List<string>();
        /// <summary>
        /// This seems to create propertyNames, such as "conrad_connect" dynamically, here is an example:
        ///  "stateReportEnabled": {
        ///    "conrad_connect": "YES"
        ///  }
        /// </summary>
        public dynamic StateReportEnabled { get; set; }
    }
}