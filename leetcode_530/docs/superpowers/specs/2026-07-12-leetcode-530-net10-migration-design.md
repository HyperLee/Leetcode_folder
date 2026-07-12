# `leetcode_530` .NET 10 Migration Design

## Context

The existing project targets .NET Framework 4.8, uses the non-SDK project format,
and includes a legacy solution, `App.config`, and handwritten assembly metadata.
Its current sample is not a valid BST because `44` is placed in the left subtree
of `1`. The migration must follow `LEETCODE_NET10_MIGRATION_SPEC.md` while
remaining limited to `leetcode_530/`.

## Chosen Design

Keep the LeetCode-compatible `TreeNode` fields and
`public static int GetMinimumDifference(TreeNode root)` API. Replace the static
`pre` and `ans` fields with per-call local state: `hasPrevious`,
`previousValue`, and `minimumDifference`. A recursive in-order helper accepts a
nullable subtree and updates these values by reference.

The BST invariant is that in-order traversal produces an increasing sequence.
Therefore the minimum difference is the minimum of each current value minus the
previous visited value. The solution remains `O(n)` time and `O(h)` auxiliary
space, where `h` is the tree height. It performs no console I/O.

## Acceptance Design

`Main` owns all output and runs eight deterministic cases: both official
examples, the two-node minimum input, a non-parent/child in-order neighbor
regression case, lower/upper value boundaries, adjacent high values, an
irregular multi-level BST, and a balanced 10,000-node spot check. Every case
prints its name, Expected, Actual, and PASS/FAIL. A failing case sets
`Environment.ExitCode = 1`; the successful transcript ends with
`Summary: 8/8 checks passed.`.

## Documentation and Delivery

The problem root receives SDK project metadata, direct VS Code build/debug
configuration, `AGENTS.md`, a Traditional Chinese teaching README, the reusable
README template, and this design/implementation plan. The legacy `.sln`,
`App.config`, and `Properties/AssemblyInfo.cs` are removed individually.

The work is performed in `/private/tmp/codex-leetcode-530-net10` on
`codex/leetcode-530-net10`, reviewed independently, committed once with
`feat(leetcode-530): migrate project to .NET 10`, and delivered through the
Issue #2-aware draft/Ready/squash-merge flow.
