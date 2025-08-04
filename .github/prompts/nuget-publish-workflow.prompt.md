---
mode: agent
description: "Add a GitHub Actions workflow to build, validate, test, and publish the Simplicity.Analyzers NuGet package using best practices."
---

# Add GitHub Actions Workflow for NuGet Package Publishing

This prompt guides you to create a `.github/workflows/publish.yml` workflow for the Simplicity.Analyzers project, following best practices from [meziantou.net: Publishing a NuGet package using GitHub and GitHub Actions](https://www.meziantou.net/publishing-a-nuget-package-following-best-practices-using-github.htm).

## Context
- Project: Simplicity.Analyzers (Roslyn analyzer, .NET Standard 2.0)
- NuGet metadata and packaging already present in `Simplicity.csproj`
- Goal: Automate build, validation, test, and publish to nuget.org on release
- Use secure secrets for API key
- Follow modern .NET and GitHub Actions best practices

## Instructions

1. **Create `.github/workflows/publish.yml`**
   - Triggers: `push` to `main`, all `pull_request`, `release` (type: `published`), and `workflow_dispatch` (manual)
   - Set environment variables: `DOTNET_SKIP_FIRST_TIME_EXPERIENCE`, `DOTNET_NOLOGO`, `NuGetDirectory`

2. **Jobs**
   - **create_nuget**: Checkout, setup .NET, build and pack, upload artifact
   - **validate_nuget**: Needs `create_nuget`, setup .NET, download artifact, install and run `Meziantou.Framework.NuGetPackageValidation.Tool`
   - **run_test**: Checkout, setup .NET, run tests
   - **deploy**: Needs `validate_nuget` and `run_test`, only on `release`, download artifact, setup .NET, push to nuget.org using `NUGET_APIKEY` secret, use `--skip-duplicate`

3. **Security**
   - Store NuGet API key as a GitHub secret named `NUGET_APIKEY`
   - Use PowerShell shell for cross-platform compatibility
   - Add comments and documentation in the workflow file

4. **Testing**
   - Verify workflow runs on PRs and pushes (build, pack, test, validate)
   - Test publish step by creating a GitHub Release
   - Confirm artifact upload and nuget.org publish only on release

5. **References**
   - [meziantou.net best practices](https://www.meziantou.net/publishing-a-nuget-package-following-best-practices-using-github.htm)
   - [NuGet package validation tool](https://www.nuget.org/packages/Meziantou.Framework.NuGetPackageValidation.Tool)
   - [GitHub Actions documentation](https://docs.github.com/en/actions)

## Example Workflow Structure

```yaml
name: publish
on:
  workflow_dispatch:
  push:
    branches: [ 'main' ]
  pull_request:
    branches: [ '*' ]
  release:
    types: [ published ]

env:
  DOTNET_SKIP_FIRST_TIME_EXPERIENCE: 1
  DOTNET_NOLOGO: true
  NuGetDirectory: ${{ github.workspace }}/nuget

defaults:
  run:
    shell: pwsh

jobs:
  create_nuget:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
        with:
          fetch-depth: 0
      - name: Setup .NET
        uses: actions/setup-dotnet@v4
      - run: dotnet pack --configuration Release --output ${{ env.NuGetDirectory }}
      - uses: actions/upload-artifact@v3
        with:
          name: nuget
          if-no-files-found: error
          retention-days: 7
          path: ${{ env.NuGetDirectory }}/*.nupkg

  validate_nuget:
    runs-on: ubuntu-latest
    needs: [ create_nuget ]
    steps:
      - name: Setup .NET
        uses: actions/setup-dotnet@v4
      - uses: actions/download-artifact@v3
        with:
          name: nuget
          path: ${{ env.NuGetDirectory }}
      - name: Install nuget validator
        run: dotnet tool update Meziantou.Framework.NuGetPackageValidation.Tool --global
      - name: Validate package
        run: meziantou.validate-nuget-package (Get-ChildItem "${{ env.NuGetDirectory }}/*.nupkg")

  run_test:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      - name: Setup .NET
        uses: actions/setup-dotnet@v4
      - name: Run tests
        run: dotnet test --configuration Release

  deploy:
    if: github.event_name == 'release'
    runs-on: ubuntu-latest
    needs: [ validate_nuget, run_test ]
    steps:
      - uses: actions/download-artifact@v3
        with:
          name: nuget
          path: ${{ env.NuGetDirectory }}
      - name: Setup .NET Core
        uses: actions/setup-dotnet@v4
      - name: Publish NuGet package
        run: |
          foreach($file in (Get-ChildItem "${{ env.NuGetDirectory }}" -Recurse -Include *.nupkg)) {
              dotnet nuget push $file --api-key "${{ secrets.NUGET_APIKEY }}" --source https://api.nuget.org/v3/index.json --skip-duplicate
          }
```

---

- Adjust the workflow as needed for your repository/project structure.
- See [copilot-prompt.prompt.md](./copilot-prompt.prompt.md) for prompt file conventions.
- For more details, see the linked best practices article.
