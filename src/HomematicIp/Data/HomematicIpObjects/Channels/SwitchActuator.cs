namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.SWITCH_ACTUATOR)]
    public class SwitchActuator : MeasuringChannelBase
    {
        //"internalLinkConfiguration": {
        //  "internalLinkConfigurationType": "SINGLE_INPUT_SWITCH",
        //  "onTime": 111600.0,
        //  "firstInputAction": "TOGGLE",
        //  "longPressOnTimeEnabled": false
        //},
        //"powerUpSwitchState": "PERMANENT_OFF"
        public dynamic InternalLinkConfiguration { get; set; }
        public dynamic PowerUpSwitchState { get; set; }
    }
}

