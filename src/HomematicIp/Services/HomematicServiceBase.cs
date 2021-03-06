﻿using System;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HomematicIp.Data;
using HomematicIp.Data.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HomematicIp.Services
{
    public abstract class HomematicServiceBase
    {
        protected readonly Func<HttpClient> HttpClientFactory;
        protected readonly HomematicConfiguration HomematicConfiguration;
        protected string _clientAuthToken;
        protected HttpClient HttpClient;
        protected string UrlWebSocket;
        protected StringContent ClientCharacteristicsStringContent;
        protected const string CLIENTAUTH = "CLIENTAUTH";

        protected HomematicServiceBase(Func<HttpClient> httpClientFactory, HomematicConfiguration homematicConfiguration)
        {
            HttpClientFactory = httpClientFactory;
            HomematicConfiguration = homematicConfiguration;
            HttpClient = httpClientFactory();
        }
        protected virtual async Task Initialize(ClientCharacteristicsRequestObject clientCharacteristicsRequestObject = null, CancellationToken cancellationToken = default)
        {
            if (clientCharacteristicsRequestObject == null)
                clientCharacteristicsRequestObject = new ClientCharacteristicsRequestObject(HomematicConfiguration.AccessPointId);
            ClientCharacteristicsStringContent = GetStringContent(clientCharacteristicsRequestObject);
            var httpResponseMessage = await HttpClient.PostAsync("https://lookup.homematic.com:48335/getHost", ClientCharacteristicsStringContent, cancellationToken);
            RestAndWebSocketUrls restAndWebSocketUrls;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var content = await httpResponseMessage.Content.ReadAsStringAsync();
                restAndWebSocketUrls = JsonConvert.DeserializeObject<RestAndWebSocketUrls>(content);
            }
            else
            {
                if ((int)httpResponseMessage.StatusCode == 418) //I'm a teapot message
                    throw new ArgumentException($"It is highly likely that you accidentally mistyped your access point id.");
                
                throw new Exception($"Error looking up the host: {httpResponseMessage.StatusCode}, {httpResponseMessage.ReasonPhrase}");
            }
            HttpClient = HttpClientFactory();

            HttpClient.BaseAddress = new Uri($"{restAndWebSocketUrls.UrlREST}/");
            if (!HttpClient.DefaultRequestHeaders.Contains("VERSION"))
                HttpClient.DefaultRequestHeaders.Add("VERSION", "12");
            if (!HttpClient.DefaultRequestHeaders.Contains(CLIENTAUTH))
                HttpClient.DefaultRequestHeaders.Add(CLIENTAUTH, ClientAuthToken);
            UrlWebSocket = restAndWebSocketUrls.UrlWebSocket;
        }

        private JsonSerializerSettings JsonSerializerSettings { get; } = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        protected StringContent GetStringContent(object obj)
        {
            var json = JsonConvert.SerializeObject(obj, JsonSerializerSettings);
            return new StringContent(json);
        }

        protected class RestAndWebSocketUrls
        {
            public string UrlREST { get; set; }
            public string UrlWebSocket { get; set; }
        }
        protected class ClientCharacteristicsRequestObject : IRequestObject
        {
            public ClientCharacteristicsRequestObject(string id)
            {
                Id = id;
            }

            public ClientCharacteristics ClientCharacteristics { get; set; } = new ClientCharacteristics();
            public string Id { get; set; }

            public override string ToString() => $"Access Point Id: {Id} with client characteristics: {ClientCharacteristics.ToString()}";
        }

        protected class StartInclusionModeForDeviceRequestObject : IRequestObject
        {
            public StartInclusionModeForDeviceRequestObject(string deviceId)
            {
                DeviceId = deviceId;
            }
            public string DeviceId { get; set; }
        }

        protected class SetDeviceLabelRequestObject:IRequestObject
        {
            public SetDeviceLabelRequestObject(string deviceId, string label)
            {
                DeviceId = deviceId;
                Label = label;
            }
            public string DeviceId { get; set; }
            public string Label { get; set; }
        }

        protected class SetSwitchStateRequestObject:IRequestObject
        {
            public SetSwitchStateRequestObject(int channelIndex, string deviceId, bool state)
            {
                DeviceId = deviceId;
                ChannelIndex = channelIndex;
                On = state;
            }
            public int ChannelIndex { get; set; }
            public string DeviceId { get; set; }
            public bool On { get; set; }
        }

        protected class SetDimLevelRequestObject : IRequestObject
        {
            public SetDimLevelRequestObject(int channelIndex, string deviceId, double dimLevel)
            {
                DeviceId = deviceId;
                ChannelIndex = channelIndex;
                DimLevel = dimLevel;
            }
            public int ChannelIndex { get; set; }
            public string DeviceId { get; set; }
            /// <summary>
            /// Min: 0, Max: 1
            /// 0 = off
            /// 0.50 = 50%
            /// 1 = on
            /// </summary>
            public double DimLevel { get; set; }
        }
        protected class SetSlatsLevelRequestObject : IRequestObject
        {
            public SetSlatsLevelRequestObject(int channelIndex, string deviceId, double shutterLevel, double slatsLevel)
            {
                DeviceId = deviceId;
                ChannelIndex = channelIndex;
                ShutterLevel = shutterLevel;
                SlatsLevel = slatsLevel;
            }
            public int ChannelIndex { get; set; }
            public string DeviceId { get; set; }
            /// <summary>
            /// Min: 0, Max: 1
            /// 0 = closed
            /// 0.50 = 50%
            /// 1 = opened
            /// </summary>
            public double ShutterLevel { get; set; }
            /// <summary>
            /// Min: 0, Max: 1
            /// 0 = closed
            /// 0.50 = 50%
            /// 1 = opened
            /// </summary>
            public double SlatsLevel { get; set; }
        }
        protected class SetShutterLevelRequestObject : IRequestObject
        {
            public SetShutterLevelRequestObject(int channelIndex, string deviceId, double shutterLevel)
            {
                DeviceId = deviceId;
                ChannelIndex = channelIndex;
                ShutterLevel = shutterLevel;
            }
            public int ChannelIndex { get; set; }
            public string DeviceId { get; set; }
            /// <summary>
            /// Min: 0, Max: 1
            /// 0 = closed
            /// 0.50 = 50%
            /// 1 = opened
            /// </summary>
            public double ShutterLevel { get; set; }
        }

        protected class StopRequestObject : IRequestObject
        {
            public StopRequestObject(int channelIndex, string deviceId)
            {
                DeviceId = deviceId;
                ChannelIndex = channelIndex;
            }
            public int ChannelIndex { get; set; }
            public string DeviceId { get; set; }
        }

        protected class SetPinRequestObject : IRequestObject
        {
            public SetPinRequestObject(string pin)
            {
                Pin = pin;
            }
            public string Pin { get; set; }
        }

        protected class ListAssignableMetaGroupsRequestObject : IRequestObject
        {
            public ListAssignableMetaGroupsRequestObject(string deviceId)
            {
                DeviceId = deviceId;
            }
            public string DeviceId { get; set; }
        }

        protected class EnableSimpleRuleRequestObject : IRequestObject
        {
            public EnableSimpleRuleRequestObject(bool enabled, string ruleId)
            {
                Enabled = enabled;
                RuleId = ruleId;
            }
            public bool Enabled { get; set; }
            public string RuleId { get; set; }
        }

        protected class RegisterFCMRequestObject : IRequestObject
        {
            public RegisterFCMRequestObject(string token)
            {
                Token = token;
            }
            public string Token { get; set; }
        }

        protected class SetPointTemperatureRequestObject : IRequestObject
        {
            public SetPointTemperatureRequestObject(string groupId, double setPointTemperature)
            {
                GroupId = groupId;
                SetPointTemperature = setPointTemperature;
            }
            public string GroupId { get; set; }
            public double SetPointTemperature { get; set; }
        }

        protected class SetEcoTemperatureRequestObject : IRequestObject
        {
            public SetEcoTemperatureRequestObject(double ecoTemperature)
            {
                EcoTemperature = ecoTemperature;
            }
            public double EcoTemperature { get; set; }
        }
        
        protected class ClientCharacteristics
        {
            public string ApiVersion => "10";
            public string ApplicationIdentifier { get; set; } = "homematicip-dotnetcore";
            public string ApplicationVersion { get; set; } = "1.0";
            public string DeviceManufacturer { get; set; } = "none";
            public ClientDeviceType DeviceType { get; set; } = ClientDeviceType.Computer;
            public string Language => CultureInfo.CurrentCulture.Name;
            public string OsType => Environment.OSVersion.Platform.ToString();
            public string OsVersion => Environment.OSVersion.Version.ToString();

            public override string ToString() => $"API: {ApiVersion} / OS Type: {OsType} / OS Version: {OsVersion} / Language: {Language} / Device Type: {DeviceType}";
        }


        public string ClientAuthToken
        {
            get
            {
                if (_clientAuthToken == null)
                {
                    using SHA512 shaM = new SHA512Managed();
                    var data = Encoding.UTF8.GetBytes($"{HomematicConfiguration.AccessPointId}jiLpVitHvWnIGD1yo7MA");
                    var hash = shaM.ComputeHash(data);
                    _clientAuthToken = BitConverter.ToString(hash).Replace("-", "").ToUpper();
                }
                return _clientAuthToken;
            }
        }

        public string CurrentAuthToken => HomematicConfiguration.AuthToken;
    }
}