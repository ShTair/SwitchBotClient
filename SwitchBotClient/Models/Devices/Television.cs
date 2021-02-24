namespace SwitchBot.Models.Devices
{
    public class Television : InfraredRemote
    {
        internal Television(InfraredRemote device) => (DeviceId, DeviceName, RemoteType, HubDeviceId) = device;
    }
}
