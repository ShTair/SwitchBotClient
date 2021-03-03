namespace SwitchBot.Models.Devices
{
    public class Curtain : Device
    {
        public string[] CurtainDevicesIds { get; set; } = default!;

        public bool Calibrate { get; set; }

        public bool Group { get; set; }

        public bool Master { get; set; }

        public string OpenDirection { get; set; } = default!;

        public bool? Moving { get; set; }

        public int? SlidePosition { get; set; }
    }
}
