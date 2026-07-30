# LeetCode 62：不同路徑

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4)

這個專案以 C# 與 .NET 10 示範 [LeetCode 62. Unique Paths](https://leetcode.com/problems/unique-paths/description/)（[力扣中文題目](https://leetcode.cn/problems/unique-paths/description/)）。目前實作使用「遞迴＋記憶化搜尋」，並提供可直接執行的固定案例 runner。

## 題目說明

機器人位於 `m × n` 網格的左上角 `(0, 0)`，目標是走到右下角 `(m - 1, n - 1)`。每一步只能：

- 向右移動一格。
- 向下移動一格。

輸入網格列數 `m` 與欄數 `n`，回傳所有不同的合法路徑數。

### 限制條件

- `1 <= m, n <= 100`
- 題目保證答案不超過 `2 × 10^9`，因此可使用 C# `int`。
- 輸入只會包含符合題目限制的正整數；本專案不另外處理非法尺寸。

## 解題概念與出發點

若要到達座標 `(i, j)`，最後一步只有兩種來源：

1. 從上方 `(i - 1, j)` 向下走。
2. 從左方 `(i, j - 1)` 向右走。

因此可以把大問題拆成兩個較小、而且會反覆出現的子問題：

```text
dfs(i, j) = dfs(i - 1, j) + dfs(i, j - 1)
```

直接遞迴會重複計算相同座標。程式使用 `memo[i][j]` 保存已算出的路徑數；再次遇到同一座標時直接回傳保存值，把指數級的重複搜尋降為每個網格狀態最多計算一次。

## 解法比較

| 解法 | 核心策略 | 時間複雜度 | 額外空間複雜度 | 專案狀態 |
| --- | --- | --- | --- | --- |
| 遞迴＋記憶化搜尋 | 從終點反推上方與左方，把每個座標答案存入 `memo` | `O(m × n)` | `O(m × n)` | 已實作 |

目前專案只有這一種解法，沒有加入未實作的迭代動態規劃或組合數學版本。

## 解法設計

### 1. 狀態定義

`dfs(i, j)` 表示從左上角 `(0, 0)` 走到 `(i, j)` 的不同路徑數。

公開方法 `UniquePaths(m, n)` 建立 `m × n` 的記憶化陣列，再從終點呼叫：

```text
dfs(m - 1, n - 1)
```

### 2. 邊界條件

- `i < 0` 或 `j < 0`：已走出網格，這個方向不能形成合法路徑，回傳 `0`。
- `i == 0`：位於第一列，只能一直向右走，回傳 `1`。
- `j == 0`：位於第一欄，只能一直向下走，回傳 `1`。

合法網格中的路徑數至少為 `1`，所以初始化後的 `0` 可以安全代表「這個狀態尚未計算」。

### 3. 記憶化流程

對一般座標 `(i, j)`：

1. 若 `memo[i][j]` 不為 `0`，直接回傳已儲存的答案。
2. 否則遞迴計算上方與左方的路徑數。
3. 將兩者相加後寫入 `memo[i][j]`。
4. 回傳保存的結果。

### 4. 複雜度

- 時間複雜度：`O(m × n)`。每個座標最多被完整計算一次。
- 空間複雜度：`O(m × n)`。二維 `memo` 保存所有狀態。
- 遞迴堆疊最深為 `O(m + n)`；它包含在整體空間分析中，但有助於理解遞迴執行成本。

## 範例演示：`m = 3, n = 2`

終點是 `(2, 1)`，程式從終點反向拆解：

```text
dfs(2, 1)
├─ dfs(1, 1)
│  ├─ dfs(0, 1) = 1
│  └─ dfs(1, 0) = 1
│  └─ memo[1][1] = 1 + 1 = 2
└─ dfs(2, 0) = 1

memo[2][1] = memo[1][1] + dfs(2, 0)
           = 2 + 1
           = 3
```

三條實際路徑分別為：

1. 右、下、下。
2. 下、右、下。
3. 下、下、右。

因此 `UniquePaths(3, 2)` 回傳 `3`。

## 專案結構

```text
leetcode_062/
├─ leetcode_062/
│  ├─ leetcode_062.csproj
│  └─ Program.cs
├─ docs/
│  └─ readme-template.md
├─ README.md
└─ leetcode_062.sln
```

## 建置與執行

請從此儲存庫根目錄執行：

```powershell
dotnet restore leetcode_062/leetcode_062.csproj
dotnet build leetcode_062/leetcode_062.csproj --nologo --no-restore
dotnet run --project leetcode_062/leetcode_062.csproj --no-build
```

專案目前沒有自動化測試專案。`Main` 中的固定 console runner 會涵蓋官方範例、對稱尺寸、單欄、最小網格與較大網格，作為目前的行為驗收。

## 執行結果

```text
PASS | Case 1 | m = 3, n = 7 | Expected: 28 | Actual: 28
PASS | Case 2 | m = 3, n = 2 | Expected: 3 | Actual: 3
PASS | Case 3 | m = 7, n = 3 | Expected: 28 | Actual: 28
PASS | Case 4 | m = 3, n = 1 | Expected: 1 | Actual: 1
PASS | Case 5 | m = 1, n = 1 | Expected: 1 | Actual: 1
PASS | Case 6 | m = 10, n = 10 | Expected: 48620 | Actual: 48620
Result: 6/6 passed.
```