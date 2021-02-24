using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SwitchBot.Models
{
    class Body
    {
        [JsonPropertyName("deviceList")]
        public List<Device>? Devices { get; set; }

        [JsonPropertyName("infraredRemoteList")]
        public List<InfraredRemote>? InfraredRemotes { get; set; }
    }
}
