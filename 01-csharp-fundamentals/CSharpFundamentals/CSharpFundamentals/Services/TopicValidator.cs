using CSharpFundamentals.Models;

namespace CSharpFundamentals.Services;

public class TopicValidator : IMessageValidator
{
    public string Name => "TopicValidator";

    public bool Validate(KafkaMessage message)
    {
        return !string.IsNullOrEmpty(message.Topic);
    }
}