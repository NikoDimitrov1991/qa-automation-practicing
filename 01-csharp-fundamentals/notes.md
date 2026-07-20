# 01 - C# Fundamentals & OOP - Notes

**Jira:** [SDP-37390](https://draftkingsofficial.atlassian.net/browse/SDP-37390)

## Sub-session A - 2026-07-06 (~1h)

### What was covered

- Set up .NET 8 console solution `CSharpFundamentals` under `01-csharp-fundamentals/`.
- Reviewed running via both Rider IDE and `dotnet run` from terminal.
- Refreshed C# basics: variables, `Console.ReadLine` + `int.Parse` input pattern, string interpolation with `$"..."`, `if/else`, `for` loops, method extraction, static classes.
- Refactored 3 exercises into their own static classes (`EvenOdd`, `SumToN`, `FizzBuzz`), each with a `Run()` entry method.
- `Program.cs` acts as menu / dispatcher via `switch`.

### Java → C# differences noticed

- **Top-level statements** — no boilerplate `class Program { static void Main() { ... } }` needed in .NET 6+.
- **String interpolation** — `$"Sum: {sum}"` instead of `String.format("Sum: %d", sum)`.
- **Method naming convention** — PascalCase (`IsEven`) instead of camelCase.
- **`bool` vs `boolean`** — C# is `bool` (lowercase, alias for `Boolean`).
- **File-scoped namespaces** — `namespace X;` (semicolon) instead of `namespace X { ... }` with braces.
- **`Console.Write`** — same as `WriteLine` but without trailing newline (useful for input prompts).
- **`switch` with strings** — allowed in C# (Java added it too, was original only for primitives).

### Mistakes made and learned from

- **Off-by-one in `for` loop:** wrote `i < n` instead of `i <= n`. Excludes the boundary → wrong result. Classic bug in every language.
- **Formatting inconsistency in `FizzBuzz.cs`** — mixed brace styles (`}else if` vs `}\nelse`). Rider's `Ctrl+Alt+L` reformats to consistent style.

### Open questions for next session

- Do we need `using CSharpFundamentals;` in `Program.cs`? Left in for now; test whether it's actually required or redundant.
- `static bool IsEven(...)` — clarify when a helper should be a private static method inside a class vs a top-level function.
- **`Console.ReadLine()` can return `null`** in some scenarios — Rider shows a nullable-context warning. To be addressed in later session (nullable reference types).

### Files produced

- `CSharpFundamentals/Program.cs` — menu dispatcher
- `CSharpFundamentals/EvenOdd.cs` — reads a number, prints Even/Odd
- `CSharpFundamentals/SumToN.cs` — sums integers 1..N
- `CSharpFundamentals/FizzBuzz.cs` — classic FizzBuzz 1..N

## Sub-session B - 2026-07-13 (~1.5h)

### What was covered

- Designed a small OOP hierarchy in the Kafka message domain (relevant to Feeds team work).
- `KafkaMessage` abstract base class in `Models/` folder — `Topic`, `Timestamp`, `Payload` as read-only properties; `virtual Describe()`.
- Three concrete subclasses: `EventMessage`, `CommandMessage`, `HeartbeatMessage` — each adds a domain-specific property and overrides `Describe()` with its own format.
- `IValidatable` interface — each subclass implements `IsValid()` with its own business rules (allowed event types, allowed commands, non-empty service name).
- `OopDemo.Run()` builds a `List<KafkaMessage>` with mixed subtypes and demonstrates:
  - Polymorphic `Describe()` dispatch via `foreach`.
  - Interface pattern matching with `is IValidatable validatable` for validation.

### Java → C# differences noticed

- **`abstract class`** — same as Java. Cannot instantiate directly.
- **Properties** — `public string Topic { get; }` is a get-only property (auto-generated backing field). Replaces Java's `private field + public getter` boilerplate. Set only inside constructor.
- **`protected` constructor** — same as Java; only subclasses can invoke it.
- **`virtual` / `override`** — in Java, all methods are virtual by default. In C#, methods are NOT virtual by default; must be marked `virtual` in the base class AND `override` in subclasses. `override` is REQUIRED (not just an annotation).
- **Inheritance + interface syntax** — single `:` with comma-separated list, base class first: `class EventMessage : KafkaMessage, IValidatable`. No `extends` / `implements`.
- **`: base(...)` constructor chaining** — outside the method body, before `{`. Java: `super(...)` inside the body.
- **Interface naming convention** — `IValidatable` (I-prefix). Java has no such convention.
- **`is` pattern matching** — `if (msg is IValidatable v)` combines type check + cast + variable declaration into one line. Java 16+ has similar syntax now.
- **`List<T>` object initializer** — `new List<Foo> { new Foo(...), new Foo(...) }` (NO parentheses after type). If you write `new List<Foo>()`, the trailing `{ ... }` is a separate block, not an initializer.
- **`GetType().Name`** — Java `getClass().getSimpleName()`. Returns runtime type name.
- **`string.IsNullOrEmpty(x)`** — one-liner for `x != null && !x.isEmpty()`.
- **Implicit usings (`GlobalUsings.g.cs`)** — .NET 6+ auto-imports `System`, `System.Linq`, `System.Collections.Generic`, etc. into every file. No manual `using System.Linq;` needed for `Contains`, `Where`, etc.

### Mistakes made and learned from

- **List initializer with parentheses** — wrote `new List<KafkaMessage>();` followed by `{ ... }`, which the compiler treats as a separate block statement, not an initializer. Fix: remove `()` and let `{ ... }` sit directly after the type.
- **Interface method casing** — initially wrote `bool isValid();` (Java habit). Corrected to `bool IsValid()` (C# PascalCase). Rider's "Apply Rename refactoring" propagated the change safely.

### Files produced this session

- `Models/KafkaMessage.cs` — abstract base
- `Models/EventMessage.cs` — subclass with `EventType`
- `Models/CommandMessage.cs` — subclass with `CommandName`
- `Models/HeartbeatMessage.cs` — subclass with `ServiceName`
- `Models/IValidatable.cs` — interface with `IsValid()`
- `OopDemo.cs` — polymorphism demonstration
- Updated `Program.cs` — added menu option 4

## Sub-session C - 2026-07-14 (~1.5h)

### What was covered

Focused on lambdas and LINQ (AC-3 and AC-4 of SDP-37390).

- Progression: **delegate → `Func<T>` → lambda → LINQ**. Each step motivates the next.
- Delegate = "type for a method reference"; `Func<int, bool>` is a built-in generic delegate for "method that takes int, returns bool".
- Lambda = anonymous inline method. Syntax: `x => x > 0`. Replaces short helper methods that would be used once.
- LINQ = collection of extension methods (`Where`, `Select`, `OrderBy`, `GroupBy`, `Any`, `All`, `Count`, `First`, ...) that accept lambdas and return new collections. Chainable with `.` — each call returns something the next can be called on.
- Extension methods explained via `this` keyword in the first parameter — that is what lets `bookings.Where(...)` work despite `Where` not being defined in `List`.
- Method syntax used throughout (`.Where(...).Select(...)`); no query syntax (`from x in y select ...`) — the method syntax dominates in real codebases.

### `LinqDemo.cs` exercises

1. **Where + Count** — filter and count `EventMessage` and `HeartbeatMessage` subtypes.
2. **Any + All** — check "at least one invalid" (using `OfType<IValidatable>()`), and "all have non-empty Topic".
3. **Select** — project to `Topic` strings with `.Distinct()`, and project valid messages to their `Describe()` output using `OfType + Where + Cast + Select`.
4. **OrderBy + ThenByDescending** — sort by Topic ASC, then Timestamp DESC.
5. **GroupBy** — group messages by Topic (iterate each group), then count-by-type projected into an anonymous type `{ Type, Count }` sorted DESC.

### Java → C# differences noticed

- **Lambda arrow** — `=>` in C#, `->` in Java.
- **`Func<>` and `Action<>`** — generic delegate types. `Func<int, bool>` = "int → bool". Java's `Predicate<Integer>` and `Function<A, B>`.
- **Extension methods** — allow "attaching" methods to existing types via `this` in the first parameter. Java has no equivalent (must use utility classes or wrappers).
- **`Select` vs `map`** — C# named its projection method `Select` (SQL-inspired), Java uses `map` (functional-inspired).
- **`OfType<T>()`** — C# specific. Filters an `IEnumerable` to only elements of type `T`, safely. Java's equivalent needs `filter + cast`.
- **`Cast<T>()`** — throws if any element cannot be cast (unlike `OfType<T>()` which silently skips). Used when you know all elements are of a given type.
- **Anonymous types** (`new { Type = g.Key, Count = g.Count() }`) — compiler-generated types with no explicit class declaration. Requires `var`. Java has no direct equivalent (records or DTOs required).
- **LINQ is lazy** — `Where`, `Select` return `IEnumerable`, not materialized `List`. Call `.ToList()` to force execution.
- **LINQ never mutates the source** — always returns a new collection. Functional style.

### On the shape of `LinqDemo.cs` — learning code vs production code

This file is intentionally **learning code**, not production-ready code. Signs:

- Lots of `Console.WriteLine` for section headers and output visualization.
- Many small named intermediate variables (`events`, `heartbeatCount`, `hasInvalid`, `topics`, ...).
- Everything in one long `Run()` method with no abstraction.

In a real project, most of this would be refactored into something like:

```csharp
public class MessageAnalyzer
{
    public MessageStats Analyze(List<KafkaMessage> messages) => new MessageStats
    {
        Total = messages.Count,
        EventCount = messages.Count(m => m is EventMessage),
        HasInvalid = messages.OfType<IValidatable>().Any(v => !v.IsValid()),
        TopicBreakdown = messages.GroupBy(m => m.Topic).ToDictionary(g => g.Key, g => g.Count()),
    };
}
```

Tests would then assert on `MessageStats`, no `Console.WriteLine` needed. Production code does not print — it returns data to be verified via test assertions or serialized to callers.

The learning-code style is fine here because the goal is to make **each LINQ operator individually visible**. That evidences AC-3 and AC-4 clearly to the QA Lead. Refactoring toward a clean `MessageAnalyzer` will happen in SDP-37391 (API automation) and SDP-37395 (real project contribution) — with test assertions replacing prints.

### Files produced this session

- `LinqDemo.cs` — 5 LINQ exercises (Where, Any/All, Select, OrderBy, GroupBy)
- Updated `Program.cs` — added menu option 5

## Sub-session D - 2026-07-18 (~1h)

### What was covered

Focus: Dependency Injection basics (last AC to fill for SDP-37390).

- Why DI: hard-coded `new` inside classes = untestable, unswappable. Solution: classes accept dependencies via constructor, work only against interfaces.
- **Composition Root** — the one place in the app where concrete implementations are instantiated. Everywhere else uses interfaces. In .NET this is usually `Program.cs`.
- **DI Container** — `Microsoft.Extensions.DependencyInjection` (installed via `dotnet add package`). Register once, container constructs the object graph via reflection.
- **Lifetimes** — brief overview: `Transient` (new every time), `Scoped` (per web request), `Singleton` (one for app lifetime). For a console app all three behave similarly; used `Singleton` throughout the demo.
- **Constructor injection** — the standard pattern. Class declares `private readonly IFoo _foo;` and initialises it in the constructor. `readonly` prevents accidental reassignment.
- **Multiple implementations of one interface** — DI automatically collects all matching registrations into `IEnumerable<T>` when requested.

### `DiDemo` architecture

Small "production-shape" refactor of Sub-session C's validation logic:

```
IMessageValidator (interface, in Services/)
    ├── string Name { get; }
    └── bool Validate(KafkaMessage msg)

TopicValidator   : IMessageValidator   — checks Topic is non-empty
PayloadValidator : IMessageValidator   — checks Payload is non-empty

MessageAnalyzer
    ├── ctor takes IEnumerable<IMessageValidator>
    └── Analyze(List<KafkaMessage>) → List<ValidationResult>

ValidationResult (record) — (ValidatorName, MessageType, IsValid)
```

`DiDemo.Run()` acts as a mini Composition Root:

1. Builds `ServiceCollection`
2. Registers 2 validators + `MessageAnalyzer`
3. Calls `BuildServiceProvider()`
4. `GetRequiredService<MessageAnalyzer>()` — container assembles the graph
5. Uses the analyzer normally, groups results by message type for display

### Java → C# differences noticed

- **`record` type** — one-line immutable class with auto Equals/HashCode/ToString. `public record ValidationResult(string A, string B, bool C);`. Java has records since v14 — same idea.
- **Expression-bodied property** — `public string Name => "TopicValidator";` is the compact form of a get-only property. No exact Java equivalent (records or lombok closest).
- **`readonly` modifier** — locks a field to constructor-only assignment. Java uses `final`.
- **Naming convention `_camelCase` for private fields** — common in C#. Java varies; some use `_` prefix, most use plain `camelCase`.
- **Constructor injection is idiomatic in C#** — same as Java Spring. The DI container inspects the constructor via reflection.
- **`AddSingleton<IFoo, ConcreteFoo>()`** — Java Spring equivalent is `@Bean` + `@Scope("singleton")` on the concrete implementation.
- **`IEnumerable<T>` for "give me all implementations"** — Java Spring uses `@Autowired List<T>` for the same behaviour.

### Mistakes made and learned from

- **`ValidatorMessage` vs `ValidatorName`** — first version of the record used `ValidatorMessage`, which was misleading (sounds like "the validator's message" rather than "the validator's name"). Corrected via Rider's Rename refactoring (Shift+F6).
- **`using` above vs below `namespace`** — first `IMessageValidator.cs` had `namespace` above `using`. Both compile, but the C# convention is `using` first. Later files corrected.

### Nullable reference warnings — acknowledged

Compilation produced 6 warnings (CS8600 / CS8604) about `Console.ReadLine()` potentially returning `null`. .NET 6+ enforces **nullable reference types**: `string` disallows null; `string?` is required for nullable strings. Left unresolved for now — will address in SDP-37391 when API responses require robust null handling. This is a real warning worth fixing eventually, not a Rider quirk.

### Files produced this session

- `Services/IMessageValidator.cs`
- `Services/TopicValidator.cs`
- `Services/PayloadValidator.cs`
- `Services/MessageAnalyzer.cs` (+ `ValidationResult` record)
- `DiDemo.cs` — Composition Root demo
- Updated `Program.cs` — added menu option 6
- Updated `CSharpFundamentals.csproj` — added `Microsoft.Extensions.DependencyInjection` NuGet reference

### AC coverage so far (SDP-37390)

- ✅ Variables, data types (`int`, `string`, `bool`, `DateTime`)
- ✅ Loops (`for`, `foreach`)
- ✅ Conditionals (`if/else`, `switch`)
- ✅ Functions / methods
- ✅ OOP: classes, inheritance, encapsulation (properties), polymorphism (`virtual`/`override`), interfaces
- ✅ LINQ queries (`Where`, `Select`, `OrderBy`, `GroupBy`, `Any`, `All`, `Count`, `Distinct`, `OfType`, `Cast`)
- ✅ Lambda expressions (single-parameter, multi-parameter, expression-bodied and block-bodied)
- ✅ Dependency Injection basics (Composition Root, `ServiceCollection`, constructor injection, `IEnumerable<T>` for multi-registration)
- ✅ Code pushed to Git repo
- ⏳ FluentAssertions — covered in SDP-37392, minimal touch here
- ⏳ Code reviewed by mentor / QA Lead — pending after Sub-session D closes

**SDP-37390 is now technically complete pending QA Lead review.** Next steps:
1. Post evidence comment on the Jira ticket (draft first, preview, then publish).
2. Transition Backlog → In Progress (if not already) → Done (or await review).
3. Begin SDP-37391 (API automation with RestSharp).
