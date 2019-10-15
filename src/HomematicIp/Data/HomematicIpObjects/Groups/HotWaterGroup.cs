namespace HomematicIp.Data.HomematicIpObjects.Groups
{
    [EnumMap(Enums.GroupType.HOT_WATER)]
    public class HotWaterGroup : HeatingChangeOverGroup
    {
        public double? OnTime { get; set; }
        public string ProfileId { get; set; }
        public string ProfileMode { get; set; }

    }
}