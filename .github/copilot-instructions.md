# Copilot Instructions for MjoMeet

## Critical Guidelines
- **MUST READ** the [COOKBOOK](../docs/development/COOKBOOK.md) before starting any work as it contains critical patterns and proven solutions.

## Core Commands

### Build & Run
- **Build solution**: `dotnet build src/MjoMeet.sln`
- **Run application**: Navigate to `src/MjoMeet.Aspire.AppHost` and run `dotnet run` (starts full stack via .NET Aspire)
- **Format code**: `dotnet format src/MjoMeet.sln` (always run after changes)
- **Watch mode**: Use VS Code tasks or `dotnet watch run --project src/MjoMeet/MjoMeet.csproj`

### Testing
- **Run all tests**: `dotnet test src/MjoMeet.sln`
- **Run specific test project**: `dotnet test src/Modules/UserAccess/Tests/MjoMeet.UserAccess.Tests.UnitTests/`
- **Run single test**: `dotnet test src/MjoMeet.Tests/ --filter "FullyQualifiedName~TestName"`
- **Detailed test output**: `dotnet test --logger "console;verbosity=detailed"`

### Database & CLI
- **Marten CLI help**: Navigate to `src/MjoMeet` and run `dotnet run -- help`
- **Generate Marten classes**: `dotnet run -- codegen write`
- **Database tools**: Navigate to `src/MjoMeet.Tools.Database` for schema management

### VS Code Tasks
Available tasks (use Ctrl+Shift+P → "Tasks: Run Task"):
- `build` - Build the Aspire AppHost project
- `watch` - Run in watch mode  
- `publish` - Publish the main project

## High-Level Architecture

### Technology Stack
- **.NET 9** with C# (latest language version)
- **PostgreSQL** with **Marten** ORM for document storage
- **.NET Aspire** for orchestration and local development
- **MediatR** for command/query handling
- **FluentValidation** for input validation
- **xUnit**, **AutoFixture**, **Bogus** for testing
- **Azure Service Bus Emulator** for messaging
- **Azure Storage Emulator** for blob storage

### Modular Monolith Structure
```
src/
├── MjoMeet.Aspire.AppHost/        # Aspire orchestration (STARTUP PROJECT)
├── MjoMeet/                       # Main API project
├── MjoMeet.BackgroundWorkers/     # Background services (outbox/inbox/scheduler)
├── BuildingBlocks/                # Shared infrastructure
└── Modules/
    ├── UserAccess/               # User registration & management
    ├── MeetingGroups/            # Meeting groups & meetings lifecycle
    └── MeetingGroupProposals/    # Meeting group proposals
    └── Attachments/              # Meeting group attachments
    └── Notifications/            # Notification and Notification preferences

```

Each module follows DDD/Hexagonal architecture:
- **Domain**: Aggregates, value objects, domain events  
- **Application**: Commands, queries, handlers, domain event listeners
- **Infrastructure**: Repositories, external service implementations
- **Integration**: Cross-module integration events

### Data Architecture
- **Document Store**: Marten stores domain objects as PostgreSQL JSON documents
- **Outbox/Inbox Pattern**: Reliable cross-module messaging via database events
- **Event Sourcing**: Domain events for internal communication, integration events for cross-module

## Repo-Specific Style Rules

### Code Organization
- Use `internal sealed` for classes by default
- Place command/handler in same file, domain event/listener in same file
- One endpoint per controller (screaming architecture)
- Commands/queries use basic data types only (no complex objects)

### Naming Conventions
- **Commands**: `{Action}{Entity}Command` (e.g., `RegisterUserCommand`)
- **Handlers**: `{CommandName}Handler`, `{QueryName}Handler`
- **Domain Events**: `{Entity}{Action}DomainEvent`
- **Value Objects**: Domain primitives (Email, UserId, etc.)
- **Files**: Match class names exactly

### Error Handling
- Return `ProblemDetails` from API endpoints
- Use `DomainException` for business rule violations  
- Global exception handling converts exceptions to HTTP responses:
  - `ArgumentException` → 400 Bad Request
  - `DomainException` → 400 Bad Request  
  - `NotFoundException` → 404 Not Found
  - `AlreadyExistingException` → 409 Conflict

### Security & Privacy
- **Always** return consistent responses (prevent enumeration attacks)
- **Always** use `TelemetryDataPrivacy.HashSensitiveData()` for PII in telemetry
- **Never** log raw emails, names, or sensitive data
- Normalize response timing to prevent timing attacks
- Plan rate limiting from day one, not as retrofit

### Testing Constraints
- **Use ONLY project-provided fakes**: `FakeCommandScheduler`, `FakeEmailSender`, `FakeActivityScope`
- **NO external mocking libraries** (Moq, NSubstitute forbidden)
- Use standard xUnit assertions only
- Layer-specific testing: domain events in domain tests, side effects in handler tests
- Integration tests use `IClassFixture<DatabaseFixture>` with real database

