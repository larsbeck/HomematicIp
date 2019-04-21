# HomematicIp
This package allows to query the HomematicIp REST and WebSocket endpoint.
## Installation
`Install-Package HomematicIp`
## Example Usage
The following code be self-explanatory. If it is not, there are explanatory comments in the HomematicIp.Console project. You can also use that project to get started
```csharp
#region 1. Authorization
HttpClient HttpClientFactory() => new HttpClient();
var accessPointId = "YOUR_ACCESS_POINT_ID";
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
var homematicService = new HomematicService(HttpClientFactory, accessPointId, authToken, new ClientWebSocket());
var cts = new CancellationTokenSource();
await homematicService.ConnectAsync(cts.Token);
var homematicIpEnvironment = await homematicService.GetCurrentState(cts.Token);
System.Console.WriteLine($"This Homematic Installation has {homematicIpEnvironment.Clients.Count} connected Clients.");
#endregion

#region 2b.QueryWebSocketEndpoint
var events = homematicService.ReceiveEvents(cts.Token);
events.Where(notification => notification.HomematicIpObjectBase is ShutterContactDevice)
    .Subscribe(notification =>
    {
        var shutterContactDevice = notification.HomematicIpObjectBase as ShutterContactDevice;
        System.Console.WriteLine($"{shutterContactDevice?.Label}: WindowState={shutterContactDevice?.WindowState}");
    });
events.Subscribe(System.Console.WriteLine);
await events.LastOrDefaultAsync();
#endregion
```
## Yet unsupported devices
Since there are a lot of Homematic devices, you might be presented with a message like the one below. In that case please open an issue, we will add the device asap:

`The HomematicIp Endpoint sent a message about an unknown HomematicIp Object (most likely a yet unsupported device). Please open an issue at [https://github.com/larsbeck/HomematicIp](https://github.com/larsbeck/HomematicIp) to have this device added to the library. We will need the following: {...}`

### A big thank you for laying the groundwork goes to the creators of this Python library [homematicip-rest-api](https://github.com/coreGreenberet/homematicip-rest-api)
