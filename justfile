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

dotnet-init:
    dotnet new globaljson
    dotnet new gitignore
    dotnet new editorconfig
    dotnet new tool-manifest
    dotnet tool install dotnet-file

