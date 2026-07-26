# LeetCode 2616 — 最小化數對的最大差值

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![Language](https://img.shields.io/badge/Language-C%23-239120)](https://learn.microsoft.com/dotnet/csharp/)

這是一個可直接建置與執行的 .NET 10 主控台專案，示範如何用「排序、二分答案、動態規劃或貪心判斷」解決 [LeetCode 2616. Minimize the Maximum Difference of Pairs](https://leetcode.com/problems/minimize-the-maximum-difference-of-pairs/description/)。

## 題目說明

給定一個從 0 開始索引的整數陣列 `nums` 與整數 `p`，需要選出 `p` 組索引配對，並符合：

- 每個索引最多只能出現在一組配對中。
- 索引 `i` 與 `j` 的配對差值為 `|nums[i] - nums[j]|`。
- 在所有合法配對方法中，讓這 `p` 組差值的最大值盡可能小。

回傳這個「最小化後的最大差值」。當 `p == 0` 時沒有任何配對，依題意回傳 `0`。

### 官方範例一

```text
輸入：nums = [10,1,2,7,1,3], p = 2
輸出：1
```

可以選擇原陣列索引 `(1, 4)` 與 `(2, 5)`：

- `|nums[1] - nums[4]| = |1 - 1| = 0`
- `|nums[2] - nums[5]| = |2 - 3| = 1`

這兩組差值的最大值是 `max(0, 1) = 1`，而且無法再降到 `0`，所以答案為 `1`。

### 官方範例二

```text
輸入：nums = [4,2,1,2], p = 1
輸出：0
```

兩個值為 `2` 的元素可以配成一組，差值為 `0`，已經是可能的最小值。

## 限制條件

- `1 <= nums.Length <= 100,000`
- `0 <= nums[i] <= 1,000,000,000`
- `0 <= p <= nums.Length / 2`

## 解題概念與出發點

### 1. 為什麼先排序

若固定要讓兩個值配對，數值越接近，差值越小。排序後，對任一組跨過中間元素的配對 `(nums[i], nums[j])`，`i < j`，相鄰元素所形成的差值不會比它更大。因此在判斷某個差值上限是否可行時，只需要由左至右考慮相鄰元素。

三個公開方法都會呼叫 `Array.Sort(nums)`。這會直接改變傳入的陣列；若呼叫端仍需要原順序，必須先傳入副本：

```csharp
int answer = solution.MinimizeMax((int[])nums.Clone(), p);
```

本專案的測試 runner 也會為每種解法複製輸入，避免前一種解法的排序影響後續驗證。

### 2. 為什麼可以二分答案

假設候選差值上限是 `maxDifference`：

- 若可以組出至少 `p` 對，任何更大的上限也一定可行。
- 若無法組出 `p` 對，任何更小的上限也一定不可行。

可行性具有單調性，因此可以在 `0` 到 `max(nums) - min(nums)` 之間二分搜尋第一個可行值。令數值範圍為 `R`，二分最多進行 `O(log R)` 次，每次以 `O(n)` 判斷可行性。

## 三種解法比較

| 方法 | 二分搜尋形式 | 可行性判斷 | 時間複雜度 | 額外空間 |
| --- | --- | --- | --- | --- |
| `MinimizeMax` | `[left, right]` 收斂 | O(1) 空間 DP | `O(n log n + n log R)` | `O(1)` |
| `MinimizeMax2` | `[left, right]` 收斂 | 內嵌貪心 | `O(n log n + n log R)` | `O(1)` |
| `MinimizeMax3` | 不可行/可行邊界 | `Check` helper 貪心 | `O(n log n + n log R)` | `O(1)` |

> [!NOTE]
> 複雜度中的 `O(n log n)` 來自排序；三種方法都採原地排序，因此表中的額外空間不包含排序實作內部可能使用的堆疊空間。

## 解法一：二分答案＋動態規劃

### 設計

`MinimizeMax` 對每個候選上限呼叫 DP helper。令 `dp[length]` 表示排序後前 `length` 個元素最多能形成幾組合法配對：

1. 不使用第 `length` 個元素：沿用 `dp[length - 1]`。
2. 若最後兩個相鄰元素的差值不超過上限，將它們配對：`dp[length - 2] + 1`。
3. 兩者取最大值。

轉移式為：

```text
dp[length] = dp[length - 1]

若 nums[length - 1] - nums[length - 2] <= maxDifference：
    dp[length] = max(dp[length - 1], dp[length - 2] + 1)
```

實作只需要 `dp[length - 2]` 與 `dp[length - 1]`，因此使用 `twoBack`、`oneBack` 兩個變數，不必配置長度為 `n` 的陣列。只要目前狀態已達到 `p`，即可提前回傳可行。

### 範例演示

以官方範例一排序後的陣列 `[1, 1, 2, 3, 7, 10]`、`p = 2` 為例。當候選上限為 `1` 時：

| `length` | 納入的前綴 | 最後相鄰差值 | `dp[length]` | 說明 |
| ---: | --- | ---: | ---: | --- |
| 0 | `[]` | — | 0 | 尚無元素 |
| 1 | `[1]` | — | 0 | 一個元素無法配對 |
| 2 | `[1,1]` | 0 | 1 | 選擇 `(1,1)` |
| 3 | `[1,1,2]` | 1 | 1 | 新配對仍只能得到一組 |
| 4 | `[1,1,2,3]` | 1 | 2 | 在第一組後再選 `(2,3)` |
| 5 | `[1,1,2,3,7]` | 4 | 2 | 差值超過上限，略過 `7` |
| 6 | `[1,1,2,3,7,10]` | 3 | 2 | 差值超過上限，維持兩組 |

二分搜尋過程：

| `left` | `right` | `mid` | DP 最多配對數 | 結果 |
| ---: | ---: | ---: | ---: | --- |
| 0 | 9 | 4 | 3 | 可行，令 `right = 4` |
| 0 | 4 | 2 | 2 | 可行，令 `right = 2` |
| 0 | 2 | 1 | 2 | 可行，令 `right = 1` |
| 0 | 1 | 0 | 1 | 不可行，令 `left = 1` |

最後 `left == right == 1`，答案為 `1`。

## 解法二：二分答案＋內嵌貪心

### 設計

`MinimizeMax2` 將可行性判斷直接寫在二分迴圈內。排序後由左至右掃描：

1. 若 `nums[i + 1] - nums[i] <= mid`，立刻把兩者配成一組。
2. 配對後將索引增加 2，確保元素不會重複使用。
3. 若差值超過上限，只將索引增加 1，讓下一個元素仍可與後方元素嘗試配對。

立即選擇最左側的合法相鄰配對不會減少後續可使用的元素數量，也不會讓後續差值變得更小，因此能得到此上限下的最大配對數。

### 範例演示

同樣使用 `[1, 1, 2, 3, 7, 10]`、`p = 2`：

- `mid = 4`：選 `(1,1)`、`(2,3)`、`(7,10)`，共 3 對，可行。
- `mid = 2`：選 `(1,1)`、`(2,3)`，共 2 對，可行。
- `mid = 1`：仍可選 `(1,1)`、`(2,3)`，共 2 對，可行。
- `mid = 0`：只能選 `(1,1)`，共 1 對，不可行。

第一個可行上限因此是 `1`。

## 解法三：邊界式二分＋貪心 helper

### 設計

`MinimizeMax3` 的貪心判斷與解法二相同，但拆成 `Check`，讓二分搜尋本身只處理邊界：

- `left = -1`：題目合法差值之外的不可行虛擬邊界。
- `right = nums[^1] - nums[0]`：所有元素的最大跨度；當 `p > 0` 時一定足以形成所需配對。
- 若 `Check(mid)` 可行，將 `right` 移到 `mid`；否則將 `left` 移到 `mid`。
- 當 `left + 1 == right` 時，`right` 就是最小可行答案。

把 `Check` 拆出來可讓二分搜尋的不變量更清楚，也便於單獨理解貪心判斷；代價是需要在方法之間傳遞候選上限、排序陣列與 `p`。

### 範例演示

對 `[1, 1, 2, 3, 7, 10]`、`p = 2`：

| `left` | `right` | `mid` | `Check(mid)` | 更新 |
| ---: | ---: | ---: | --- | --- |
| -1 | 9 | 4 | `true` | `right = 4` |
| -1 | 4 | 1 | `true` | `right = 1` |
| -1 | 1 | 0 | `false` | `left = 0` |

此時 `left + 1 == right`，回傳 `right = 1`。

## 專案結構

```text
leetcode_2616/
├── leetcode_2616/
│   ├── Program.cs
│   └── leetcode_2616.csproj
├── docs/
│   └── readme-template.md
└── README.md
```

- `leetcode_2616/Program.cs`：三種演算法、可行性 helper、固定案例 runner 與進入點。
- `leetcode_2616/leetcode_2616.csproj`：目標框架為 .NET 10 的主控台專案。
- `docs/readme-template.md`：README 的內容與驗證原則範本。

## 建置與執行

需要安裝 [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)。請從此儲存庫根目錄執行：

```powershell
dotnet restore leetcode_2616/leetcode_2616.csproj
dotnet build leetcode_2616/leetcode_2616.csproj
dotnet run --project leetcode_2616/leetcode_2616.csproj
dotnet format leetcode_2616/leetcode_2616.csproj --verify-no-changes
```

此專案目前沒有額外的自動化測試專案；`Main` 會固定執行六個案例，讓三種解法各接受一次相同輸入，共驗證 18 次。只要任一結果失敗，程序結束碼就會設為 `1`。

## 實際執行結果

```text
[官方範例一] nums = [10, 1, 2, 7, 1, 3], p = 2, expected = 1
  MinimizeMax: expected = 1, actual = 1, PASS
  MinimizeMax2: expected = 1, actual = 1, PASS
  MinimizeMax3: expected = 1, actual = 1, PASS

[官方範例二] nums = [4, 2, 1, 2], p = 1, expected = 0
  MinimizeMax: expected = 0, actual = 0, PASS
  MinimizeMax2: expected = 0, actual = 0, PASS
  MinimizeMax3: expected = 0, actual = 0, PASS

[空配對] nums = [7], p = 0, expected = 0
  MinimizeMax: expected = 0, actual = 0, PASS
  MinimizeMax2: expected = 0, actual = 0, PASS
  MinimizeMax3: expected = 0, actual = 0, PASS

[全重複值] nums = [5, 5, 5, 5], p = 2, expected = 0
  MinimizeMax: expected = 0, actual = 0, PASS
  MinimizeMax2: expected = 0, actual = 0, PASS
  MinimizeMax3: expected = 0, actual = 0, PASS

[最小可配對長度] nums = [1, 100], p = 1, expected = 99
  MinimizeMax: expected = 99, actual = 99, PASS
  MinimizeMax2: expected = 99, actual = 99, PASS
  MinimizeMax3: expected = 99, actual = 99, PASS

[多組候選] nums = [1, 3, 6, 19, 20], p = 2, expected = 2
  MinimizeMax: expected = 2, actual = 2, PASS
  MinimizeMax2: expected = 2, actual = 2, PASS
  MinimizeMax3: expected = 2, actual = 2, PASS

Summary: 18/18 PASS
```
