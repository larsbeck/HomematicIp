﻿using HomematicIp.Data;
using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects;
using HomematicIp.Data.HomematicIpObjects.Devices;
using HomematicIp.Data.HomematicIpObjects.Home;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

namespace HomematicIp.Services
{
    public class HomematicService : HomematicServiceBase
    {
        public static IDictionary<string, string> UnsupportedTypes { get; set; } = new Dictionary<string, string>();

        public WebSocketState WebSocketState => _clientWebSocket?.State ?? WebSocketState.None;

        private readonly ClientWebSocket _clientWebSocket;
        private readonly ILogger<HomematicService> _logger;
        protected const string AUTHTOKEN = "AUTHTOKEN";
        protected const string PIN_HEADER_KEY = "PIN";

        public HomematicService(Func<HttpClient> httpClientFactory, HomematicConfiguration homematicConfiguration, ClientWebSocket clientWebSocket, ILogger<HomematicService> logger, ClientCharacteristics clientCharacteristics) : base(httpClientFactory, homematicConfiguration, clientCharacteristics)
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

        public async Task<bool> SetPin(string pin, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetPinRequestObject(pin);
            return await Set(requestObject, "hmip/device/setPin", cancellationToken);
        }

        public async Task<bool> SetDeviceLabel(string deviceId, string label, CancellationToken cancellationToken = default)
        {
            // the label is the name of the device
            var requestObject = new SetDeviceLabelRequestObject(deviceId, label);
            return await Set(requestObject, "hmip/device/setDeviceLabel", cancellationToken);
        }


