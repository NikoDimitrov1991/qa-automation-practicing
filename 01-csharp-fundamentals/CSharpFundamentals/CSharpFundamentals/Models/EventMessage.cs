namespace CSharpFundamentals.Models;

public class EventMessage : KafkaMessage, IValidatable
{
    public string EventType { get; }

    public EventMessage(string topic, string payload, string eventType)
        : base(topic, payload)
    {
        EventType = eventType;
    }

    public override string Describe()
    {
        return $"{base.Describe()} | EventType={EventType}";
    }

    public bool IsValid()
    {
        string[] allowed = { "Live", "GoalScored", "Penalty", "Finished" };
        return allowed.Contains(EventType);
    }
}