# leetcode_1218 .NET 10 遷移設計

## 目標

將 LeetCode 1218「Longest Arithmetic Subsequence of Given Difference／最長定差子序列」從舊式 .NET Framework 4.8 主控台專案遷移為可在 .NET 10 執行、可重複驗證且可完整發布的單題專案，同時保留兩種具有不同教學取捨的線性時間動態規劃解法。

## 範圍與交付

- 所有 tracked changes 僅限 `leetcode_1218/`，不調整其他題目或 repository 根目錄結構。
- 使用分支 `codex/leetcode-1218-net10` 與 worktree `/private/tmp/codex-leetcode-1218-net10` 隔離工作。
- 最終交付為一個 Conventional Commit、Draft PR、已驗證的 squash merge，以及合併後對 GitHub Issue #2 唯一 `leetcode_1218` 核取方塊的更新。
- 舊 `leetcode_1218.sln`、`App.config`、`Properties/AssemblyInfo.cs` 會逐檔移除，不使用批次刪除命令。

## 專案結構

題目根目錄新增 `.editorconfig`、`.gitattributes`、`.gitignore`、`.vscode/`、`AGENTS.md`、`README.md`、`docs/readme-template.md`、本設計紀錄與後續實作計畫。巢狀 `leetcode_1218/` 保留 `Program.cs`，並將 `leetcode_1218.csproj` 改為 SDK-style `net10.0`，啟用 implicit usings 與 nullable。

VS Code 設定與 README 命令以直接開啟題目根目錄 `leetcode_1218/` 為 workspace 的使用方式設計，明確指向巢狀專案 `leetcode_1218/leetcode_1218.csproj`。

## 公開 API 與共用狀態定義

保留兩個既有公開 API：

```csharp
public static int LongestSubsequence(int[] arr, int difference)
public static int LongestSubsequence2(int[] arr, int difference)
```

兩者都依照輸入順序掃描陣列，不修改輸入，也不寫入主控台。共同的動態規劃狀態為：`dp[x]` 表示目前已掃描前綴中，以值 `x` 結尾且相鄰差為 `difference` 的最長子序列長度。讀到 `x` 時，以 `dp[x - difference] + 1` 更新 `dp[x]`。依題目有效輸入契約，陣列非空，因此兩者最終至少回傳 `1`；不新增 null、空陣列或越界值的替代語意與例外。

## 解法一：雜湊動態規劃

`LongestSubsequence` 使用 `Dictionary<int, int>`，只為實際出現的結尾值保存最佳長度。每個元素以平均 `O(1)` 查找前驅狀態並更新目前狀態，總時間複雜度為 `O(n)`，結果空間為 `O(1)`，輔助空間為 `O(u)`；`u` 是陣列中不同值的數量。

此解法不依賴題目值域大小，狀態公式最直接，作為主要標準解法。

## 解法二：定值域陣列動態規劃

`LongestSubsequence2` 利用題目限制 `-10^4 <= arr[i] <= 10^4`，以長度 `20,001` 的整數陣列及位移量 `10,000` 儲存各合法結尾值的最佳長度。計算 `x - difference` 後，只有前驅值仍落在 `[-10,000, 10,000]` 時才讀取陣列狀態；超出值域代表輸入中不可能存在該前驅，前驅長度視為零。

此解法同樣為 `O(n)` 時間、`O(1)` 結果空間，並使用固定 `20,001` 個整數的輔助空間。它以固定記憶體換取無雜湊成本，與第一個解法形成清楚的資料結構取捨，而不是保留兩份等價的 Dictionary 寫法。

## Acceptance Harness

`Main` 集中所有主控台輸出。每個案例分別執行兩個公開方法，因此 8 組案例產生 16 項獨立檢查：

