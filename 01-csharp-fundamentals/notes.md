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

### AC coverage so far (SDP-37390)

- ✅ Variables, data types (`int`, `string`, `bool`, `DateTime`)
- ✅ Loops (`for`, `foreach`)
- ✅ Conditionals (`if/else`, `switch`)
- ✅ Functions / methods
- ✅ OOP: classes, inheritance, encapsulation (properties), polymorphism (`virtual`/`override`), interfaces
- ✅ Code pushed to Git repo
- ⏳ LINQ (basic `.Contains()` used, no full LINQ query yet) — next session
- ⏳ Lambda expressions — next session
- ⏳ Dependency Injection — later session
- ⏳ FluentAssertions — covered in SDP-37392, minimal touch here
