# LeetCode 516 .NET 10 Migration Implementation Plan

> **For agentic workers:** Use the Superpowers execution workflow and verify every
> task before moving to the next one.

**Goal:** 將 `leetcode_516/` 依開發規格升級至 .NET 10 並完成完整發布。

**Architecture:** 使用 problem-root workspace 與巢狀 SDK-style `net10.0`
console project。`LongestPalindromeSubseq` 保持純二維區間 DP，所有 console
輸出集中在 `Main`。

## Execution Checklist

- [x] Convert the legacy project and remove `.sln`, `App.config`, and `AssemblyInfo.cs`.
- [x] Implement the two-dimensional DP API and ten deterministic acceptance checks.
- [x] Add shared settings, VS Code configuration, README, AGENTS.md, and Superpowers artifacts.
- [x] Run the complete local gate and independent read-only review.
- [ ] Create the single migration commit, publish the PR, merge it, update Issue #2, and verify `main`.

## Required Commands

```bash
jq empty leetcode_516/.vscode/launch.json leetcode_516/.vscode/tasks.json
dotnet build leetcode_516/leetcode_516/leetcode_516.csproj --nologo
dotnet run --no-build --project leetcode_516/leetcode_516/leetcode_516.csproj
git diff --check -- leetcode_516
```

The README transcript must be extracted from a fresh run and compare equal with
`diff -u`. The final commit must contain only `leetcode_516/` and use
`feat(leetcode-516): migrate project to .NET 10`.
