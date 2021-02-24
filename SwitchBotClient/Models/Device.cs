namespace SwitchBot.Models
{
    public abstract class DeviceBase
    {
        public string DeviceId { get; set; } = default!;

        public string DeviceName { get; set; } = default!;

        public string HubDeviceId { get; set; } = default!;

        public override string ToString() => $"{DeviceId}:{DeviceName}";

        public void Deconstruct(out string deviceId, out string deviceName, out string hubDeviceId) =>
            (deviceId, deviceName, hubDeviceId) = (DeviceId, DeviceName, HubDeviceId);
    }

    public class Device : DeviceBase
    {
        public string DeviceType { get; set; } = default!;

        public bool EnableCloudService { get; set; }

        public string[]? CurtainDevicesIds { get; set; }

        public bool Calibrate { get; set; }

        public bool Group { get; set; }

        public bool Master { get; set; }

        public string? OpenDirection { get; set; }

        public override string ToString() => $"{DeviceId}:{DeviceType}:{DeviceName}";

        public void Deconstruct(out string deviceId, out string deviceName, out string deviceType, out string hubDeviceId, out bool enableCloudService, out string[]? curtainDevicesIds, out bool calibrate, out bool group, out bool master, out string? openDirection) =>
        ((deviceId, deviceName, hubDeviceId), deviceType, enableCloudService, curtainDevicesIds, calibrate, group, master, openDirection) = (this, DeviceType, EnableCloudService, CurtainDevicesIds, Calibrate, Group, Master, OpenDirection);
    }

    public class InfraredRemote : DeviceBase
    {
        public string RemoteType { get; set; } = default!;

        public override string ToString() => $"{DeviceId}:{RemoteType}:{DeviceName}";

        public void Deconstruct(out string deviceId, out string deviceName, out string remoteType, out string hubDeviceId) =>
            ((deviceId, deviceName, hubDeviceId), remoteType) = (this, RemoteType);
    }
}
