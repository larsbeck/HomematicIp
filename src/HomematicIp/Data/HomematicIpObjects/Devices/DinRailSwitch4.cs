namespace HomematicIp.Data.HomematicIpObjects.Devices
{
    [EnumMap(Enums.DeviceType.DIN_RAIL_SWITCH_4)]
    public class DinRailSwitch4 : Device
    {
        //"connectionType": "HMIP_RF"
        public string ConnectionType { get; set; }
    }
}
