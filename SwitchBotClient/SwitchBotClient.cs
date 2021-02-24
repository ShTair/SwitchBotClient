using SwitchBot.Models;
using SwitchBot.Models.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SwitchBot
{
    public class SwitchBotClient
    {
        // https://github.com/OpenWonderLabs/SwitchBotAPI

        private readonly Uri _base;
        private readonly string _token;

        public SwitchBotClient(string token)
        {
            _base = new Uri("https://api.switch-bot.com");
            _token = token;
        }

        public async Task<IEnumerable<IDevice>> GetDevicesAsync()
        {
            var uri = new Uri(_base, "/v1.0/devices");
            using var client = CreateClient();
            using var message = await client.GetAsync(uri);
            var result = await message.Content.ReadFromJsonAsync<Result>();
            return Enumerable.Empty<IDevice>()
                .Concat(result!.Body!.Devices!)
                .Concat(result!.Body.InfraredRemotes!)
                .Select(device => device switch
                {
                    InfraredRemote { RemoteType: "TV" } temp => new Television(temp),
                    _ => device,
                });
        }

        public async Task GetDeviceStatusAsync(string deviceId)
        {
            var uri = new Uri(_base, $"/v1.0/devices/{deviceId}/status");
            using var client = CreateClient();
            using var message = await client.GetAsync(uri);
            var result = await message.Content.ReadAsStringAsync();
        }

        public Task GetDeviceStatusAsync(Device device)
            => GetDeviceStatusAsync(device.DeviceId);

        public async Task SendCommandAsync(string deviceId, string command, string parameter)
        {
            var uri = new Uri(_base, $"/v1.0/devices/{deviceId}/commands");
            var request = new CommandRequest(command, parameter);
            using var client = CreateClient();
            using var message = await client.PostAsJsonAsync(uri, request);
            var content = message.Content.ReadAsStringAsync();
        }

        public Task SendCommandAsync(IDevice device, string command, string parameter)
            => SendCommandAsync(device.DeviceId, command, parameter);

        private HttpClient CreateClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_token);
            return client;
        }
    }
}
