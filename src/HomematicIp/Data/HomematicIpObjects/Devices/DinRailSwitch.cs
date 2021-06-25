namespace HomematicIp.Data.HomematicIpObjects.Devices
{
    [EnumMap(Enums.DeviceType.DIN_RAIL_SWITCH)]
    public class DinRailSwitch : Device
    {
        //"connectionType": "HMIP_RF"
        public string ConnectionType { get; set; }
    }
}
