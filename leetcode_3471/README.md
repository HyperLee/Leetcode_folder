# 3471. 找出最大的幾乎缺失整數

LeetCode 題目：[3471. Find the Largest Almost Missing Integer](https://leetcode.com/problems/find-the-largest-almost-missing-integer/description/)

本專案使用 .NET 10 Console App，示範兩種解法：

1. 依照 `k` 與陣列長度的關係進行分類討論，利用題目的結構得到 O(n) 解法。
2. 直接枚舉每一個固定長度視窗，以 `HashSet<int>` 統計每個值出現於多少個視窗。

## 題目說明

給定整數陣列 `nums` 與整數 `k`。

如果整數 `x` 出現在 `nums` 中**恰好一個**大小為 `k` 的連續子陣列，就稱 `x` 是幾乎缺失整數（almost missing integer）。請回傳所有幾乎缺失整數中的最大值；如果不存在，回傳 `-1`。

這裡的「出現在子陣列中」是指子陣列包含該值。即使同一個值在同一個視窗中重複出現，也只算該值出現在這一個視窗，不會重複計算。

## 限制條件

題目限制如下：

- `1 <= nums.Length <= 50`
- `0 <= nums[i] <= 50`
- `1 <= k <= nums.Length`

因此本專案可以使用長度 51 的陣列，直接以數值作為索引保存頻率；第二種解法即使使用較直接的視窗枚舉，也能在限制範圍內快速完成。[題目限制參考](https://leetcode.doocs.org/lc/3471/)

## 解題概念與出發點

題目表面上要求我們檢查所有大小為 `k` 的子陣列，但真正要計算的是：

> 每一個整數，究竟被多少個固定長度視窗包含？
> 也可以理解為: 把長度為 k 的 sliding window 全部列出來，找「只出現在一個 window 裡」的數字，再取最大的。

總共有 `nums.Length - k + 1` 個視窗。若某個值的視窗出現次數恰好是 1，它才是候選答案；最後從候選中取最大值。

本專案用兩種角度處理同一個定義：

- 解法一觀察不同 `k` 範圍下，哪些位置可能只被一個視窗覆蓋，藉此避免枚舉視窗。
- 解法二完全按照題目定義枚舉視窗，利用集合避免同一視窗內的重複值被重複計算。

## 解法一：分類討論

API：

```csharp
public int LargestInteger(int[] nums, int k)
```

### 分支一：`k == nums.Length`

此時整個陣列只有一個大小為 `k` 的視窗。陣列中的每個值只要存在於陣列中，就只會出現在這一個視窗，因此答案就是 `nums` 的最大值。

### 分支二：`k == 1`

每一個元素各自形成一個大小為 1 的視窗。例如：

```text
nums = [4, 1, 4, 2, 3], k = 1
```

視窗依序為 `[4]`、`[1]`、`[4]`、`[2]`、`[3]`。因此：

- `4` 出現在 2 個視窗，不符合條件。
- `1`、`2`、`3` 各出現在 1 個視窗。
- 最大答案是 `3`。

此時「出現在一個視窗」等價於「在整個陣列中只出現一次」。先統計全域頻率，再由 50 向下尋找第一個頻率為 1 的值即可。

### 分支三：`1 < k < nums.Length`

當視窗長度大於 1 且小於整個陣列長度時：

- 第一個元素只會被最左側視窗包含一次。
- 最後一個元素只會被最右側視窗包含一次。
- 中間的每個位置至少會被兩個相鄰視窗包含。

所以幾乎缺失整數只能來自 `nums[0]` 或 `nums[nums.Length - 1]`。但端點的值若在陣列其他位置再次出現，就會被其他視窗包含，不能成為答案。因此只需檢查兩端值的全域頻率是否為 1，再取其中較大者。
> 這題其實有一個滿漂亮的規律：當 1 < k < n 時，通常只有 nums[0] 和 nums[n-1] 有可能符合，因為中間的元素一定會被多個長度 k 的 window 包到。

### 正確性重點

分類討論並不是忽略中間值，而是利用「中間位置必定被至少兩個視窗覆蓋」這個結構性質，證明中間位置不可能是幾乎缺失整數。端點則恰好各自只屬於一個視窗，所以還需要用全域頻率排除端點值的其他副本。

### 複雜度

- 時間複雜度：`O(n)`。
- 空間複雜度：`O(V)`，本題 `V = 51`，可視為常數空間。

## 解法二：固定視窗枚舉

API：

```csharp
public int LargestIntegerByWindowEnumeration(int[] nums, int k)
```

這個方法不依賴解法一的端點觀察，而是直接按照題目定義處理：

1. 從每個可能的起點 `start` 建立長度為 `k` 的視窗。
2. 將視窗內的值加入 `HashSet<int>`。
3. 遍歷集合中的值，讓該值的「視窗出現次數」增加 1。
4. 所有視窗處理完後，找出視窗出現次數恰好為 1 的最大值。

使用 `HashSet<int>` 很重要。例如視窗 `[7, 2, 1, 7]` 中，`7` 雖然出現兩次，但它只出現在這一個視窗，所以對 `7` 的視窗計數只能增加一次。

### 範例流程

以官方範例一為例：

```text
nums = [3, 9, 2, 1, 7], k = 3
```

所有視窗與去重後的值如下：

| 視窗 | 去重後的值 |
|---|---|
| `[3, 9, 2]` | `3, 9, 2` |
| `[9, 2, 1]` | `9, 2, 1` |
| `[2, 1, 7]` | `2, 1, 7` |

累計視窗出現次數後：

| 值 | 出現的視窗數 |
|---:|---:|
| 1 | 2 |
| 2 | 3 |
| 3 | 1 |
| 7 | 1 |
| 9 | 2 |

只有 `3` 與 `7` 符合「恰好一個視窗」，所以回傳最大值 `7`。

### 複雜度

令 `n = nums.Length`，值域大小為 `V`：

- 視窗數量為 `n - k + 1`。
- 每個視窗最多處理 `k` 個元素。
- 時間複雜度：`O((n-k+1) * k + V)`。
- 空間複雜度：`O(V+k)`，包括視窗集合與視窗出現次數陣列。

此方法比解法一更直接地對應題目定義，代價是需要枚舉視窗；在 `n <= 50` 的限制下，這個取捨很安全，也適合作為容易驗證的比較解法。

## 兩種解法比較

| 比較項目 | 解法一：分類討論 | 解法二：固定視窗枚舉 |
|---|---|---|
| 主要觀察 | 利用 `k` 與端點覆蓋次數的關係 | 直接統計每個值出現於多少視窗 |
| 是否枚舉視窗 | 否 | 是 |
| 時間複雜度 | `O(n)` | `O((n-k+1) * k + V)` |
| 空間複雜度 | `O(V)` | `O(V+k)` |
| 優點 | 快速且充分利用題目結構 | 定義直觀，容易泛化與驗證 |
| 代價 | 需要理解三種 `k` 情況 | 需要建立每個視窗的集合 |

兩個 API 都不修改輸入陣列；`Main` 仍為每種解法傳入獨立的 `ToArray()` 副本，讓測試案例彼此隔離。

## 可執行範例與測試

請在本專案目錄執行：

```bash
dotnet restore leetcode_3471/leetcode_3471.csproj
dotnet build leetcode_3471/leetcode_3471.csproj --nologo
dotnet run --no-build --project leetcode_3471/leetcode_3471.csproj
dotnet format leetcode_3471/leetcode_3471.csproj --verify-no-changes --no-restore
```

專案沒有額外的測試框架或測試專案，`Main` 中的 8 個具名案例會同時驗證兩種解法，共 16 項檢查。程式遇到失敗時會設定非零 `Environment.ExitCode`，適合在非互動環境中執行。

## 範例執行結果

以下內容由 `dotnet run --no-build --project leetcode_3471/leetcode_3471.csproj` 新鮮執行產生。`RUN-OUTPUT-START` 與 `RUN-OUTPUT-END` 之間的內容應與命令輸出完全一致。

```text
<!-- RUN-OUTPUT-START -->
案例：官方範例一
輸入：nums = [3, 9, 2, 1, 7], k = 3
預期：7
實際：LargestInteger = 7 => PASS
實際：LargestIntegerByWindowEnumeration = 7 => PASS

案例：官方範例二
輸入：nums = [3, 9, 7, 2, 1, 7], k = 4
預期：3
實際：LargestInteger = 3 => PASS
實際：LargestIntegerByWindowEnumeration = 3 => PASS

案例：官方範例三
輸入：nums = [0, 0], k = 1
預期：-1
實際：LargestInteger = -1 => PASS
實際：LargestIntegerByWindowEnumeration = -1 => PASS

案例：k 等於陣列長度
輸入：nums = [2, 1, 3], k = 3
預期：3
實際：LargestInteger = 3 => PASS
實際：LargestIntegerByWindowEnumeration = 3 => PASS

案例：k 等於 1 且包含重複值
輸入：nums = [4, 1, 4, 2, 3], k = 1
預期：3
實際：LargestInteger = 3 => PASS
實際：LargestIntegerByWindowEnumeration = 3 => PASS

案例：單元素陣列
輸入：nums = [6], k = 1
預期：6
實際：LargestInteger = 6 => PASS
實際：LargestIntegerByWindowEnumeration = 6 => PASS

案例：兩端值重複、無答案
輸入：nums = [5, 1, 2, 5], k = 2
預期：-1
實際：LargestInteger = -1 => PASS
實際：LargestIntegerByWindowEnumeration = -1 => PASS

案例：兩端值唯一
輸入：nums = [8, 2, 3, 4, 9], k = 2
預期：9
實際：LargestInteger = 9 => PASS
實際：LargestIntegerByWindowEnumeration = 9 => PASS

總結：16/16 項測試通過
<!-- RUN-OUTPUT-END -->
```

## 專案結構

```text
leetcode_3471/
├── leetcode_3471/
│   ├── Program.cs
│   └── leetcode_3471.csproj
├── docs/
│   └── readme-template.md
└── README.md
```

- `Program.cs`：兩種演算法、XML 文件與可執行測試 harness。
- `leetcode_3471.csproj`：目標框架為 .NET 10 的主控台專案。
- `README.md`：題目、解法推導、範例流程與驗證結果。