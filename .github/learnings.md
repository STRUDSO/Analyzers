
## Learnings from August 2, 2025: Copilot Enablement and dotnet-file Usage

- Enabled the project for GitHub Copilot by importing best-practice resources from the MjoMeet repository.
- Used the `dotnet-file` tool to fetch:
   - `.github/copilot-instructions.md` for Copilot configuration and guidance
   - `.github/chatmodes/` for chat mode definitions
   - `.github/instructions/` for instruction sets
   - `.github/prompts/` for prompt templates
- Noted that some subfolders (like `.github/copilot`) may not exist or be accessible; verified successful fetches for chatmodes, instructions, and prompts.
- Documented the process in this file for future reference and reproducibility.
- Committed all changes with a clear message to track Copilot setup and documentation improvements.

## Learnings from August 2, 2025: Enabling Copilot and Using dotnet-file

- To enable the project for Copilot and reuse best practices, we used the `dotnet-file` tool to fetch process documentation and GitHub workflow files from remote repositories.
   - Ran `dotnet file add https://github.com/LearnWithLlew/AgenticAi.Java.StarterProject/tree/master/.windsurf/processes` to import process documentation.
   - Ran `dotnet file add https://github.com/guidmaster/MjoMeet/tree/master/.github` to import GitHub workflow/config files (note: some files may require retry if network issues occur).
- This approach allows rapid onboarding of best practices and automation templates from established projects.
- Continue to use `dotnet-file` for keeping shared process and workflow files up to date.
## Learnings from August 2, 2025 Session

### Handling .NET Initialization Issues
- An empty `global.json` file will break all `dotnet new` commands. Always ensure `global.json` is either valid or absent before initializing a project.
- If you encounter persistent errors about JSON parsing, check for empty or malformed config files early.
- To make SDK version selection more robust, add a `rollForward` property to `global.json` (e.g., `"rollForward": "latestFeature"`). This allows the .NET CLI to use a compatible SDK if the exact version is not installed, improving cross-environment reliability.

### justfile Recipe Improvements
- Recipes that require user input should use parameters, not shell prompts, as `just` does not support interactive input in recipes.
- Example fix: Change `license:` recipe from using `read` to accepting a parameter, e.g., `license license_type:`.

### Commit Notation
- Use arlobee commit notation for clarity and consistency. Example: `[setup] Initialize dotnet project scaffolding and justfile improvements`.

### General Workflow
- If a setup step fails, check for hidden or empty files that may block initialization.
- Automate as much as possible, but always verify that automation works in a clean environment.
- Always test changes after making updates to configuration or code (e.g., run `dotnet --info` or `dotnet build` after editing `global.json`).
- Commit changes with clear messages and verify that the repository state is correct after each step.

### Next Steps
- After scaffolding, create a project (e.g., `dotnet new console -n MyApp`) before running `dotnet build`.
- Continue to capture learnings and update documentation as the project evolves.
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
- When working with an assistant, expect to be prompted to capture learnings at the end of a session to help maintain up-to-date documentation and continuous improvement.

## Capturing Learnings at Session End
- At the end of each development session or chat, consider capturing key learnings, workflow improvements, and issues encountered.
- For future sessions, the assistant should prompt: "Would you like to capture learnings from this session now?" to help maintain up-to-date documentation and continuous improvement.
