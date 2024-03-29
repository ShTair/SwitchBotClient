﻿using SwitchBot.Models;
using SwitchBot.Models.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
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
            var result = await message.Content.ReadFromJsonAsync<Result<DevicesBody>>();
            var body = result!.Body!;
            foreach (var device in body.Devices) device.SetClient(this);
            foreach (var device in body.InfraredRemotes) device.SetClient(this);
            return body.Devices.Cast<IDevice>().Concat(body.InfraredRemotes).ToList();
        }

        public async Task<T> GetDeviceStatusAsync<T>(string deviceId) where T : notnull
        {
            var uri = new Uri(_base, $"/v1.0/devices/{deviceId}/status");
            using var client = CreateClient();
            using var message = await client.GetAsync(uri);
            var options = new JsonSerializerOptions();
            var result = await message.Content.ReadFromJsonAsync<Result<T>>();
            return result!.Body;
        }

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
