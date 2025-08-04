---
mode: agent
description: "Expand the Greater Than Analyzer to provide a code fix that flips the operands and changes '>' to '<'."
---

# Greater Than Analyzer with Code Fix Prompt

## Task
Expand the existing Greater Than Analyzer so that it not only reports diagnostics for the use of the `>` operator, but also provides a code fix that flips the operands and changes the operator to `<`.

## Context
- The analyzer is implemented in `Simplicity/Simplicity/GreaterThanAnalyzer.cs`.
- The analyzer reports diagnostics for all usages of the `>` operator (SyntaxKind.GreaterThanExpression).
- The code fix should:
  - Register for the same diagnostic ID as the analyzer (e.g., `SIMP1001`).
  - When triggered, replace the `>` operator with `<` and swap the left and right operands.
  - Preserve trivia (comments/whitespace) and formatting.
  - Work for all valid binary expressions using `>`.

## Implementation Guidelines
- Create a new `CodeFixProvider` in the same project as the analyzer.
- In `RegisterCodeFixesAsync`, locate the `BinaryExpressionSyntax` node for the diagnostic.
- Use Roslyn's `SyntaxFactory` to create a new `BinaryExpressionSyntax` node:
  - Operator: `SyntaxKind.LessThanExpression`
  - Left: original right operand
  - Right: original left operand
- Replace the original node in the syntax tree with the new node.
- Ensure the code fix is only offered for valid `>` expressions.
- Add or update tests in `Simplicity.Tests` to verify the code fix:
  - The code fix should transform `a > b` to `b < a`.
  - Test with various whitespace, comments, and edge cases.

## References
- [greater-than-analyzer.prompt.md](greater-than-analyzer.prompt.md)
- [Roslyn CodeFixProvider documentation](https://learn.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/analyzers-and-code-fixes-overview)
- [Roslyn SyntaxFactory API](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxfactory)

## Acceptance Criteria
- The analyzer continues to report diagnostics for all `>` usages.
- The code fix is available and correctly flips the operands and operator.
- All tests pass, including new tests for the code fix.
- The implementation follows repo and Roslyn conventions.

---

This prompt is standalone and can be used to guide the implementation or review of the expanded analyzer and code fix feature.
