using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Devices;

namespace HomematicIp.Data.JsonConverters
{
    public class DevicesConverter : AbstractListConverter<Device, DeviceType>
    {
        public DevicesConverter(string typeDefiningProperty = "type") : base(typeDefiningProperty)
        {
        }
    }
}