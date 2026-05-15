# LeetCode 026 - Remove Duplicates from Sorted Array

使用 .NET Console App 示範 LeetCode 26 的兩種雙指標解法，並在 `Main` 中提供可直接執行的測試資料，方便觀察原地去重後的 `k` 與陣列前綴結果。

## 題目說明

- 題目連結: <https://leetcode.com/problems/remove-duplicates-from-sorted-array/description/>
- 給定一個依非遞減順序排序的整數陣列 `nums`，需要原地移除重複元素，讓每個唯一值只保留一次。
- 回傳唯一元素數量 `k`，並保證 `nums` 的前 `k` 個位置就是答案，其餘位置可以忽略。

## 限制條件

- `1 <= nums.length <= 3 * 10^4`
- `-100 <= nums[i] <= 100`
- `nums` 已依非遞減順序排序

> [!NOTE]
> `Program.cs` 額外示範了空陣列 `[]` 的 defensive case，方便本地驗證邊界行為；這不是 LeetCode 原題保證的輸入條件。

## 解題概念與出發點

因為陣列已排序，相同數字一定會連續出現，所以不需要真的刪除元素，只要把「新的唯一值」往陣列前段覆寫即可。核心觀念如下：

1. 用一個指標掃描整個陣列，找出和前一個值不同的新元素。
2. 用另一個指標記錄「下一個唯一值應該寫回的位置」。
3. 掃描結束後，前段區間就是答案，回傳其長度 `k`。

兩個解法的時間複雜度都是 `O(n)`，空間複雜度都是 `O(1)`。

## 解法一: 雙指標 `while`

### 設計說明

- `fast` 負責從左到右掃描整個陣列。
- `slow` 指向下一個要寫入唯一值的位置。
- 當 `nums[fast] != nums[fast - 1]` 時，代表找到新的唯一值，將它寫到 `nums[slow]`，再把 `slow` 往前推進。

### 範例演示流程

以 `nums = [1, 1, 2]` 為例：

| 步驟 | `fast` | 判斷 | 動作 | 有效前綴 |
| --- | --- | --- | --- | --- |
| 初始 | `1` | - | `slow = 1` | `[1]` |
| 1 | `1` | `nums[1] == nums[0]` | 重複值，略過 | `[1]` |
| 2 | `2` | `nums[2] != nums[1]` | `nums[1] = 2`，`slow = 2` | `[1, 2]` |

最後回傳 `k = 2`，陣列前 `2` 個元素為 `[1, 2]`。

## 解法二: 雙指標 `for`

### 設計說明

- `right` 負責用 `for` 迴圈掃描整個陣列。
- `left` 指向下一個要被唯一值覆寫的位置。
- 邏輯和解法一相同，只是把掃描流程改成 `for` 寫法，讓索引遞增更集中在迴圈宣告中。

### 範例演示流程

同樣以 `nums = [1, 1, 2]` 為例：

| 步驟 | `right` | 判斷 | 動作 | 有效前綴 |
| --- | --- | --- | --- | --- |
| 初始 | `1` | - | `left = 1` | `[1]` |
| 1 | `1` | `nums[1] == nums[0]` | 重複值，略過 | `[1]` |
| 2 | `2` | `nums[2] != nums[1]` | `nums[1] = 2`，`left = 2` | `[1, 2]` |

最後回傳 `k = 2`，陣列前 `2` 個元素為 `[1, 2]`。

## 範例執行

### 建置

```bash
dotnet build leetcode_026/leetcode_026.csproj
```

預期結果：

```text
建置成功。
    0 個警告
    0 個錯誤
```

### 執行 Demo

```bash
dotnet run --project leetcode_026/leetcode_026.csproj
```

輸出節錄：

```text
LeetCode 026 - Remove Duplicates from Sorted Array

=== Example 1 ===
解法一 - 雙指標 while
Input: [1, 1, 2]
k = 2
Unique prefix: [1, 2]
Array after in-place update: [1, 2, 2]
解法二 - 雙指標 for
Input: [1, 1, 2]
k = 2
Unique prefix: [1, 2]
Array after in-place update: [1, 2, 2]

=== Example 2 ===
解法一 - 雙指標 while
Input: [0, 0, 1, 1, 1, 2, 2, 3, 3, 4]
k = 5
Unique prefix: [0, 1, 2, 3, 4]
Array after in-place update: [0, 1, 2, 3, 4, 2, 2, 3, 3, 4]
解法二 - 雙指標 for
Input: [0, 0, 1, 1, 1, 2, 2, 3, 3, 4]
k = 5
Unique prefix: [0, 1, 2, 3, 4]
Array after in-place update: [0, 1, 2, 3, 4, 2, 2, 3, 3, 4]
```

完整輸出還會依序展示：

- `Single Element`
- `All Duplicates`
- `Defensive Empty Array`

## 專案結構

```text
.
├─ leetcode_026/
│  ├─ Program.cs
│  └─ leetcode_026.csproj
└─ docs/
   └─ readme-template.md
```
