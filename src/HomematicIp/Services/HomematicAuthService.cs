using HomematicIp.Data;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using HomematicIp.Data.HomematicIpObjects.Clients;

namespace HomematicIp.Services
{
    public class HomematicAuthService : HomematicServiceBase
    {
        private AuthData _authData;
        public HomematicAuthService(Func<HttpClient> httpClientFactory, HomematicConfiguration homematicConfiguration, ClientCharacteristics clientCharacteristics = null) : base(httpClientFactory, homematicConfiguration, clientCharacteristics)
        {
        }

        /// <summary>
        /// Starts the authorization process
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ConnectionRequest(CancellationToken cancellationToken = default)
        {
            await base.Initialize(cancellationToken: cancellationToken);
            var request = new HttpRequestMessage(HttpMethod.Post, "hmip/auth/connectionRequest");
            if (HomematicConfiguration.Pin != null)
                request.Headers.Add("PIN", HomematicConfiguration.Pin);
            _authData = new AuthData { DeviceId = HomematicConfiguration.DeviceId, Sgtin = HomematicConfiguration.AccessPointId };
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
                var authToken = JsonConvert.DeserializeObject<AuthData>(content).AuthToken;
                await ConfirmAuthToken(authToken, cancellationToken);
                return authToken;
            }
            throw new AuthenticationException($"RequestAuthToken failed. {httpResponseMessage.ReasonPhrase}");
        }

        public async Task<AuthData> RequestAuthData(CancellationToken cancellationToken = default)
        {
            var httpResponseMessage = await HttpClient.PostAsync("hmip/auth/requestAuthToken", GetStringContent(_authData), cancellationToken);
            if (!httpResponseMessage.IsSuccessStatusCode)
                throw new AuthenticationException($"RequestAuthToken failed. {httpResponseMessage.ReasonPhrase}");

            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            var authData = JsonConvert.DeserializeObject<AuthData>(content);
            var client = await ConfirmAuthToken(authData.AuthToken, cancellationToken);
            authData.ClientId = client.ClientId;
            return authData;
        }
        
        /// <summary>
        /// Checks the AuthToken and returns the Client (id)
        /// </summary>
        /// <param name="authToken">AuthToken that was received from the server and has to be confirmed</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The Client</returns>
        protected async Task<AppClient> ConfirmAuthToken(string authToken, CancellationToken cancellationToken = default)
        {
            _authData.AuthToken = authToken;
            var httpResponseMessage = await HttpClient.PostAsync("hmip/auth/confirmAuthToken", GetStringContent(_authData), cancellationToken);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                _authData.AuthToken = null;
                throw new AuthenticationException($"RequestAuthToken failed. {httpResponseMessage.ReasonPhrase}");
            }
            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            var appClient = JsonConvert.DeserializeObject<AppClient>(content);
            return appClient;
        }

        public class AuthData
        {
            public string DeviceId { get; set; }
            public string DeviceName { get; set; } = "homematicip-dotnetcore";
            public string Sgtin { get; set; }
            public string AuthToken { get; set; }
            public string ClientId { get; set; }
        }
    }
}
