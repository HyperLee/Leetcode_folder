# Repository Guidelines

## Project Structure & Commands

This folder contains one .NET 10 console project. The nested executable is
`leetcode_1436/leetcode_1436.csproj`. From this folder, run:

```bash
dotnet build leetcode_1436/leetcode_1436.csproj --nologo
dotnet run --no-build --project leetcode_1436/leetcode_1436.csproj
```

Build before using `--no-build`. Use the `Debug leetcode_1436` VS Code configuration.
There is no root project, solution, or formal test project.

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow, explicit
types, PascalCase public members, and camelCase locals. Preserve the bilingual XML
problem summary and Traditional Chinese `DestCity` summary.

Keep `public static string DestCity(IList<IList<string>> paths)` console-free. It
must collect every departure city, then return the unique destination absent from
that set without modifying the outer path list or any nested city pair. Do not add
behavior outside LeetCode's valid-input contract. `Main` alone owns acceptance
output.

## Testing & Git Scope

The executable harness has eight checks. Each case deep-clones its input before the
API call and passes only when both the answer and the complete nested path structure
match expectations. Success ends with `Summary: 8/8 checks passed.` and exit code 0.
Git metadata is at the parent repository root; commits and pull requests must remain
scoped to `leetcode_1436/`.
