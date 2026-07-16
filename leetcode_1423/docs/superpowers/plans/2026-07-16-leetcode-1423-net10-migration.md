# leetcode_1423 .NET 10 Migration Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Migrate `leetcode_1423` to a deterministic, documented .NET 10 console project and complete its publish flow.

**Architecture:** `Main` owns all acceptance output. `MaxScore` computes the total card value and subtracts the minimum fixed-length `n-k` contiguous window, preserving the existing `O(n)`/`O(1)` teaching approach.

**Tech Stack:** C# 14, .NET 10 SDK-style console project, VS Code CoreCLR, Git worktree, GitHub CLI.

## Global Constraints

- Keep every tracked change below `leetcode_1423/`.
- Target `net10.0` with implicit usings and nullable enabled.
- Preserve `public static int MaxScore(int[] cardPoints, int k)` and valid-input LeetCode semantics.
- Remove `.sln`, `App.config`, and `Properties/AssemblyInfo.cs` one exact path at a time.
- Keep exactly eight harness checks and the green summary `Summary: 8/8 checks passed.`.
- Keep the complete README transcript as its only `text` fence.
- Create one commit named `feat(leetcode-1423): migrate project to .NET 10`.

---

### Task 1: Project shape and implementation-specific RED

**Files:**
- Modify: `leetcode_1423/leetcode_1423/leetcode_1423.csproj`
- Modify: `leetcode_1423/leetcode_1423/Program.cs`
- Delete: `leetcode_1423/leetcode_1423.sln`
- Delete: `leetcode_1423/leetcode_1423/App.config`
- Delete: `leetcode_1423/leetcode_1423/Properties/AssemblyInfo.cs`

**Interfaces:**
- Produces: SDK-style `net10.0` executable and a harness that expects `public static int MaxScore(int[] cardPoints, int k)`.

- [x] Replace the project file with the exact SDK-style .NET 10 contract and remove the three legacy files individually.
- [x] Write the eight-case harness and `CaseResult` support without defining `MaxScore`.
- [x] Run `dotnet build leetcode_1423/leetcode_1423/leetcode_1423.csproj --nologo`; require an implementation-specific `CS0103` failure for missing `MaxScore`.

### Task 2: GREEN and refactor

**Files:**
- Modify: `leetcode_1423/leetcode_1423/Program.cs`

**Interfaces:**
- Produces: `public static int MaxScore(int[] cardPoints, int k)` returning the maximum sum obtainable by taking exactly `k` cards from either end.

- [x] Implement total sum plus minimum `n-k` sliding window; special-case `windowLength == 0` by returning the total.
- [x] Add a Traditional Chinese XML summary and only high-signal comments for the complement-window invariant.
- [x] Run build and harness; require 0 warnings, 0 errors, exit code 0, and `Summary: 8/8 checks passed.`.

### Task 3: Project documentation and editor support

**Files:**
- Create: `leetcode_1423/.editorconfig`, `.gitattributes`, `.gitignore`
- Create: `leetcode_1423/.vscode/tasks.json`, `.vscode/launch.json`
- Create: `leetcode_1423/AGENTS.md`, `README.md`, `docs/readme-template.md`
- Create: `leetcode_1423/docs/superpowers/specs/2026-07-16-leetcode-1423-net10-migration-design.md`
- Create: `leetcode_1423/docs/superpowers/plans/2026-07-16-leetcode-1423-net10-migration.md`

**Interfaces:**
- Consumes: the verified eight-case transcript and `MaxScore` complexity.
- Produces: problem-root build/debug commands and an exact README transcript.

- [x] Copy stable shared settings and README template from the latest migrated neighboring project, adjusting only problem-specific paths.
- [x] Write problem-root VS Code configuration, `AGENTS.md`, and Traditional Chinese README.
- [x] Generate the README `text` block from a fresh `dotnet run --no-build` transcript.

### Task 4: Local gate and review

**Files:**
- Review: every changed or added path below `leetcode_1423/`

**Interfaces:**
- Consumes: completed migration tree.
- Produces: a review-clean, release-ready diff.

- [x] Validate VS Code JSON with `jq empty`.
- [x] Re-run build and harness; diff the fresh transcript byte-for-byte against README and require exactly one `text` fence.
- [x] Run `git diff --check -- leetcode_1423`, confirm changed-path scope, confirm all legacy files are absent, and inspect all untracked files explicitly.
- [x] Perform a separate read-only review against `origin/main`; repair and repeat the gate for every Critical, Important, or contract inconsistency.

### Task 5: Publish and close out

**Files:**
- Stage: only `leetcode_1423/`
- External: GitHub PR and Issue #2

**Interfaces:**
- Consumes: verified review-clean diff.
- Produces: one squash commit on `main`, checked Issue #2 entry, and post-merge verification evidence.

- [ ] Stage only the target folder, run cached whitespace/scope checks, and commit once with `feat(leetcode-1423): migrate project to .NET 10`.
- [ ] Push and create a draft PR with migration, algorithm, complexity, verification, review, and `Refs #2`; validate head SHA, one commit, changed files, clean merge state, and checks before marking ready.
- [ ] Squash merge with the expected head SHA, then change exactly one Issue #2 line from unchecked to checked and read it back.
- [ ] Fast-forward local `main` and rerun JSON, build, run, transcript, whitespace, scope, legacy-absence, and clean-status checks.
