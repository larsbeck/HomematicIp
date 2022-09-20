using HomematicIp.Data;
using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        private readonly ClientCharacteristics _clientCharacteristics;
        protected HomematicServiceBase(Func<HttpClient> httpClientFactory, HomematicConfiguration homematicConfiguration, ClientCharacteristics clientCharacteristics = null)
        {
            HttpClientFactory = httpClientFactory;
            HomematicConfiguration = homematicConfiguration;
            HttpClient = httpClientFactory();
            _clientCharacteristics = clientCharacteristics ?? new ClientCharacteristics();
        }
        protected virtual async Task Initialize(ClientCharacteristicsRequestObject clientCharacteristicsRequestObject = null, CancellationToken cancellationToken = default)
        {
            if (clientCharacteristicsRequestObject == null)
                clientCharacteristicsRequestObject = new ClientCharacteristicsRequestObject(HomematicConfiguration.AccessPointId, _clientCharacteristics);

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
        protected StringContent GetStringContent(object obj, bool useCamelCaseProperties = true)
        {
            var json = useCamelCaseProperties ? JsonConvert.SerializeObject(obj, JsonSerializerSettings) : JsonConvert.SerializeObject(obj);
            return new StringContent(json);
        }

        protected class RestAndWebSocketUrls
        {
            public string UrlREST { get; set; }
            public string UrlWebSocket { get; set; }
        }
        protected class ClientCharacteristicsRequestObject : IRequestObject
        {
            public ClientCharacteristicsRequestObject(string id, ClientCharacteristics clientCharacteristics)
            {
                Id = id;
                ClientCharacteristics = clientCharacteristics;
            }

            public ClientCharacteristics ClientCharacteristics { get; set; }
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

        protected class SetDeviceLabelRequestObject : IRequestObject
        {
            public SetDeviceLabelRequestObject(string deviceId, string label)
            {
                DeviceId = deviceId;
                Label = label;
            }
            public string DeviceId { get; set; }
            public string Label { get; set; }
        }

        protected class SetSwitchStateRequestObject : IRequestObject
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

        protected class SetExtendedZonesActivationRequestObject
        {
            public SetExtendedZonesActivationRequestObject(bool ignoreLowBat, bool activateExternalZone, bool activateInternalZone)
            {
                IgnoreLowBat = ignoreLowBat;
                ZonesActivation = new ZonesActivation { External = activateExternalZone, Internal = activateInternalZone };
            }
            [JsonProperty(PropertyName = "ignoreLowBat")]
            public bool IgnoreLowBat { get; set; }
            [JsonProperty(PropertyName = "zonesActivation")]
            public ZonesActivation ZonesActivation { get; set; }
        }

        protected class SetSimpleRGBColorDimLevelRequestObject
        {
            public SetSimpleRGBColorDimLevelRequestObject(int channelIndex, string deviceId, string simpleRGBColorState, double dimLevel)
            {
                DeviceId = deviceId;
                ChannelIndex = channelIndex;
                SimpleRGBColorState = simpleRGBColorState;
                DimLevel = dimLevel;
            }
            public int ChannelIndex { get; set; }
            public string DeviceId { get; set; }
            /// <summary>
            /// YELLOW, RED, GREEN etc.
            /// </summary>
            public string SimpleRGBColorState { get; set; }
            /// <summary>
            /// Min: 0, Max: 1
            /// 0 = off
            /// 0.50 = 50%
            /// 1 = on
            /// </summary>
            public double DimLevel { get; set; }
        }

        protected class ZonesActivation
        {
            [JsonProperty(PropertyName = "EXTERNAL")]
            public bool External { get; set; }
            [JsonProperty(PropertyName = "INTERNAL")]
            public bool Internal { get; set; }
        }

        protected class SetActiveProfileRequestObject
        {
            public SetActiveProfileRequestObject(string groupId, string profileIndex)
            {
                GroupId = groupId;
                ProfileIndex = profileIndex;
            }
            public string GroupId { get; set; }
            public string ProfileIndex { get; set; }
        }

        protected class SetControlModeRequestObject
        {
            public SetControlModeRequestObject(string groupId, ClimateControlMode controlMode)
            {
                GroupId = groupId;
                ControlMode = controlMode;
            }
            public string GroupId { get; set; }
            public ClimateControlMode ControlMode { get; set; }
        }

        protected class SetBoostRequestObject
        {
            public SetBoostRequestObject(bool boost, string groupId)
            {
                Boost = boost;
                GroupId = groupId;
            }
            public bool Boost { get; set; }
            public string GroupId { get; set; }
        }

        protected class ActivatePartyModeRequestObject
        {
            public ActivatePartyModeRequestObject(string groupId, string endTime, double temperature)
            {
                GroupId = groupId;
                EndTime = endTime;
                Temperature = temperature;
            }
            public string GroupId { get; set; }
            public string EndTime { get; set; }
            public double Temperature { get; set; }
        }

        protected class SendDoorCommandRequestObject
        {
            public SendDoorCommandRequestObject(int channelIndex, string deviceId, DoorCommandType doorCommand)
            {
                ChannelIndex = channelIndex;
                DeviceId = deviceId;
                DoorCommand = doorCommand;
            }
            public int ChannelIndex { get; set; }
            public string DeviceId { get; set; }
            public DoorCommandType DoorCommand { get; set; }
        }

        protected class RegisterGCMRequestObject
        {
            public RegisterGCMRequestObject(string registrationId)
            {
                RegistrationId = registrationId;
            }
            public string RegistrationId { get; set; }
        }

        protected class RegisterAPNSRequestObject
        {
            public RegisterAPNSRequestObject(string clientToken)
            {
                ClientToken = clientToken;
            }
            public string ClientToken { get; set; }
        }

        protected class ActivateVacationRequestObject
        {
            public ActivateVacationRequestObject(string endTime, double temperature)
            {
                EndTime = endTime;
                Temperature = temperature;
            }
            public string EndTime { get; set; }
            public double Temperature { get; set; }
        }

        protected class SetEcoDurationRequestObject
        {
            public SetEcoDurationRequestObject(EcoDuration ecoDuration)
            {
                EcoDuration = ecoDuration;
            }
            public EcoDuration EcoDuration { get; set; }
        }

        protected class GetProfileRequestObject
        {
            public GetProfileRequestObject(string groupId, string profileIndex)
            {
                GroupId = groupId;
                ProfileIndex = profileIndex;
            }
            public string GroupId { get; set; }
            public string ProfileIndex { get; set; }
        }

        protected class UpdateProfileRequestObject
        {
            public UpdateProfileRequestObject(string groupId, string profileIndex, HomematicIp.Data.HomematicIpObjects.Groups.Profile profile)
            {
                GroupId = groupId;
                ProfileIndex = profileIndex;

                Profile = new SendProfile
                {
                    GroupId = groupId,
                    HomeId = profile.HomeId,
                    Id = profile.Id,
                    Index = profile.Index,
                    Name = profile.Name,
                    Type = profile.Type,

                    ProfileDays = new SendProfileDays
                    {
                        Monday = new SendDay
                        {
                            BaseValue = profile.ProfileDays.Monday.BaseValue,
                            DayOfWeek = profile.ProfileDays.Monday.DayOfWeek,
                            Periods = ToSendProfile(profile.ProfileDays.Monday.Periods)
                        },
                        Tuesday = new SendDay
                        {
                            BaseValue = profile.ProfileDays.Tuesday.BaseValue,
                            DayOfWeek = profile.ProfileDays.Tuesday.DayOfWeek,
                            Periods = ToSendProfile(profile.ProfileDays.Tuesday.Periods)
                        },
                        Wednesday = new SendDay
                        {
                            BaseValue = profile.ProfileDays.Wednesday.BaseValue,
                            DayOfWeek = profile.ProfileDays.Wednesday.DayOfWeek,
                            Periods = ToSendProfile(profile.ProfileDays.Wednesday.Periods)
                        },
                        Thursday = new SendDay
                        {
                            BaseValue = profile.ProfileDays.Thursday.BaseValue,
                            DayOfWeek = profile.ProfileDays.Thursday.DayOfWeek,
                            Periods = ToSendProfile(profile.ProfileDays.Thursday.Periods)
                        },
                        Friday = new SendDay
                        {
                            BaseValue = profile.ProfileDays.Friday.BaseValue,
                            DayOfWeek = profile.ProfileDays.Friday.DayOfWeek,
                            Periods = ToSendProfile(profile.ProfileDays.Friday.Periods)
                        },
                        Saturday = new SendDay
                        {
                            BaseValue = profile.ProfileDays.Saturday.BaseValue,
                            DayOfWeek = profile.ProfileDays.Saturday.DayOfWeek,
                            Periods = ToSendProfile(profile.ProfileDays.Saturday.Periods)
                        },
                        Sunday = new SendDay
                        {
                            BaseValue = profile.ProfileDays.Sunday.BaseValue,
                            DayOfWeek = profile.ProfileDays.Sunday.DayOfWeek,
                            Periods = ToSendProfile(profile.ProfileDays.Sunday.Periods)
                        }
                    }
                };

                static List<SendPeriod> ToSendProfile(List<Data.HomematicIpObjects.Groups.Period> periods)
                {
                    var mondayPeriods = new List<SendPeriod>();
                    foreach (var mondayPeriod in periods)
                    {
                        mondayPeriods.Add(new SendPeriod
                        {
                            EndTime = mondayPeriod.EndTime,
                            StartTime = mondayPeriod.StartTime,
                            Value = mondayPeriod.Value,
                            EndTimeAsMinutesOfDay = mondayPeriod.EndTimeAsMinutesOfDay,
                            StartTimeAsMinutesOfDay = mondayPeriod.StartTimeAsMinutesOfDay
                        });
                    }
                    return mondayPeriods;
                }
            }

            [JsonProperty(PropertyName = "groupId")]
            public string GroupId { get; set; }
            [JsonProperty(PropertyName = "profileIndex")]
            public string ProfileIndex { get; set; }

            [JsonProperty(PropertyName = "profile")]
            public SendProfile Profile { get; set; }

            public class SendProfile
            {
                [JsonProperty(PropertyName = "groupId")]
                public string GroupId { get; set; }
                [JsonProperty(PropertyName = "homeId")]
                public string HomeId { get; set; }
                [JsonProperty(PropertyName = "id")]
                public string Id { get; set; }
                [JsonProperty(PropertyName = "index")]
                public string Index { get; set; }
                [JsonProperty(PropertyName = "name")]
                public string Name { get; set; }
                [JsonProperty(PropertyName = "profileDays")]
                public SendProfileDays ProfileDays { get; set; } = new SendProfileDays();
                [JsonProperty(PropertyName = "type")]
                public string Type { get; set; }
            }
            public class SendProfileDays
            {
                [JsonProperty("SATURDAY")]
                public SendDay Saturday { get; set; } = new SendDay();

                [JsonProperty("TUESDAY")]
                public SendDay Tuesday { get; set; } = new SendDay();

                [JsonProperty("FRIDAY")]
                public SendDay Friday { get; set; } = new SendDay();

                [JsonProperty("WEDNESDAY")]
                public SendDay Wednesday { get; set; } = new SendDay();

                [JsonProperty("THURSDAY")]
                public SendDay Thursday { get; set; } = new SendDay();

                [JsonProperty("MONDAY")]
                public SendDay Monday { get; set; } = new SendDay();

                [JsonProperty("SUNDAY")]
                public SendDay Sunday { get; set; } = new SendDay();
            }

            public class SendDay
            {
                [JsonProperty("baseValue")]
                public double BaseValue { get; set; }

                [JsonProperty("dayOfWeek")]
                public string DayOfWeek { get; set; }

                [JsonProperty("periods")]
                public List<SendPeriod> Periods { get; set; } = new List<SendPeriod>();
            }

            public class SendPeriod
            {
                //e.g 22:00
                [JsonProperty("endtime")]
                public string EndTime { get; set; }
                //e.g 17:00
                [JsonProperty("starttime")]
                public string StartTime { get; set; }

                [JsonProperty("value")]
                public double Value { get; set; }

                [JsonProperty("endtimeAsMinutesOfDay")]
                public int EndTimeAsMinutesOfDay { get; set; }

                [JsonProperty("starttimeAsMinutesOfDay")]
                public int StartTimeAsMinutesOfDay { get; set; }
            }
        }

        protected class SetSwitchGroupStateRequestObject
        {
            public SetSwitchGroupStateRequestObject(string groupId, bool state)
            {
                GroupId = groupId;
                On = state;
            }
            public string GroupId { get; set; }
            public bool On { get; set; }
        }

        protected class SetSwitchGroupStateWithTimeRequestObject
        {
            public SetSwitchGroupStateWithTimeRequestObject(string groupId, bool state, double onTime)
            {
                GroupId = groupId;
                On = state;
                OnTime = onTime;
            }
            public string GroupId { get; set; }
            public bool On { get; set; }
            public double OnTime { get; set; }
        }

        protected class SetDimGroupLevelRequestObject
        {
            public SetDimGroupLevelRequestObject(string groupId, double dimLevel)
            {
                GroupId = groupId;
                DimLevel = dimLevel;
            }
            public string GroupId { get; set; }
            /// <summary>
            /// Min: 0, Max: 1
            /// 0 = off
            /// 0.50 = 50%
            /// 1 = on
            /// </summary>
            public double DimLevel { get; set; }
        }

        protected class SetDimGroupLevelWithTimeRequestObject
        {
            public SetDimGroupLevelWithTimeRequestObject(string groupId, double dimLevel, double onTime, double rampTime)
            {
                GroupId = groupId;
                DimLevel = dimLevel;
                OnTime = onTime;
                RampTime = rampTime;
            }
            public string GroupId { get; set; }
            /// <summary>
            /// Min: 0, Max: 1
            /// 0 = off
            /// 0.50 = 50%
            /// 1 = on
            /// </summary>
            public double DimLevel { get; set; }
            public double OnTime { get; set; }
            public double RampTime { get; set; }
        }

        protected class StopGroupRequestObject
        {
            public StopGroupRequestObject(string groupId)
            {
                GroupId = groupId;
            }
            public string GroupId { get; set; }
        }

        protected class SetPrimaryShadingLevelRequestObject
        {
            public SetPrimaryShadingLevelRequestObject(string groupId, double primaryShadingLevel)
            {
                GroupId = groupId;
                PrimaryShadingLevel = primaryShadingLevel;
            }
            public string GroupId { get; set; }
            /// <summary>
            /// Min: 0, Max: 1
            /// 0 = closed
            /// 0.50 = 50%
            /// 1 = opened
            /// </summary>
            public double PrimaryShadingLevel { get; set; }
        }

        protected class SetSecondaryShadingLevelRequestObject
        {
            public SetSecondaryShadingLevelRequestObject(string groupId, double primaryShadingLevel, double shutterLevel, double slatsLevel, double secondaryShadingLevel)
            {
                GroupId = groupId;
                PrimaryShadingLevel = primaryShadingLevel;
                ShutterLevel = shutterLevel;
                SlatsLevel = slatsLevel;
                SecondaryShadingLevel = secondaryShadingLevel;
            }
            public string GroupId { get; set; }
            /// <summary>
            /// Min: 0, Max: 1
            /// 0 = closed
            /// 0.50 = 50%
            /// 1 = opened
            /// </summary>
            public double PrimaryShadingLevel { get; set; }
            public double ShutterLevel { get; set; }
            public double SlatsLevel { get; set; }
            public double SecondaryShadingLevel { get; set; }
        }

        protected class SetDevicePrimaryShadingLevelRequestObject
        {
            public SetDevicePrimaryShadingLevelRequestObject(string deviceId, int channelIndex, double primaryShadingLevel)
            {
                DeviceId = deviceId;
                ChannelIndex = channelIndex;
                PrimaryShadingLevel = primaryShadingLevel;
            }
            public string DeviceId { get; set; }
            public int ChannelIndex { get; set; }
            /// <summary>
            /// Min: 0, Max: 1
            /// 0 = closed
            /// 0.50 = 50%
            /// 1 = opened
            /// </summary>
            public double PrimaryShadingLevel { get; set; }
        }

        protected class SetDeviceSecondaryShadingLevelRequestObject
        {
            public SetDeviceSecondaryShadingLevelRequestObject(string deviceId, int channelIndex, double primaryShadingLevel, double secondaryShadingLevel)
            {
                DeviceId = deviceId;
                ChannelIndex = channelIndex;
                PrimaryShadingLevel = primaryShadingLevel;
                SecondaryShadingLevel = secondaryShadingLevel;
            }
            public string DeviceId { get; set; }
            public int ChannelIndex { get; set; }
            /// <summary>
            /// Min: 0, Max: 1
            /// 0 = closed
            /// 0.50 = 50%
            /// 1 = opened
            /// </summary>
            public double PrimaryShadingLevel { get; set; }
            public double SecondaryShadingLevel { get; set; }
        }

        protected class StopDeviceRequestObject
        {
            public StopDeviceRequestObject(string deviceId, int channelIndex)
            {
                DeviceId = deviceId;
                ChannelIndex = channelIndex;
            }
            public string DeviceId { get; set; }
            public int ChannelIndex { get; set; }
        }

        protected class SetLockStateRequestObject
        {
            public SetLockStateRequestObject(string deviceId, int channelIndex, string authorizationPin, LockState targetLockState)
            {
                DeviceId = deviceId;
                ChannelIndex = channelIndex;
                AuthorizationPin = authorizationPin;
                TargetLockState = targetLockState;
            }
            public string DeviceId { get; set; }
            public int ChannelIndex { get; set; }
            public string AuthorizationPin { get; set; }
            public LockState TargetLockState { get; set; }
        }

        protected class SetLockStateResponseObject
        {
            public string ErrorCode { get; set; }
            public long BlockingTime { get; set; }
        }

        protected class ActivateAbsenceWithPeriodRequestObject
        {
            public ActivateAbsenceWithPeriodRequestObject(string endTime)
            {
                EndTime = endTime;
            }

            public string EndTime { get; set; }
        }

        protected class ActivateAbsenceWithDurationRequestObject
        {
            public ActivateAbsenceWithDurationRequestObject(int duration)
            {
                Duration = duration;
            }

            public int Duration { get; set; }
        }
        protected class SetHotWaterProfileModeRequestObject
        {
            public SetHotWaterProfileModeRequestObject(string groupId, string profileMode)
            {
                GroupId = groupId;
                ProfileMode = profileMode;
            }
            public string GroupId { get; set; }
            public string ProfileMode { get; set; }
        }

        protected class SetHotWaterStateRequestObject : IRequestObject
        {
            public SetHotWaterStateRequestObject(string groupId, bool state)
            {
                GroupId = groupId;
                On = state;
            }
            public string GroupId { get; set; }
            public bool On { get; set; }
        }

        protected class StartImpulseRequestObject
        {
            public StartImpulseRequestObject(string deviceId, int channelIndex)
            {
                DeviceId = deviceId;
                ChannelIndex = channelIndex;
            }
            public string DeviceId { get; set; }
            public int ChannelIndex { get; set; }
        }

        protected class SetFloorHeatingSpecificGroupActiveRequestObject
        {
            public SetFloorHeatingSpecificGroupActiveRequestObject(string deviceId, int channelIndex, bool state)
            {
                DeviceId = deviceId;
                ChannelIndex = channelIndex;
                FloorHeatingspecificGroupActive = state;
            }
            public string DeviceId { get; set; }
            public int ChannelIndex { get; set; }
            public bool FloorHeatingspecificGroupActive { get; set; }
        }

        public class ClientCharacteristics
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