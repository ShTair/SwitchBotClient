using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SwitchBot.Models.Devices
{
    public interface IDevice
    {
        string DeviceId { get; }
    }

    public class Device : IDevice
    {
        protected SwitchBotClient Client { get; private set; } = default!;

        public string DeviceId { get; set; } = default!;

        public string DeviceName { get; set; } = default!;

        public string DeviceType { get; set; } = default!;

        public bool EnableCloudService { get; set; }

        public string HubDeviceId { get; set; } = default!;

        public Device() { }

        public Device(SwitchBotClient client, string deviceId) => (Client, DeviceId) = (client, deviceId);

        internal void SetClient(SwitchBotClient client) => Client = client;
    }

    class DevicesJsonConverter : JsonConverter<List<Device>>
    {
        public override List<Device>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var doc = JsonDocument.ParseValue(ref reader);
            return doc.RootElement.EnumerateArray().Select(t =>
            {
                var type = t.GetProperty("deviceType").GetString();
                var text = t.GetRawText();
                return type switch
                {
                    "Curtain" => JsonSerializer.Deserialize<Curtain>(t.GetRawText(), options)!,
                    "Meter" => JsonSerializer.Deserialize<Meter>(t.GetRawText(), options)!,
                    _ => JsonSerializer.Deserialize<Device>(t.GetRawText(), options)!,
                };
            }).ToList();
        }

        public override void Write(Utf8JsonWriter writer, List<Device> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