### Domain Patterns
- Private constructors with static factory methods for aggregates/value objects
- Use `IDomainEventListener<T>` for domain event handlers (not MediatR directly)
- Repository pattern with interfaces, Marten implementation
- Scheduled commands need parameterless constructor + private setters for serialization
- Always inject `IActivityScope` in handlers: `using var activity = _activityScope.Start(activityName)`

## Critical Prerequisites

Before starting ANY feature implementation:

1. **Read [consolidated_learnings.md](../docs/consolidated_learnings.md)** - Contains critical insights from previous implementations
2. **Infrastructure Discovery** - Run searches to find existing patterns before coding:
   ```bash
   grep -r "ExceptionType\|DomainMethod" src/
   find src/ -name "*Fake*Repository*"
   grep -r "GetById\|Update\|SaveChanges" src/
   ```
3. **Security Analysis** - Plan security measures upfront, never retrofit
4. **Domain Event Storming** - Identify aggregates, events, and business rules first

## Workflow

Before starting any coding task, follow this structured workflow to ensure a thorough understanding and effective implementation of features or fixes in the MjoMeet codebase.

You MUST read [consolidated_learnings](../docs/consolidated_learnings.md) to understand the overall architecture, design patterns, and conventions used in the MjoMeet project.

I recommend you read all relevant documentation and code comments to gain a deeper understanding of the system.

## Querying Microsoft Documentation

You have access to an MCP server called `microsoft.docs.mcp` - this tool allows you to search through Microsoft's latest official documentation, and that information might be more detailed or newer than what's in your training data set.

When handling questions around how to work with native Microsoft technologies, such as C#, F#, ASP.NET Core, Microsoft.Extensions, NuGet, Entity Framework, the `dotnet` runtime - please use this tool for research purposes when dealing with specific / narrowly defined questions that may occur.

## High-Level Problem Solving Strategy

1. Understand the problem deeply. Carefully read the instructions and think critically about what is required.
2. Investigate the codebase. Explore relevant files, search for key functions, and gather context.
3. Develop a clear, step-by-step plan. Break down the feature into manageable, incremental steps.
4. Always ask questions if something is unclear before starting to implement.
5. Before each code change, always consult the relevant rule files, to make sure you understand the rules and follow the conventions in the codebase.
6. Implement the feature incrementally. Make small, testable code changes.
7. Debug as needed. Use debugging techniques to isolate and resolve issues.
8. Test frequently. Run tests after each change to verify correctness.
9. Iterate until the feature is fully implemented and all tests pass.
10. Reflect and validate comprehensively. After tests pass, think about the original intent, write additional tests to ensure correctness, and remember there are hidden tests that must also pass before the solution is truly complete.

Refer to the detailed sections below for more information on each step.

### 1. Deeply Understand the Problem

Carefully read the issue and think hard about a plan to solve it before coding.

### 2. Codebase Investigation

- Explore relevant files and directories.
- Search for key functions, classes, or variables related to the issue.
- Read and understand relevant code snippets.
- Identify the root cause of the problem.
- Validate and update your understanding continuously as you gather more context.

### 3. Develop a Detailed Plan

- Outline a specific, simple, and verifiable sequence of steps to implement the feature.
- Break down the feature into small, incremental changes.

### 4. Ask Questions
- If something is unclear, ask questions before starting to implement. DO NOT assume anything.

### 5. Before Making Code Changes

- Review the relevant design and architectural guidelines.
- Ensure you understand the impact of your changes on the overall system.
- Consider potential edge cases and how they will be handled.

### 6. Making Code Changes

- Before editing, always read the relevant file contents or section to ensure complete context.
- If a patch is not applied correctly, attempt to reapply it.
- Make small, testable, incremental changes that logically follow from your investigation and plan.

### 7. Debugging

- Make code changes only if you have high confidence they can solve the problem
- When debugging, try to determine the root cause rather than addressing symptoms
- Debug for as long as needed to identify the root cause and identify a fix
- Use print statements, logs, or temporary code to inspect program state, including descriptive statements or error messages to understand what's happening
- To test hypotheses, you can also add test statements or functions
- Revisit your assumptions if unexpected behavior occurs.

### 8. Testing

- Run tests frequently using `dotnet test` (or equivalent).
- Write tests to cover the changes you made.
- Run all tests to ensure nothing is broken.
- If a test fails, use debugging techniques to identify the issue.
- After each change, verify correctness by running relevant tests.
- If tests fail, analyze failures and revise your patch.
- Write additional tests if needed to capture important behaviors or edge cases.
- Ensure all tests pass before finalizing.

### 9. Final Verification
- Confirm the feature works as intended.
- Review your solution for logic correctness and robustness.
- Iterate until you are extremely confident the feature is complete and all tests pass.
- When all tests pass, run the command `dotnet format` to ensure code formatting is consistent with the project's style.

### 10. Final Reflection and Additional Testing
- After tests pass, think about the original intent, write additional tests to ensure correctness, and remember there are hidden tests that must also pass before the solution is truly complete.

