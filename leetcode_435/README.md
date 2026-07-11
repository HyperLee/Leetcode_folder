# LeetCode 435：Non-overlapping Intervals（無重疊區間）

這是一個以 C# 撰寫的 .NET 10 主控台專案。`EraseOverlapIntervals` 使用依結束點
排序的貪心策略，求出讓其餘區間互不重疊時必須移除的最小區間數；`Main` 則是可重複
執行的 acceptance harness。

- [英文題目：435. Non-overlapping Intervals](https://leetcode.com/problems/non-overlapping-intervals/)
- [中文題目：435. 無重疊區間](https://leetcode.cn/problems/non-overlapping-intervals/)

## 題目說明

給定 `intervals`，其中每個 `intervals[i] = [starti, endi]` 表示一個區間。請回傳
最少要移除多少個區間，才能使保留下來的所有區間彼此不重疊。兩個區間只在端點接觸
不算重疊，例如 `[1, 2]` 與 `[2, 3]` 可以同時保留。

## 限制條件

- `1 <= intervals.length <= 10^5`
- `intervals[i].length == 2`
- `-5 * 10^4 <= starti < endi <= 5 * 10^4`

實作只處理題目定義的有效輸入，不另外加入無效輸入的例外或回傳規則。

## 貪心不變量

每次保留「目前可選且結束得最早」的區間。它留下的可用時間最多，因此後面能相容的
區間數不會比保留任何較晚結束的區間少。具體做法如下：

1. 依 `end` 由小到大排序。
2. 先保留第一個區間，記錄其結束點 `selectedEnd`。
3. 若下一個區間的 `start >= selectedEnd`，它只是在端點接觸或完全在後方，可保留並更新結束點。
4. 否則它與已保留區間重疊；因為已保留區間結束得更早，移除目前區間最有利，移除數加一。

`Array.Sort` 會就地重新排列傳入的 `intervals`。這是保留既有解法的空間取捨；因此
驗證器在每次呼叫前會深複製案例輸入，避免排序結果影響其他檢查。

公開介面：

```csharp
public static int EraseOverlapIntervals(int[][] intervals)
```

## 複雜度

- 時間複雜度：`O(n log n)`，主要來自排序；掃描一次為 `O(n)`。
- 結果空間：`O(1)`，只回傳移除數。
- 輔助空間：`O(log n)`，排序所需的遞迴／堆疊空間；不額外複製公開 API 的輸入。

## 逐步走查

以 `[[1,100],[2,3],[3,4],[4,5]]` 為例。若貪心地留下長區間 `[1,100]`，會錯失三個
短區間；依結束點排序後即可避免此錯誤。

| 排序後區間 | 與 `selectedEnd` 的關係 | 動作 | `selectedEnd` | 移除數 |
| --- | --- | --- | ---: | ---: |
| `[2,3]` | 第一個區間 | 保留 | 3 | 0 |
| `[3,4]` | `3 >= 3`，端點接觸 | 保留 | 4 | 0 |
| `[4,5]` | `4 >= 4`，端點接觸 | 保留 | 5 | 0 |
| `[1,100]` | `1 < 5`，與已選區間重疊 | 移除 | 5 | 1 |

最後只需移除 `[1,100]`，答案是 `1`。

## 可執行驗證案例

`Main` 共執行八項確定性檢查：

| 案例 | 預期移除數 | 驗證目的 |
| --- | ---: | --- |
| 官方範例 1 | 1 | 基本重疊與非重疊混合 |
| 官方範例 2 | 2 | 三個完全相同的區間 |
| 官方範例 3 | 0 | 端點接觸不算重疊 |
| 單一極值區間 | 0 | 最小有效輸入與座標上下限 |
| 長區間與短鏈 | 1 | 最早結束點的核心貪心選擇 |
| 未排序多重重疊 | 2 | 先排序再決策的必要性 |
| 100000 個相接區間 | 0 | 題目長度上限與端點規則 |
| 100000 個相同區間 | 99999 | 題目長度上限與移除計數 |

每項都印出案例名稱、輸入摘要、Expected、Actual 與 PASS/FAIL。任何失敗都會設定
非零 exit code；這個專案沒有獨立的 test project 或測試框架。

## 建置與執行

請從此 README 所在的外層 `leetcode_435` 目錄執行：

```bash
dotnet build leetcode_435/leetcode_435.csproj --nologo
dotnet run --no-build --project leetcode_435/leetcode_435.csproj
```

以下為重新建置後執行第二個命令的完整輸出：

```text
LeetCode 435 acceptance harness

Case 1: Official example 1
Input: [[1, 2], [2, 3], [3, 4], [1, 3]]
PASS | Minimum removals | Expected: 1 | Actual: 1

Case 2: Official example 2
Input: [[1, 2], [1, 2], [1, 2]]
PASS | Minimum removals | Expected: 2 | Actual: 2

Case 3: Official example 3
Input: [[1, 2], [2, 3]]
PASS | Minimum removals | Expected: 0 | Actual: 0

Case 4: Single interval at coordinate limits
Input: [[-50000, 50000]]
PASS | Minimum removals | Expected: 0 | Actual: 0

Case 5: Greedy keeps the short chain
Input: [[1, 100], [2, 3], [3, 4], [4, 5]]
PASS | Minimum removals | Expected: 1 | Actual: 1

Case 6: Unsorted multiple overlaps
Input: [[1, 100], [11, 22], [1, 11], [2, 12]]
PASS | Minimum removals | Expected: 2 | Actual: 2

Case 7: Maximum adjacent intervals
Input: 100000 adjacent intervals from [-50000, -49999] through [49999, 50000]
PASS | Minimum removals | Expected: 0 | Actual: 0

Case 8: Maximum identical intervals
Input: 100000 identical [-50000, 50000] intervals
PASS | Minimum removals | Expected: 99999 | Actual: 99999

Summary: 8/8 checks passed.
```

## 專案結構

```plaintext
.
├── .editorconfig              # C# 與結構化檔案的格式規範
├── .gitattributes             # 文字與二進位檔案屬性
├── .gitignore                 # .NET／IDE 產生檔案排除規則
├── .vscode/
│   ├── launch.json            # 直接偵錯 net10.0 輸出
│   └── tasks.json             # 預設建置工作
├── docs/
│   └── readme-template.md     # 初次建立 README 的範本
├── leetcode_435/
│   ├── Program.cs             # 貪心解法與可執行驗證器
│   └── leetcode_435.csproj    # .NET 10 SDK 專案設定
├── AGENTS.md                  # 本專案協作指南
└── README.md                  # 題目、解法與驗證紀錄
```
