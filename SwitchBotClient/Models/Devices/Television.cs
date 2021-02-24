namespace SwitchBot.Models.Devices
{
    public class Television : InfraredRemote
    {
        public Television(InfraredRemote device)
        {
            DeviceId = device.DeviceId;
            DeviceName = device.DeviceName;
            RemoteType = device.RemoteType;
            HubDeviceId = device.HubDeviceId;
        }
    }
}
