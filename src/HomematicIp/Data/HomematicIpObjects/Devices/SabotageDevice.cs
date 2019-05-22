using System.Collections.Generic;
using System.Linq;
using HomematicIp.Data.HomematicIpObjects.Channels;

namespace HomematicIp.Data.HomematicIpObjects.Devices
{
    public class SabotageDevice : Device
    {
        private AbstractDeviceSabotageChannel AbstractDeviceSabotageChannel =>
            FunctionalChannels?.OfType<AbstractDeviceSabotageChannel>().FirstOrDefault();
        public bool? IsUnreachable => AbstractDeviceSabotageChannel?.IsUnreachable;
        public bool? IsLowBattery => AbstractDeviceSabotageChannel?.HasLowBattery;
        public bool? RouterModuleEnabled => AbstractDeviceSabotageChannel?.RouterModuleEnabled;
        public bool? RouterModuleSupported => AbstractDeviceSabotageChannel?.RouterModuleSupported;
        public int? RssiDeviceValue => AbstractDeviceSabotageChannel?.RssiDeviceValue;
        public int? RssiPeerValue => AbstractDeviceSabotageChannel?.RssiPeerValue;
        public bool? ConfigPending => AbstractDeviceSabotageChannel?.ConfigPending;
        public bool? IsDutyCycle => AbstractDeviceSabotageChannel?.DutyCycle;
        public bool? Sabotage => AbstractDeviceSabotageChannel?.Sabotage;
        public int? SabotageGroupIndex => AbstractDeviceSabotageChannel?.GroupIndex;
        public int? SabotageIndex => AbstractDeviceSabotageChannel?.Index;
        public List<string> SabotageGroups => AbstractDeviceSabotageChannel?.Groups;
    }
}