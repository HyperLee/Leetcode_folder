# leetcode_589 .NET 10 Migration Design

## Goal

依 `LEETCODE_NET10_MIGRATION_SPEC.md` 將 legacy .NET Framework 專案遷移為
SDK-style .NET 10 console project，保留遞迴 DFS 解法並提供可重複驗證的
acceptance harness。

## Approved Design

- `Preorder(Node? root)` 保持純函式；空 root 回傳空集合。
- `Node.children` 以空集合初始化，`PreorderVisit` 先記錄根值，再依 children
  原順序遞迴。
- `Main` 集中建立並輸出 8 個 deterministic checks，helper 不直接輸出。
- workspace root 採 `leetcode_589/`，不建立正式測試專案或共用 solution。
- 交付包含隔離 worktree、單一 commit、PR、squash merge 與 Issue #2 更新。

## Verification Contract

必須通過 JSON 解析、.NET 10 build、8/8 run、README transcript diff、唯一
`text` fence、legacy file absence 與 `git diff --check`。PR merge 後重新執行
相同驗證，並確認 `leetcode_589` 已勾選而 `leetcode_590` 仍未勾選。
