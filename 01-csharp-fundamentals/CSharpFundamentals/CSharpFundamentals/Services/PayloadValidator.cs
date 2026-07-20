using CSharpFundamentals.Models;

namespace CSharpFundamentals.Services;

public class PayloadValidator : IMessageValidator
{
    public string Name => "PayloadValidator";

    public bool Validate(KafkaMessage message)
    {
        return !string.IsNullOrEmpty(message.Payload);
    }
}