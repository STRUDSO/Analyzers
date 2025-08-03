---
mode: agent
description: "Detect all usages of the greater than sign ('>') in C# code using a Roslyn analyzer."
---

# Roslyn Analyzer: Detect Usage of Greater Than Sign ('>')

This prompt implements a workflow for creating a Roslyn analyzer that detects any usage of the greater than sign (`>`) in C# code. It follows best practices for Copilot Agent Mode and Model Context Protocol (MCP) as described in [Developing with GitHub Copilot Agent Mode and MCP](../docs/Developing%20with%20GitHub%20Copilot%20Agent%20Mode%20and%20MCP.md).

## Requirements
- Create a new analyzer in the `Simplicity` project.
- The analyzer must:
  - Detect all usages of the `>` operator in C# source code.
  - Report a diagnostic at the location of each usage.
  - Provide a clear diagnostic message (e.g., "Usage of '>' is not allowed").
- The analyzer should be unit tested in the `Simplicity.Tests` project.
- Follow repo conventions (internal sealed classes, unique diagnostic ID, documentation, etc.).
- No code fix is required unless specified.

## Implementation Steps
1. **Preparation**
   - Review the [COOKBOOK](../docs/development/COOKBOOK.md) for Roslyn analyzer patterns.
   - Ensure the `Simplicity` project references the necessary Roslyn SDK packages.
2. **Analyzer Implementation**
   - Create a new file, e.g., `GreaterThanAnalyzer.cs` in `Simplicity/Simplicity/`.
   - Define a unique diagnostic ID and message.
   - Implement the analyzer:
     - Register for `SyntaxKind.GreaterThanExpression` in the syntax node action.
     - Optionally, consider other relevant syntax kinds if needed (e.g., `GreaterThanToken`).
     - In the action, report a diagnostic at the location of the `>` operator.
3. **Documentation**
   - Add documentation comments to the analyzer class.
   - Update `AnalyzerReleases.Unshipped.md` with the new diagnostic ID and description.
4. **Testing**
   - Create a new test class, e.g., `GreaterThanAnalyzerTests.cs` in `Simplicity.Tests/`.
   - Write unit tests to verify:
     - The analyzer reports diagnostics for code using `>`.
     - No diagnostics are reported for code not using `>`.
   - Use source snippets and diagnostic verifiers as per repo conventions.
5. **Validation**
   - Build the solution and run all tests.
   - Format code using `dotnet format`.
   - Ensure the analyzer is discoverable and documented.

## Testing
- **Positive Tests**: Code samples using `>` (e.g., `if (a > b) { }`) should trigger diagnostics.
- **Negative Tests**: Code samples without `>` should not trigger diagnostics.
- **Edge Cases**: Test with generics, lambdas, and other contexts where `>` might appear.
- **Diagnostic Location**: Ensure the diagnostic is reported exactly at the `>` token.

## References
- [Developing with GitHub Copilot Agent Mode and MCP](../docs/Developing%20with%20GitHub%20Copilot%20Agent%20Mode%20and%20MCP.md)
- [COOKBOOK](../docs/development/COOKBOOK.md)
- [copilot-prompt.prompt.md](copilot-prompt.prompt.md)

## Notes
- The analyzer is for demonstration and may be overly broad for real-world use.
- No code fix is required unless requested.
- The plan follows the repo's analyzer conventions and structure.
