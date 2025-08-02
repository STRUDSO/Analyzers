# Learnings from .NET Project Initialization (August 2, 2025)

## Key Steps and Best Practices

1. **Planning is Essential**
   - Start with a clear plan before running commands or scaffolding code.
   - Identify the minimal setup needed (e.g., ignore files, config, tools) before adding projects.

2. **.NET CLI is Powerful**
   - The `dotnet` command can generate most boilerplate files: `.gitignore`, `.editorconfig`, `global.json`, and even license files.
   - Local tools can be managed with `dotnet new tool-manifest` and installed per project.

3. **Automation with justfile**
   - Recipes in a `justfile` can automate repetitive setup tasks.
   - Interactive prompts (using `read`) can be used for user input, e.g., license selection.

4. **Version Control Hygiene**
   - Commit configuration and setup files early for reproducibility.
   - Use `.gitignore` to keep the repository clean.

5. **Optional Enhancements**
   - Consider adding a `README.md`, `LICENSE`, and directory structure (`src/`, `tests/`) for clarity and maintainability.
   - `.gitattributes` and pre-commit hooks can further improve collaboration and code quality.

## Notable Commands Used
- `dotnet new gitignore`
- `dotnet new editorconfig`
- `dotnet new globaljson --sdk-version <version>`
- `dotnet new tool-manifest`
- `dotnet tool install dotnet-file`
- `dotnet new license --license <type>`

## Interactive Automation
- The justfile can prompt for user input (e.g., license type) to make setup more flexible.
- Not all shell interactivity is supported, but basic prompts work well for simple choices.

## General Advice
- Start small, automate the basics, and expand as the project grows.
- Use the .NET CLI and justfile together for a robust, repeatable setup.
