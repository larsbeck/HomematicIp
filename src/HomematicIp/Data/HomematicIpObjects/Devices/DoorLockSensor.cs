namespace HomematicIp.Data.HomematicIpObjects.Devices
{
    [EnumMap(Enums.DeviceType.DOOR_LOCK_SENSOR)]
    public class DoorLockSensor : Device
    {
        //"connectionType": "HMIP_RF"
        public string ConnectionType { get; set; }
    }
}
