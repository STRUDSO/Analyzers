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

gh-cp-beast-mode:
    # prompts from https://gist.github.com/burkeholland/88af0249c4b6aff3820bf37898c8bacf#file-beastmode3-1-chatmode-md
    dotnet file add https://gist.githubusercontent.com/burkeholland/88af0249c4b6aff3820bf37898c8bacf/raw/f2bf4380b2dd886f1344544f8125c5ea10854a9c/beastmode3.1.chatmode.md .github/chatmodes/

dotnet-init:
    dotnet new globaljson
    dotnet new gitignore
    dotnet new editorconfig
    dotnet new tool-manifest
    dotnet tool install dotnet-file

