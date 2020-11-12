using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HomematicIp.Data;
using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects;
using HomematicIp.Data.HomematicIpObjects.Devices;
using HomematicIp.Data.HomematicIpObjects.Home;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HomematicIp.Services
{
    public class HomematicService : HomematicServiceBase
    {
        public static IDictionary<string, string> UnsupportedTypes { get; set; } = new Dictionary<string, string>();

        public WebSocketState WebSocketState => _clientWebSocket?.State ?? WebSocketState.None;

        private readonly ClientWebSocket _clientWebSocket;
        private readonly ILogger<HomematicService> _logger;
        protected const string AUTHTOKEN = "AUTHTOKEN";
        public HomematicService(Func<HttpClient> httpClientFactory, HomematicConfiguration homematicConfiguration, ClientWebSocket clientWebSocket, ILogger<HomematicService> logger) : base(httpClientFactory, homematicConfiguration)
        {
            _clientWebSocket = clientWebSocket;
            _logger = logger;
        }

        protected override async Task Initialize(
            ClientCharacteristicsRequestObject clientCharacteristicsRequestObject = null, CancellationToken cancellationToken = default)
        {
            await base.Initialize(clientCharacteristicsRequestObject, cancellationToken);

            if (!HttpClient.DefaultRequestHeaders.Contains(AUTHTOKEN))
                HttpClient.DefaultRequestHeaders.Add(AUTHTOKEN, HomematicConfiguration.AuthToken);

            _clientWebSocket.Options.SetRequestHeader(AUTHTOKEN, HomematicConfiguration.AuthToken);
            _clientWebSocket.Options.SetRequestHeader(CLIENTAUTH, ClientAuthToken);
        }

        public async Task ConnectAsync(CancellationToken cancellationToken = default)
        {
            await Initialize(cancellationToken: cancellationToken);
        }

        public async Task<HomematicIpEnvironment> GetCurrentState(CancellationToken cancellationToken = default)
        {
            var httpResponseMessage = await HttpClient.PostAsync("hmip/home/getCurrentState", ClientCharacteristicsStringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var content = await httpResponseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<HomematicIpEnvironment>(content);
            }
            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        public async Task SetPin(string pin, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetPinRequestObject(pin);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/home/setPin", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode) return;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        public async Task StartDeviceInclusionProcess(CancellationToken cancellationToken = default)
        {
            var httpResponseMessage = await HttpClient.PostAsync("hmip/home/startDeviceInclusionProcess", ClientCharacteristicsStringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode) return;
            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        public async Task<Device> WaitForPairingResponse(CancellationToken cancellationToken = default)
        {
            var tcs = new TaskCompletionSource<Device>();
            var inclusionRequestedObservable = ReceiveEvents().Where(notification => notification.EventType == EventType.INCLUSION_REQUESTED);
            IDisposable disposeWhenFirstDeviceIsPaired = null;
            disposeWhenFirstDeviceIsPaired = inclusionRequestedObservable.Subscribe(async notification =>
            {
                await StartInclusionModeForDevice(notification.HomematicIpObjectBase.Id, cancellationToken);
                disposeWhenFirstDeviceIsPaired.Dispose(); //compiler warning can be ignored. disposeWhenFirstDeviceIsPaired is never null and cannot be disposed due to the awaited TaskCompletionSource.Task
                tcs.TrySetResult(notification.HomematicIpObjectBase as Device);
            });
            return await tcs.Task;
        }
        /// <summary>
        /// Starts the inclusion process for a new device
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>The device that was included. Note that the device object only has its Id and DeviceType set at this point</returns>
        public async Task<Device> StartDeviceInclusionProcessAndWaitForPairingResponse(CancellationToken cancellationToken = default)
        {
            await StartDeviceInclusionProcess(cancellationToken);
            return await WaitForPairingResponse(cancellationToken);
        }

        private async Task StartInclusionModeForDevice(string deviceId, CancellationToken cancellationToken = default)
        {
            var requestObject = new StartInclusionModeForDeviceRequestObject(deviceId);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/home/startInclusionModeForDevice", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode) return;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        public async Task<bool> SetDeviceLabel(string deviceId, string label, CancellationToken cancellationToken = default)
        {
            // the label is the name of the device
            var requestObject = new SetDeviceLabelRequestObject(deviceId, label);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/device/setDeviceLabel", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        public async Task<bool> SetSwitchState(int channelIndex, string deviceId, bool state, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetSwitchStateRequestObject(channelIndex, deviceId, state);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/device/control/setSwitchState", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        public async Task<bool> SetDimLevel(int channelIndex, string deviceId, double dimLevel, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetDimLevelRequestObject(channelIndex, deviceId, dimLevel);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/device/control/setDimLevel", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        public async Task<bool> SetSlatsLevel(int channelIndex, string deviceId, double shutterLevel, double slatsLevel, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetSlatsLevelRequestObject(channelIndex, deviceId, shutterLevel, slatsLevel);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/device/control/setSlatsLevel", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        public async Task<bool> SetShutterLevel(int channelIndex, string deviceId, double shutterLevel, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetShutterLevelRequestObject(channelIndex, deviceId, shutterLevel);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/device/control/setShutterLevel", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        public async Task<bool> Stop(int channelIndex, string deviceId, CancellationToken cancellationToken = default)
        {
            var requestObject = new StopRequestObject(channelIndex, deviceId);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/device/control/stop", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        public async Task<bool> EnableSimpleRule(bool enabled, string ruleId, CancellationToken cancellationToken = default)
        {
            var requestObject = new EnableSimpleRuleRequestObject(enabled, ruleId);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/rule/enableSimpleRule", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        //https://srv04.homematic.com:6969/hmip/group/heating/setSetPointTemperature
        //{"groupId":"772882ff-4026-4de1-9de8-c4fe673d8a7b","setPointTemperature":21.5}
        public async Task<bool> SetSetPointTemperature(string groupId, double setPointTemperature, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetPointTemperatureRequestObject(groupId, setPointTemperature);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/group/heating/setSetPointTemperature", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        //https://srv04.homematic.com:6969/hmip/home/heating/setEcoTemperature
        //{"ecoTemperature":16.0}
        public async Task<bool> SetEcoTemperature(double ecoTemperature, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetEcoTemperatureRequestObject(ecoTemperature);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/home/heating/setEcoTemperature", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        public async Task<bool> RegisterFCM(string token, CancellationToken cancellationToken = default)
        {
            var requestObject = new RegisterFCMRequestObject(token);
            using (var stringContent = GetStringContent(requestObject))
            {
                using (var httpResponseMessage = await HttpClient.PostAsync("hmip/client/registerFCM", stringContent, cancellationToken))
                {
                    if (httpResponseMessage.IsSuccessStatusCode)
                        return true;

                    throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
                }
            }
        }

        public async Task<bool> RegisterGCM(string registrationId, CancellationToken cancellationToken = default)
        {
            var requestObject = new RegisterGCMRequestObject(registrationId);
            using var stringContent = GetStringContent(requestObject);
            using var httpResponseMessage = await HttpClient.PostAsync("hmip/client/registerGCM", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        public async Task<bool> UnregisterGCM(CancellationToken cancellationToken = default)
        {
            using var stringContent = new StringContent("");
            using var httpResponseMessage = await HttpClient.PostAsync("hmip/client/unregisterGCM", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        public async Task<bool> RegisterAPNS(string clientToken, CancellationToken cancellationToken = default)
        {
            var requestObject = new RegisterAPNSRequestObject(clientToken);
            using var stringContent = GetStringContent(requestObject);
            using var httpResponseMessage = await HttpClient.PostAsync("hmip/client/registerAPNS", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        public async Task<bool> UnregisterAPNS(CancellationToken cancellationToken = default)
        {
            using var stringContent = new StringContent("");
            using var httpResponseMessage = await HttpClient.PostAsync("hmip/client/unregisterAPNS", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        public async Task<ZonesActivationResult> SetExtendedZonesActivation(bool ignoreLowBat, bool activiateExternalZone, bool activiateInternalZone, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetExtendedZonesActivationRequestObject(ignoreLowBat, activiateExternalZone, activiateInternalZone);
            using var stringContent = GetStringContent(requestObject, false);
            using var httpResponseMessage = await HttpClient.PostAsync("hmip/home/security/setExtendedZonesActivation", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var content = await httpResponseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ZonesActivationResult>(content);
            }
            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }
        //https://srv08.homematic.com:6969/hmip/device/control/setSimpleRGBColorDimLevel
        //{"channelIndex":2,"deviceId":"3014F711A0001A5A498A9238","dimLevel":1.0,"simpleRGBColorState":"GREEN"}
        public async Task<bool> SetSimpleRGBColorDimLevel(int channelIndex, string deviceId, string simpleRGBColorState, double dimLevel = 1.0d, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetSimpleRGBColorDimLevelRequestObject(channelIndex, deviceId, simpleRGBColorState, dimLevel);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/device/control/setSimpleRGBColorDimLevel", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        /// <summary>
        /// Activate the profile on the applicable devices in the heating group.
        /// Possible event bus notifications: DEVICE_CHANGED, GROUP_CHANGED
        /// </summary>
        /// <param name="groupId">The identifier of the heating group of which the active profile should be set.</param>
        /// <param name="profileIndex">The index of the profile that should be activated.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> SetActiveProfile(string groupId, string profileIndex, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetActiveProfileRequestObject(groupId, profileIndex);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/group/heating/setActiveProfile", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        //hmip/home/group/heating/setControlMode
        //set menu mode: {"controlMode":"MANUAL","groupId":"5ba14748-1b83-4bd6-aff0-2b11f8b5c361"}
        public async Task<bool> SetControlMode(string groupId, ClimateControlMode controlMode = ClimateControlMode.AUTOMATIC, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetControlModeRequestObject(groupId, controlMode);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/group/heating/setControlMode", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        //hmip/home/group/heating/setBoost
        //{"boost":true,"groupId":"5ba14748-1b83-4bd6-aff0-2b11f8b5c361"}
        public async Task<bool> SetBoost(string groupId, bool boost, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetBoostRequestObject(boost, groupId);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/group/heating/setBoost", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        public async Task<bool> ActivatePartyMode(string groupId, DateTime endTime, double temperature, CancellationToken cancellationToken = default)
        {
            var endTimeFormatted = endTime.ToString("yyyy_MM_dd HH:MM");
            var requestObject = new ActivatePartyModeRequestObject(groupId, endTimeFormatted, temperature);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/group/heating/activatePartyMode", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        public async Task<bool> SendDoorCommand(int channelIndex, string deviceId, DoorCommandType doorCommand, CancellationToken cancellationToken = default)
        {
            var requestObject = new SendDoorCommandRequestObject(channelIndex, deviceId, doorCommand);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/device/control/sendDoorCommand", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        /// <summary>
        /// Activate the vacation mode. 
        /// Possible event bus notifications HOME_CHANGED. 
        /// Solution = INDOOR_CLIMATE
        /// </summary>
        /// <param name="endTime">The end time of the vacation mode</param>
        /// <param name="temperature">The temperature 5-30 that should be used during vacation mode</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> ActivateVacation(DateTime endTime, double temperature, CancellationToken cancellationToken = default)
        {
            var endTimeFormatted = endTime.ToString("yyyy_MM_dd HH:MM");
            var requestObject = new ActivateVacationRequestObject(endTimeFormatted, temperature);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/home/heating/activateVacation", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        /// <summary>
        /// Deactivate the vacation mode. 
        /// Possible event bus notifications HOME_CHANGED. 
        /// Solution = INDOOR_CLIMATE
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> DeactivateVacation(CancellationToken cancellationToken = default)
        {
            using var stringContent = new StringContent("");
            using var httpResponseMessage = await HttpClient.PostAsync("hmip/home/heating/deactivateVacation", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        /// <summary>
        /// Set the default duration of the economy mode if the eco push button is pressed. 
        /// Possible event bus notifications HOME_CHANGED.
        /// Solution = INDOOR_CLIMATE
        /// </summary>
        /// <param name="ecoDuration">The duration of the economy mode for activation with the eco push button</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> SetEcoDuration(EcoDuration ecoDuration, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetEcoDurationRequestObject(ecoDuration);
            using var stringContent = GetStringContent(requestObject);
            using var httpResponseMessage = await HttpClient.PostAsync("hmip/home/heating/setEcoDuration", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        /// <summary>
        /// Get the profile of the heating group with the given index. 
        /// Solution = INDOOR_CLIMATE
        /// </summary>
        /// <param name="groupId">The identifier of the heating group of which the profileis requested.</param>
        /// <param name="profileIndex">The index of the profile that is requested. e.g PROFILE_1</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Data.HomematicIpObjects.Groups.Profile> GetProfile(string groupId, string profileIndex, CancellationToken cancellationToken = default)
        {
            var requestObject = new GetProfileRequestObject(groupId, profileIndex);
            using var stringContent = GetStringContent(requestObject);
            using var httpResponseMessage = await HttpClient.PostAsync("hmip/group/heating/getProfile", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var content = await httpResponseMessage.Content.ReadAsStringAsync();
                var profileDays = JsonConvert.DeserializeObject<Data.HomematicIpObjects.Groups.ProfileDays>(content);
                return new Data.HomematicIpObjects.Groups.Profile
                {
                    ProfileDays = profileDays,
                    GroupId = groupId,
                    ProfileId = profileIndex,
                    Index = profileIndex
                };
            }
            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        /// <summary>
        /// Update the profile of a heating group.
        /// Possible event bus notifications: GROUP_CHANGED
        /// Solution = INDOOR_CLIMATE
        /// </summary>
        /// <param name="groupId">The identifier of the heating group of which the profile should be updated.</param>
        /// <param name="profileIndex">The index of the profile that should be updated.</param>
        /// <param name="profile">The profile with its new values.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> UpdateProfile(string groupId, string profileIndex, Data.HomematicIpObjects.Groups.Profile profile, CancellationToken cancellationToken = default)
        {
            var requestObject = new UpdateProfileRequestObject(groupId, profileIndex, profile);
            using var stringContent = GetStringContent(requestObject);
            using var httpResponseMessage = await HttpClient.PostAsync("hmip/group/heating/updateProfile", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        //copyProfile
        //setProfileLabel
        //setProfileVisible

        private readonly Subject<EventNotification> _subject = new Subject<EventNotification>();
        private Task _webSocketReceiveTask;
        private int _receiveEventsIsEntered;
        public IObservable<EventNotification> ReceiveEvents(CancellationToken cancellationToken = default)
        {
            if (Interlocked.CompareExchange(ref _receiveEventsIsEntered, 1, 0) != 0) return _subject;
            if (_webSocketReceiveTask == null || _webSocketReceiveTask.IsCompleted)
            {
                var buffer = new byte[1024];
                var list = new List<byte>();
                _webSocketReceiveTask = Task.Run(async () =>
                {
                    await _clientWebSocket.ConnectAsync(new Uri(UrlWebSocket), cancellationToken);
                    while (_clientWebSocket.State == WebSocketState.Open)
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            await _clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                            _subject.OnCompleted();
                            break;
                        }
                        try
                        {
                            var result = await _clientWebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);
                            switch (result.MessageType)
                            {
                                case WebSocketMessageType.Text:
                                    break;
                                case WebSocketMessageType.Binary:
                                    list.AddRange(buffer.Take(result.Count));
                                    if (result.EndOfMessage)
                                    {
                                        var msgString = Encoding.UTF8.GetString(list.ToArray());
                                        _logger?.LogDebug($"Message received: {msgString}");
                                        var msg = JsonConvert.DeserializeObject<JObject>(msgString);
                                        foreach (var hEvent in msg["events"].Values())
                                        {
                                            Enum.TryParse(hEvent["pushEventType"].Value<string>(), out EventType eventType);
                                            HomematicIpObjectBase homematicIpObjectBase = null;

                                            switch (eventType)
                                            {
                                                case EventType.SECURITY_JOURNAL_CHANGED:
                                                    break;
                                                case EventType.GROUP_ADDED:
                                                case EventType.GROUP_CHANGED:
                                                case EventType.GROUP_REMOVED:
                                                    Enum.TryParse(hEvent["group"]["type"].Value<string>(), out GroupType groupType);
                                                    var rawGroup = hEvent["group"].ToString();
                                                    var type = EnumToType.GetType(groupType, rawGroup);
                                                    var typedGroup = JsonConvert.DeserializeObject(rawGroup, type);
                                                    homematicIpObjectBase = typedGroup as HomematicIpObjectBase;
                                                    if (homematicIpObjectBase != null)
                                                        homematicIpObjectBase.RawJson = rawGroup;
                                                    break;
                                                case EventType.DEVICE_REMOVED:
                                                case EventType.DEVICE_CHANGED:
                                                case EventType.DEVICE_ADDED:
                                                    Enum.TryParse(hEvent["device"]["type"].Value<string>(), out DeviceType deviceType);
                                                    var rawDevice = hEvent["device"].ToString();
                                                    var dType = EnumToType.GetType(deviceType, rawDevice);
                                                    var typedDevice = JsonConvert.DeserializeObject(rawDevice, dType);
                                                    homematicIpObjectBase = typedDevice as HomematicIpObjectBase;
                                                    if (homematicIpObjectBase != null)
                                                        homematicIpObjectBase.RawJson = rawDevice;
                                                    break;
                                                case EventType.CLIENT_REMOVED:
                                                case EventType.CLIENT_CHANGED:
                                                case EventType.CLIENT_ADDED:
                                                    break;
                                                case EventType.HOME_CHANGED:
                                                    var rawHome = hEvent["home"].ToString();
                                                    homematicIpObjectBase = JsonConvert.DeserializeObject<Home>(rawHome);
                                                    homematicIpObjectBase.RawJson = rawHome;
                                                    break;
                                                case EventType.INCLUSION_REQUESTED:
                                                    Enum.TryParse(hEvent["deviceType"].Value<string>(), out DeviceType deviceTypeRequested);
                                                    Enum.TryParse(hEvent["errorReason"].Value<string>(), out ErrorReasonType errorReasonType);
                                                    var deviceId = hEvent["deviceId"].Value<string>();
                                                    homematicIpObjectBase = new Device { Id = deviceId, DeviceType = deviceTypeRequested };
                                                    break;
                                                default:
                                                    throw new ArgumentOutOfRangeException();
                                            }
                                            if (homematicIpObjectBase != null)
                                            {
                                                _subject.OnNext(new EventNotification { EventType = eventType, HomematicIpObjectBase = homematicIpObjectBase });
                                            }
                                        }
                                        list.Clear();
                                    }
                                    break;
                                case WebSocketMessageType.Close:
                                    await _clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, cancellationToken);
                                    _subject.OnCompleted();
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                        catch (TaskCanceledException)
                        {
                            if (!cancellationToken.IsCancellationRequested)
                                throw;
                        }
                    }
                });
            }

            _receiveEventsIsEntered = 0;
            return _subject;
        }
    }
}