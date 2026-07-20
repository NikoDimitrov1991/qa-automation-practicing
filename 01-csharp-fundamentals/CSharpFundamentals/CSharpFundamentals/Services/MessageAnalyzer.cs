using CSharpFundamentals.Models;

namespace CSharpFundamentals.Services;

public record ValidationResult(string ValidatorName, string MessageType, bool IsValid);

public class MessageAnalyzer
{
    private readonly IEnumerable<IMessageValidator> _validators;

    public MessageAnalyzer(IEnumerable<IMessageValidator> validators)
    {
        _validators = validators;
    }

    public List<ValidationResult> Analyze(List<KafkaMessage> messages)
    {
        List<ValidationResult> results = new List<ValidationResult>();
        foreach (KafkaMessage message in messages)
        {
            foreach (IMessageValidator validator in _validators)
            {
                bool isValid = validator.Validate(message);
                results.Add(new ValidationResult(
                    validator.Name,
                    message.GetType().Name,
                    isValid));
            }
        }

        return results;
    }
}