## General Principles
- Follow Domain-Driven Design (DDD) tactical patterns: use Aggregates, Value Objects, Domain Events, and Entities.
- Use factory methods for object creation and validation.
- Apply CQRS: separate Commands (write) and Queries (read).
- Use rich domain models with business logic encapsulated in the domain layer.
- Use domain events for internal communication and integration events for cross-module communication.
- Follow the Hexagonal/Ports and Adapters architecture: keep domain logic independent from infrastructure.
- Write tests first (TDD) for new features and business rules.
- Use ubiquitous language from the domain in naming.
- Use private setters and constructors for domain objects; expose only what is necessary.
- Use the repository pattern for data access.
- Use the outbox/inbox pattern for reliable messaging between modules.
- Ensure scheduled commands include a private parameterless constructor and private setters to support serialization.
- Use the `IDomainEventListener<T>` interface for domain event handlers instead of MediatR directly.
- Inject and use `IActivityScope` in every handler to add tracing and observability.

## Feature Implementation Workflow
1. **Start with Domain Modeling** (Event Storming/Story Telling): identify aggregates, value objects, and domain events.
2. **Write TDD Unit Tests** for aggregates and command handlers (include happy and edge cases).
3. **Implement Domain Logic**: add methods on aggregates that enforce rules and raise domain events.
4. **Define Commands & Handlers**: use immutable types with factory methods; validate with FluentValidation; handlers orchestrate but delegate logic to domain.
5. **Implement Domain Event Listeners**: use `IDomainEventListener<T>`; schedule follow-up commands; include activity tracing.
6. **Create Scheduled Commands**: ensure serialization support (parameterless ctor, private setters); implement handlers with infrastructure concerns (e.g., email).
7. **Repository Methods**: add or update (e.g., `GetByEmail`) to support queries.
8. **Expose via API**: add endpoint, enforce validation, return ProblemDetails on errors.
9. **Integration Tests**: cover the full HTTP-to-domain workflow, outbox/inbox patterns, scheduled actions, and repository methods (e.g., `GetByEmail`, `GetById`).

## Example: ResendUserRegistration Feature
- Command: `ResendUserRegistrationCommand` (input: email)
- Handler: Checks if a UserRegistration exists for the email and is waiting for confirmation
- Aggregate: Adds `RequestResendConfirmation()` method, raises `UserRegistrationResendRequestedDomainEvent`
- Domain Event Handler: Schedules a `SendUserRegistrationEmailCommand` via the command scheduler
- Scheduled Command Handler: Sends the confirmation email
- Repository: Add `GetByEmail` method
- API: Add endpoint to trigger the command
- Tests: Cover all business rules and edge cases

## Coding Style
- Use `internal sealed` for classes by default.
- Place command and handler in the same file, and domain event with its listener in the same location.
- Use value objects for domain primitives (e.g., Email, UserName).
- Use strongly typed IDs for aggregates.
- Use private constructors and static factory methods for value objects and aggregates.
- Use FluentValidation for command/query validation.
- Return ProblemDetails from API on errors.
- Scheduled commands must include a private parameterless constructor and private setters for all properties.
- Use `internal sealed` for classes by default; handlers, validators, and authorizers follow this rule.
- Use project-provided fakes (`FakeCommandScheduler`, `FakeEmailSender`, `FakeActivityScope`) instead of external mocking libraries.

## Documentation
- Document new features and patterns in the `docs/features/{module}/{use-case}` folder as needed.
- Update ADRs in `docs/adr` if architectural decisions change.
- If changes has been made to any Module, update the Module's README.md file to reflect the changes.

## Additional Notes
- DO NOT use any external libraries unless specified like FluentAssertions and Moq.
- Follow existing project conventions and patterns.
- Use the existing test framework and patterns.
- Keep the code clean and maintainable.
- Use meaningful names for classes, methods, and variables.
- Avoid code duplication and follow DRY principles.
- Always instrument handlers with clear activity names: `${ModuleName}/${HandlerName}/${EventOrCommand}` and add relevant tags/events.
- Avoid MediatR for domain event listeners; prefer `IDomainEventListener<T>` to keep consistency.
- Use the repository pattern to abstract data access behind interfaces.

## Important Documentation Resources

This project has extensive documentation to guide development:

- **[docs/consolidated_learnings.md](../docs/consolidated_learnings.md)** - Critical patterns and proven solutions (MUST READ)
- **[docs/BuildingBlocks.md](../docs/BuildingBlocks.md)** - Shared infrastructure overview
- **[docs/DevelopmentOfAFeature.md](../docs/DevelopmentOfAFeature.md)** - Step-by-step feature development guide
- **[docs/adr/](../docs/adr/)** - Architecture Decision Records (30+ decisions documented)
- **Module READMEs** - Each module has detailed documentation of its purpose and features
- **Feature Implementation Logs** - Detailed logs of actual feature implementations in `docs/features/`