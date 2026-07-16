# leetcode_1372 .NET 10 Migration Implementation Plan

> **For agentic workers:** Follow the approved TDD and release gates task-by-task; use an isolated worktree and verify every completion claim with fresh command output.

**Goal:** Migrate `leetcode_1372` to a deterministic, documented .NET 10 console project and complete its publish flow.

**Architecture:** `Main` owns acceptance output. `LongestZigZag` performs iterative DFS with explicit direction and length state, avoiding recursion and mutable instance fields.

**Tech Stack:** C# 14, .NET 10 SDK-style console project, VS Code CoreCLR, Git worktree, GitHub CLI.

## Global Constraints

- Keep every tracked change below `leetcode_1372/`.
- Target `net10.0` with implicit usings and nullable enabled.
- Remove `.sln`, `App.config`, and `Properties/AssemblyInfo.cs` one exact path at a time.
- Keep exactly nine harness checks and the green summary `Summary: 9/9 checks passed.`.
- Keep the complete README transcript as its only `text` fence.
- Create one commit named `feat(leetcode-1372): migrate project to .NET 10`.

## Task 1: Project shape and RED

- [x] Create `codex/leetcode-1372-net10` in an isolated worktree based on current `origin/main`.
- [x] Record the legacy `MSB3644` baseline.
- [x] Replace the project with SDK-style .NET 10 and remove each legacy file individually.
- [x] Write the complete harness without `LongestZigZag`; observe implementation-specific `CS1061`.

## Task 2: GREEN

- [x] Implement `LongestZigZag` with an explicit DFS stack containing node, previous direction, and length.
- [x] Continue alternating edges with `length + 1` and reset same-direction edges to `1`.
- [x] Verify all nine cases, including both 50,000-node chains and same-instance isolation.
- [x] Build with 0 warnings and 0 errors and run to `Summary: 9/9 checks passed.`.

## Task 3: Documentation and local gate

- [x] Add problem-root settings, VS Code files, `AGENTS.md`, Traditional Chinese README, template, design, and plan.
- [x] Validate JSON, build, run, README transcript identity, single `text` fence, whitespace, scope, and legacy absence.
- [x] Complete a read-only review and repair every Critical, Important, or contract inconsistency.

## Task 4: Publish and close out

- [ ] Stage only `leetcode_1372/` and create the single required commit.
- [ ] Push, create a draft PR, validate its head and checks, mark ready, and squash merge with the expected head SHA.
- [ ] Update only the `leetcode_1372` checkbox in Issue #2 and verify the readback.
- [ ] Fast-forward local `main` and rerun the complete post-merge gate.
