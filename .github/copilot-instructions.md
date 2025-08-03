
# Copilot Instructions for Roslyn Analyzer Projects


## Critical Guidelines
- [PLACEHOLDER] **MUST READ** the [COOKBOOK](../docs/development/COOKBOOK.md) before starting any work as it contains critical patterns and proven solutions for Roslyn analyzers.


## Core Commands

### Build & Run
- **Build solution**: `dotnet build` (run in repo root)
- **Format code**: `dotnet format` (run in repo root after changes)
- **Watch mode**: `dotnet watch test` (run in test project folder)

### Testing
- **Run all tests**: `dotnet test` (run in repo root)
- **Run specific test project**: `dotnet test Simplicity.Tests/Simplicity.Tests.csproj`
- **Run single test**: `dotnet test Simplicity.Tests/Simplicity.Tests.csproj --filter "FullyQualifiedName~TestName"`
- **Detailed test output**: `dotnet test --logger "console;verbosity=detailed"`

### VS Code Tasks
Available tasks (use Ctrl+Shift+P → "Tasks: Run Task"):
- `build` - Build the analyzer project
- `test` - Run all tests
- `format` - Format code

src/

## Project Structure

- `Simplicity/` — Analyzer implementation
- `Simplicity.Sample/` — Sample project referencing the analyzer
- `Simplicity.Tests/` — xUnit test project for analyzer/code fix


## Repo-Specific Style Rules

- Use `internal sealed` for classes by default
- Keep analyzers focused on a single responsibility
- Each analyzer should have a unique diagnostic ID and be documented
- Prefer semantic analysis for logic, syntax analysis for structure
- Code fix providers should only address diagnostics produced by your analyzers
- Use source snippets and diagnostic verifiers to test analyzer/code fix behavior


## Placeholders for AI Agent Instruction Files

- [PLACEHOLDER] /docs/consolidated_learnings.md — Add critical patterns and proven solutions for Roslyn analyzers here.
- [PLACEHOLDER] /docs/BuildingBlocks.md — Document shared infrastructure for analyzers here.
- [PLACEHOLDER] /docs/DevelopmentOfAFeature.md — Step-by-step feature development for analyzers.
- [PLACEHOLDER] /docs/adr/ — Architecture Decision Records for analyzer repo.

