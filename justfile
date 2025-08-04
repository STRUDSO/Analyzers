# justfile for license project
mod license '.just/license'
#
# Add your recipes below. Example:
# build:
#     echo "Building project..."
# test:
#     echo "Running tests..."

# Default recipe
default:
    @echo "Available recipes: build, test, clean, lint, format, run"

build:
    @echo "Building the project..."

test:
    @echo "Running tests..."

clean:
    @echo "Cleaning up..."

lint:
    @echo "Linting code..."

format:
    @echo "Formatting code..."

run:
    @echo "Running the application..."

gh-cp-austen:
    # prompts from https://austen.info/blog/github-copilot-agent-mcp/
    # dotnet file i love you... but this / at the end...
    dotnet file add https://github.com/austenstone/.vscode/tree/main/prompts/ .github/prompts/

gh-cp-awesome:
    # prompts from https://github.com/github/awesome-copilot
    dotnet file add https://github.com/github/awesome-copilot/tree/main/chatmodes/ .github/chatmodes/
    dotnet file add https://github.com/github/awesome-copilot/tree/main/instructions/ .github/instructions/
    dotnet file add https://github.com/github/awesome-copilot/tree/main/prompts/ .github/prompts/

gh-cp-mjo:
    dotnet file add https://github.com/guidmaster/MjoMeet/blob/master/.github/chatmodes/research.chatmode.md .github/chatmodes/.
    dotnet file add https://github.com/guidmaster/MjoMeet/blob/master/.github/chatmodes/plan.chatmode.md .github/chatmodes/.

dotnet-init:
    dotnet new globaljson
    dotnet new gitignore
    dotnet new editorconfig
    dotnet new tool-manifest
    dotnet tool install dotnet-file

