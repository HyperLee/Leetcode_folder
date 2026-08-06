# LeetCode 1863：找出所有子集的 XOR 總和再求和

[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/download/dotnet/10.0)

本專案使用 .NET 10 console application 示範 LeetCode 1863「Sum of All Subset XOR Totals」。程式保留容易直接觀察所有子集的位元遮罩枚舉法，並提供從位元貢獻推導出的 O(n) 數學最佳化解法；`Main` 內含可直接執行的固定案例，會比較兩種實作的結果。

## 目錄

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：位元遮罩枚舉](#解法一位元遮罩枚舉)
- [解法二：數學最佳化](#解法二數學最佳化)
- [兩種解法比較](#兩種解法比較)
- [可執行測試資料](#可執行測試資料)
- [執行方式](#執行方式)
- [實際執行結果](#實際執行結果)
- [專案結構](#專案結構)

## 題目說明

官方題目：[1863. Sum of All Subset XOR Totals](https://leetcode.com/problems/sum-of-all-subset-xor-totals/)

一個陣列的 XOR total，是將其中所有元素依序進行 bitwise XOR；空陣列的 XOR total 定義為 `0`。給定正整數陣列 `nums`，需要列出它的所有子集，計算每個子集的 XOR total，再將這些結果全部相加。

子集是從原陣列刪除零個或多個元素後得到的陣列。即使兩個子集包含相同數值，只要它們選取的是原陣列中的不同位置，就必須分別計算。

例如 `nums = [1, 3]` 共有四個子集：

| 子集 | XOR total |
| --- | ---: |
| `[]` | `0` |
| `[1]` | `1` |
| `[3]` | `3` |
| `[1, 3]` | `1 XOR 3 = 2` |

所以答案為：

```text
0 + 1 + 3 + 2 = 6
```

## 限制條件

| 條件 | 範圍 |
| --- | --- |
| 陣列長度 | `1 <= nums.Length <= 12` |
| 元素範圍 | `1 <= nums[i] <= 20` |
| 子集數量 | `2^nums.Length`，包含空子集 |

題目不接受空的 `nums`，因此兩個公開方法都以至少一個元素為前提，測試資料也不額外定義空輸入的行為。長度最多為 12，代表完整枚舉最多處理 `2^12 = 4096` 個子集，在題目範圍內可以安全執行。

## 解題概念與出發點

### XOR 的基本性質

XOR 逐 bit 比較兩個數字：兩個 bit 不同時結果為 `1`，相同時結果為 `0`。

```text
1 = 01₂
3 = 11₂
-------- XOR
2 = 10₂
```

這題會用到以下性質：

- `x XOR 0 = x`
- `x XOR x = 0`
- XOR 具交換律與結合律，所以元素處理順序不影響子集的 XOR total。

### 為什麼先從枚舉開始

每個原陣列元素對一個子集都只有兩種選擇：選入或不選。若陣列長度為 `n`，總共有 `2^n` 種選擇組合。二進位數字的每個 bit 同樣只有 `0` 與 `1`，因此可以用一個 `n`-bit mask 表示一個子集：

- bit 為 `0`：不選對應位置的元素。
- bit 為 `1`：選入對應位置的元素。

枚舉法直接落實題目定義，容易驗證，也能作為最佳化解法的正確性基準。

### 如何進一步避免枚舉

題目最後只要求所有 XOR total 的總和，不需要保留每一個子集。可以改為逐一考慮每個 bit 對總答案的貢獻：

1. 如果某個 bit 在所有 `nums` 元素中都是 `0`，它不可能出現在任何子集的 XOR 結果。
2. 如果某個 bit 至少出現一次，固定其中一個帶有此 bit 的元素。
3. 對任一子集切換是否選取該元素，會讓這個 bit 的 XOR 結果在 `0` 與 `1` 間翻轉。
4. 所有 `2^n` 個子集因此可兩兩配對，每一對恰有一個結果含有該 bit。
5. 該 bit 總共出現 `2^(n-1)` 次。

哪些 bit 至少出現一次，正好可以用所有元素的 bitwise OR 表示，因此得到：

```text
所有子集 XOR 總和 = (nums[0] OR nums[1] OR ... OR nums[n - 1]) × 2^(n - 1)
```

## 解法一：位元遮罩枚舉

### 設計方式

`SubsetXORSum` 使用 `mask` 從 `0` 走到 `2^n - 1`。每一個 `mask` 都代表唯一的索引選取組合：

1. 將目前子集的 XOR 值 `value` 初始化為 `0`。
2. 檢查 mask 的第 `i` 個 bit。
3. 若該 bit 為 `1`，執行 `value ^= nums[i]`。
4. 檢查完所有元素後，把 `value` 加入總和。

虛擬碼：

```text
sum = 0
total = 2^n

for mask from 0 to total - 1:
    value = 0

    for i from 0 to n - 1:
        if mask 的第 i 個 bit 是 1:
            value = value XOR nums[i]

    sum = sum + value

return sum
```

### 正確性說明

長度為 `n` 的 mask 對每個索引提供選入或不選兩種狀態，因此 `0` 到 `2^n - 1` 與所有索引子集形成一對一對應。內層迴圈只 XOR bit 為 `1` 的元素，所以 `value` 就是該子集的 XOR total。外層迴圈將所有 mask 的結果累加，最後得到所有子集 XOR total 的總和。

mask `0` 代表空子集，`value` 保持為 `0`，自然符合題目定義，不需要額外判斷。

### `[1, 3]` 演示流程

`nums.Length = 2`，所以共有 `1 << 2 = 4` 個 mask：

| mask | bit 1 | bit 0 | 選取的索引 | 子集 | XOR total |
| --- | ---: | ---: | --- | --- | ---: |
| `00₂` | 0 | 0 | 無 | `[]` | `0` |
| `01₂` | 0 | 1 | `0` | `[1]` | `1` |
| `10₂` | 1 | 0 | `1` | `[3]` | `3` |
| `11₂` | 1 | 1 | `0, 1` | `[1, 3]` | `1 XOR 3 = 2` |

累加結果為 `0 + 1 + 3 + 2 = 6`。

### 複雜度

- 時間複雜度：`O(n × 2^n)`；共有 `2^n` 個 mask，每個 mask 最多檢查 `n` 個元素。
- 額外空間複雜度：`O(1)`；只使用計數器與 XOR 累積值，沒有建立實際子集。

### 適用情境

這個版本適合用來學習子集與 bitmask 的對應關係，也容易改造成需要逐一檢查子集內容的問題。缺點是工作量會隨 `n` 指數成長；若只需要本題的加總值，第二種解法更直接。

## 解法二：數學最佳化

### 設計方式

`SubsetXORSumOptimized` 不產生子集，而是先求出：

```text
combinedOr = nums[0] OR nums[1] OR ... OR nums[n - 1]
```

`combinedOr` 中為 `1` 的每個 bit，都會對一半的子集 XOR 結果產生貢獻。全部共有 `2^n` 個子集，因此乘數是 `2^(n-1)`：

```text
answer = combinedOr × 2^(n - 1)
```

虛擬碼：

```text
combinedOr = 0

for each num in nums:
    combinedOr = combinedOr OR num

return combinedOr × 2^(nums.Length - 1)
```

### 正確性說明

任取一個在 `combinedOr` 中為 `1` 的 bit，表示至少存在一個元素 `x` 帶有這個 bit。把所有子集依「是否包含 `x`」兩兩配對：每一對的其他元素完全相同，只差 `x` 是否被選入。加入或移除 `x` 必定翻轉該 bit 的 XOR 狀態，因此每對中恰有一個子集的 XOR total 含有這個 bit。

`2^n` 個子集可形成 `2^(n-1)` 對，所以此 bit 出現 `2^(n-1)` 次。對每個可能的 bit 套用相同推理，將所有 bit 的數值貢獻合併，便等價於 `combinedOr × 2^(n-1)`。

### `[1, 3]` 演示流程

先觀察二進位表示：

```text
1 = 01₂
3 = 11₂
```

所有元素的 OR：

```text
01₂ OR 11₂ = 11₂ = 3
```

陣列長度 `n = 2`，每個曾出現的 bit 會存在於一半的子集 XOR 結果：

```text
2^(n - 1) = 2^(2 - 1) = 2
```

所以：

```text
combinedOr × 2^(n - 1) = 3 × 2 = 6
```

不需要實際列出四個子集，也能得到相同答案。

### 複雜度

- 時間複雜度：`O(n)`；只需掃描輸入陣列一次。
- 額外空間複雜度：`O(1)`。

### 適用情境

這是本題專用且效率最佳的解法，適合只需要總和、不需要取得各子集內容時使用。它依賴 XOR 與子集配對的數學性質；若題目改成過濾特定子集、列出子集或計算其他函式，就不一定能沿用此公式。

## 兩種解法比較

| 比較項目 | `SubsetXORSum` | `SubsetXORSumOptimized` |
| --- | --- | --- |
| 核心策略 | 用 mask 枚舉每個子集 | 計算每個 bit 對所有子集的總貢獻 |
| 是否實際走訪每個子集 | 是 | 否 |
| 時間複雜度 | `O(n × 2^n)` | `O(n)` |
| 額外空間複雜度 | `O(1)` | `O(1)` |
| 是否修改輸入 | 否 | 否 |
| 教學重點 | bitmask 與子集的一對一關係 | XOR bit 配對與貢獻公式 |
| 延伸彈性 | 容易加入逐子集處理邏輯 | 適合本題只求總和的需求 |

## 可執行測試資料

`Main` 會執行六組固定案例，每組分別呼叫兩種解法，共進行 12 項比較：

| 案例 | 輸入 | 預期結果 | 涵蓋重點 |
| --- | --- | ---: | --- |
| 官方範例一 | `[1, 3]` | `6` | 最小的多元素典型案例 |
| 官方範例二 | `[5, 1, 6]` | `28` | 多個 bit 交錯 |
| 官方範例三 | `[3, 4, 5, 6, 7, 8]` | `480` | 較長的典型輸入 |
| 單一元素 | `[5]` | `5` | `2^(n-1) = 1` 的長度下界 |
| 重複值 | `[2, 2]` | `4` | 相同數值但不同索引的子集仍分別計算 |
| 長度上界 | 12 個 `20` | `40960` | `nums.Length = 12` 與大量重複元素 |

每一項會顯示 `Expected`、`Actual` 與 `PASS/FAIL`。全部通過時程序結束碼為 `0`；任一結果不符時，最終總結會反映失敗數量，程序結束碼為 `1`，方便在非互動環境或 CI 中偵測問題。

## 執行方式

需求：安裝 [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)。

從 `leetcode_1863` repository 根目錄執行：

```bash
dotnet restore leetcode_1863/leetcode_1863.csproj
dotnet build leetcode_1863/leetcode_1863.csproj --nologo
dotnet run --no-build --project leetcode_1863/leetcode_1863.csproj
```

也可以省略預先建置，直接使用會自動建置的命令：

```bash
dotnet run --project leetcode_1863/leetcode_1863.csproj
```

## 實際執行結果

以下內容來自完成程式後實際執行 `dotnet run --no-build --project leetcode_1863/leetcode_1863.csproj` 的輸出：

```text

案例：1. 官方範例一
Input：nums = [1, 3]
解法一：位元遮罩枚舉
Expected：6
Actual：6
Result：PASS
解法二：數學最佳化
Expected：6
Actual：6
Result：PASS

案例：2. 官方範例二
Input：nums = [5, 1, 6]
解法一：位元遮罩枚舉
Expected：28
Actual：28
Result：PASS
解法二：數學最佳化
Expected：28
Actual：28
Result：PASS

案例：3. 官方範例三
Input：nums = [3, 4, 5, 6, 7, 8]
解法一：位元遮罩枚舉
Expected：480
Actual：480
Result：PASS
解法二：數學最佳化
Expected：480
Actual：480
Result：PASS

案例：4. 單一元素
Input：nums = [5]
解法一：位元遮罩枚舉
Expected：5
Actual：5
Result：PASS
解法二：數學最佳化
Expected：5
Actual：5
Result：PASS

案例：5. 重複值
Input：nums = [2, 2]
解法一：位元遮罩枚舉
Expected：4
Actual：4
Result：PASS
解法二：數學最佳化
Expected：4
Actual：4
Result：PASS

案例：6. 長度上界
Input：nums = [20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20]
解法一：位元遮罩枚舉
Expected：40960
Actual：40960
Result：PASS
解法二：數學最佳化
Expected：40960
Actual：40960
Result：PASS

總結：12/12 項測試通過
```

## 專案結構

```text
leetcode_1863/
├── docs/
│   └── readme-template.md
├── leetcode_1863/
│   ├── leetcode_1863.csproj
│   └── Program.cs
├── leetcode_1863.sln
└── README.md
```
