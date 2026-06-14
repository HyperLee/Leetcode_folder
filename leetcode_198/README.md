# LeetCode 198 - House Robber

這個專案使用 C# `net10.0` 示範 LeetCode 198「打家劫舍」的兩種動態規劃解法，並在 `Main` 中提供可直接執行的範例驗證流程。程式目前保留兩個主要方法：

- `Rob(int[] nums)`: 使用 DP 陣列記錄每一間房屋位置的最佳解。
- `Rob2(int[] nums)`: 使用滾動變數將空間複雜度從 `O(n)` 優化為 `O(1)`。

## 題目說明

給定一個整數陣列 `nums`，其中 `nums[i]` 代表第 `i` 間房屋可以偷到的金額。你不能在同一個晚上偷相鄰的兩間房屋，因為相鄰房屋的警報系統會互相連動。請回傳在不觸發警報的前提下，最多可以偷到多少金額。

換句話說，每間房屋只有兩種選擇：

1. 偷這一間房屋，那前一間就不能偷。
2. 不偷這一間房屋，保留前面已經算出的最佳結果。

## 限制條件

- `nums` 至少包含一間房屋。
- `nums[i]` 代表單間房屋金額，範例中也涵蓋金額為 `0` 的情況。
- 不能同時偷相鄰的兩間房屋。
- 目標不是列出偷哪些房屋，而是求出最大可偷總金額。

## 解題概念與出發點

這題的核心在於：當我們走到第 `i` 間房屋時，最佳答案只會來自兩種可能。

- `跳過第 i 間`: 最佳值等於前一間房屋的最佳值。
- `偷第 i 間`: 最佳值等於前兩間房屋的最佳值加上 `nums[i]`。

因此可以得到同一條轉移公式：

```text
best[i] = max(best[i - 1], best[i - 2] + nums[i])
```

差別只在於我們要不要把所有中間結果都存下來。

## 解法一: `Rob(int[] nums)` - DP 陣列版本

### 設計說明

`Rob` 會建立一個 `dp` 陣列，其中 `dp[i]` 表示「考慮到第 `i` 間房屋為止，最多可以偷到多少錢」。

初始化方式如下：

- `dp[0] = nums[0]`
  只有一間房屋時，只能偷它。
- `dp[1] = max(nums[0], nums[1])`
  只有兩間房屋時，只能挑金額較高的那一間。

從第 3 間房屋開始，也就是 `i = 2` 起，每一步都用同一個公式：

```text
dp[i] = max(dp[i - 1], dp[i - 2] + nums[i])
```

這代表：

- `dp[i - 1]`: 不偷目前這一間，沿用前一格最佳解。
- `dp[i - 2] + nums[i]`: 偷目前這一間，前一間必須放棄，因此接上前兩格最佳解。

最後回傳 `dp[n - 1]` 即可。

### 複雜度

- 時間複雜度: `O(n)`
- 空間複雜度: `O(n)`

### 範例演示流程

以 `nums = [2, 7, 9, 3, 1]` 為例：

| 索引 `i` | 房屋金額 `nums[i]` | 計算方式 | `dp[i]` |
| --- | --- | --- | --- |
| 0 | 2 | 只有第一間，直接偷 | 2 |
| 1 | 7 | `max(2, 7)` | 7 |
| 2 | 9 | `max(dp[1], dp[0] + 9) = max(7, 11)` | 11 |
| 3 | 3 | `max(dp[2], dp[1] + 3) = max(11, 10)` | 11 |
| 4 | 1 | `max(dp[3], dp[2] + 1) = max(11, 12)` | 12 |

最終答案是 `12`。

從偷竊組合來看，可以理解為選擇第 `0`、`2`、`4` 間房屋，總和為 `2 + 9 + 1 = 12`。

## 解法二: `Rob2(int[] nums)` - 滾動變數版本

### 設計說明

`Rob2` 的轉移公式與 `Rob` 完全相同，但觀察後會發現：每次只會用到前兩個狀態，因此不需要保存整個 `dp` 陣列。

在這個版本中：

