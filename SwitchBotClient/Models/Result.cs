namespace SwitchBot.Models
{
    class Result<T> where T : notnull
    {
        public int StatusCode { get; set; }

        public T Body { get; set; } = default!;

        public string Message { get; set; } = default!;
    }
}
