﻿using System;
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

        public async Task<bool> StartDeviceInclusionProcess(CancellationToken cancellationToken = default)
        {
            var httpResponseMessage = await HttpClient.PostAsync("hmip/home/startDeviceInclusionProcess", ClientCharacteristicsStringContent, cancellationToken);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var tcs=new TaskCompletionSource<string>();
                var inclusionRequestedObservable = ReceiveEvents().Where(notification => notification.EventType== EventType.INCLUSION_REQUESTED);
                IDisposable disposeWhenFirstDeviceIsPaired=null;
                disposeWhenFirstDeviceIsPaired = inclusionRequestedObservable.Subscribe(async notification =>
                {
                    await StartInclusionModeForDevice(notification.HomematicIpObjectBase.Id, cancellationToken);
                    disposeWhenFirstDeviceIsPaired.Dispose(); //compiler warning can be ignored. disposeWhenFirstDeviceIsPaired is never null and cannot be disposed due to the awaited TaskCompletionSource.Task
                    tcs.TrySetResult(null);
                });
                await tcs.Task;
                return true;
            }
            throw new ArgumentException($"Request failed: {httpResponseMessage.ReasonPhrase}");
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
                                                    homematicIpObjectBase = new Device { Id = deviceId, DeviceType = deviceTypeRequested};
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