- `prev`: 代表前兩格，也就是「到 `i - 2` 為止的最佳解」
- `curr`: 代表前一格，也就是「到 `i - 1` 為止的最佳解」
- `next`: 代表這一輪剛算出的新最佳解

每前進一間房屋，就做一次：

```text
next = max(curr, prev + nums[i])
prev = curr
curr = next
```

這樣就能在不開新陣列的情況下，持續維持同樣的 DP 狀態轉移。

### 複雜度

- 時間複雜度: `O(n)`
- 空間複雜度: `O(1)`

### 範例演示流程

同樣以 `nums = [2, 7, 9, 3, 1]` 為例：

初始值：

- `prev = 2`
- `curr = max(2, 7) = 7`

接著從索引 `2` 開始往後走：

| 索引 `i` | 房屋金額 `nums[i]` | `next = max(curr, prev + nums[i])` | 更新後 `prev` | 更新後 `curr` |
| --- | --- | --- | --- | --- |
| 2 | 9 | `max(7, 2 + 9) = 11` | 7 | 11 |
| 3 | 3 | `max(11, 7 + 3) = 11` | 11 | 11 |
| 4 | 1 | `max(11, 11 + 1) = 12` | 11 | 12 |

最後 `curr = 12`，因此答案也是 `12`。

這個版本的重點不是改變思路，而是把完整陣列壓縮成兩個必要狀態。

## `Main` 中的可執行測試資料

目前 `Main` 會依序驗證以下六筆資料，並同時比對 `Rob` 與 `Rob2` 的結果：

| 案例 | 輸入 | 預期輸出 |
| --- | --- | --- |
| 1 | `[1, 2, 3, 1]` | `4` |
| 2 | `[2, 7, 9, 3, 1]` | `12` |
| 3 | `[2, 1, 1, 2]` | `4` |
| 4 | `[5]` | `5` |
| 5 | `[2, 1]` | `2` |
| 6 | `[0]` | `0` |

這些案例涵蓋了：

- 一般情況
- 經典範例
- 首尾較大值分散的情況
- 只有一間房屋
- 只有兩間房屋
- 金額為 `0` 的邊界案例

## 專案執行方式

### 建置

```bash
dotnet build leetcode_198/leetcode_198.csproj
```

### 執行範例

```bash
dotnet run --project leetcode_198/leetcode_198.csproj
```

### 檢查 diff 空白問題

```bash
git diff --check
```

### 關於 `dotnet test`

目前 repository root 沒有 `.sln` 或獨立測試專案，因此直接在 root 執行 `dotnet test` 會失敗，錯誤訊息如下：

```text
MSBUILD : error MSB1003: 指定專案或方案檔。目前工作目錄未包含專案或方案檔。
```

## 實際執行輸出

以下是目前 `dotnet run --project leetcode_198/leetcode_198.csproj` 的實際輸出：

```text
LeetCode 198 - House Robber Sample Verification
Case 1: nums = [1, 2, 3, 1], expected = 4
  Rob  -> actual = 4, PASS
  Rob2 -> actual = 4, PASS
Case 2: nums = [2, 7, 9, 3, 1], expected = 12
  Rob  -> actual = 12, PASS
  Rob2 -> actual = 12, PASS
Case 3: nums = [2, 1, 1, 2], expected = 4
  Rob  -> actual = 4, PASS
  Rob2 -> actual = 4, PASS
Case 4: nums = [5], expected = 5
  Rob  -> actual = 5, PASS
  Rob2 -> actual = 5, PASS
Case 5: nums = [2, 1], expected = 2
  Rob  -> actual = 2, PASS
  Rob2 -> actual = 2, PASS
Case 6: nums = [0], expected = 0
  Rob  -> actual = 0, PASS
  Rob2 -> actual = 0, PASS
Overall: PASS
```

## 專案結構

```text
leetcode_198/
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_198/
    ├── Program.cs
    └── leetcode_198.csproj
```

如果之後要補更多案例，建議優先更新 `Program.cs` 中的 sample case 清單，再重新執行 `dotnet run`，確保 README 中的輸出區塊與實際結果保持一致。
