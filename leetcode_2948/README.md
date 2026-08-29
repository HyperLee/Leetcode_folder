# LeetCode 2948：交換得到字典序最小的陣列

[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)

本專案以 .NET 10 主控台程式實作 LeetCode 2948，並在 `Main` 中提供兩種可以直接執行的解法驗證案例。

- 題目：[2948. Make Lexicographically Smallest Array by Swapping Elements](https://leetcode.com/problems/make-lexicographically-smallest-array-by-swapping-elements/description/)
- 中文題目：[2948. 交換得到字典序最小的陣列](https://leetcode.cn/problems/make-lexicographically-smallest-array-by-swapping-elements/)
- 解法程式：[leetcode_2948/Program.cs](leetcode_2948/Program.cs)

## 題目說明

給定一個以 `0` 為起始索引的正整數陣列 `nums`，以及一個正整數 `limit`。

一次操作可以選擇任意兩個索引 `i` 和 `j`。當目前兩個位置上的元素滿足：

```text
|nums[i] - nums[j]| <= limit
```

就可以交換這兩個元素。操作可以進行任意次，最後要回傳所有可能結果中字典序最小的陣列。

若兩個陣列在第一個不同的位置上，陣列 `a` 的元素小於陣列 `b` 的元素，則稱 `a` 的字典序小於 `b`。例如：

```text
[2, 10, 3] < [10, 2, 3]
```

因為兩個陣列在索引 `0` 首次不同，而 `2 < 10`。

## 限制條件

依照官方題目限制：

- `1 <= nums.length <= 10^5`
- `1 <= nums[i] <= 10^9`
- `1 <= limit <= 10^9`

這些限制代表不能對所有元素兩兩比較並建立完整交換關係，否則最壞情況會產生 `O(n²)` 的時間複雜度。

## 解題概念與出發點

### 1. 合法交換形成連通塊

可以把每個元素視為一個節點：如果兩個元素的值差不超過 `limit`，就在它們之間建立一條邊。

如果 `x` 可以和 `y` 交換，而 `y` 又可以和 `z` 交換，即使 `x` 和 `z` 不能直接交換，也可以透過 `y` 間接調整位置。因此，同一個連通塊中的元素可以透過多次合法交換重新排列；不同連通塊之間則不能互相交換。

### 2. 排序後只需要觀察相鄰差值

將所有元素按照數值由小到大排序，並保留原始索引：

```text
(value, originalIndex)
```

排序後，如果相鄰元素滿足：

```text
sortedValues[i] - sortedValues[i - 1] <= limit
```

它們屬於同一個連通塊。

如果某個相鄰差值大於 `limit`，由於陣列已排序，右側元素和左側所有元素的差值都會更大，因此連通塊必定在這裡切開。這讓我們不必真的建立圖，也不必執行 DFS 或 BFS，只要一次線性掃描就可以找出所有群組。

### 3. 群組內的最佳放法

對每一個連通群組：

1. 群組中的值已經按照非遞減順序排列。
2. 將群組中的原始索引另外排序。
3. 把較小的值依序放到較小的原始索引。

這樣可以讓最前面能被改善的位置盡量放入最小的可用值，所以得到該群組能產生的字典序最小結果。

### 連鎖連通範例

輸入：

```text
nums = [10, 1, 5], limit = 5
```

按照值排序並保留索引：

```text
(1, index 1), (5, index 2), (10, index 0)
```

相鄰差值是 `4` 與 `5`，都不超過 `limit = 5`，因此三個元素屬於同一個連通塊。群組原始索引排序後為 `[0, 1, 2]`，將值 `[1, 5, 10]` 依序放回，就得到：

```text
[1, 5, 10]
```

這個例子說明不需要每一對元素都能直接交換；只要能透過中間元素連成同一個群組，就可以在多次操作後重新排列。

## 解法一：值與原始索引一起排序

實作方法：[`LexicographicallySmallestArray`](leetcode_2948/Program.cs)。

### 設計流程

1. 建立 `(value, index)` Tuple 清單，把每個值和它的原始索引綁在一起。
2. 按照 `value` 由小到大排序。
3. 將排序後的值與索引分別放入 `values` 和 `indices`，方便掃描群組。
4. 從左到右掃描排序結果：只要相鄰值差不超過 `limit`，就繼續放進目前群組；否則結束目前群組。
5. 將群組中的原始索引由小到大排序。
6. 因為群組值本來已經排序，所以把 `groupValues[k]` 放到 `groupIndices[k]`。
7. 所有群組完成後，`ans` 就是字典序最小陣列。

### 範例演示

輸入：

```text
nums = [1, 5, 3, 9, 8], limit = 2
```

排序後的 `(value, index)`：

```text
[(1, 0), (3, 2), (5, 1), (8, 4), (9, 3)]
```

逐一檢查相鄰差值：

| 相鄰值 | 差值 | 判斷 |
|---|---:|---|
| `1, 3` | `2` | `2 <= limit`，留在同一群組 |
| `3, 5` | `2` | `2 <= limit`，留在同一群組 |
| `5, 8` | `3` | `3 > limit`，切開群組 |
| `8, 9` | `1` | `1 <= limit`，留在同一群組 |

因此得到兩個群組：

```text
群組一：值 [1, 3, 5]，原始索引 [0, 2, 1]
群組二：值 [8, 9]，原始索引 [4, 3]
```

將索引排序後：

```text
群組一：索引 [0, 1, 2]，放入值 [1, 3, 5]
群組二：索引 [3, 4]，放入值 [8, 9]
```

最後結果為：

```text
[1, 3, 5, 8, 9]
```

### 複雜度

- 時間複雜度：`O(n log n)`。主要成本是排序；群組掃描與結果重建是 `O(n)`。
- 空間複雜度：`O(n)`。需要保存 Tuple 清單、群組資料與答案陣列。

## 解法二：只排序原始索引

實作方法：[`LexicographicallySmallestArray2`](leetcode_2948/Program.cs)。

### 設計流程

這個解法和解法一使用相同的連通群組觀察，但不建立 `(value, index)` Tuple 清單，而是只保存索引：

1. 建立 `idx = [0, 1, ..., n - 1]`。
2. 依照 `nums[idx[i]]` 的值排序 `idx`，讓索引陣列代表值排序後的順序。
3. 使用兩個指標 `i` 和 `j` 掃描目前群組；當 `nums[idx[j]] - nums[idx[j - 1]] <= limit` 時，擴大群組。
4. 取出 `idx[i..j]` 作為目前群組的索引，將這些索引排序。
5. 按照值排序後的 `idx` 順序，將 `nums[idx[k]]` 放回排序後的原始索引。
6. 移動 `i` 到 `j`，繼續處理下一個群組。

### 範例演示

仍使用：

```text
nums = [1, 5, 3, 9, 8], limit = 2
```

初始索引：

```text
idx = [0, 1, 2, 3, 4]
```

依照 `nums[idx[i]]` 排序後：

```text
idx = [0, 2, 1, 4, 3]
```

此時 `nums[idx]` 的值順序是：

```text
[1, 3, 5, 8, 9]
```

掃描相鄰差值後，得到區間 `[0, 3)` 與 `[3, 5)`：

```text
第一群組：idx[0..3] = [0, 2, 1]
排序群組索引後：[0, 1, 2]
放入值：[1, 3, 5]

第二群組：idx[3..5] = [4, 3]
排序群組索引後：[3, 4]
放入值：[8, 9]
```

重建結果同樣是：

```text
[1, 3, 5, 8, 9]
```

### 複雜度

- 時間複雜度：`O(n log n)`。索引排序與各群組內的索引排序合計仍為 `O(n log n)`。
- 空間複雜度：`O(n)`。需要索引陣列、群組切片與答案陣列。

## 兩種解法比較

| 比較項目 | 解法一 | 解法二 |
|---|---|---|
| 排序對象 | `(value, originalIndex)` Tuple | 原始索引 `idx` |
| 值的取得方式 | 排序後直接保存到 `values` | 透過 `nums[idx[k]]` 取得 |
| 群組索引 | 使用 `groupIndices` | 使用索引切片 `idx[i..j]` |
| 核心觀察 | 排序值並保留位置 | 排序位置的索引參考 |
| 時間複雜度 | `O(n log n)` | `O(n log n)` |
| 空間複雜度 | `O(n)` | `O(n)` |

兩種解法的數學觀察相同，差異在於資料整理方式：解法一把值和索引拆開保存，解法二則讓索引陣列作為排序後的間接存取順序。

## 可執行測試資料

程式進入點會依序使用 6 組固定案例驗證兩種解法，總共執行 12 次比對。每次呼叫解法前都會複製 `nums`，讓每個案例彼此獨立；輸出會列出：

- 測試解法與案例名稱
- `Input`
- `Expected`
- `Actual`
- `Result: PASS` 或 `Result: FAIL`

所有案例完成後會輸出總結。如果任一案例失敗，程式會設定非零 `Environment.ExitCode`，方便命令列或自動化流程判定失敗。

測試資料涵蓋：

1. 官方三組案例。
2. 相鄰差值剛好等於 `limit` 的連鎖連通情境。
3. 含有重複值的群組。
4. 多個互相分離的連通群組。

## 建置、執行與驗證

請在本專案根目錄 `/Users/qiuzili/Leetcode/Leetcode_folder/leetcode_2948` 執行。

### 還原與一般建置

```bash
dotnet restore leetcode_2948/leetcode_2948.csproj
dotnet build leetcode_2948/leetcode_2948.csproj --no-restore
```

### 執行固定案例

```bash
dotnet run --project leetcode_2948/leetcode_2948.csproj --no-build
```

### 嚴格 XML 文件驗證

這個命令會產生 XML 文件，並將 XML 註解格式錯誤與重複參數標籤視為錯誤：

```bash
dotnet build leetcode_2948/leetcode_2948.csproj --no-restore -t:Rebuild -p:GenerateDocumentationFile=true -warnaserror:CS1570,CS1571
```

### 格式與差異檢查

```bash
dotnet format leetcode_2948/leetcode_2948.csproj --verify-no-changes --no-restore
git diff --check
```

本專案目前沒有獨立的測試專案，因此不使用 `dotnet test`；固定案例 harness 是目前可直接執行的 smoke test。

## 實際執行輸出

以下內容來自最近一次以 `dotnet run --project leetcode_2948/leetcode_2948.csproj --no-build` 執行的完整輸出：

```text
LeetCode 2948 - Make Lexicographically Smallest Array by Swapping Elements

[解法一：排序] 官方案例一
Input: nums = [1, 5, 3, 9, 8], limit = 2
Expected: [1, 3, 5, 8, 9]
Actual: [1, 3, 5, 8, 9]
Result: PASS

[解法二：索引排序] 官方案例一
Input: nums = [1, 5, 3, 9, 8], limit = 2
Expected: [1, 3, 5, 8, 9]
Actual: [1, 3, 5, 8, 9]
Result: PASS

[解法一：排序] 官方案例二
Input: nums = [1, 7, 6, 18, 2, 1], limit = 3
Expected: [1, 6, 7, 18, 1, 2]
Actual: [1, 6, 7, 18, 1, 2]
Result: PASS

[解法二：索引排序] 官方案例二
Input: nums = [1, 7, 6, 18, 2, 1], limit = 3
Expected: [1, 6, 7, 18, 1, 2]
Actual: [1, 6, 7, 18, 1, 2]
Result: PASS

[解法一：排序] 官方案例三
Input: nums = [1, 7, 28, 19, 10], limit = 3
Expected: [1, 7, 28, 19, 10]
Actual: [1, 7, 28, 19, 10]
Result: PASS

[解法二：索引排序] 官方案例三
Input: nums = [1, 7, 28, 19, 10], limit = 3
Expected: [1, 7, 28, 19, 10]
Actual: [1, 7, 28, 19, 10]
Result: PASS

[解法一：排序] 連鎖連通
Input: nums = [10, 1, 5], limit = 5
Expected: [1, 5, 10]
Actual: [1, 5, 10]
Result: PASS

[解法二：索引排序] 連鎖連通
Input: nums = [10, 1, 5], limit = 5
Expected: [1, 5, 10]
Actual: [1, 5, 10]
Result: PASS

[解法一：排序] 重複值
Input: nums = [4, 3, 3, 1], limit = 1
Expected: [3, 3, 4, 1]
Actual: [3, 3, 4, 1]
Result: PASS

[解法二：索引排序] 重複值
Input: nums = [4, 3, 3, 1], limit = 1
Expected: [3, 3, 4, 1]
Actual: [3, 3, 4, 1]
Result: PASS

[解法一：排序] 多個群組
Input: nums = [4, 1, 7, 6, 10, 3], limit = 1
Expected: [3, 1, 6, 7, 10, 4]
Actual: [3, 1, 6, 7, 10, 4]
Result: PASS

[解法二：索引排序] 多個群組
Input: nums = [4, 1, 7, 6, 10, 3], limit = 1
Expected: [3, 1, 6, 7, 10, 4]
Actual: [3, 1, 6, 7, 10, 4]
Result: PASS

Summary: 12/12 PASS
```

## 專案結構

```text
leetcode_2948/
├── leetcode_2948/
│   ├── leetcode_2948.csproj
│   └── Program.cs
├── docs/
│   └── readme-template.md
└── README.md
```

`bin/` 與 `obj/` 是 .NET 建置產物，已由 Git 忽略，不應提交到版本庫。