1. 官方範例 `[1, 2, 3, 4]`、`difference = 1`，預期 `4`。
2. 官方範例 `[1, 3, 5, 7]`、`difference = 1`，預期 `1`。
3. 官方負公差範例 `[1, 5, 7, 8, 5, 3, 4, 2, 1]`、`difference = -2`，預期 `4`。
4. 最小有效輸入 `[5]`，預期 `1`。
5. 零公差與重複值 `[7, 7, 7, 7]`、`difference = 0`，預期 `4`。
6. 順序不可重排回歸案例 `[4, 1, 2, 3]`、`difference = 1`，預期 `3`。
7. 值域邊界 `[-10000, 0, 10000]`、`difference = 10000`，預期 `3`。
8. 100,000 個相同值與零公差，預期 `100000`，驗證題目長度上限的合理 spot check。

每項檢查顯示案例名稱、Input、解法名稱、Expected、Actual 與 PASS/FAIL。所有檢查通過時輸出 `Summary: 16/16 checks passed.`；任一失敗會設定 `Environment.ExitCode = 1`。大型案例只輸出摘要，不展開全部元素。

## TDD 與錯誤路徑驗證

舊專案 baseline 已預期因缺少 .NET Framework 4.8 reference assemblies 而出現 `MSB3644`，此結果只作為 legacy 證據，不算有效 RED。

有效 RED 會先建立 SDK-style .NET 10 專案與完整 harness，但暫不加入兩個 production API，使 build 明確因缺少 `LongestSubsequence` 與 `LongestSubsequence2` 而產生 `CS0103`。GREEN 階段加入兩種最小正確實作，以相同 build 與 run 命令確認 0 warnings、0 errors、16 項全數通過且 exit code 為零。

GREEN 後會暫時將一個案例的 expected value 改錯，確認 harness 顯示 FAIL、summary 減少且 process exit code 非零；隨即還原正確期望值並重新執行完整驗證。這個暫時變更不會進入最終 commit。

## 文件與設定

`Program.cs` 的 `Main` XML summary 包含雙語題名、官方中英文網址與精簡雙語題述。兩個公開 API 以繁體中文 XML summary 說明用途、狀態轉移、有效輸入條件與回傳結果；行內註解只解釋 DP 狀態更新、順序限制與陣列值域判斷。

README 使用繁體中文說明題意、限制、共同 DP 不變量、兩個解法的資料結構取捨、時間／結果／輔助空間複雜度、逐步案例、acceptance cases、實際 build/run 命令與 fresh run 完整輸出。完整 transcript 是 README 唯一的 `text` fence，並以精確 diff 驗證。

`AGENTS.md` 記錄真實巢狀結構、題目根目錄命令、沒有正式 test project、兩個公開 API 的純函式要求、DP 不變量、harness 成功條件，以及 Git metadata 位於 parent repository 的提醒。

## 驗證、審查與發布

本機門檻包含：

- 以 `jq empty` 驗證 `.vscode/launch.json` 與 `.vscode/tasks.json`。
- 明確 build 巢狀 `.csproj`，結果為 0 warnings、0 errors。
- 以 `--no-build` 執行 harness，確認 `Summary: 16/16 checks passed.` 與 exit code 0。
- 抽取 README 唯一 `text` fence，與 fresh run transcript 執行 `diff -u` 並得到空差異。
- 執行 `git diff --check -- leetcode_1218`、changed-path scope 檢查、SDK-style 契約檢查與三個 legacy files absence 檢查。

完成後進行獨立唯讀 code review；reviewer 不修改 tracked files。Critical 或 Important 問題必須修正、重跑全部門檻並重新審查。

發布前將設計、計畫與遷移內容維持為相對 `origin/main` 的單一 Conventional Commit。推送後建立 Draft PR，確認 head SHA、changed files、commit count、checks 與 merge state，再轉 Ready 並以已驗證 head SHA squash merge。只有 GitHub 確認 PR 已合併後，才將 Issue #2 唯一 `leetcode_1218` 行改為已勾選並讀回確認 `leetcode_1232` 仍未勾選。最後 fast-forward 本機 `main`，在合併後狀態重跑完整驗證。

## 自審結果

- 文件沒有 placeholder、未決需求或未定義 API。
- 兩個保留解法具有不同的資料結構與空間取捨，沒有保留等價的重複實作。
- API、題目值域、harness 預期值、README 規劃與 TDD 順序彼此一致。
- 所有 tracked changes 與發布內容均限制在 `leetcode_1218/`，Issue 更新只針對目標題。
