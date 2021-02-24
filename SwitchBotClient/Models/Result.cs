namespace SwitchBot.Models
{
    class Result
    {
        public int StatusCode { get; set; }

        public Body? Body { get; set; }

        public string Message { get; set; } = default!;
    }
}
