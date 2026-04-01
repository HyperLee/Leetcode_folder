# AGENTS.md

## Scope

This repository contains one .NET 10 console application for LeetCode 316 and one xUnit test project.

## Commands

- Build solution: `dotnet build leetcode_316.sln`
- Run console app: `dotnet run --project leetcode_316/leetcode_316.csproj`
- Run all tests: `dotnet test leetcode_316.sln`
- Run test project: `dotnet test leetcode_316.Tests/leetcode_316.Tests.csproj`
- Re-run tests without rebuilding: `dotnet test leetcode_316.Tests/leetcode_316.Tests.csproj --no-build`
- Run one test class: `dotnet test leetcode_316.Tests/leetcode_316.Tests.csproj --filter "FullyQualifiedName~leetcode_316.Tests.RemoveDuplicateLettersTests"`

## Testing Model

- Framework: xUnit v2
- Runner: VSTest via `Microsoft.NET.Test.Sdk` and `xunit.runner.visualstudio`
- Use `dotnet test` for local verification and CI compatibility.

## Notes

- Production logic lives in `leetcode_316/Program.cs`.
- Keep tests deterministic and isolated.
- Prefer `[Theory]` for input/output coverage and `[Fact]` for invariant or exception checks.