# LeetCode 416：分割等和子集

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/language-C%23-239120)
![LeetCode](https://img.shields.io/badge/LeetCode-416%20Medium-FFA116)

這是一個以 .NET 10 主控台程式實作的教學範例。專案保留二維 0/1 背包動態規劃，
並加入自頂向下的記憶化深度優先搜尋，從兩個方向理解「每個元素只能選一次」的
子集合加總問題。`Main` 內含六筆固定案例，會同時檢查答案與輸入陣列是否保持不變。

## 快速導覽

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：二維 0/1 背包動態規劃](#解法一二維-01-背包動態規劃)
- [解法二：記憶化深度優先搜尋](#解法二記憶化深度優先搜尋)
- [兩種解法比較](#兩種解法比較)
- [可執行驗證案例](#可執行驗證案例)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)
- [專案結構](#專案結構)

## 題目說明

給定一個只包含正整數的非空陣列 `nums`，判斷能否將所有元素分成兩個子集，
使兩個子集的元素總和相等。每個輸入元素必須恰好屬於其中一個子集。

題目連結：

- [LeetCode 416 - Partition Equal Subset Sum](https://leetcode.com/problems/partition-equal-subset-sum/description/)
- [力扣 416 - 分割等和子集](https://leetcode.cn/problems/partition-equal-subset-sum/description/)

### 官方範例

```text
輸入：nums = [1, 5, 11, 5]
輸出：true
說明：可以分成 [1, 5, 5] 與 [11]，兩邊總和都是 11。

輸入：nums = [1, 2, 3, 5]
輸出：false
說明：不存在兩個元素總和相等的分割方式。
```

## 限制條件

- `1 <= nums.Length <= 200`
- `1 <= nums[i] <= 100`
- 輸入陣列非空，且只包含正整數。
- 兩種公開解法都假設輸入符合題目限制，不另外定義空陣列、負數或 `null` 的行為。
- `CanPartition` 與 `CanPartition2` 都只讀取輸入，不會重新排序或修改陣列內容。

## 解題概念與出發點

若陣列總和為 `totalSum`，兩個等和子集的總和都必須是：

```text
target = totalSum / 2
```

因此可以先做必要條件判斷：

1. `totalSum` 為奇數時，無法平均分成兩個整數總和，直接回傳 `false`。
2. `totalSum` 為偶數時，問題轉換為：能否從 `nums` 選出部分元素，使總和恰好為
   `target`？
3. 因為每個陣列元素只能使用一次，這正是 0/1 背包的可行性問題。

例如 `nums = [1, 5, 11, 5]`：

```text
totalSum = 22
target   = 11
```

只要找到總和為 `11` 的子集，例如 `[11]` 或 `[1, 5, 5]`，其餘元素的總和也必然
是 `11`，所以不必另外建立第二個子集。

本專案從兩個方向求解同一組狀態：

- 二維動態規劃由較小問題逐步建立完整表格。
- 記憶化 DFS 從最終目標出發，只展開實際需要的選取與略過分支。

## 解法一：二維 0/1 背包動態規劃

### 設計說明

`CanPartition` 定義：

```text
dp[i, currentSum]
```

表示只考慮前 `i` 個元素時，能否選出總和為 `currentSum` 的子集。

表格大小是 `(nums.Length + 1) × (target + 1)`。多出來的第 0 列代表尚未考慮
任何元素：

```text
dp[i, 0] = true
```

無論考慮多少元素，都可以藉由完全不選取來組成總和 `0`。其餘第 0 列狀態皆為
`false`，因為沒有元素時無法組成正數總和。

考慮目前元素 `currentNumber = nums[i - 1]` 時有兩種選擇：

1. 不選目前元素：沿用 `dp[i - 1, currentSum]`。
2. 選目前元素：若 `currentNumber <= currentSum`，檢查前一列能否組成
   `currentSum - currentNumber`。

轉移式為：

```text
dp[i, currentSum] =
    dp[i - 1, currentSum]
    OR
    dp[i - 1, currentSum - currentNumber]
```

選取分支刻意讀取前一列，而不是目前列，確保同一個輸入元素不會在一次轉移中被
重複使用。

### 正確性說明

對每個 `dp[i, currentSum]`，任何可行子集對第 `i` 個元素只有兩種互斥情況：

- 未包含它，此時答案完全等同前 `i - 1` 個元素的同一目標。
- 包含它，此時其餘元素必須從前 `i - 1` 個元素組成
  `currentSum - currentNumber`。

轉移式完整涵蓋這兩種情況，且沒有遺漏其他可能。從正確的基底狀態
`dp[i, 0] = true` 逐列推導後，`dp[nums.Length, target]` 就精確表示能否找到
目標子集。

### 範例演示：`[1, 5, 11, 5]`

總和為 `22`，目標為 `11`。以下只列出每一列中為 `true` 的可達總和：

| 已考慮元素 | 可達總和 |
| --- | --- |
| 無 | `{0}` |
| `[1]` | `{0, 1}` |
| `[1, 5]` | `{0, 1, 5, 6}` |
| `[1, 5, 11]` | `{0, 1, 5, 6, 11}` |
| `[1, 5, 11, 5]` | `{0, 1, 5, 6, 10, 11}` |

處理數字 `11` 時：

```text
dp[3, 11]
= dp[2, 11] OR dp[2, 11 - 11]
= false OR dp[2, 0]
= true
```

因此能找到總和為 `11` 的子集，最終回傳 `true`。

### 複雜度

令 `n = nums.Length`，`target = totalSum / 2`：

- 時間複雜度：`O(n × target)`，每個表格狀態只計算一次。
- 額外空間複雜度：`O(n × target)`，用於二維布林表格。
- 輸出空間：`O(1)`，只回傳一個布林值。

## 解法二：記憶化深度優先搜尋

### 設計說明

`CanPartition2` 仍先計算 `target`，接著呼叫：

```text
CanReachTarget(nums, index, remaining, memo)
```

狀態代表：從索引 `index` 開始，能否選出總和恰好為 `remaining` 的元素。

每個狀態依序處理：

1. `remaining == 0`：已組成目標，回傳 `true`。
2. `index == nums.Length`：元素已用完但目標尚未歸零，回傳 `false`。
3. 快取已有結果：直接回傳 `memo[index, remaining]`。
4. 若目前元素不超過剩餘目標，先嘗試選取它。
5. 若選取分支失敗，再嘗試略過目前元素。
6. 將兩個分支的結果存入記憶化表後回傳。

使用 `bool?[,]` 是為了區分三種狀態：

- `null`：尚未計算。
- `true`：此狀態可以到達目標。
- `false`：此狀態無法到達目標。

沒有記憶化時，不同選取路徑可能反覆進入相同的 `(index, remaining)`，最壞情況會
展開接近 `2^n` 個分支。快取使每個有效狀態最多實際求解一次。

### 正確性說明

對狀態 `(index, remaining)`，任何解只有兩種可能：

- 包含 `nums[index]`，後續需組成 `remaining - nums[index]`。
- 不包含 `nums[index]`，後續仍需組成 `remaining`。

DFS 對可選取的元素完整探索這兩種情況，任一分支成功就回傳 `true`。當
`remaining` 歸零時已找到合法子集；當索引到達陣列尾端仍未歸零時則不可能成功。
因此遞迴涵蓋所有合法子集，記憶化只重用既有答案，不會改變搜尋結果。

### 範例演示：`[1, 5, 11, 5]`

目標為 `11`。其中一條成功路徑如下：

```text
CanReachTarget(index: 0, remaining: 11)
選取 1
  CanReachTarget(index: 1, remaining: 10)
  選取 5
    CanReachTarget(index: 2, remaining: 5)
    11 大於剩餘目標，無法選取
      CanReachTarget(index: 3, remaining: 5)
      選取 5
        CanReachTarget(index: 4, remaining: 0)
        回傳 true
```

這條路徑找到子集 `[1, 5, 5]`。由於選取分支已成功，短路邏輯不必繼續探索其餘
略過分支。若其他路徑再次遇到相同 `(index, remaining)`，則直接讀取 `memo`。

### 複雜度

令 `n = nums.Length`，`target = totalSum / 2`：

- 時間複雜度：`O(n × target)`，每個索引與剩餘目標組合最多求解一次。
- 記憶化表空間：`O(n × target)`。
- 遞迴堆疊空間：`O(n)`，每層至少將索引向後推進一格。
- 輸出空間：`O(1)`。

## 兩種解法比較

| 比較項目 | 解法一：二維動態規劃 | 解法二：記憶化 DFS |
| --- | --- | --- |
| 公開 API | `CanPartition(int[] nums)` | `CanPartition2(int[] nums)` |
| 思考方向 | 自底向上建立所有狀態 | 自頂向下搜尋需要的狀態 |
| 狀態 | 前 `i` 個元素、目前總和 | 目前索引、剩餘目標 |
| 分支表達 | 表格中的選取／不選取轉移 | 遞迴呼叫的選取／略過分支 |
| 剪枝 | 仍填滿整張 DP 表 | 成功時短路，且不選取超過剩餘目標的元素 |
| 時間複雜度 | `O(n × target)` | `O(n × target)` |
| 主要額外空間 | `O(n × target)` DP 表 | `O(n × target)` memo 表 |
| 其他空間 | `O(1)` | `O(n)` 遞迴堆疊 |
| 是否修改輸入 | 否 | 否 |
| 教學重點 | 0/1 背包狀態轉移 | 選擇樹、重疊子問題與快取 |

## 可執行驗證案例

`Main` 為每一種解法建立獨立輸入副本。單項檢查必須同時滿足「回傳值符合預期」
與「呼叫後輸入內容未改變」才會顯示 `PASS`。六筆案例、兩種解法共十二項驗證。

| 案例 | 輸入 | 預期 | 涵蓋重點 |
| ---: | --- | --- | --- |
| 1 | `[1, 5, 11, 5]` | `true` | 官方可分割範例 |
| 2 | `[1, 2, 3, 5]` | `false` | 官方不可分割範例、奇數總和 |
| 3 | `[1]` | `false` | 陣列長度下界 |
| 4 | `[100, 100]` | `true` | 元素值上界、兩個相等子集 |
| 5 | `[2, 2, 3, 5]` | `false` | 總和為偶數仍不一定可分割 |
| 6 | `[3, 3, 3, 4, 5]` | `true` | 重複值與多種可達目標組合 |

## 建置與執行

需求：

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

從 `leetcode_416` repository 根目錄執行：

```bash
dotnet restore leetcode_416/leetcode_416.csproj
dotnet build leetcode_416/leetcode_416.csproj --no-restore --nologo
dotnet run --project leetcode_416/leetcode_416.csproj --no-build
```

目前沒有獨立的自動化測試專案；驗收方式是成功建置，再執行 `Main` 內固定的
Expected/Actual 與輸入保持性檢查。

## 實際執行結果

以下內容來自上述 `dotnet run` 命令：

```text
案例 1：官方範例一
  輸入：[1, 5, 11, 5]
  預期：True
  解法一（二維動態規劃）：True => PASS
  解法二（記憶化 DFS）：True => PASS

案例 2：官方範例二
  輸入：[1, 2, 3, 5]
  預期：False
  解法一（二維動態規劃）：False => PASS
  解法二（記憶化 DFS）：False => PASS

案例 3：單一元素
  輸入：[1]
  預期：False
  解法一（二維動態規劃）：False => PASS
  解法二（記憶化 DFS）：False => PASS

案例 4：最大元素值
  輸入：[100, 100]
  預期：True
  解法一（二維動態規劃）：True => PASS
  解法二（記憶化 DFS）：True => PASS

案例 5：偶數總和但不可分割
  輸入：[2, 2, 3, 5]
  預期：False
  解法一（二維動態規劃）：False => PASS
  解法二（記憶化 DFS）：False => PASS

案例 6：多種組合可達目標
  輸入：[3, 3, 3, 4, 5]
  預期：True
  解法一（二維動態規劃）：True => PASS
  解法二（記憶化 DFS）：True => PASS

總結：12/12 項驗證通過
```

## 專案結構

```text
leetcode_416/
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_416.sln
└── leetcode_416/
    ├── leetcode_416.csproj
    └── Program.cs
```

- `leetcode_416/Program.cs`：兩種解法、XML 文件與可執行驗證案例。
- `leetcode_416/leetcode_416.csproj`：目標框架為 `net10.0` 的主控台專案。
- `docs/readme-template.md`：README 首次建立的內容與驗證規範。
