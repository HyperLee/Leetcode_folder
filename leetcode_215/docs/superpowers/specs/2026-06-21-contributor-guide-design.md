# Contributor Guide Design

## Goal

Add a concise `AGENTS.md` for this LeetCode 215 module. The guide will help contributors locate the console application, run it with the installed .NET SDK, follow the parent repository's established C# conventions, and submit focused changes.

## Contents

The guide will use the title **Repository Guidelines** and contain these sections:

1. Project structure: `leetcode_215/Program.cs`, `leetcode_215/leetcode_215.csproj`, and `docs/`.
2. Commands: `dotnet build` and `dotnet run --project`, using the project file because no solution or test project exists here.
3. Style: 4-space C# indentation, explicit types, PascalCase members, file-scoped namespaces, nullable-safe code, and preserved bilingual XML problem text.
4. Testing: build and run validation today; a future test project should be named `leetcode_215.Tests` with behavior-focused test names.
5. Collaboration: concise single-purpose commits, with either brief Chinese messages or focused `feat:` messages; PRs include a summary, validation commands, and relevant prompt link.
6. Hygiene: do not add secrets, local paths, or generated artifacts.

## Constraints and Validation

The document will be 200–400 words, make no unsupported claims about formatters, linters, automated tests, or solutions, and use commands verified against the local project file. Review its word count and Markdown structure after creation.
