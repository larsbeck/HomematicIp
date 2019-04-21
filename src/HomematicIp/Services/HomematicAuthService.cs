using System;
using System.Globalization;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace HomematicIp.Services
{
    public class HomematicAuthService:HomematicServiceBase
    {
        private readonly string _pin;
        private readonly string _deviceName;
        private readonly Guid _guid = Guid.NewGuid();
        private AuthData _authData;
        public HomematicAuthService(Func<HttpClient> httpClientFactory, string accessPointId, string pin= null, string deviceName = null) : base(httpClientFactory, accessPointId)
        {
            _pin = pin;
            _deviceName = deviceName;
        }

        /// <summary>
        /// Starts the authorization process
        /// </summary>
        /// <param name="accessPointId">Also called SGTIN</param>
        /// <param name="pin">Your private PIN (if any)</param>
        /// <param name="deviceName">An optional name for your device</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ConnectionRequest(CancellationToken cancellationToken= default)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "hmip/auth/connectionRequest");
            if(_pin!=null)
                request.Headers.Add("PIN", _pin);
            HttpClient.DefaultRequestHeaders.Add("CLIENTAUTH", ClientAuthToken);
            _authData = new AuthData {DeviceId = _guid.ToString(), Sgtin = AccessPointId };
            if (_deviceName != null)
                _authData.DeviceName = _deviceName;
            request.Content = GetStringContent(_authData);
            var httpResponseMessage = await HttpClient.SendAsync(request, cancellationToken);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                var content = await httpResponseMessage.Content.ReadAsStringAsync();
                throw new ArgumentException($"Authorization failed: {httpResponseMessage.ReasonPhrase}; {content}");
            }
        }

        public async Task<bool> IsRequestAcknowledged(CancellationToken cancellationToken = default)
        {
            var httpResponseMessage = await HttpClient.PostAsync("hmip/auth/isRequestAcknowledged", GetStringContent(_authData), cancellationToken);
            return httpResponseMessage.IsSuccessStatusCode;
        }

        public async Task<string> RequestAuthToken(CancellationToken cancellationToken = default)
        {
            var httpResponseMessage = await HttpClient.PostAsync("hmip/auth/requestAuthToken", GetStringContent(_authData), cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var content = await httpResponseMessage.Content.ReadAsStringAsync();
                var authToken= JsonConvert.DeserializeObject<AuthData>(content).AuthToken;
                if (await ConfirmAuthToken(authToken, cancellationToken))
                    return authToken;
            }
            throw new AuthenticationException($"RequestAuthToken failed. {httpResponseMessage.ReasonPhrase}");
        }

        protected async Task<bool> ConfirmAuthToken(string authToken, CancellationToken cancellationToken = default)
        {
            _authData.AuthToken = authToken;
            var httpResponseMessage = await HttpClient.PostAsync("hmip/auth/confirmAuthToken", GetStringContent(_authData), cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            _authData.AuthToken = null;
            throw new AuthenticationException($"RequestAuthToken failed. {httpResponseMessage.ReasonPhrase}");
        }

        private class AuthData
        {
            public string DeviceId { get; set; }
            public string DeviceName { get; set; } = "homematicip-dotnetcore";
            public string Sgtin { get; set; }
            public string AuthToken { get; set; }
        }
    }
}
