
## Roslyn Analyzer Sample Projects

This repository provides a template for building Roslyn analyzers and code fix providers, including:

- A core analyzer library
- A sample project that consumes the analyzers
- Unit tests for analyzer and code fix validation

---

### Project Structure

#### `Simplicity`
A .NET Standard project containing sample Roslyn analyzers and code fix providers.

> **Note:** You must build this project to see analyzer warnings in your IDE.

- [`GreaterThanAnalyzer.cs`](Analyzers/GreaterThan/GreaterThanAnalyzer.cs): Detects usage of the greater-than sign (`>`) in C# code and reports a warning.
- [`GreaterThanCodeFixProvider.cs`](Analyzers/GreaterThan/GreaterThanCodeFixProvider.cs): Offers a code fix to flip operands and replace `>` with `<` for flagged expressions.

#### `Simplicity.Sample`
A sample project that references the analyzers. See the `ProjectReference` parameters in [`Simplicity.Sample.csproj`](../Simplicity.Sample/Simplicity.Sample.csproj) to ensure analyzers are referenced correctly.

#### `Simplicity.Tests`
Unit tests for the analyzers and code fix provider. Developing language features is easiest when starting with unit tests.

---

## Getting Started

### Debugging
- Use the [launchSettings.json](Properties/launchSettings.json) profile.
- Debug analyzer tests directly.

### Syntax Node Exploration
Use the Roslyn Visualizer tool window to inspect syntax trees and better understand which nodes to target in your analyzers.

### Further Reading
- [Roslyn GitHub Wiki](https://github.com/dotnet/roslyn/blob/main/docs/wiki/README.md) — Complete documentation on building and wiring analyzers.