# LeetCode 1608：特殊陣列的特徵值

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![Language](https://img.shields.io/badge/language-C%23-239120)](https://learn.microsoft.com/dotnet/csharp/)

這是一個可直接執行的 .NET 10 主控台教學專案，使用三種不同思路解決 [LeetCode 1608. Special Array With X Elements Greater Than or Equal X](https://leetcode.com/problems/special-array-with-x-elements-greater-than-or-equal-x/description/)，並以固定案例比較結果與驗證輸入陣列不會被修改。

## 題目說明

給定一個由非負整數組成的陣列 `nums`。若存在整數 `x`，使陣列中恰好有 `x` 個元素大於或等於 `x`，則稱這個陣列為特殊陣列。

- 找到符合條件的 `x` 時回傳 `x`。
- 找不到時回傳 `-1`。
- `x` 不必出現在 `nums` 中。
- 若答案存在，答案必定唯一。

例如 `nums = [3, 5]` 時，恰好有兩個元素大於或等於 `2`，所以答案是 `2`。

## 限制條件

- `1 <= nums.Length <= 100`
- `0 <= nums[i] <= 1000`

由於陣列最多只有 `n` 個元素，符合題意的正整數 `x` 不可能大於 `n`。此外，陣列非空且所有元素皆為非負數，所以所有元素都大於或等於 `0`，不可能「恰好有 0 個元素大於或等於 0」。因此只需檢查 `1` 到 `n`。

## 解題概念與出發點

題目的核心不是尋找某個特定元素，而是尋找一個計數固定點：

```text
x = 陣列中大於或等於 x 的元素數量
```

三種解法分別從直接枚舉、排序後觀察分界，以及預先統計數量出發。它們都遵守相同的 API 契約：只讀取 `nums`，不改變呼叫端傳入的陣列內容與順序。

| 解法 | 方法 | 時間複雜度 | 額外空間 | 特色 |
| --- | --- | --- | --- | --- |
| `SpecialArray` | 複製、降序排序、檢查分界 | `O(n log n)` | `O(n)` | 分界條件清楚，容易由排序結果理解 |
| `SpecialArray2` | 枚舉候選值並逐項計數 | `O(n²)` | `O(1)` | 最直覺，適合建立正確性基準 |
| `SpecialArray3` | 計數桶與後綴累加 | `O(n)` | `O(n)` | 不需排序，漸進時間最佳 |

## 解法一：排序後檢查分界

### 設計說明

`SpecialArray` 先複製輸入，再將副本由大到小排序。對候選值 `x` 而言：

1. 排序後索引 `x - 1` 的值必須大於或等於 `x`，代表前 `x` 個元素都合格。
2. 若 `x < n`，索引 `x` 的值必須小於 `x`，代表剩下的元素全部不合格。
3. 若 `x == n`，右側已沒有元素，只需確認最後一個元素仍大於或等於 `x`。

兩側條件同時成立時，合格元素數量才會「恰好」等於 `x`。排序只作用於副本，因此不會修改原始輸入。

### 範例演示：`[0, 4, 3, 0, 4]`

1. 複製並降序排序為 `[4, 4, 3, 0, 0]`。
2. `x = 1`：第一個元素 `4 >= 1`，但下一個元素 `4` 也大於或等於 `1`，合格數量超過 1。
3. `x = 2`：第二個元素 `4 >= 2`，但下一個元素 `3` 也大於或等於 `2`，合格數量超過 2。
4. `x = 3`：第三個元素 `3 >= 3`，下一個元素 `0 < 3`。
5. 分界兩側都成立，表示恰好有 3 個元素大於或等於 3，回傳 `3`。

## 解法二：枚舉候選值

### 設計說明

`SpecialArray2` 直接枚舉 `x = 1..n`。每次掃描整個陣列，計算 `number >= x` 的元素數量；若計數等於 `x` 就立即回傳。

這個方法忠實翻譯題意，沒有額外資料結構，也不會改變輸入。代價是每個候選值都要重新掃描一次陣列，因此時間複雜度為 `O(n²)`。

### 範例演示：`[0, 4, 3, 0, 4]`

1. `x = 1`：符合的元素是 `4、3、4`，共有 3 個，`3 != 1`。
2. `x = 2`：符合的元素仍是 `4、3、4`，共有 3 個，`3 != 2`。
3. `x = 3`：符合的元素是 `4、3、4`，共有 3 個，`3 == 3`。
4. 找到固定點，回傳 `3`。

## 解法三：計數桶與後綴累加

### 設計說明

`SpecialArray3` 建立長度為 `n + 1` 的 `counts`：

1. 值小於 `n` 的元素放入對應索引的桶。
2. 值大於或等於 `n` 的元素全部放入索引 `n`。因為候選值最大只會是 `n`，更大的實際數值不必分開保存。
3. 從 `n` 向 `1` 掃描並累加桶數。走到 `x` 時，累加值正好代表大於或等於 `x` 的元素數量。
4. 累加值等於 `x` 時回傳答案。

每個輸入元素只計數一次，每個候選值也只檢查一次，因此時間複雜度為 `O(n)`。

### 範例演示：`[0, 4, 3, 0, 4]`

陣列長度 `n = 5`，建立 `counts[0..5]`：

```text
數值 0 出現 2 次
數值 3 出現 1 次
數值 4 出現 2 次
counts = [2, 0, 0, 1, 2, 0]
```

由右向左累加：

1. `x = 5`：累加數量為 `0`，不等於 5。
2. `x = 4`：累加 `counts[4]` 後為 `2`，不等於 4。
3. `x = 3`：再加上 `counts[3]` 後為 `3`，等於 3。
4. 回傳 `3`。

## 輸入不變契約

三個解法都不會修改傳入的 `nums`：

- 排序解法使用陣列副本。
- 枚舉解法只讀取元素。
- 計數桶解法將統計結果寫入新的 `counts` 陣列。

`Main` 在呼叫每種解法前建立獨立輸入，並保留快照。呼叫後使用 `SequenceEqual` 比較兩者；只有答案正確且輸入未變時，該項檢查才會顯示 `PASS`。

## 建置與執行

需要 .NET 10 SDK。從此題目的 repository 根目錄執行：

```bash
dotnet restore leetcode_1608/leetcode_1608.csproj
dotnet build leetcode_1608/leetcode_1608.csproj --nologo
dotnet run --no-build --project leetcode_1608/leetcode_1608.csproj
```

目前沒有獨立的自動化測試專案；可執行的 `Main` 案例就是本專案的行為驗收器。

## 實際執行結果

以下內容來自上述 `dotnet run --no-build` 指令：

```text
Case 1: 官方範例 1
Input: [3, 5]
Expected: 2
  SpecialArray
    Actual: 2
    Input unchanged: True
    Result: PASS
  SpecialArray2
    Actual: 2
    Input unchanged: True
    Result: PASS
  SpecialArray3
    Actual: 2
    Input unchanged: True
    Result: PASS

Case 2: 全為零
Input: [0, 0]
Expected: -1
  SpecialArray
    Actual: -1
    Input unchanged: True
    Result: PASS
  SpecialArray2
    Actual: -1
    Input unchanged: True
    Result: PASS
  SpecialArray3
    Actual: -1
    Input unchanged: True
    Result: PASS

Case 3: 含重複值的官方範例
Input: [0, 4, 3, 0, 4]
Expected: 3
  SpecialArray
    Actual: 3
    Input unchanged: True
    Result: PASS
  SpecialArray2
    Actual: 3
    Input unchanged: True
    Result: PASS
  SpecialArray3
    Actual: 3
    Input unchanged: True
    Result: PASS

Case 4: 單一元素且有解
Input: [1]
Expected: 1
  SpecialArray
    Actual: 1
    Input unchanged: True
    Result: PASS
  SpecialArray2
    Actual: 1
    Input unchanged: True
    Result: PASS
  SpecialArray3
    Actual: 1
    Input unchanged: True
    Result: PASS

Case 5: 元素值上界
Input: [1000]
Expected: 1
  SpecialArray
    Actual: 1
    Input unchanged: True
    Result: PASS
  SpecialArray2
    Actual: 1
    Input unchanged: True
    Result: PASS
  SpecialArray3
    Actual: 1
    Input unchanged: True
    Result: PASS

Case 6: 無符合的候選值
Input: [3, 6, 7, 7, 0]
Expected: -1
  SpecialArray
    Actual: -1
    Input unchanged: True
    Result: PASS
  SpecialArray2
    Actual: -1
    Input unchanged: True
    Result: PASS
  SpecialArray3
    Actual: -1
    Input unchanged: True
    Result: PASS

Case 7: 特徵值等於陣列長度
Input: [4, 4, 4, 4]
Expected: 4
  SpecialArray
    Actual: 4
    Input unchanged: True
    Result: PASS
  SpecialArray2
    Actual: 4
    Input unchanged: True
    Result: PASS
  SpecialArray3
    Actual: 4
    Input unchanged: True
    Result: PASS

Summary: 21/21 checks passed.
```

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_1608.sln
└── leetcode_1608/
    ├── leetcode_1608.csproj
    └── Program.cs
```
