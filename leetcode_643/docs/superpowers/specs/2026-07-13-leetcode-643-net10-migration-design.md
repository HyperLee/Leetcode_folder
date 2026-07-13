# leetcode_643 .NET 10 遷移設計

**日期：** 2026-07-13

**狀態：** 使用者已核可設計，待實作計畫核可
**範圍：** 僅限 `leetcode_643/`

## 目標

將舊式 .NET Framework 4.8 主控台專案完整遷移為可直接執行的 .NET 10 SDK-style
專案，保留 LeetCode 公開 API `FindMaxAverage(int[] nums, int k)`，並完成 PR、
squash merge、Issue #2 勾選與合併後驗證。

題目要求在所有長度恰為 `k` 的連續子陣列中，回傳最大平均值。有效輸入滿足
`1 <= k <= nums.Length <= 100000`，每個數值介於 `-10000` 與 `10000`。

## 演算法決策

舊版以原地前綴和計算區間總和，但當 `k = 1` 且陣列含多個元素時會忽略索引 0，
而且會改動呼叫端傳入的陣列。

最終採用固定長度滑動視窗：

1. 先計算前 `k` 個數字的總和，作為第一個候選視窗與目前最大總和。
2. 視窗每向右移一格，加入新右端值並扣除離開左端的值。
3. 保留最大視窗總和，最後只除以一次 `k` 得到最大平均值。

此作法維持 `O(n)` 時間與 `O(1)` 額外空間，不輸出主控台訊息且不改動輸入陣列。
README 會說明它是將舊版「快速取得固定區間總和」的教學意圖，改寫為更直接且
沒有副作用的 rolling sum 形式。

## 程式與文件結構

在 `leetcode_643/` 題目根目錄新增共用設定、VS Code 設定、協作指南、README 與
README 範本；巢狀專案保留於 `leetcode_643/leetcode_643/`。

- `.csproj` 改為 SDK-style，指定 `net10.0`、`ImplicitUsings` 與 nullable。
- `Program.cs` 只讓 `Main` 印出驗收結果；解題 API 保持純函式。
- `Main` 補齊中英題名、官方連結與雙語題述 XML summary；
  `FindMaxAverage` 補上繁體中文 XML summary 與高訊號視窗不變量註解。
- 逐一移除舊 `leetcode_643.sln`、`App.config` 與 `Properties/AssemblyInfo.cs`。
- `.vscode` 以直接開啟 `leetcode_643/` 為前提，直接建置及啟動巢狀 `.csproj`。

## Acceptance Harness

`Main` 將建立資料型別化的 case result，集中輸出每個案例的 Input、Expected、Actual
與 PASS/FAIL，並在最後輸出精確摘要；任何失敗會設定非零 exit code。

驗收案例至少包含：

1. 官方範例 `[1, 12, -5, -6, 50, 3]`、`k = 4`，結果 `12.75`。
2. 最小有效輸入 `[5]`、`k = 1`，結果 `5`。
3. 舊版回歸案例 `[-1, -2]`、`k = 1`，結果 `-1`。
4. 全負值 `[-8, -6, -7]`、`k = 2`，結果 `-6.5`。
5. `k = n` 的整段視窗案例。
6. 視窗持續滑動且最後一個視窗勝出的案例。
7. `100000` 個相同值的上限 spot check，僅輸出摘要而非整份陣列。
8. 輸入陣列不變動的不變量案例。

浮點比較會以題目接受誤差更嚴格的 epsilon 判斷，輸出採固定格式以保持 README
transcript 穩定。

## TDD、驗證與交付

在完成 SDK-style 專案形狀後，先以最小的 `k = 1` deterministic harness 執行舊前綴和
邏輯；它必須 FAIL，作為可重現且與實作行為直接相關的 RED。接著擴充為完整 eight-case
acceptance harness 並改為滑動視窗，以相同 build/run 命令取得 0 warnings、0 errors 與
全數 PASS 的 GREEN。

驗證還包括 VS Code JSON、README 唯一 `text` fence 與 fresh-run transcript diff、
legacy 檔案缺席、`git diff --check`、改動範圍和獨立唯讀 code review。

通過後，分支只會有一個 Conventional Commit：
`feat(leetcode-643): migrate project to .NET 10`。接著建立 draft PR（使用 `Refs #2`）、
核對已驗證 head SHA 與 checks 後轉 Ready 並 squash merge；只有 GitHub 回報 merged
與 merge SHA 後，才會勾選 Issue #2 的唯一 `leetcode_643` 項目，最後在合併後的 `main`
重新執行驗證。

## 非目標

- 不修改其他題目、共用 repository 設定或 Issue #2 的其他項目。
- 不新增正式測試專案或題目契約外的 invalid-input 例外。
- 不保留原地改寫輸入或任何 helper 的主控台輸出。