        private async Task<bool> Set(IRequestObject requestObject, string url, CancellationToken cancellationToken = default)
        {
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync(url, stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        public async Task<bool> SetSwitchState(int channelIndex, string deviceId, bool state, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetSwitchStateRequestObject(channelIndex, deviceId, state);
            return await Set(requestObject, "hmip/device/control/setSwitchState", cancellationToken);
        }

        public async Task<bool> SetDimLevel(int channelIndex, string deviceId, double dimLevel, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetDimLevelRequestObject(channelIndex, deviceId, dimLevel);
            return await Set(requestObject, "hmip/device/control/setDimLevel", cancellationToken);
        }

        public async Task<bool> SetSlatsLevel(int channelIndex, string deviceId, double shutterLevel, double slatsLevel, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetSlatsLevelRequestObject(channelIndex, deviceId, shutterLevel, slatsLevel);
            return await Set(requestObject, "hmip/device/control/setSlatsLevel", cancellationToken);
        }

        public async Task<bool> SetShutterLevel(int channelIndex, string deviceId, double shutterLevel, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetShutterLevelRequestObject(channelIndex, deviceId, shutterLevel);
            return await Set(requestObject, "hmip/device/control/setShutterLevel", cancellationToken);
        }

        public async Task<bool> Stop(int channelIndex, string deviceId, CancellationToken cancellationToken = default)
        {
            var requestObject = new StopRequestObject(channelIndex, deviceId);
            return await Set(requestObject, "hmip/device/control/stop", cancellationToken);
        }

        public async Task<bool> EnableSimpleRule(bool enabled, string ruleId, CancellationToken cancellationToken = default)
        {
            var requestObject = new EnableSimpleRuleRequestObject(enabled, ruleId);
            return await Set(requestObject, "hmip/rule/enableSimpleRule", cancellationToken);
        }

        public async Task<bool> SetSetPointTemperature(string groupId, double setPointTemperature, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetPointTemperatureRequestObject(groupId, setPointTemperature);
            return await Set(requestObject, "hmip/group/heating/setSetPointTemperature", cancellationToken);
        }

        public async Task<bool> SetEcoTemperature(double ecoTemperature, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetEcoTemperatureRequestObject(ecoTemperature);
            return await Set(requestObject, "hmip/home/heating/setEcoTemperature", cancellationToken);
        }

        public async Task<bool> RegisterFCM(string token, CancellationToken cancellationToken = default)
        {
            var requestObject = new RegisterFCMRequestObject(token);
            return await Set(requestObject, "hmip/client/registerFCM", cancellationToken);
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
            var endTimeFormatted = endTime.ToString("yyyy_MM_dd HH:mm");
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
        /// <param name="temperature">The temperature 5-30 that should be used during vacation mode. The default ist 17.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> ActivateVacation(DateTime endTime, double temperature, CancellationToken cancellationToken = default)
        {
            var endTimeFormatted = endTime.ToString("yyyy_MM_dd HH:mm");
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
        public async Task<Data.HomematicIpObjects.Groups.Profile> GetProfile(string groupId, string profileIndex, string profileName, CancellationToken cancellationToken = default)
        {
            var requestObject = new GetProfileRequestObject(groupId, profileIndex, profileName);
            using var stringContent = GetStringContent(requestObject);
            using var httpResponseMessage = await HttpClient.PostAsync("hmip/group/heating/getProfile", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var content = await httpResponseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Data.HomematicIpObjects.Groups.Profile>(content);
            }
            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}. Content: {await httpResponseMessage.Content.ReadAsStringAsync()}");
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
            using var stringContent = GetStringContent(requestObject, useCamelCaseProperties: false);
            using var httpResponseMessage = await HttpClient.PostAsync("hmip/group/heating/updateProfile", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        //copyProfile
        //setProfileLabel
        //setProfileVisible

        /// <summary>
        /// Set the switch state of all devices of the applicable type of a group to the given value.
        /// Solution = LIGHT_AND_SHADOW
        /// Possible event bus notifications: DEVICE_CHANGED, GROUP_CHANGED
        /// Notes: This request is valid for all types of switching groups like room based, user created or predefined alarm light.
        /// </summary>
        /// <param name="groupId">The id of the affected group.</param>
        /// <param name="state">The desired switch state.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> SetSwitchGroupState(string groupId, bool state, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetSwitchGroupStateRequestObject(groupId, state);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/group/switching/setState", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        /// <summary>
        /// Sets the switch state of all devices of the applicable type of a group to the given value at a defined time.
        /// Notes: This request is valid for all types of switching groups like room based, user created or predefined alarm light.
        /// Possible event bus notifications: GROUP_CHANGED
        /// </summary>
        /// <param name="groupId">The id of the affected group.</param>
        /// <param name="state">The desired switch state.</param>
        /// <param name="onTime">Time when the switch state should be changed. Allowed values: 0.1 - 16383</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> SetSwitchGroupStateWithTime(string groupId, bool state, double onTime, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetSwitchGroupStateWithTimeRequestObject(groupId, state, onTime);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/group/switching/setSwitchStateWithTime", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        /// <summary>
        /// Sets the dimming level of all devices of the applicable type of a group to the given value.
        /// Solution = LIGHT_AND_SHADOW
        /// Possible event bus notifications: DEVICE_CHANGED, GROUP_CHANGED
        /// Notes: If value is > 0 also the state of switching actors of this group will be set to on, i.e. 0 to off.
        /// This request is valid for all types of switching groups like room based, user created or predefined
        /// alarm light.
        /// </summary>
        /// <param name="groupId">The id of the affected group.</param>
        /// <param name="dimLevel">The desired dimming level.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> SetDimGroupLevel(string groupId, double dimLevel, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetDimGroupLevelRequestObject(groupId, dimLevel);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/group/switching/setDimLevel", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        /// <summary>
        /// Sets the dimming level of all devices of the applicable type of a group to the given value at a defined time.
        /// Solution = LIGHT_AND_SHADOW
        /// Possible event bus notifications: DEVICE_CHANGED, GROUP_CHANGED
        /// Notes: This request is valid for all types of switching groups like room based, user created or predefined alarm light.
        /// </summary>
        /// <param name="groupId">The id of the affected group.</param>
        /// <param name="dimLevel">The desired dimming level.</param>
        /// <param name="onTime">The duration of switching on. Allowed values: 0.1 - 16383</param>
        /// <param name="rampTime">The switch-on ramp. Allowed values: 0.1 - 16383</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> SetDimGroupLevelWithTime(string groupId, double dimLevel, double onTime, double rampTime, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetDimGroupLevelWithTimeRequestObject(groupId, dimLevel, onTime, rampTime);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/group/switching/setDimLevel", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        /// <summary>
        /// Stop all moving shutter/blind of a group
        /// Solution = LIGHT_AND_SHADOW
        /// Notes: This request is valid for all types of switching groups like room based, user created or predefined alarm light.
        /// </summary>
        /// <param name="groupId">The id of the affected group.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> StopGroup(string groupId, CancellationToken cancellationToken = default)
        {
            var requestObject = new StopGroupRequestObject(groupId);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/group/switching/stop", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        /// <summary>
        /// Sets the primary shading level.
        /// </summary>
        /// <param name="groupId">The id of the affected group.</param>
        /// <param name="primaryShadingLevel">The shading level. Allowed values: 0.0 - 1.0</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> SetPrimaryShadingLevel(string groupId, double primaryShadingLevel, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetPrimaryShadingLevelRequestObject(groupId, primaryShadingLevel);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/group/switching/setPrimaryShadingLevel", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        public async Task<bool> SetDevicePrimaryShadingLevel(string deviceId, int channelIndex, double primaryShadingLevel, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetDevicePrimaryShadingLevelRequestObject(deviceId, channelIndex, primaryShadingLevel);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/device/control/setPrimaryShadingLevel", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        public async Task<bool> SetDeviceSecondaryShadingLevel(string deviceId, int channelIndex, double primaryShadingLevel, double secondaryShadingLevel, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetDeviceSecondaryShadingLevelRequestObject(deviceId, channelIndex, primaryShadingLevel, secondaryShadingLevel);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/device/control/setSecondaryShadingLevel", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        public async Task<bool> StopDevice(string deviceId, int channelIndex, CancellationToken cancellationToken = default)
        {
            var requestObject = new StopDeviceRequestObject(deviceId, channelIndex);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/device/control/stop", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        public async Task<bool> CheckPin(string pin, CancellationToken cancellationToken = default)
        {
            try
            {
                if (HttpClient.DefaultRequestHeaders.Contains(PIN_HEADER_KEY))
                    HttpClient.DefaultRequestHeaders.Remove(PIN_HEADER_KEY);

                HttpClient.DefaultRequestHeaders.Add(PIN_HEADER_KEY, pin);
                var httpResponseMessage = await HttpClient.PostAsync("hmip/home/checkPin", new StringContent(""), cancellationToken);
                if (httpResponseMessage.IsSuccessStatusCode)
                    return true;

                throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
            }
            finally
            {
                if (HttpClient.DefaultRequestHeaders.Contains(PIN_HEADER_KEY))
                    HttpClient.DefaultRequestHeaders.Remove(PIN_HEADER_KEY);
            }
        }

        public async Task<bool> SetLockState(string deviceId, int channelIndex, string authorizationPin, LockState targetLockState, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetLockStateRequestObject(deviceId, channelIndex, authorizationPin, targetLockState);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/device/control/setLockState", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(responseContent))
                throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");

            var response = JsonConvert.DeserializeObject<SetLockStateResponseObject>(responseContent);
            throw new Exception($"{response.ErrorCode}");
        }

        /// <summary>
        /// Activate the economy mode for the given period.
        /// Possible Eventbus Notifications: HOME_CHANGED
        /// </summary>
        /// <param name="endTime">The end time of the economy mode. Format: yyyy_MM_dd HH:mm</param>
        /// <param name="cancellationToken"></param>
        /// <returns>true if success</returns>
        public async Task<bool> ActivateAbsenceWithPeriod(DateTime endTime, CancellationToken cancellationToken = default)
        {
            var endTimeFormatted = endTime.ToString("yyyy_MM_dd HH:mm");
            var requestObject = new ActivateAbsenceWithPeriodRequestObject(endTimeFormatted);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/home/heating/activateAbsenceWithPeriod", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        /// <summary>
        /// Activate the economy mode for the given amount of minutes.
        /// Possible Eventbus Notifications: HOME_CHANGED
        /// </summary>
        /// <param name="duration">The amount of minutes that the economy mode should be activated.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> ActivateAbsenceWithDuration(EcoDuration ecoDuration, CancellationToken cancellationToken = default)
        {
            HttpResponseMessage httpResponseMessage;

            if (ecoDuration != EcoDuration.PERMANENT)
            {
                var duration = ecoDuration switch
                {
                    EcoDuration.ONE => 1 * 60,
                    EcoDuration.TWO => 2 * 60,
                    EcoDuration.FOUR => 4 * 60,
                    EcoDuration.SIX => 6 * 60,
                    _ => throw new NotImplementedException()
                };
                var requestObject = new ActivateAbsenceWithDurationRequestObject(duration);
                var stringContent = GetStringContent(requestObject);

                httpResponseMessage = await HttpClient.PostAsync("hmip/home/heating/activateAbsenceWithDuration", stringContent, cancellationToken);
            }
            else
                httpResponseMessage = await HttpClient.PostAsync("hmip/home/heating/activateAbsencePermanent", ClientCharacteristicsStringContent, cancellationToken);

            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        /// <summary>
        /// Deactivate the economy mode = Automatic Mode.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> DeactivateAbsence(CancellationToken cancellationToken = default)
        {
            var httpResponseMessage = await HttpClient.PostAsync("hmip/home/heating/deactivateAbsence", ClientCharacteristicsStringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }
        /// <summary>
        /// /hmip/group/heating/setHotWaterProfileMode
        /// {
        ///     "groupId": "1524ef3a-adfd-4b68-9014-017c1602892d",
        ///     "profileMode": "AUTOMATIC"
        /// }
        /// </summary>
        /// <param name="groupId">The HOT_WATER group id.</param>
        /// <param name="profileMode">Can be AUTOMATIC or MANUAL.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> SetHotWaterProfileMode(string groupId, string profileMode, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetHotWaterProfileModeRequestObject(groupId, profileMode);
            using var stringContent = GetStringContent(requestObject);
            using var httpResponseMessage = await HttpClient.PostAsync("/hmip/group/heating/setHotWaterProfileMode", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        /// <summary>
        /// /hmip/group/heating/setHotWaterState
        /// {
        ///     "groupId": "1524ef3a-adfd-4b68-9014-017c1602892d",
        ///     "on": false
        /// }
        /// </summary>
        /// <param name="groupId">The HOT_WATER group id.</param>
        /// <param name="state">true or false</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> SetHotWaterState(string groupId, bool state, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetHotWaterStateRequestObject(groupId, state);
            using var stringContent = GetStringContent(requestObject);
            using var httpResponseMessage = await HttpClient.PostAsync("/hmip/group/heating/setHotWaterState", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }
        
        public async Task<bool> StartImpulse(int channelIndex, string deviceId, CancellationToken cancellationToken = default)
        {
            var requestObject = new StartImpulseRequestObject(deviceId, channelIndex);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/device/control/startImpulse", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

        /// <summary>
        /// Sets the climate control type of the device
        /// </summary>
        /// <param name="channelIndex">The channel index.</param>
        /// <param name="deviceId">The id of the device to be assigned.</param>
        /// <param name="state">Flag whether the floor heating specific groups should be activated or deactivated for the device.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task SetFloorHeatingSpecificGroupActive(int channelIndex, string deviceId, bool state, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetFloorHeatingSpecificGroupActiveRequestObject(deviceId, channelIndex, state);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/device/configuration/setFloorHeatingSpecificGroupActive", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }
        
        public async Task<bool> SetHueSaturationDimLevel(int channelIndex, string deviceId, double dimLevel, int hue, double saturationLevel, CancellationToken cancellationToken = default)
        {
            var requestObject = new SetHueSaturationDimLevelObject(channelIndex, deviceId, dimLevel, hue, saturationLevel);
            var stringContent = GetStringContent(requestObject);

            var httpResponseMessage = await HttpClient.PostAsync("hmip/device/control/setHueSaturationDimLevel", stringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
                return true;

            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
        }

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