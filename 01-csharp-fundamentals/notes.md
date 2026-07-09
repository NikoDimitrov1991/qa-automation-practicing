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

### AC coverage so far (SDP-37390)

- ✅ Variables, data types (`int`, `string`, `bool`)
- ✅ Loops (`for`)
- ✅ Conditionals (`if/else`, `switch`)
- ✅ Functions (`Run()`, `IsEven()`)
- ✅ Code pushed to Git repo
- ⏳ OOP concepts (inheritance, encapsulation, polymorphism, interfaces) — next session
- ⏳ LINQ, lambdas, Dependency Injection — later sessions
- ⏳ FluentAssertions — covered in SDP-37392, minimal touch here
