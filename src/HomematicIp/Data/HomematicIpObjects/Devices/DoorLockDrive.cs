namespace HomematicIp.Data.HomematicIpObjects.Devices
{
    [EnumMap(Enums.DeviceType.DOOR_LOCK_DRIVE)]
    public class DoorLockDrive : Device
    {
        //"connectionType": "HMIP_RF"
        public string ConnectionType { get; set; }
    }
}
