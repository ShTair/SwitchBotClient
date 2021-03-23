using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SwitchBot.Models.Devices
{
    public class InfraredRemote : IDevice
    {
        protected SwitchBotClient Client { get; private set; } = default!;

        public string DeviceId { get; } = default!;

        public string DeviceName { get; } = default!;

        public string RemoteType { get; } = default!;

        public string HubDeviceId { get; } = default!;

        public InfraredRemote() { }

        public InfraredRemote(SwitchBotClient client, string deviceId) => (Client, DeviceId) = (client, deviceId);

        internal void SetClient(SwitchBotClient client) => Client = client;
    }

    class InfraredRemoteJsonConverter : JsonConverter<List<InfraredRemote>>
    {
        public override List<InfraredRemote>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var doc = JsonDocument.ParseValue(ref reader);
            return doc.RootElement.EnumerateArray().Select(t =>
            {
                var type = t.GetProperty("remoteType").GetString();
                return type switch
                {
                    _ => JsonSerializer.Deserialize<InfraredRemote>(t.GetRawText(), options)!,
                };
            }).ToList();
        }

        public override void Write(Utf8JsonWriter writer, List<InfraredRemote> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
