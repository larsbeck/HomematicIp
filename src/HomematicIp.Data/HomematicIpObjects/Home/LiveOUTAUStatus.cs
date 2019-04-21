namespace HomematicIp.Data.HomematicIpObjects.Home
{
    public class LiveOUTAUStatus
    {
        /// <summary>
        /// This looks like a candidate for an enum, States we know so far: INACTIVE
        /// </summary>
        public string LoveOTAUState { get; set; }
        public double Progress { get; set; }
        public string DeviceId { get; set; }
    }
}