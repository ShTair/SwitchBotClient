namespace SwitchBot.Models.Devices
{
    public class Curtain : Device
    {
        public bool Moving { get; set; }

        public int SlidePosition { get; set; }
    }
}
