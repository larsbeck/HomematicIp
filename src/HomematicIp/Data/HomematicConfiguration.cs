using System;

namespace HomematicIp.Data
{
    public class HomematicConfiguration
    {
        private string _accessPointId;

        /// <summary>
        /// Also called SGTIN on the back of your access point
        /// </summary>
        public string AccessPointId
        {
            get => _accessPointId;
            set => _accessPointId = GetAccessPointIdWithoutDashes(value);
        }

        private string GetAccessPointIdWithoutDashes(string accessPointId)
        {
            var accessPointIdWithoutDashes = accessPointId.Replace("-", "");
            if (accessPointIdWithoutDashes.Length != 24)
                throw new ArgumentException($"The accesspoint id (SGTIN) {accessPointId} is invalid. It needs to have exactly 24 digits without the dashes.");
            return accessPointIdWithoutDashes;
        }

        public string DeviceId { get; set; } = Guid.NewGuid().ToString();

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