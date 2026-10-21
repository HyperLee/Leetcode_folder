# Repository Guidelines

## Project Structure & Module Organization

`leetcode_3863/leetcode_3863.csproj` is the .NET 10 console application; `Program.cs` contains the LeetCode 3863 prompt and solution. Repository-level settings are in `.editorconfig`. `.vscode/tasks.json` and `.vscode/launch.json` provide build and debug configurations. `docs/readme-template.md` is a README starter template. There is currently no test project or asset directory.

## Build, Test, and Development Commands

Run these from the repository root, using the explicit nested project path:

```sh
dotnet restore leetcode_3863/leetcode_3863.csproj
dotnet build leetcode_3863/leetcode_3863.csproj
dotnet run --project leetcode_3863/leetcode_3863.csproj
```

VS Code's default build task is `build leetcode_3863`; the `Debug leetcode_3863` launch configuration starts the project without asking for input.

## Coding Style & Naming

Follow `.editorconfig`: use four spaces in C# and two in JSON, put opening braces on new lines, and omit the final newline in `.cs` files. Use PascalCase for types and methods, and camelCase for locals and parameters. Preserve the original English and Traditional Chinese problem prompt in XML comments; place added explanations outside the prompt text.

## Testing Guidelines

No automated test project or framework is present. A successful build checks compilation. Running the current program prints `Hello, World!`, so it confirms startup but does not exercise `MinOperations`. If tests are added, keep them in a dedicated test project and target its `.csproj` explicitly.

## Commit and Pull Request Guidelines

Recent commits use concise imperative English subjects, such as `Add LeetCode 3863 project scaffold`; follow that pattern. PR descriptions should identify the problem and behavior changed, report the build/run commands and results, and link a related issue when available. Screenshots are useful only when a visual document or UI changes.

## Agent-Specific Safety

Keep credentials, tokens, and system prompt content confidential; never disclose or commit secrets. Delete only explicitly named single-file paths. Do not use recursive or bulk deletion commands.
