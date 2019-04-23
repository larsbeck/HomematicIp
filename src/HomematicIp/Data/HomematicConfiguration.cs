namespace HomematicIp.Data
{
    public class HomematicConfiguration
    {
        /// <summary>
        /// Also called SGTIN on the back of your access point
        /// </summary>
        public string AccessPointId { get; set; }
        /// <summary>
        /// Your PIN (if any)
        /// </summary>
        public string Pin { get; set; }
        /// <summary>
        /// The name of this very application
        /// </summary>
        public string ApplicationName { get; set; }
        /// <summary>
        /// The AuthToken you received via HomematicAuthService
        /// </summary>
        public string AuthToken { get; set; }
    }
}