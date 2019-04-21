using HomematicIp.Data.Enums;

namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    public abstract class SecurityGroupBase : Group
    {
        public bool? Sabotage { get; set; }
        public WindowState? WindowState { get; set; }
        public bool? MotionDetected { get; set; }
        public bool? PresenceDetected { get; set; }
    }
}