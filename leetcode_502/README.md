# LeetCode 502：IPO／首次公開募股

此 .NET 10 主控台專案以「資金門檻排序 + 可啟動專案最大堆」實作貪心解法，在
最多 `k` 次選擇後取得最大最終資金。公開 API 不進行主控台 I/O；`Main` 專責
執行可重複的九項 acceptance checks。

## 題目連結

- [English: 502. IPO](https://leetcode.com/problems/ipo/)
- [中文：502. IPO](https://leetcode.cn/problems/ipo/)

## 題意與限制

**English.** Given `n` projects, project `i` needs at least `capital[i]` capital
to start and yields `profits[i]` pure profit when completed. Starting with `w`,
choose at most `k` distinct feasible projects to maximize the final capital.

**中文。** 有 `n` 個專案；第 `i` 個專案至少要有 `capital[i]` 的資金才能
啟動，完成後可獲得 `profits[i]` 的純利潤。初始資金為 `w`，請在最多選擇
`k` 個不同專案的前提下，回傳最大的最終資金。題目保證答案落在 32 位元帶號
整數範圍內。

官方輸入限制如下：

- `1 <= k <= 100000`
- `0 <= w <= 10^9`
- `n == profits.Length == capital.Length`，且 `1 <= n <= 100000`
- `0 <= profits[i] <= 10000`
- `0 <= capital[i] <= 10^9`

## 核心不變量與解法

先把每個專案表示為 `(RequiredCapital, Profit)`，再依 `RequiredCapital` 升冪
排序。`nextProject` 是尚未檢查的最小索引，永遠只向前走：在每一輪選擇前，將
所有 `RequiredCapital <= w` 的專案放入候選堆。因為候選堆只含「目前可啟動」
且尚未選過的專案，堆頂就是本輪最佳選擇。

C# 的 `PriorityQueue<TElement, TPriority>` 是最小優先權堆；實作以 `-profit`
作為 priority，故數值最大的利潤有最小 priority，會優先被取出。取出利潤後加
到 `w`，這可能讓下一輪的掃描納入更多專案。若候選堆為空，排序後剩下的專案
都需要更高資金，便可立即停止。

容易出錯的地方：

- 不能只選「第一個」可啟動的專案；必須從所有可啟動專案中選最高利潤。
- 每個專案只能入堆與取出各一次；`nextProject` 不可在每輪重掃。
- `PriorityQueue` 預設是最小堆，遺漏負 priority 會錯把最低利潤優先取出。
- 堆為空並非失敗；它表示沒有任何剩餘專案可由目前資金啟動。

公開 API `FindMaximizedCapital` 為純計算函式，不寫入主控台，也不修改
`profits` 或 `capital`；案例的呈現、PASS/FAIL 判斷與結束碼全部集中在 `Main`。

## 複雜度

- 時間複雜度：`O(n log n)`。排序為 `O(n log n)`；每個專案至多入堆與出堆
  一次，總計也不超過 `O(n log n)`。
- 額外空間：`O(n)`，用於排序後的專案陣列與目前可啟動專案的優先權堆。

## 逐步範例

以官方範例 `k = 2`、`w = 0`、`profits = [1, 2, 3]`、
`capital = [0, 1, 1]` 為例。排序後專案為 `(0, 1)`、`(1, 2)`、`(1, 3)`。

1. 初始 `w = 0`，只有 `(0, 1)` 可進堆；取出利潤 `1` 後，`w = 1`。
2. 現在 `(1, 2)` 與 `(1, 3)` 都可進堆；最大堆先取利潤 `3`。
3. 已完成兩次選擇，因此回傳 `w = 0 + 1 + 3 = 4`。

這個選擇同時說明為何第二輪必須選 `3` 而非先遇到的 `2`。

## Acceptance Harness

專案沒有正式測試專案。`Main` 是目前的 acceptance harness；每個案例都顯示
輸入、預期值、實際值與 `PASS`／`FAIL`，且只要任一案例失敗就設定非零結束碼。

| 案例名稱 | 輸入／預期結果 | 驗證重點 |
| --- | --- | --- |
| `Official example 1` | `k=2, w=0`，結果 `4` | 官方範例與第二輪最高利潤選擇 |
| `Official example 2` | `k=3, w=0`，結果 `6` | 逐輪解鎖後連續挑選 |
| `Single affordable project` | 一個可啟動專案，結果 `1` | 最小的單專案情況 |
| `No affordable project` | 無可啟動專案，結果 `1` | 空堆時提早停止 |
| `Choose highest affordable profit` | 同輪候選利潤 `1`、`7`，結果 `10` | 最大堆的貪心選擇 |
| `Unlock a later project` | 利潤依序解鎖，結果 `13` | 資金成長會擴張候選集合 |
| `More selections than projects` | `k=5`、兩個專案，結果 `3` | 選擇次數超過專案數 |
| `Zero-profit projects` | 利潤皆為 `0`，結果 `4` | 零利潤的可行專案 |
| `Upper bound n=100000` | 十萬個利潤 `1` 的專案，結果 `100000` | `n` 與 `k` 上限 |

成功條件為九個案例都通過，並輸出精確的
`Summary: 9/9 checks passed.`。

## 建置與執行

請從此 README 所在的外層 `leetcode_502` 目錄執行：

```bash
dotnet build leetcode_502/leetcode_502.csproj --nologo
dotnet run --no-build --project leetcode_502/leetcode_502.csproj
```

第二個命令使用 `--no-build`，因此必須先成功執行建置命令。

## 實際執行輸出

以下是完成建置後，以 `dotnet run --no-build` 取得的完整 fresh-run 輸出：

```text
Case: Official example 1
Input: k = 2, w = 0, profits = [1, 2, 3], capital = [0, 1, 1]
Expected: 4
Actual: 4
Result: PASS

Case: Official example 2
Input: k = 3, w = 0, profits = [1, 2, 3], capital = [0, 1, 2]
Expected: 6
Actual: 6
Result: PASS

Case: Single affordable project
Input: k = 1, w = 0, profits = [1], capital = [0]
Expected: 1
Actual: 1
Result: PASS

Case: No affordable project
Input: k = 2, w = 1, profits = [2, 3], capital = [2, 3]
Expected: 1
Actual: 1
Result: PASS

Case: Choose highest affordable profit
Input: k = 2, w = 0, profits = [1, 7, 3], capital = [0, 0, 1]
Expected: 10
Actual: 10
Result: PASS

Case: Unlock a later project
Input: k = 3, w = 0, profits = [1, 2, 10], capital = [0, 1, 3]
Expected: 13
Actual: 13
Result: PASS

Case: More selections than projects
Input: k = 5, w = 0, profits = [2, 1], capital = [0, 1]
Expected: 3
Actual: 3
Result: PASS

Case: Zero-profit projects
Input: k = 3, w = 4, profits = [0, 0], capital = [0, 4]
Expected: 4
Actual: 4
Result: PASS

Case: Upper bound n=100000
Input: k = 100000, w = 0, profits = all 1 (n = 100000), capital = all 0 (n = 100000)
Expected: 100000
Actual: 100000
Result: PASS

Summary: 9/9 checks passed.
```

## 專案結構

```plaintext
.
├── .editorconfig                         # C# 與結構化檔案格式
├── .gitattributes                        # 文字與二進位檔案屬性
├── .gitignore                            # .NET／IDE 產生檔案排除規則
├── .vscode/
│   ├── launch.json                       # 直接偵錯 net10.0 輸出
│   └── tasks.json                        # 預設建置工作
├── AGENTS.md                             # 本題協作指南
├── README.md                             # 題目、解法與驗證紀錄
├── docs/
│   ├── readme-template.md                # 初次建立 README 的範本
│   └── superpowers/
│       ├── plans/                        # 遷移實作計畫
│       └── specs/
│           └── 2026-07-12-leetcode-502-net10-migration-design.md
│                                           # .NET 10 遷移設計紀錄
└── leetcode_502/
    ├── Program.cs                        # 純解法與 acceptance harness
    └── leetcode_502.csproj               # .NET 10 SDK 專案設定
```
