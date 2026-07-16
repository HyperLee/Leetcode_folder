# leetcode_1351 .NET 10 Migration Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Migrate `leetcode_1351` to a deterministic, documented .NET 10 console project with one scoped commit.

**Architecture:** `Main` owns acceptance output. `CountNegatives` performs a top-right staircase scan, using row and column ordering to eliminate one row or column at each step without mutating the matrix.

**Tech Stack:** C# 14, .NET 10 SDK-style console project, VS Code CoreCLR, Git worktree.

## Global Constraints

- All tracked changes remain below `leetcode_1351/`.
- Target `net10.0` with implicit usings and nullable enabled.
- Delete the old `.sln`, `App.config`, and `Properties/AssemblyInfo.cs` one exact path at a time.
- Keep exactly `public static int CountNegatives(int[][] grid)` as the public API.
- Keep eight harness cases and the green summary `Summary: 8/8 checks passed.`.
- Keep README's complete transcript as its only `text` fence.
- Create one commit with subject `feat(leetcode-1351): migrate project to .NET 10`.

---

## Task 1: Project shape and RED

**Files:**

- Modify: `leetcode_1351/leetcode_1351/leetcode_1351.csproj`
- Modify: `leetcode_1351/leetcode_1351/Program.cs`
- Delete: `leetcode_1351/leetcode_1351.sln`
- Delete: `leetcode_1351/leetcode_1351/App.config`
- Delete: `leetcode_1351/leetcode_1351/Properties/AssemblyInfo.cs`

**Interfaces:**

- Consumes: LeetCode-valid rectangular `int[][]` matrices sorted non-increasingly by row and column.
- Produces: a harness reference to the wished-for `CountNegatives(int[][])` API.

- [x] Replace the legacy project with the minimal SDK-style executable project.
- [x] Remove each legacy file individually.
- [x] Write the complete eight-case harness while leaving `CountNegatives` absent.
- [x] Run `dotnet build leetcode_1351/leetcode_1351/leetcode_1351.csproj --nologo` and observe `CS0103` for the missing method.

## Task 2: GREEN

**Files:**

- Modify: `leetcode_1351/leetcode_1351/Program.cs`

**Interfaces:**

- Consumes: `int[][] grid` under the official sorted-matrix contract.
- Produces: `public static int CountNegatives(int[][] grid)` returning the total negative count.

- [x] Add a Traditional Chinese XML summary defining purpose, staircase invariant, valid input, and result.
- [x] Implement the top-right scan: add `grid.Length - row` and move left for a negative value; otherwise move down.
- [x] Build with 0 warnings and 0 errors.
- [x] Run all eight cases to `Summary: 8/8 checks passed.` with exit code 0.

## Task 3: Documentation and release gate

**Files:**

- Create: `leetcode_1351/.editorconfig`
- Create: `leetcode_1351/.gitattributes`
- Create: `leetcode_1351/.gitignore`
- Create: `leetcode_1351/.vscode/launch.json`
- Create: `leetcode_1351/.vscode/tasks.json`
- Create: `leetcode_1351/AGENTS.md`
- Create: `leetcode_1351/README.md`
- Create: `leetcode_1351/docs/readme-template.md`
- Create: `leetcode_1351/docs/superpowers/specs/2026-07-16-leetcode-1351-net10-migration-design.md`
- Create: `leetcode_1351/docs/superpowers/plans/2026-07-16-leetcode-1351-net10-migration.md`

**Interfaces:**

- Consumes: fresh GREEN output and the problem-root workspace convention.
- Produces: reproducible build/debug commands, matching transcript, and a single scoped commit.

- [x] Add problem-root settings, VS Code files, `AGENTS.md`, Traditional Chinese README, template, design, and plan.
- [x] Validate JSON, build, run, README transcript identity, single `text` fence, whitespace, scope, and legacy-file absence.
- [x] Self-review every migration-spec requirement and repair any discrepancy.
- [x] Stage only `leetcode_1351/`, create the required commit, and confirm exactly one commit relative to `origin/main`.
