namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    public class Channel : IHaveARawJsonProperty
    {
        public string DeviceId { get; set; }
        public int Index { get; set; }
        public int GroupIndex { get; set; }
        public int ChannelIndex { get; set; }
        public string RawJson { get; set; }
    }
}