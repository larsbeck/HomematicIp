using System;
using System.Globalization;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using HomematicIp.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace HomematicIp.Services
{
    public class HomematicAuthService:HomematicServiceBase
    {
        private readonly Guid _guid = Guid.NewGuid();
        private AuthData _authData;
        public HomematicAuthService(Func<HttpClient> httpClientFactory, HomematicConfiguration homematicConfiguration) : base(httpClientFactory, homematicConfiguration)
        {
        }

        /// <summary>
        /// Starts the authorization process
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ConnectionRequest(CancellationToken cancellationToken= default)
        {
            await base.Initialize(cancellationToken: cancellationToken);
            var request = new HttpRequestMessage(HttpMethod.Post, "hmip/auth/connectionRequest");
            if(HomematicConfiguration.Pin!=null)
                request.Headers.Add("PIN", HomematicConfiguration.Pin);
            _authData = new AuthData {DeviceId = _guid.ToString(), Sgtin = HomematicConfiguration.AccessPointId };
            if (HomematicConfiguration.ApplicationName != null)
                _authData.DeviceName = HomematicConfiguration.ApplicationName;
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
