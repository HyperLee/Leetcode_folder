# Repository Guidelines

## Project Structure & Module Organization

此題目資料夾包含一個 .NET 10 主控台專案。Keep the pure
`FindTarget` solution, the bilingual problem XML summary, and the deterministic
acceptance harness in `leetcode_653/Program.cs`. 實際可執行專案位於巢狀路徑
`leetcode_653/leetcode_653.csproj`；`.vscode/` 提供直接建置與偵錯設定，
而 `docs/readme-template.md` 僅供首次建立 README 使用。

## Build, Run, and Development Commands

請從本題目資料夾（`leetcode_653/`）以相對於該資料夾的巢狀專案路徑執行：

```bash
dotnet build leetcode_653/leetcode_653.csproj --nologo
dotnet run --no-build --project leetcode_653/leetcode_653.csproj
```

使用 `--no-build` 前必須先建置。In VS Code, use `Debug leetcode_653`。
不要使用裸的 `dotnet build` 或 `dotnet test`：此資料夾沒有根專案/方案檔，
也沒有正式測試專案。

## Coding Style & Solution Contract

Follow `.editorconfig`: four-space C# indentation, braces for control flow,
explicit types instead of `var`, file-scoped namespaces, PascalCase for public
members, and camelCase for locals/parameters. 保留 `Main` 上方的雙語題目 XML
摘要。

`public static bool FindTarget(TreeNode? root, int k)` 必須保持 API purity：
不輸出到主控台、不改寫輸入樹、也不依賴靜態解題狀態。每次呼叫都要建立自己的
`HashSet<int>` 與 `Stack<TreeNode>`，以迭代 DFS 先檢查補數、再加入目前節點；
`Main` 是唯一負責輸出驗收資訊的位置。

## Testing Guidelines

可執行的 acceptance harness 是目前的驗證機制，沒有額外正式測試專案。它必須執行
九個確定性的案例，每個案例輸出輸入、預期、實際與 PASS/FAIL，並在成功時印出
`Summary: 9/9 checks passed.` 且以結束碼 0 離開。任何失敗都必須令
`Environment.ExitCode = 1`；不要宣稱有測試框架 coverage。

## Commits and Pull Requests

Git metadata 位於父層 repository root。From that root, review scoped changes
with `git diff --check -- leetcode_653` and `git status --short`, then stage
only `leetcode_653/`. 使用簡短、限縮範圍的提交訊息，例如
`feat(leetcode-653): migrate project to .NET 10`。Pull requests 應說明
local-HashSet DFS invariant、O(n) 時間/O(n) 空間，以及已驗證的 9/9 harness
結果。
