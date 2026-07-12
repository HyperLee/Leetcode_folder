# Repository Guidelines

## Project Structure & Module Organization

此題目根目錄包含文件、VS Code 設定與一個巢狀 .NET 10 console project：
`leetcode_515/leetcode_515.csproj`。核心 API、`TreeNode` 與 deterministic
acceptance harness 位於 `leetcode_515/Program.cs`；`docs/readme-template.md`
只作為 README 初始範本。

## Build, Run, and Testing

從此題目根目錄執行：

```bash
dotnet build leetcode_515/leetcode_515.csproj --nologo
dotnet run --no-build --project leetcode_515/leetcode_515.csproj
```

本題沒有正式 test project；console acceptance harness 是驗證機制，成功時必須
輸出 `Summary: 10/10 checks passed.` 並以 exit code 0 結束。

## Coding Style & Solution Contract

遵循 `.editorconfig`：四格縮排、public member 使用 PascalCase、區域變數與參數
使用 camelCase。`public static IList<int> LargestValues(TreeNode? root)` 不輸出
主控台，只回傳各樹層最大值；空樹回傳空集合。

DFS 以目前深度作為結果索引。第一次抵達某深度時加入節點值，後續節點以
`Math.Max` 更新同層最大值，再遞迴左、右子樹。解法不修改節點值或左右連結，
acceptance harness 必須涵蓋空樹、負數、整數邊界、同層更新與 10,000 節點 spot check。

## Commits and Pull Requests

Git metadata 位於 parent repository `/Users/qiuzili/Leetcode/Leetcode_folder`。
從 parent repository 檢查 `git diff --check -- leetcode_515`，並只 stage
`leetcode_515/`。本題 commit 與 pull request 必須限制在當題資料夾，commit subject
為 `feat(leetcode-515): migrate project to .NET 10`。
