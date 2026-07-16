using CSharpFundamentals.Models;

namespace CSharpFundamentals;

public static class LinqDemo
{
    public static void Run()
    {
        List<KafkaMessage> messages = BuildSampleMessages();
        Console.WriteLine($"Total messages: {messages.Count}");
        Console.WriteLine();
        Console.WriteLine("--- Exercise 1a: filter EventMessages with Where ---");
        var events = messages.Where(m => m is EventMessage).ToList();
        Console.WriteLine($"EventMessages count: {events.Count}");
        Console.WriteLine();
        Console.WriteLine("--- Exercise 1b: count HeartbeatMessages directly ---");
        int heartbeatCount = messages.Count(m => m is HeartbeatMessage);
        Console.WriteLine($"HeartbeatMessages count: {heartbeatCount}");
        Console.WriteLine();
        Console.WriteLine("--- Exercise 2: Any + All ---");
        bool hasInvalid = messages
            .OfType<IValidatable>()
            .Any(v => !v.IsValid());
        Console.WriteLine($"Has at least one invalid message? {hasInvalid}");

        bool allHaveTopic = messages.All(m => !string.IsNullOrEmpty(m.Topic));
        Console.WriteLine($"All messages have a non-empty topic? {allHaveTopic}");
        Console.WriteLine();
        Console.WriteLine("--- Exercise 3a: extract topics ---");
        List<string> topics = messages
            .Select(m => m.Topic)
            .Distinct()
            .ToList();
        foreach (string t in topics)
        {
            Console.WriteLine(t);
        }

        Console.WriteLine();
        Console.WriteLine("--- Exercise 3b: descriptions of valid messages only ---");
        List<string> validDescriptions = messages
            .OfType<IValidatable>()
            .Where(v => v.IsValid())
            .Cast<KafkaMessage>()
            .Select(m => m.Describe())
            .ToList();
        foreach (string d in validDescriptions)
        {
            Console.WriteLine(d);
        }

        Console.WriteLine();
        Console.WriteLine("--- Exercise 4: OrderBy ---");
        var sorted = messages
            .OrderBy(m => m.Topic)
            .ThenByDescending(m => m.Timestamp)
            .ToList();
        foreach (var m in sorted)
        {
            Console.WriteLine(m.Describe());
        }

        Console.WriteLine();
        Console.WriteLine("--- Exercise 5a: GroupBy ---");
        var byTopic = messages.GroupBy(m => m.Topic);
        foreach (var group in byTopic)
        {
            Console.WriteLine($"Topic: {group.Key} ({group.Count()} messages)");
            foreach (var m in group)
            {
                Console.WriteLine(m.GetType().Name);
            }
        }

        Console.WriteLine();
        Console.WriteLine("--- Exercise 5b: count by message type ---");
        var typeCounts = messages
            .GroupBy(m => m.GetType().Name)
            .Select(g => new { Type = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ToList();
        foreach (var tc in typeCounts)
        {
            Console.WriteLine($"{tc.Type}: {tc.Count}");
        }
    }

    private static List<KafkaMessage> BuildSampleMessages()
    {
        return new List<KafkaMessage>
        {
            new EventMessage("bg.live.event", "{}", "Live"),
            new EventMessage("bg.live.event", "{}", "GoalScored"),
            new EventMessage("bg.live.event", "{}", "UnknownEvent"),
            new CommandMessage("bg.live.command", "{}", "Restart"),
            new CommandMessage("bg.live.command", "{}", "InvalidCommand"),
            new HeartbeatMessage("feeds-status", "{}", "BetGenius"),
            new HeartbeatMessage("feeds-status", "{}", "OddsJam"),
            new HeartbeatMessage("feeds-status", "{}", "")
        };
    }
}