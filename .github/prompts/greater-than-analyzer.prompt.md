# Copilot Review: Greater Than Analyzer

## Step-by-Step Plan (After Implementation)

### 1. Preparation
- Reviewed the implementation prompt ([greater-than-analyzer.prompt.md](../prompts/greater-than-analyzer.prompt.md)) and repo conventions.
- Verified Roslyn SDK references in `Simplicity/Simplicity.csproj`.

### 2. Analyzer Implementation
- Created `GreaterThanAnalyzer.cs` in `Simplicity/Simplicity/`.
- Used `SyntaxKind.GreaterThanExpression` to detect all usages of the `>` operator.
- Reported a diagnostic at the operator token location with a clear message and unique ID (`SIMP1001`).
- Made the analyzer `public sealed` for test accessibility.

### 3. Documentation
- Added XML doc comments to the analyzer class.
- Updated `AnalyzerReleases.Unshipped.md` with the new rule and description.

### 4. Testing
- Created `GreaterThanAnalyzerTests.cs` in `Simplicity.Tests`.
- Used the same test structure as other analyzers in the repo.
- Wrote positive, negative, and edge case tests:
  - Detected `>` in `if` statements and lambdas.
  - Ensured no diagnostics for code without `>`.
- Adjusted test column numbers to match actual diagnostic locations.
- Ensured all tests pass.

### 5. Validation
- Built the solution and ran all tests using `dotnet test`.
- Fixed test failures by correcting diagnostic column numbers.
- Confirmed all tests pass and the analyzer is discoverable.

## Key Learnings & Insights
- **Diagnostic Location**: Always verify the exact column reported by the analyzer; test expectations may need adjustment.
- **Accessibility**: Analyzer classes must be `public` for test projects to access them.
- **Test Patterns**: Reusing the Verifier pattern from other tests ensures consistency and reduces boilerplate.
- **Release Tracking**: Update `AnalyzerReleases.Unshipped.md` immediately after adding a new rule to avoid confusion.
- **Prompt-Driven Development**: Following a detailed prompt file ensures all requirements are met and nothing is missed.

## What Could Be Improved Next Time
- Automate column detection in tests to reduce manual trial and error.
- Consider edge cases for generics and other usages of `>` (not just binary expressions).
- Add code fix support if required by future prompts.
- Refactor test utilities for even more DRYness if adding more analyzers.

---

See also: [greater-than-analyzer.prompt.md](../prompts/greater-than-analyzer.prompt.md)
