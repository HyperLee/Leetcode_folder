# leetcode_1436 .NET 10 遷移設計

## 目標與範圍

將 LeetCode 1436「Destination City／旅行終點站」從舊式 .NET Framework 4.8 主控台專案遷移為可在 .NET 10 建置、執行與驗證的單題專案。所有 tracked changes 僅限 `leetcode_1436/`。完整交付包含隔離 worktree、單一功能 commit、唯讀審查、draft PR、Ready 門檻、squash merge、Issue #2 精確更新，以及合併後重新驗證。

VS Code 與題目文件以 `leetcode_1436/` 為工作區根目錄；巢狀 executable project 位於 `leetcode_1436/leetcode_1436.csproj`。

## 專案與演算法

- 使用 SDK-style `net10.0` executable project，啟用 implicit usings 與 nullable。
- 公開 API 固定為 `public static string DestCity(IList<IList<string>> paths)`，不新增題目有效輸入之外的例外或 fallback 行為。
- 保留既有兩趟 HashSet 解法：第一趟收集所有出發城市，第二趟回傳唯一不在出發城市集合中的抵達城市。
- 核心不變量是終點沒有任何外出路徑，因此不會出現在出發城市集合中；其他中途城市都會出現在該集合中。
- `DestCity` 不輸出、不修改輸入，所有 console I/O 只由 `Main` 負責。
- 時間複雜度為 `O(n)`；結果空間為 `O(1)`，輔助空間為 `O(n)`。

## Acceptance Harness

`Main` 依序執行八個確定性案例：三個官方範例、路徑順序打亂、終點候選先出現但稍後成為出發點的回歸案例、含空格的城市名稱、大小寫敏感案例，以及 100 條路徑的題目上限 spot check。上限案例使用 `CityA` 到 `CityCW` 共 101 個只含英文字母且不超過 10 字元的唯一城市名稱；大型案例只輸出摘要，避免列出完整路徑。

每個案例在呼叫 `DestCity` 前深層複製外層路徑清單與每個二元素城市清單，並把答案正確及完整巢狀結構相等共同列為 PASS 條件；接著輸出 Case、Input、Expected、Actual 與 PASS/FAIL。成功結尾固定為 `Summary: 8/8 checks passed.`；任一失敗設定 `Environment.ExitCode = 1`。此專案沒有正式測試專案，acceptance harness 是 executable verification mechanism。

## 固定產物與文件

- 在題目根目錄加入 `.editorconfig`、`.gitattributes`、`.gitignore`、`.vscode/tasks.json`、`.vscode/launch.json`、`AGENTS.md`、繁體中文 `README.md` 與 `docs/readme-template.md`。
- 逐檔移除 `leetcode_1436.sln`、`leetcode_1436/App.config` 與 `leetcode_1436/Properties/AssemblyInfo.cs`。
- `Main` 的 XML summary 包含雙語題名、英中官方網址，以及精簡完整的英語與繁體中文題述。
- `DestCity` 的繁體中文 XML summary 說明用途、兩趟 HashSet 概念、有效輸入條件與回傳結果；行內註解只說明終點判斷不變量。
- README 包含題述、限制、核心不變量、演算法與取捨、結果與輔助空間複雜度、逐步範例、八案例表、已驗證命令、fresh run 完整 transcript，以及最終專案結構。

## TDD 與驗證

先完成 SDK 專案與 acceptance harness，但暫不提供 `DestCity`，以 harness 對缺少 API 產生的 `CS0103` 作為有效 RED。接著加入兩趟 HashSet 最小實作取得 GREEN，再於綠燈後整理 XML 文件、註解與 README。

最終控制端驗證包含：

- `jq empty` 驗證兩個 VS Code JSON。
- `dotnet build` 為 0 warnings、0 errors。
- `dotnet run --no-build` 為八個 PASS、exit code 0。
- README 唯一 `text` fence 與 fresh run transcript 精確一致。
- `git diff --check`、changed-path scope 與 legacy absence 全部通過。
- 唯讀 review 沒有未解決的 Critical 或 Important finding。

## 發佈流程

從最新 `origin/main` 建立 `/private/tmp/codex-leetcode-1436-net10` 與 `codex/leetcode-1436-net10`。所有題目改動整理為單一 `feat(leetcode-1436): migrate project to .NET 10` commit。驗證與審查完成後推送、建立 draft PR、核對 head SHA、changed files、commit 數、merge state 與 checks，再轉 Ready 並以 expected head SHA squash merge。

只有 GitHub 確認合併成功後，才把 merge commit SHA 寫入 `/private/tmp/leetcode_1436-merge-sha.txt`，再把 Issue #2 唯一的未勾選行（內容為 ``- [ ] `leetcode_1436` ``）更新為 `[x]`，讀回確認目標已勾選且下一個依序項目仍未勾選。最後 fast-forward 本機 `main`，從該檔案載入已確認的 merge SHA，明確斷言本機 `HEAD` 與 `origin/main` 都等於它，再於合併後狀態重新執行完整驗證。

## 自審重點

- 沒有 placeholder、無關套件、正式測試專案或目標資料夾外變更。
- API、HashSet 不變量、複雜度、八案例輸出與工作區路徑彼此一致。
- 上限案例遵守城市名稱字元與長度限制，且不依賴輸入排列或排序 oracle。
- 八個案例都以深層結構相等驗證 `DestCity` 不修改輸入。
- Issue 更新與合併順序符合 `LEETCODE_NET10_MIGRATION_SPEC.md`。
