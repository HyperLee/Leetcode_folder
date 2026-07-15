# leetcode_912 .NET 10 Migration Implementation Plan

**Goal:** Migrate `leetcode_912` to a deterministic, documented .NET 10 console project with one scoped commit.

**Architecture:** `Main` owns acceptance output. `SortArray` performs in-place max-heap sort, while iterative `SiftDown` restores the heap without recursion or extra collections.

**Tech Stack:** C# 14, .NET 10 SDK-style console project, VS Code CoreCLR, Git worktree.

## Global Constraints

- All tracked changes remain below `leetcode_912/`.
- Target `net10.0` with implicit usings and nullable enabled.
- Delete the old `.sln`, `App.config`, and `Properties/AssemblyInfo.cs` one exact path at a time.
- Keep eight harness cases, exactly nine checks, and the green summary `Summary: 9/9 checks passed.`.
- Keep README's complete transcript as its only `text` fence.
- Create one commit with subject `feat(leetcode-912): migrate project to .NET 10`.

## Task 1: Project shape and RED

- [x] Replace the legacy project with the minimal SDK-style executable project.
- [x] Remove each legacy file individually.
- [x] Write the complete harness while leaving `SortArray` absent.
- [x] Build and observe implementation-specific `CS0103` with no framework or duplicate-attribute failure.

## Task 2: GREEN

- [x] Implement `SortArray` with an in-place max heap built from the last non-leaf node.
- [x] Implement iterative `SiftDown` and keep the sorted suffix outside the shrinking heap.
- [x] Verify all eight cases and nine checks, including the 50,000-element order and reference checks.
- [x] Build with 0 warnings and 0 errors and run to `Summary: 9/9 checks passed.`.

## Task 3: Documentation and release gate

- [x] Add problem-root settings, VS Code files, `AGENTS.md`, Traditional Chinese README, template, design, and plan.
- [x] Validate JSON, build, run, README transcript identity, single `text` fence, whitespace, scope, and legacy-file absence.
- [x] Stage only `leetcode_912/`, create the required commit, self-review, and confirm exactly one commit relative to `origin/main`.
