using SwitchBot.Models.Devices;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SwitchBot.Models
{
    class DevicesBody
    {
        [JsonPropertyName("deviceList")]
        [JsonConverter(typeof(DevicesJsonConverter))]
        public List<Device> Devices { get; set; } = default!;

        [JsonPropertyName("infraredRemoteList")]
        [JsonConverter(typeof(InfraredRemoteJsonConverter))]
        public List<InfraredRemote> InfraredRemotes { get; set; } = default!;
    }
}
