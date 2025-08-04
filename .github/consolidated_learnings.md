# Consolidated Learnings

## Roslyn Analyzer Development

### Diagnostic Location
- Always verify the exact column and span reported by the analyzer. Test expectations may need to be adjusted to match the actual diagnostic location.

### Accessibility
- Analyzer classes must be `public` for test projects to access them. Using `internal` will cause test failures due to inaccessibility.

### Test Patterns
- Reuse the Verifier pattern from other tests for consistency and to reduce boilerplate.

### Code Fixes & Trivia Handling
- When writing code fixes that flip or swap operands, trivia (comments/whitespace) is attached to the syntax node, not the identifier. Tests must expect trivia to move with the node, not the logical variable.
- For analyzer+code fix tests, use `CodeFixVerifier<Analyzer, CodeFixProvider>`, not `AnalyzerVerifier<Analyzer>`, to access `VerifyCodeFixAsync`.
- Always declare variables in test code to avoid compiler errors that can interfere with diagnostic expectations.
- When the prompt requires a specific transformation (e.g., flip operands and change operator), ensure the code fix and tests match the exact semantics and trivia model of Roslyn.
- Diagnostic locations and trivia handling often require iterative adjustment in tests to match actual analyzer/code fix output.

### Release Tracking
- Update `AnalyzerReleases.Unshipped.md` immediately after adding a new rule. This avoids confusion and ensures proper release tracking.

### Prompt-Driven Development
- Following a detailed prompt file ensures all requirements are met and nothing is missed. Prompts help drive a thorough, repeatable process.

### Troubleshooting
- If tests fail due to column mismatches, check the actual diagnostic output and update test expectations accordingly.

### Continuous Improvement
- Automate column detection in tests to reduce manual trial and error.
- Consider edge cases for all relevant syntax kinds, not just the most common ones.
- Refactor test utilities for DRYness as the analyzer/test suite grows.

---

_Last updated: 2025-08-03_
