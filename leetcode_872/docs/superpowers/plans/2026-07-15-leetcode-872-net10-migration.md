# leetcode_872 .NET 10 Migration Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development and superpowers:test-driven-development. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Migrate `leetcode_872` to a deterministic, documented .NET 10 console project, then publish it through one squash-merged PR and update only its Issue #2 checkbox after merge.

**Architecture:** `Main` owns the acceptance output. `LeafSimilar` remains the sole public solution API and compares two left-to-right leaf sequences produced by a private recursive DFS helper.

**Tech Stack:** C# 14, .NET 10 SDK-style console project, VS Code CoreCLR, Git worktree, GitHub CLI.

## Global Constraints

- All tracked changes must remain below `leetcode_872/`.
- The project must target `net10.0` with implicit usings and nullable enabled.
- Delete the old `.sln`, `App.config`, and `Properties/AssemblyInfo.cs` one exact file at a time; never use recursive deletion.
- Preserve `public static bool LeafSimilar(TreeNode root1, TreeNode root2)` and make child references nullable-safe.
- `LeafSimilar` and `CollectLeaves` must not write to the console or mutate either tree; `Main` owns all console output.
- The harness must have exactly nine checks and end with `Summary: 9/9 checks passed.` when green.
- README must be Traditional Chinese, contain exactly one `text` fence, and byte-match a fresh run transcript.
- Before publication the branch must contain one commit relative to `origin/main`, subject `feat(leetcode-872): migrate project to .NET 10`.

### Task 1: Build the .NET 10 project shape and prove RED

**Files:**
- Modify: `leetcode_872/leetcode_872/leetcode_872.csproj`
- Modify: `leetcode_872/leetcode_872/Program.cs`
- Create: `leetcode_872/.editorconfig`, `.gitattributes`, `.gitignore`, `.vscode/tasks.json`, `.vscode/launch.json`
- Delete: `leetcode_872/leetcode_872.sln`, `leetcode_872/leetcode_872/App.config`, `leetcode_872/leetcode_872/Properties/AssemblyInfo.cs`

**Interfaces:** The harness calls an intentionally absent `LeafSimilar(TreeNode, TreeNode)` so the migrated project produces an implementation-specific compiler RED.

- [ ] Replace the project file with the minimal SDK-style `Microsoft.NET.Sdk` executable configuration containing `TargetFramework=net10.0`, `ImplicitUsings=enable`, and `Nullable=enable`.
- [ ] Copy `.editorconfig`, `.gitattributes`, and `.gitignore` from the current migrated `leetcode_859` project.
- [ ] Create problem-root VS Code files whose task label is `build leetcode_872`, whose project path is `${workspaceFolder}/leetcode_872/leetcode_872.csproj`, and whose launch DLL is `${workspaceFolder}/leetcode_872/bin/Debug/net10.0/leetcode_872.dll`.
- [ ] Delete each confirmed legacy file by its exact path and verify all three paths are absent.
- [ ] Replace `Program.cs` with nullable-safe `TreeNode`, the bilingual `Main` XML block, fixture helpers, output formatting, and exactly nine planned checks, but do not define `LeafSimilar` or `CollectLeaves` yet.
- [ ] Build with `dotnet build leetcode_872/leetcode_872/leetcode_872.csproj --nologo`. Expected: exit 1, 0 warnings, and `CS0103` naming missing `LeafSimilar`; no legacy-framework or duplicate-attribute error.
- [ ] Commit the RED state plus the approved design and plan documents using `feat(leetcode-872): migrate project to .NET 10`.

Harness cases, in order:

1. Official example true: `[3,5,1,6,2,9,8,null,null,7,4]` versus `[3,5,1,6,7,4,2,null,null,null,null,null,null,9,8]`.
2. Official example false: `[1,2,3]` versus `[1,3,2]`.
3. Same single node (`7`, `7`) is true.
4. Different single nodes (`0`, `200`) is false.
5. Different internal structure with the same leaf sequence `[6,7,4]` is true.
6. Same leaf values in different left-to-right order `[1,2]` versus `[2,1]` is false.
7. Repeated leaf multiplicity `[5,5,8]` versus `[5,8,8]` is false.
8. Boundary leaf sequence `[0,200]` in both trees is true.
9. Two 200-node right chains with different internal values and final leaf `200` are true.

### Task 2: Implement DFS, prove GREEN, document, and pass the release gate

**Files:**
- Modify: `leetcode_872/leetcode_872/Program.cs`
- Create: `leetcode_872/AGENTS.md`, `leetcode_872/README.md`, `leetcode_872/docs/readme-template.md`
- Amend: the single Task 1 commit

**Interfaces:**
- Produce `public static bool LeafSimilar(TreeNode root1, TreeNode root2)`.
- Produce `private static void CollectLeaves(TreeNode node, IList<int> leaves)`.

- [ ] Implement `LeafSimilar` by collecting both left-to-right leaf sequences into separate `List<int>` instances and returning `SequenceEqual`.
- [ ] Implement `CollectLeaves`: append a node only when both children are null; otherwise recurse into non-null left then non-null right. Add Traditional Chinese XML summaries to both major functions and only high-signal invariant comments.
- [ ] Run the same build command and require exit 0, 0 warnings, and 0 errors. Run `dotnet run --no-build --project leetcode_872/leetcode_872/leetcode_872.csproj` and require all nine PASS plus the exact summary.
- [ ] Temporarily invert one expected boolean, rebuild and run to prove a FAIL and exit 1, then restore it and rerun to 9/9 with exit 0.
- [ ] Create task-specific `AGENTS.md`; copy the latest verified README template from `leetcode_859`; write a Traditional Chinese README with official links, constraints, DFS invariant, walkthrough, all nine cases, project-root commands, complexity `O(n + m)`, leaf-sequence space `O(l1 + l2)`, recursion space `O(h1 + h2)`, and the fresh complete transcript.
- [ ] Run `jq empty` on both VS Code JSON files; capture a fresh run to `/private/tmp/leetcode_872.actual.txt`; extract the README's sole `text` fence and require `diff -u` to be empty; require `rg -c '^```text$'` to print `1`.
- [ ] Require `git diff --check`, only `leetcode_872/` changed paths, absent legacy files, correct SDK properties, and no untracked files outside the target.
- [ ] Stage only `leetcode_872/`, amend without changing the commit subject, and require exactly one commit relative to `origin/main`.

### Task 3: Final review and publication

**Files:** Review every changed path below `leetcode_872/`; after merge update only the Issue #2 line for `leetcode_872`.

- [ ] Generate a full `origin/main..HEAD` review package and complete an independent whole-branch review. Fix every Critical/Important or specification inconsistency, rerun the full release gate, amend the single commit, and re-review.
- [ ] Push `codex/leetcode-872-net10`, create a Draft PR with migration summary, DFS invariant, complexities, exact verification evidence, independent review result, and `Refs #2`.
- [ ] Confirm one commit, expected changed paths, verified head SHA, clean merge state, and no failed/cancelled/pending checks; then mark Ready and squash merge using the expected head SHA.
- [ ] After GitHub reports merged, replace exactly one unchecked Issue #2 line for `leetcode_872`, read it back, and verify `leetcode_875` remains unchecked.
- [ ] Fast-forward local `main` to the merge SHA and rerun JSON, build, run, transcript, fence, whitespace, scope, legacy-absence, and clean-status checks on merged code.
