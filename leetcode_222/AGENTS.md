# Repository Guidelines

## Project Structure & Module Organization

This repository is a small .NET console solution for LeetCode 222, **Count
Complete Tree Nodes**. The executable project is nested at
`leetcode_222/leetcode_222.csproj`; its current entry point and problem XML
documentation are in `leetcode_222/Program.cs`. Keep solution types, helpers,
and runnable examples in this project unless a real test project is added.
`docs/readme-template.md` is a template for a future initial README. Do not
edit generated `leetcode_222/bin/` or `leetcode_222/obj/` output.

## Build, Run, and Development Commands

Run commands from this repository directory and always name the nested project:

```bash
dotnet build leetcode_222/leetcode_222.csproj
dotnet run --project leetcode_222/leetcode_222.csproj
dotnet test leetcode_222/leetcode_222.csproj
```

The first command compiles the `net10.0` executable; the second runs the
sample harness in `Main`; the last is safe to use after a test project is
introduced. There is currently no test project or solution file, so bare
`dotnet test` at the repository root fails with `MSB1003`. Use `dotnet run`
and representative tree inputs to check changes in the meantime.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use four spaces for C# indentation, braces for control
blocks, explicit types instead of `var`, and file-scoped namespaces where
practical. Use PascalCase for types, methods, properties, and local functions;
use camelCase for parameters and local variables. Prefix private instance
fields with `_` and private static fields with `s_`. Keep the existing problem
description XML comments intact; add concise comments only where an algorithm
or invariant needs explanation. Prefer clear helper names such as
`CountNodes` or `GetTreeHeight` over abbreviations.

## Testing Guidelines

When adding tests, create a dedicated `*.Tests` project, use descriptive names
such as `CountNodes_CompleteTree_ReturnsExpectedCount`, and execute it with an
explicit project path. Cover an empty tree, a perfect tree, and a partially
filled final level. Until then, make `Main` print enough input and expected
output to make manual verification unambiguous.

## Commit & Pull Request Guidelines

Recent parent-repository history mixes short Traditional Chinese summaries with
Conventional Commit subjects (for example, `feat: add samples`). Keep commits
small and imperative; `feat(leetcode-222): implement logarithmic node count`
is a good model. Pull requests should explain the algorithm and complexity,
list the commands run, and include sample output when behavior changes.
