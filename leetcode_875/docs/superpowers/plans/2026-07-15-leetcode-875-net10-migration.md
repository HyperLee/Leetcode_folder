# leetcode_875 .NET 10 Migration Implementation Plan

**Goal:** Migrate `leetcode_875` to a deterministic, documented .NET 10 console project with one scoped commit.

**Architecture:** `Main` owns acceptance output. `MinEatingSpeed` performs lower-bound binary search, while `CalculateRequiredHours` is a pure `long`-based ceiling-division helper.

**Tech Stack:** C# 14, .NET 10 SDK-style console project, VS Code CoreCLR, Git worktree.

## Global Constraints

- All tracked changes remain below `leetcode_875/`.
- Target `net10.0` with implicit usings and nullable enabled.
- Delete the old `.sln`, `App.config`, and `Properties/AssemblyInfo.cs` one exact path at a time.
- Keep exactly nine harness checks and the exact green summary `Summary: 9/9 checks passed.`.
- Keep README's complete transcript as its only `text` fence.
- Create one commit with subject `feat(leetcode-875): migrate project to .NET 10`.

## Task 1: Project shape and RED

- [x] Replace the legacy project with the minimal SDK-style executable project.
- [x] Add problem-root shared configuration and VS Code files using nested project paths.
- [x] Remove each legacy file individually.
- [x] Write the complete nine-case harness while leaving `MinEatingSpeed` absent.
- [x] Build and observe implementation-specific `CS0103` with no legacy-framework or duplicate-attribute failure.

## Task 2: GREEN and failure-path proof

- [x] Implement `MinEatingSpeed` over `[1, piles.Max()]` with lower-bound movement.
- [x] Implement `CalculateRequiredHours` with `long` ceiling division.
- [x] Build with 0 warnings and 0 errors; run all nine checks successfully.
- [x] Temporarily corrupt one expected value, observe FAIL and exit 1, restore it, and rerun to 9/9.

## Task 3: Documentation and release gate

- [x] Add task-specific `AGENTS.md`, Traditional Chinese `README.md`, README template, design, and plan.
- [x] Validate VS Code JSON, build, run, README transcript identity, single `text` fence, whitespace, scope, and legacy-file absence.
- [x] Stage only `leetcode_875/`, create the required commit, and verify exactly one commit relative to `origin/main`.
