# `leetcode_530` .NET 10 Migration Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use `superpowers:subagent-driven-development` (recommended) or `superpowers:executing-plans` to implement this plan task-by-task with review checkpoints.

**Goal:** Migrate `leetcode_530` from .NET Framework 4.8 to a verified SDK-style .NET 10 console project and publish it through the Issue #2 workflow.

**Architecture:** Preserve the LeetCode-compatible `TreeNode` data shape and `GetMinimumDifference` API. Use recursive in-order traversal with per-call state, and keep all acceptance output in `Main`.

**Tech Stack:** .NET 10 SDK, C#, VS Code CoreCLR, Git worktree, GitHub CLI.

## Global Constraints

- Scope is limited to `leetcode_530/`.
- Target framework is exactly `net10.0`; implicit usings and nullable are enabled.
- No formal test project or unrelated third-party dependency is added.
- Acceptance output is deterministic and failures set `Environment.ExitCode = 1`.
- One scoped Conventional Commit is used before PR publication.

---

### Task 1: Isolate the migration

**Files:**
- Worktree: `/private/tmp/codex-leetcode-530-net10`
- Branch: `codex/leetcode-530-net10`

- [x] Pull `origin/main` fast-forward-only, confirm Issue #2 has exactly one unchecked `leetcode_530` entry, and record the base SHA.
- [x] Run the legacy nested project build and retain its `MSB3644` result as baseline evidence.
- [x] Create the isolated worktree from `origin/main`.

### Task 2: Convert the project and implement the TDD cycle

**Files:**
- Modify: `leetcode_530/leetcode_530/leetcode_530.csproj`
- Modify: `leetcode_530/leetcode_530/Program.cs`
- Delete: `leetcode_530/leetcode_530.sln`
- Delete: `leetcode_530/leetcode_530/App.config`
- Delete: `leetcode_530/leetcode_530/Properties/AssemblyInfo.cs`

- [x] Replace the legacy project with SDK-style `net10.0`, `ImplicitUsings`, and `Nullable`.
- [x] Add the eight-case harness while leaving `GetMinimumDifference` absent.
- [x] Run `dotnet build leetcode_530/leetcode_530/leetcode_530.csproj --nologo` and verify the real RED is `CS0103` for the missing method.
- [x] Add `GetMinimumDifference(TreeNode root)` and `InorderTraversal(TreeNode? node, ref bool, ref int, ref int)` with Traditional Chinese XML summaries.
- [x] Run the same build and the no-build harness; correct only harness data if an expected value is mathematically inconsistent.

### Task 3: Add documentation and developer tooling

**Files:**
- Create: `leetcode_530/.editorconfig`, `.gitattributes`, `.gitignore`
- Create: `leetcode_530/.vscode/tasks.json`, `launch.json`
- Create: `leetcode_530/AGENTS.md`, `README.md`, `docs/readme-template.md`
- Create: `leetcode_530/docs/superpowers/specs/2026-07-12-leetcode-530-net10-migration-design.md`
- Create: `leetcode_530/docs/superpowers/plans/2026-07-12-leetcode-530-net10-migration.md`

- [x] Configure direct problem-root VS Code paths with matching `build leetcode_530` and `preLaunchTask` values.
- [x] Document the public API, BST in-order invariant, complexity, no-test-project boundary, and parent Git metadata.
- [x] Copy the fresh run transcript into the README's only `text` fence.

### Task 4: Run the complete local gate and review

**Verification commands:**

```bash
jq empty leetcode_530/.vscode/launch.json leetcode_530/.vscode/tasks.json
dotnet build leetcode_530/leetcode_530/leetcode_530.csproj --nologo
dotnet run --no-build --project leetcode_530/leetcode_530/leetcode_530.csproj
awk '/^```text$/{copy=1;next} copy && /^```$/{exit} copy' leetcode_530/README.md > /private/tmp/leetcode_530.readme.txt
dotnet run --no-build --project leetcode_530/leetcode_530/leetcode_530.csproj > /private/tmp/leetcode_530.actual.txt
diff -u /private/tmp/leetcode_530.readme.txt /private/tmp/leetcode_530.actual.txt
test "$(rg -c '^```text$' leetcode_530/README.md)" = 1
git diff --check -- leetcode_530
```

- [x] Confirm build has 0 warnings and 0 errors, all 8 checks pass, and run exit code is 0.
- [x] Confirm legacy files are absent and every changed path starts with `leetcode_530/`.
- [ ] Dispatch an independent read-only reviewer; fix Critical/Important issues and rerun the full gate.

### Task 5: Commit and publish

- [ ] Confirm staged paths are limited to `leetcode_530/` and create exactly one commit:
  `feat(leetcode-530): migrate project to .NET 10`.
- [ ] Push `codex/leetcode-530-net10` and create a Draft PR whose body includes the algorithm invariant, complexity, fresh verification, review result, and `Refs #2`.
- [ ] Mark the PR Ready only after checks are complete and merge it with the expected head SHA using squash merge.
- [ ] After `merged: true`, update only the unchecked `leetcode_530` Issue #2 line, read it back, and confirm `leetcode_589` remains unchecked.
- [ ] Fast-forward local `main` and rerun JSON, build, run, README transcript, scope, and whitespace verification.
