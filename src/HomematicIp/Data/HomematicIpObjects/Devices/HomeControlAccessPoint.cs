namespace HomematicIp.Data.HomematicIpObjects.Devices
{
    [EnumMap(Enums.DeviceType.HOME_CONTROL_ACCESS_POINT)]
    public class HomeControlAccessPoint : Device
    {
	    //"connectionType": "HMIP_LAN",
        public string ConnectionType { get; set; }
    }
}
