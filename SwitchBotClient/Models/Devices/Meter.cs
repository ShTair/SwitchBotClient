using System.Text.Json;
using System.Threading.Tasks;

namespace SwitchBot.Models.Devices
{
    public class Meter : Device
    {
        public double? Humidity { get; set; }

        public double? Temperature { get; set; }

        public Meter() { }

        public Meter(SwitchBotClient client, string deviceId) : base(client, deviceId) { }

        public async Task GetStatusAsync()
        {
            var data = await Client.GetDeviceStatusAsync<JsonElement>(DeviceId);
        }
    }
}
