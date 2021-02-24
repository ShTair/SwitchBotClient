namespace SwitchBot.Models
{
    class CommandRequest
    {
        public string Command { get; }

        public string Parameter { get; }

        public string CommandType { get; } = "command";

        public CommandRequest(string command, string parameter) => (Command, Parameter) = (command, parameter);
    }
}
