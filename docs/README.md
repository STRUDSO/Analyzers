# Simplicity.Analyzers Rules

This document lists the rules included in the Simplicity Roslyn Analyzer package.

| ID           | Category | Title                        | Enabled | Code Fix |
|--------------|----------|------------------------------|---------|----------|
| [SIMP1001](https://github.com/STRUDSO/Analyzers/blob/main/docs/Rules/SIMP1001.md) | Syntax   | Usage of '>' is not allowed  |   ✔️    |    ✔️    | 

## Rule Details


### SIMP1001: Usage of '>' is not allowed
- **Category:** Syntax
- **Description:**
  Detects all usages of the greater than sign (`>`) in C# code and reports a warning.
- **Code Fix:**
  Offers a code fix to flip operands and use '<' instead of '>'.

