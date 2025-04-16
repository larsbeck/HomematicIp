using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Channels;
using HomematicIp.Data.JsonConverters;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HomematicIp.Data.HomematicIpObjects.Devices
{
    public class Device : HomematicIpObjectBase
    {
        [JsonProperty(PropertyName = "type")]
        public DeviceType DeviceType { get; set; }
        public DeviceUpdateState UpdateState { get; set; }
        public LiveUpdateState LiveUpdateState { get; set; }
        public string FirmwareVersion { get; set; }
        public string AvailableFirmwareVersion { get; set; }
        public string SerializedGlobalTradeItemNumber { get; set; }
        public string ModelType { get; set; }
        public int ModelId { get; set; }
        public int FirmwareVersionInteger { get; set; }
        public int ManufacturerCode { get; set; }
        public string Oem { get; set; }
        public string DeviceArchetype { get; set; }
        public bool PermanentlyReachable { get; set; }
        [JsonConverter(typeof(FunctionalChannelsConverter), "functionalChannelType")]
        public List<Channel> FunctionalChannels { get; set; } = new List<Channel>();
    }
}