namespace CSharpFundamentals.Models;

public class CommandMessage : KafkaMessage, IValidatable
{
    public string CommandName { get; }

    public CommandMessage(string topic, string payload, string commandName)
        : base(topic, payload)
    {
        CommandName = commandName;
    }

    public override string Describe()
    {
        return $"{base.Describe()} | Command={CommandName}";
    }

    public bool IsValid()
    {
        string[] allowed = { "Restart", "Reload", "Shutdown" };
        return allowed.Contains(CommandName);
    }
}