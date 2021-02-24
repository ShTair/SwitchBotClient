namespace SwitchBot.Models
{
    public interface IDevice
    {
        string DeviceId { get; }

        string DeviceName { get; }

        string HubDeviceId { get; }
    }

    public class Device : IDevice
    {
        public string DeviceId { get; set; } = default!;

        public string DeviceName { get; set; } = default!;

        public string DeviceType { get; set; } = default!;

        public bool EnableCloudService { get; set; }

        public string HubDeviceId { get; set; } = default!;

        public string[]? CurtainDevicesIds { get; set; }

        public bool Calibrate { get; set; }

        public bool Group { get; set; }

        public bool Master { get; set; }

        public string? OpenDirection { get; set; }
    }

    public class InfraredRemote : IDevice
    {
        public string DeviceId { get; set; } = default!;

        public string DeviceName { get; set; } = default!;

        public string RemoteType { get; set; } = default!;

        public string HubDeviceId { get; set; } = default!;
    }
}
