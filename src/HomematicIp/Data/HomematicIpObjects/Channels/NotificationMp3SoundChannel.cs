namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.NOTIFICATION_MP3_SOUND_CHANNEL)]
    public class NotificationMp3SoundChannel : AbstractDeviceBaseChannel
    {
        public double? VolumeLevel { get; set; }
        public string SoundFile { get; set; }
        public string ProfileMode { get; set; }
        public string UserDesiredProfileMode { get; set; }
    }
}
