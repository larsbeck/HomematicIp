using System.Collections.Generic;
using System.Linq;
using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Channels;

namespace HomematicIp.Data.HomematicIpObjects.Devices
{
    [EnumMap(DeviceType.ALARM_SIREN_OUTDOOR)]
    public class AlarmSirenOutdoor : SabotageDevice
    {
        private AlarmSirenChannel AlarmSirenChannel =>
            FunctionalChannels?.OfType<AlarmSirenChannel>().FirstOrDefault();
        public List<string> AlarmSirenGroups => AlarmSirenChannel?.Groups;
        public int? AlarmSirenGroupIndex => AlarmSirenChannel?.GroupIndex;
        public int? AlarmSirenIndex => AlarmSirenChannel?.Index;
    }
}