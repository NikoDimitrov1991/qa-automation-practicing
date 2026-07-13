namespace CSharpFundamentals.Models;

public abstract class KafkaMessage
{
    public string Topic { get; }
    public DateTime Timestamp { get; }
    public string Payload { get; }

    protected KafkaMessage(string topic, string payload)
    {
        Topic = topic;
        Payload = payload;
        Timestamp = DateTime.UtcNow;
    }

    public virtual string Describe()
    {
        return $"[{Timestamp:HH:mm:ss}] {Topic}: {Payload}";
    }
}