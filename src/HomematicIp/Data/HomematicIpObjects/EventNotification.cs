using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects
{
    public class EventNotification
    {
        public HomematicIpObjectBase HomematicIpObjectBase { get; set; }
        public EventType EventType { get; set; }
        public override string ToString()
        {
            return $"EventType: {EventType}; HomematicIpObject: {HomematicIpObjectBase}";
        }
    }
}