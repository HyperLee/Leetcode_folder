# leetcode_859 .NET 10 Migration Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Migrate `leetcode_859` to a documented, deterministic .NET 10 console project and publish it through a squash-merged pull request with Issue #2 updated only after merge.

**Architecture:** `Program.Main` is a deterministic ten-case acceptance harness and owns every console write. `BuddyStrings` is the sole public, pure API; a single scan tracks at most two mismatches and whether the source contains a duplicate lowercase letter.

**Tech Stack:** C# / .NET 10 SDK-style console project, VS Code CoreCLR configuration, GitHub CLI, Git worktree.

## Global Constraints

- All tracked changes remain below `leetcode_859/`; no other problem or repository-root file changes.
- The project is SDK-style `net10.0` with implicit usings and nullable enabled.
- Preserve exactly `public static bool BuddyStrings(string s, string goal)` and do not define behavior outside the LeetCode valid-input contract.
- `BuddyStrings` cannot mutate input or write to the console; `Main` sets `Environment.ExitCode = 1` if any check fails.
- README is Traditional Chinese, contains exactly one `text` fence, and its transcript byte-matches a fresh run.
- End with exactly one commit relative to `origin/main`: `feat(leetcode-859): migrate project to .NET 10`.

## Task 1: Establish the isolated baseline and meaningful RED

**Files:** Modify the nested `.csproj` and `Program.cs`; delete the three exact legacy artifacts.

**Produces:** A .NET 10 harness whose only build failure is missing `BuddyStrings`.

- [x] Confirm remote `main`, local `main`, GitHub authentication, and the unique unchecked Issue #2 entry.
- [x] Create `/private/tmp/codex-leetcode-859-net10` on `codex/leetcode-859-net10` from `origin/main`.
- [x] Run the legacy build and record `MSB3644` as baseline evidence.
- [x] Replace the project declaration with SDK-style `net10.0` and remove `.sln`, `App.config`, and `AssemblyInfo.cs` individually.
- [x] Write the acceptance harness before production behavior.
- [x] Run `dotnet build leetcode_859/leetcode_859.csproj --nologo` from the problem root and record the single expected `CS0103` RED.

## Task 2: Implement and document the pure API

**Files:** Modify `leetcode_859/leetcode_859/Program.cs`.

**Produces:** `public static bool BuddyStrings(string s, string goal)` returning whether exactly one swap of two distinct source indices yields `goal`.

- [x] Reject unequal lengths.
- [x] Scan once, tracking duplicate letters and at most two mismatch indices.
- [x] For equal strings return whether a duplicate exists; otherwise require exactly two cross-matching mismatches.
- [x] Run the same build command and require 0 warnings and 0 errors.
- [x] Run `dotnet run --no-build --project leetcode_859/leetcode_859.csproj` and require the documented all-pass summary with exit 0.
- [x] Add Traditional Chinese XML documentation and only high-signal invariant comments without changing behavior.

## Task 3: Add project-root tooling and documentation

**Files:** Create shared config, `.vscode`, `AGENTS.md`, `README.md`, `docs/readme-template.md`, and Superpowers records below `leetcode_859/`.

- [x] Reuse the latest merged shared `.editorconfig`, `.gitattributes`, `.gitignore`, and README template.
- [x] Configure VS Code to build and launch `${workspaceFolder}/leetcode_859/leetcode_859.csproj` and its `net10.0` DLL.
- [x] Document the public API, exact commands, test mechanism, coding style, Git scope, algorithm, edge cases, and complexity.
- [x] Populate README's sole `text` fence from the verified run.

## Task 4: Verify, review, and publish

**Files:** Review every changed path below `leetcode_859/`; update Issue #2 only after merge.

- [ ] Parse `.vscode` JSON with `jq empty`.
- [ ] Run fresh build and run commands, extract the README transcript, require an empty `diff -u`, and require one `text` fence.
- [ ] Prove a deliberate harness expectation failure yields a FAIL line and exit 1, then restore and reverify 10/10.
- [ ] Check whitespace, changed-path scope, legacy absence, and worktree status.
- [ ] Create the single Conventional Commit and dispatch an independent read-only reviewer against `origin/main..HEAD`.
- [ ] Fix all Critical/Important and spec-inconsistent Minor findings, amend, reverify, and rereview.
- [ ] Push, open Draft PR with `Refs #2`, confirm the verified head SHA, one commit, expected files, clean merge, and successful checks.
- [ ] Mark Ready and squash merge with the expected head SHA.
- [ ] Change only the unique `leetcode_859` checkbox after merged confirmation; read back `leetcode_859` checked and `leetcode_872` unchecked.
- [ ] Fast-forward local `main` and repeat the full verification bundle on the merged SHA.
