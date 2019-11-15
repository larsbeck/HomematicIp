using System.Collections.Generic;
using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Rules;
using HomematicIp.Data.JsonConverters;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Home
{
    public class Home : HomematicIpObjectBase
    {
        public Weather Weather { get; set; }
        public bool Connected { get; set; }
        public string CurrentAPVersion { get; set; }
        public string AvailableAPVersion { get; set; }
        public string TimeZoneId { get; set; }
        public Location Location { get; set; }
        public bool PinAssigned { get; set; }
        public bool IsLiveUpdateSupported { get; set; }
        public double? DutyCycle { get; set; }
        public double? CarrierSense { get; set; }
        public HomeUpdateState UpdateState { get; set; }
        public double? PowerMeterUnitPrice { get; set; }
        public string PowerMeterCurrency { get; set; }
        public DeviceUpdateStrategy DeviceUpdateStrategy { get; set; }
        public double? LastReadyForUpdateTimestamp { get; set; }
        [JsonConverter(typeof(FunctionalHomesConverter), "solution")]
        public List<FunctionalHome> FunctionalHomes { get; set; } = new List<FunctionalHome>();
        public string InboxGroup { get; set; }
        public string ApExchangeClientId { get; set; }
        public ApExchangeState ApExchangeState { get; set; }
        public VoiceControlSettings VoiceControlSettings { get; set; }
        public List<string> RuleGroups { get; set; }
        [JsonConverter(typeof(RuleMetaDatasConverter), "type")]
        public List<RuleMetaData> RuleMetaDatas { get; set; } = new List<RuleMetaData>();
        public LiveOUTAUStatus LiveOutauStatus { get; set; }
        public bool LiveUpdateSupported { get; set; }
    }
}