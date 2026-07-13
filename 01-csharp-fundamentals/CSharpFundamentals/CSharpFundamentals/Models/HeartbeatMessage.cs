namespace CSharpFundamentals.Models;

public class HeartbeatMessage : KafkaMessage, IValidatable
{
    public string ServiceName { get; }

    public HeartbeatMessage(string topic, string payload, string serviceName)
        : base(topic, payload)
    {
        ServiceName = serviceName;
    }

    public override string Describe()
    {
        return $"{base.Describe()} | Service={ServiceName}";
    }

    public bool IsValid()
    {
        return !string.IsNullOrEmpty(ServiceName);
    }
}