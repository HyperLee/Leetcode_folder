# LeetCode 515 .NET 10 Migration Design

## Scope

Migrate only `leetcode_515/` from the legacy .NET Framework 4.8 project format to
an SDK-style `net10.0` console project. Preserve the existing depth-first search
solution and nested `TreeNode` shape. The final delivery includes a single
commit, pull request, squash merge, Issue #2 update, and post-merge verification.

## Code and API

`Program` remains the executable entry point. `TreeNode` keeps the existing
`val`, `left`, and `right` fields, with nullable child references. The public
solution API is:

```csharp
public static IList<int> LargestValues(TreeNode? root)
```

`LargestValues` returns an empty list for an empty tree. Its private DFS helper
uses the current depth as the result index: the first node at a depth creates the
level value, and later nodes update it with `Math.Max`. The solution and helpers
do not write to the console; `Main` owns all acceptance-harness rendering.

## Acceptance harness

The deterministic harness runs ten checks: both official examples, empty tree,
`int.MinValue`, negative and zero values, a left-skewed tree, same-level maximum
updates, mixed integer boundaries, structural read-only behavior, and a
10,000-node complete tree. The final output is `Summary: 10/10 checks passed.`;
any failed check sets `Environment.ExitCode` to `1`.

## Documentation and tooling

The problem root contains the shared editor/Git settings, problem-root VS Code
configuration, `AGENTS.md`, a Traditional Chinese teaching README, and the
README template. The README has exactly one `text` fence containing the fresh
run transcript; walkthroughs use `plaintext` fences.

## Verification and delivery

Verification uses `jq empty`, an explicit nested-project `dotnet build`,
`dotnet run --no-build`, README transcript comparison, legacy-file absence
checks, and `git diff --check`. A separate read-only review checks the API,
algorithm, documentation, harness, scope, and project cleanup before the single
commit is pushed and merged.

## TDD evidence

The legacy baseline command:

```bash
dotnet build leetcode_515/leetcode_515/leetcode_515.csproj --nologo
```

failed with `MSB3644` because the macOS environment does not provide the old
.NET Framework 4.8 reference assemblies. After the SDK project was created, the
acceptance-first RED referenced the not-yet-implemented `BuildCompleteTree`
fixture and failed with `CS0103`. Implementing the nullable-safe DFS solution,
fixture, and harness produced the GREEN result: zero warnings, zero errors, and
`Summary: 10/10 checks passed.`
