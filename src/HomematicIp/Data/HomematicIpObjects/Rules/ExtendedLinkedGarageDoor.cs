using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Groups;
using HomematicIp.Data.JsonConverters;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Rules
{
    /// <summary>
    /// {
    //	"id": "5a535728-4916-4800-b6e6-504761575264",
    //	"homeId": "34ee39f1-5014-4649-810a-7e7355eeb671",
    //	"metaGroupId": null,
    //	"label": "Garagentaster Manuell",
    //	"lastStatusUpdate": 1623176175320,
    //	"unreach": false,
    //	"lowBat": false,
    //	"dutyCycle": false,
    //	"type": "EXTENDED_LINKED_GARAGE_DOOR",
    //	"channels": [{
    //		"deviceId": "3014F711A000131709AE3835",
    //		"channelIndex": 1
    //	}, {
    //	"deviceId": "3014F711A000131709AE3835",
    //		"channelIndex": 2
    //	}],
    //	"processing": false,
    //	"doorState": null,
    //	"sensorSpecificParameters": {
    //	"3014F711A000131709AE3835:1": {
    //		"type": "GARAGE_DOOR_INPUT_ACTION",
    //			"garageDoorInputAction": "TOGGLE"
    //		}
    //},
    //	"groupVisibility": "VISIBLE",
    //	"ventilationPositionSupported": false
    //}
    /// </summary>
    [EnumMap(GroupType.EXTENDED_LINKED_GARAGE_DOOR)]
    public class ExtendedLinkedGarageDoor : Group
    {
        [JsonProperty(PropertyName = "processing")]
        public bool? IsProcessing { get; set; }
        public DoorState? DoorState { get; set; }
        public LockState? LockState { get; set; }
        public GroupVisibility? GroupVisibility { get; set; }
        [JsonConverter(typeof(TimespanConverter))]
        [JsonProperty(PropertyName = "ventilationPositionSupported")]
        public bool? IsVentilationPositionSupported { get; set; }
    }
}

