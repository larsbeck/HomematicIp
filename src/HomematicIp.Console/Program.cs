using System;
using System.IO;
using System.Net.Http;
using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using HomematicIp.Data.HomematicIpObjects.Devices;
using HomematicIp.Services;

namespace HomematicIp.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            #region 1. Authorization
            //This region shows how to create an access token. You only need to do this once (until you revoke it)
            HttpClient HttpClientFactory() => new HttpClient();
            //also called SGTIN on the back of your access point
            var accessPointId = "YOUR_ACCESS_POINT_ID";
            //the pin is optional, pass null if you haven't secured your access point
            var homematicAuthService = new HomematicAuthService(HttpClientFactory, accessPointId, "YOUR_PIN", "MyHomematicApp");
            await homematicAuthService.ConnectionRequest();
            System.Console.WriteLine("Please press the blue button on the access point.");
            while (!await homematicAuthService.IsRequestAcknowledged())
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
            var authToken = await homematicAuthService.RequestAuthToken();
            await File.WriteAllTextAsync("config.txt", authToken);
            #endregion

            #region 2a. QueryRestEndpoint
            //We are reusing the authToken that was received in the authorization process. Since you are only running that part once, you may consider reading the authToken from the config file: var authToken = await File.ReadAllTextAsync("config.txt");
            var homematicService = new HomematicService(HttpClientFactory, accessPointId, authToken, new ClientWebSocket());
            //if you ever want to cancel a request, use a CancellationToken
            var cts = new CancellationTokenSource();
            await homematicService.ConnectAsync(cts.Token);
            var homematicIpEnvironment = await homematicService.GetCurrentState(cts.Token);
            //look at the properties of homematicIpEnvironment to see what information it holds
            System.Console.WriteLine($"This Homematic Installation has {homematicIpEnvironment.Clients.Count} connected Clients.");
            #endregion

            #region 2b.QueryWebSocketEndpoint
            var events = homematicService.ReceiveEvents(cts.Token);
            //filter events by using Where and Subscribe. Here we only want to see events about ShutterContactDevices
            events.Where(notification => notification.HomematicIpObjectBase is ShutterContactDevice)
                .Subscribe(notification =>
                {
                    var shutterContactDevice = notification.HomematicIpObjectBase as ShutterContactDevice;
                    System.Console.WriteLine($"{shutterContactDevice?.Label}: WindowState={shutterContactDevice?.WindowState}");
                });
            //output every message without processing it
            events.Subscribe(System.Console.WriteLine);
            //wait for the last event - if you don't cancel and nothing unexpected happens, that means forever
            await events.LastOrDefaultAsync();
            #endregion

        }
    }
}
