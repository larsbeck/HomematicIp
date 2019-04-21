using System.Collections.Generic;
using System.Linq;
using HomematicIp.Data.HomematicIpObjects.Channels;

namespace HomematicIp.Data.HomematicIpObjects.Devices
{
    public class SabotageDevice : Device
    {
        private DeviceSabotageChannel DeviceSabotageChannel =>
            FunctionalChannels?.OfType<DeviceSabotageChannel>().FirstOrDefault();
        public bool? IsUnreachable => DeviceSabotageChannel?.IsUnreachable;
        public bool? IsLowBattery => DeviceSabotageChannel?.HasLowBattery;
        public bool? RouterModuleEnabled => DeviceSabotageChannel?.RouterModuleEnabled;
        public bool? RouterModuleSupported => DeviceSabotageChannel?.RouterModuleSupported;
        public int? RssiDeviceValue => DeviceSabotageChannel?.RssiDeviceValue;
        public int? RssiPeerValue => DeviceSabotageChannel?.RssiPeerValue;
        public bool? ConfigPending => DeviceSabotageChannel?.ConfigPending;
        public bool? IsDutyCycle => DeviceSabotageChannel?.DutyCycle;
        public bool? Sabotage => DeviceSabotageChannel?.Sabotage;
        public int? SabotageGroupIndex => DeviceSabotageChannel?.GroupIndex;
        public int? SabotageIndex => DeviceSabotageChannel?.Index;
        public List<string> SabotageGroups => DeviceSabotageChannel?.Groups;
    }
}