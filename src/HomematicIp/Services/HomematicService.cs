using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HomematicIp.Data;
using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects;
using HomematicIp.Data.HomematicIpObjects.Groups;
using HomematicIp.Data.HomematicIpObjects.Home;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HomematicIp.Services
{
    public class HomematicService : HomematicServiceBase
    {
        private readonly string _authToken;
        private readonly ClientWebSocket _clientWebSocket;
        protected const string AUTHTOKEN = "AUTHTOKEN";
        public HomematicService(Func<HttpClient> httpClientFactory, string accessPointId, string authToken, ClientWebSocket clientWebSocket) : base(httpClientFactory, accessPointId)
        {
            _authToken = authToken;
            _clientWebSocket = clientWebSocket;
        }

        protected override async Task Initialize(
            ClientCharacteristicsRequestObject clientCharacteristicsRequestObject = null, CancellationToken cancellationToken = default)
        {
            await base.Initialize(clientCharacteristicsRequestObject, cancellationToken);
            HttpClient.DefaultRequestHeaders.Add(AUTHTOKEN, _authToken);
            _clientWebSocket.Options.SetRequestHeader(AUTHTOKEN, _authToken);
            _clientWebSocket.Options.SetRequestHeader(CLIENTAUTH, ClientAuthToken);
        }

        public async Task ConnectAsync(CancellationToken cancellationToken = default)
        {
            await Initialize(cancellationToken: cancellationToken);
            await _clientWebSocket.ConnectAsync(new Uri(UrlWebSocket), cancellationToken);
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

        public IObservable<EventNotification> ReceiveEvents(CancellationToken cancellationToken = default)
        {
            var subject = new Subject<EventNotification>();
            var buffer = new byte[1024];
            var list = new List<byte>();
            Task.Run(async () =>
            {
                while (_clientWebSocket.State == WebSocketState.Open)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        await _clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty,CancellationToken.None);
                        subject.OnCompleted();
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
                                                var type=EnumToType.GetType(groupType, rawGroup);
                                                var typedGroup = JsonConvert.DeserializeObject(rawGroup, type);
                                                homematicIpObjectBase = typedGroup as HomematicIpObjectBase;
                                                if(homematicIpObjectBase!=null)
                                                    homematicIpObjectBase.RawJson = rawGroup;
                                                break;
                                            case EventType.DEVICE_REMOVED:
                                            case EventType.DEVICE_CHANGED:
                                            case EventType.DEVICE_ADDED:
                                                Enum.TryParse(hEvent["device"]["type"].Value<string>(), out DeviceType deviceType);
                                                var rawDevice = hEvent["device"].ToString();
                                                var dType =EnumToType.GetType(deviceType, rawDevice);
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
                                            default:
                                                throw new ArgumentOutOfRangeException();
                                        }
                                        if (homematicIpObjectBase != null)
                                        {
                                            subject.OnNext(new EventNotification{EventType = eventType, HomematicIpObjectBase = homematicIpObjectBase });
                                        }
                                    }
                                    list.Clear();
                                }
                                break;
                            case WebSocketMessageType.Close:
                                await _clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, cancellationToken);
                                subject.OnCompleted();
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
            return subject;
        }
    }
}