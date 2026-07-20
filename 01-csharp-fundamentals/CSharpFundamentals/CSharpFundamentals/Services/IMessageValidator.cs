using CSharpFundamentals.Models;

namespace CSharpFundamentals.Services;

public interface IMessageValidator
{
    string Name { get; }
    bool Validate(KafkaMessage message);
}