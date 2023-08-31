using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.DEVICE_RECHARGEABLE_WITH_SABOTAGE)]
    public class DeviceRechargeableWithSabotage : AbstractDeviceBaseChannel
    {
        [JsonProperty(PropertyName = "badBatteryHealth")]
        public bool? IsBadBatteryHealth { get; set; }
    }
}
