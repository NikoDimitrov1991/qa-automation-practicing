using CSharpFundamentals.Models;

namespace CSharpFundamentals;

public static class OopDemo
{
    public static void Run()
    {
        List<KafkaMessage> messages = new List<KafkaMessage>
        {
            new EventMessage("bg.live.event", "{\"minute\":42}", "GoalScored"),
            new EventMessage("bg.live.event", "{\"minute\":90}", "UnknownEvent"),
            new CommandMessage("bg.live.command", "{}", "Restart"),
            new HeartbeatMessage("feeds-status", "{}", "BetGenius"),
            new HeartbeatMessage("feeds-status", "{}", ""),
        };

        Console.WriteLine("--- Describing all messages ---");
        foreach (KafkaMessage msg in messages)
        {
            Console.WriteLine(msg.Describe());
        }

        Console.WriteLine();
        Console.WriteLine("--- Validating all messages ---");
        foreach (KafkaMessage msg in messages)
        {
            if (msg is IValidatable validatable)
            {
                bool valid = validatable.IsValid();
                Console.WriteLine($"{msg.GetType().Name}: {(valid ? "VALID" : "INVALID")}");
            }
            else
            {
                Console.WriteLine($"{msg.GetType().Name}: not validatable");
            }
        }
    }
}