# Copilot Review: Greater Than Analyzer Code Fix

## Step-by-Step Plan (After Implementation)

### 1. Preparation
- Reviewed the code fix prompt ([greater-than-analyzer-codefix.prompt.md](../prompts/greater-than-analyzer-codefix.prompt.md)) and repo conventions.
- Used `git diff --name-only` and `git log` to confirm all files changed and steps taken.

### 2. Implementation
- Created `GreaterThanCodeFixProvider.cs` in `Simplicity/Simplicity/`.
- Implemented a code fix that:
  - Registers for `SIMP1001` diagnostics from `GreaterThanAnalyzer`.
  - Flips operands and changes `>` to `<` using Roslyn's `SyntaxFactory`.
  - Swaps leading/trailing trivia between operands to preserve comments/whitespace as much as possible.
- Ensured the code fix is only offered for valid `>` binary expressions.

### 3. Testing
- Updated `GreaterThanAnalyzerTests.cs` to use `CodeFixVerifier<Analyzer, CodeFixProvider>` for code fix tests.
- Added tests to verify:
  - The code fix transforms `a > b` to `b < a`.
  - Trivia and comments are preserved and swapped with the operands.
  - The code fix works in lambdas and with various whitespace/comment edge cases.
- Declared all variables in test code to avoid compiler errors interfering with diagnostics.
- Adjusted expected diagnostic locations and fixed code to match Roslyn's trivia model.

### 4. Validation
- Built the solution and ran all tests using `dotnet test`.
- Iteratively fixed test failures by:
  - Updating test expectations for trivia and diagnostic locations.
  - Ensuring the code fix output matches Roslyn's trivia attachment model.
- Confirmed all tests pass and the code fix is discoverable and correct.

## New Knowledge & Insights
- **Trivia Handling**: When flipping operands, trivia (comments/whitespace) is attached to the syntax node, not the identifier. Tests must expect trivia to move with the node, not the logical variable.
- **Test Infrastructure**: For analyzer+code fix tests, use `CodeFixVerifier<Analyzer, CodeFixProvider>`, not `AnalyzerVerifier<Analyzer>`, to access `VerifyCodeFixAsync`.
- **Test Inputs**: Always declare variables in test code to avoid compiler errors that can interfere with diagnostic expectations.
- **Prompt-Driven Refactoring**: When the prompt requires a specific transformation (e.g., flip operands and change operator), ensure the code fix and tests match the exact semantics and trivia model of Roslyn.
- **Iterative Test Correction**: Diagnostic locations and trivia handling often require iterative adjustment in tests to match actual analyzer/code fix output.

---

See also: [greater-than-analyzer-codefix.prompt.md](../prompts/greater-than-analyzer-codefix.prompt.md)
