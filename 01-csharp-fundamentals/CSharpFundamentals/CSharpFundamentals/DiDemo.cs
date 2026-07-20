using CSharpFundamentals.Models;
using CSharpFundamentals.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CSharpFundamentals;

public static class DiDemo
{
    public static void Run()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IMessageValidator, TopicValidator>();
        services.AddSingleton<IMessageValidator, PayloadValidator>();
        services.AddSingleton<MessageAnalyzer>();

        var provider = services.BuildServiceProvider();
        var analyzer = provider.GetRequiredService<MessageAnalyzer>();

        List<KafkaMessage> messages = BuildSampleMessages();
        List<ValidationResult> results = analyzer.Analyze(messages);

        Console.WriteLine($"Analyzed {messages.Count} messages with {results.Count / messages.Count} validators.");
        Console.WriteLine();

        var grouped = results.GroupBy(r => r.MessageType);
        foreach (var group in grouped)
        {
            Console.WriteLine($"--- {group.Key} ---");
            foreach (var r in group)
            {
                string status = r.IsValid ? "Valid" : "Invalid";
                Console.WriteLine($"  [{status}] {r.ValidatorName}");
            }
        }
    }


    private static List<KafkaMessage> BuildSampleMessages()
    {
        return new List<KafkaMessage>
        {
            new EventMessage("bg.live.event", "{\"minute\":42}", "GoalScored"),
            new EventMessage("", "{}", "Live"),
            new CommandMessage("bg.live.command", "", "Restart"),
            new HeartbeatMessage("feeds-status", "{}", "OddsJam")
        };
    }
}