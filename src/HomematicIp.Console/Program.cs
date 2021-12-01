using System;
using System.IO;
using System.Net.Http;
using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using HomematicIp.Data;
using HomematicIp.Data.HomematicIpObjects.Devices;
using HomematicIp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace HomematicIp.Console
{
    class Program
    {
        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Register the IOptions object
            services.Configure<HomematicConfiguration>(configuration.GetSection(nameof(HomematicConfiguration)));
            // Explicitly register the settings object by delegating to the IOptions object
            services.AddSingleton(resolver =>
                resolver.GetRequiredService<IOptions<HomematicConfiguration>>().Value);
            services.AddLogging(configure => configure.AddConsole())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Debug)
                .AddTransient<HomematicService>()
                .AddTransient<HomematicAuthService>()
                .AddTransient<ClientWebSocket>()
                .AddTransient<HomematicServiceBase.ClientCharacteristics>()
                .AddTransient<Func<HttpClient>>(provider => () => new HttpClient());
        }
        static async Task Main(string[] args)
        {
            #region 0. Configuration and DependencyInjection
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("config.json", false, true)
                .Build();
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, configuration);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            #endregion

            #region 1. Authorization
            //This region shows how to create an access token. You only need to do this once (until you revoke it)
            var homematicAuthService = serviceProvider.GetService<HomematicAuthService>();
            await homematicAuthService.ConnectionRequest();
            System.Console.WriteLine("Please press the blue button on the access point.");
            while (!await homematicAuthService.IsRequestAcknowledged())
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
            var authToken = await homematicAuthService.RequestAuthToken();
            System.Console.WriteLine($"The AuthToken is {authToken}. Put it in you config to use it for further requests.");
            var homematicConfiguration = serviceProvider.GetService<HomematicConfiguration>();
            homematicConfiguration.AuthToken = authToken;
            System.Console.ReadLine();
            #endregion

            #region 2a. QueryRestEndpoint
            var homematicService = serviceProvider.GetService<HomematicService>();
            //if you ever want to cancel a request, use a CancellationToken
            var cts = new CancellationTokenSource();
            await homematicService.ConnectAsync(cts.Token);
            var homematicIpEnvironment = await homematicService.GetCurrentState(cts.Token);
            //look at the properties of homematicIpEnvironment to see what information it holds
            System.Console.WriteLine($"This Homematic Installation has {homematicIpEnvironment.Clients.Count} connected Clients.");
            #endregion

            #region 2b. QueryWebSocketEndpoint
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
