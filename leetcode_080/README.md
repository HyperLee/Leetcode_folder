# LeetCode 80 - Remove Duplicates from Sorted Array II

這個專案是 LeetCode 80「刪除排序陣列中的重複項 II」的 C# console 解法整理，目標是在不配置額外結果陣列的前提下，原地修改已排序陣列，讓每個不同數字最多保留兩次。

目前 `Program.cs` 保留三種解法，並在 `Main` 中提供可直接執行的範例資料，方便用 `dotnet run` 檢查每個解法的有效長度與陣列前綴內容。

## 題目說明

給定一個非遞減排序的整數陣列 `nums`，請原地移除多餘的重複元素，使每個不同元素最多出現兩次。元素的相對順序必須維持不變。

因為題目要求原地修改陣列，所以不需要真的縮短陣列長度。假設處理後的有效長度是 `k`，只要確保：

- `nums[0]` 到 `nums[k - 1]` 是符合條件的結果。
- 每個不同數字在有效前綴中最多出現兩次。
- `k` 之後的內容不影響判題。
- 方法最後回傳 `k`。

## 限制條件

- `nums` 已依非遞減順序排序。
- 必須原地修改 `nums`。
- 額外空間複雜度需為 `O(1)`。
- 不需要處理有效長度 `k` 之後的元素。
- 本專案的三種解法時間複雜度皆為 `O(n)`，空間複雜度皆為 `O(1)`。

## 解題概念與出發點

題目最重要的條件是「陣列已排序」。相同數字一定會連續出現，因此只要判斷目前候選值是否會造成第三個相同數字被寫入即可。

三種解法其實都建立在同一個核心判斷上：

```text
若目前候選值 == 已保留結果中倒數第 2 個值
代表它會成為第三個相同元素，必須跳過。

若目前候選值 != 已保留結果中倒數第 2 個值
代表寫入後仍符合最多保留兩次，可以保留。
```

差異只在於如何描述「已保留結果」：

- `RemoveDuplicates`：抽象成通用的「每個數字最多保留 k 次」。
- `RemoveDuplicates2`：用 `slow` / `fast` 雙指針描述讀寫位置。
- `RemoveDuplicates3`：把陣列前段視為一個原地棧。

## 解法一：通用保留 k 次寫入指標

方法：`RemoveDuplicates(int[] nums)`，內部呼叫 `Process(nums, 2)`。

這個解法把題目推廣成「每個數字最多保留 `maxOccurrences` 次」。對 LeetCode 80 來說，`maxOccurrences = 2`。

設 `writeIndex` 為下一個可以寫入結果的位置：

- 當 `writeIndex < maxOccurrences` 時，前幾個元素可以直接保留。
- 後續每個候選值 `value` 都和 `nums[writeIndex - maxOccurrences]` 比較。
- 如果兩者不同，代表有效結果中距離目前寫入點兩格之前的值不是 `value`，所以寫入 `value` 不會形成第三個重複值。
- 如果兩者相同，代表有效結果中已經有兩個相同值，候選值必須跳過。

範例：`nums = [1, 1, 1, 2, 2, 3]`，最多保留 `2` 次。

| 候選值 | writeIndex | 比較對象 | 動作 | 有效前綴 |
| --- | ---: | --- | --- | --- |
| 1 | 0 | 無 | 前兩個直接保留 | `[1]` |
| 1 | 1 | 無 | 前兩個直接保留 | `[1, 1]` |
| 1 | 2 | `nums[0] = 1` | 相同，跳過 | `[1, 1]` |
| 2 | 2 | `nums[0] = 1` | 不同，寫入 | `[1, 1, 2]` |
| 2 | 3 | `nums[1] = 1` | 不同，寫入 | `[1, 1, 2, 2]` |
| 3 | 4 | `nums[2] = 2` | 不同，寫入 | `[1, 1, 2, 2, 3]` |

最後回傳 `k = 5`。

## 解法二：雙指針

方法：`RemoveDuplicates2(int[] nums)`。

這是官方常見寫法，使用兩個指針：

- `fast`：掃描原始陣列的讀取位置。
- `slow`：下一個可寫入結果的位置，也等於目前有效結果長度。

因為前兩個元素一定可以保留，所以當陣列長度大於 2 時，`slow` 和 `fast` 都從 `2` 開始。每次檢查 `nums[fast]` 是否等於 `nums[slow - 2]`：

- 相同：候選值會造成第三次重複，跳過。
- 不同：把 `nums[fast]` 寫到 `nums[slow]`，再將 `slow` 往後移。

範例：`nums = [0, 0, 1, 1, 1, 1, 2, 3, 3]`。

| fast | 候選值 | slow | 比較 `nums[slow - 2]` | 動作 | 有效前綴 |
| ---: | ---: | ---: | ---: | --- | --- |
| 2 | 1 | 2 | 0 | 寫入 | `[0, 0, 1]` |
| 3 | 1 | 3 | 0 | 寫入 | `[0, 0, 1, 1]` |
| 4 | 1 | 4 | 1 | 跳過 | `[0, 0, 1, 1]` |
| 5 | 1 | 4 | 1 | 跳過 | `[0, 0, 1, 1]` |
| 6 | 2 | 4 | 1 | 寫入 | `[0, 0, 1, 1, 2]` |
| 7 | 3 | 5 | 1 | 寫入 | `[0, 0, 1, 1, 2, 3]` |
| 8 | 3 | 6 | 2 | 寫入 | `[0, 0, 1, 1, 2, 3, 3]` |

最後回傳 `k = 7`。

## 解法三：原地陣列當棧

方法：`RemoveDuplicates3(int[] nums)`。

這個解法把 `nums` 的前段視為一個棧，`stackSize` 表示目前棧大小，也就是有效結果長度。

處理方式：

- 長度小於等於 2 時，所有元素都可以保留，直接回傳原長度。
- 從索引 `2` 開始掃描候選值。
- 若 `nums[i] != nums[stackSize - 2]`，代表候選值可以入棧。
- 入棧時將 `nums[stackSize] = nums[i]`，再遞增 `stackSize`。

範例：`nums = [1, 1, 1, 2, 2, 3]`。

| i | 候選值 | stackSize | 棧頂下方第二格 | 動作 | 有效前綴 |
| ---: | ---: | ---: | ---: | --- | --- |
| 2 | 1 | 2 | 1 | 相同，跳過 | `[1, 1]` |
| 3 | 2 | 2 | 1 | 不同，入棧 | `[1, 1, 2]` |
| 4 | 2 | 3 | 1 | 不同，入棧 | `[1, 1, 2, 2]` |
| 5 | 3 | 4 | 2 | 不同，入棧 | `[1, 1, 2, 2, 3]` |

最後回傳 `stackSize = 5`。

## 專案結構

```text
leetcode_080/
├── docs/
│   └── readme-template.md
├── leetcode_080/
│   ├── Program.cs
│   └── leetcode_080.csproj
└── README.md
```

## 建置與執行

從本專案根目錄執行：

```bash
dotnet build leetcode_080/leetcode_080.csproj
```

成功時會看到 `建置成功`，並顯示 `0 個警告`、`0 個錯誤`。

執行範例資料：

```bash
dotnet run --project leetcode_080/leetcode_080.csproj
```

應用程式輸出會列出三個解法與三筆範例資料，每筆都應為 `PASS`：

```text
LeetCode 80 - Remove Duplicates from Sorted Array II

RemoveDuplicates:
  PASS case 1: input=[1, 1, 1, 2, 2, 3], k=5, result=[1, 1, 2, 2, 3], expected=[1, 1, 2, 2, 3]
  PASS case 2: input=[0, 0, 1, 1, 1, 1, 2, 3, 3], k=7, result=[0, 0, 1, 1, 2, 3, 3], expected=[0, 0, 1, 1, 2, 3, 3]
  PASS case 3: input=[1], k=1, result=[1], expected=[1]

RemoveDuplicates2:
  PASS case 1: input=[1, 1, 1, 2, 2, 3], k=5, result=[1, 1, 2, 2, 3], expected=[1, 1, 2, 2, 3]
  PASS case 2: input=[0, 0, 1, 1, 1, 1, 2, 3, 3], k=7, result=[0, 0, 1, 1, 2, 3, 3], expected=[0, 0, 1, 1, 2, 3, 3]
  PASS case 3: input=[1], k=1, result=[1], expected=[1]

RemoveDuplicates3:
  PASS case 1: input=[1, 1, 1, 2, 2, 3], k=5, result=[1, 1, 2, 2, 3], expected=[1, 1, 2, 2, 3]
  PASS case 2: input=[0, 0, 1, 1, 1, 1, 2, 3, 3], k=7, result=[0, 0, 1, 1, 2, 3, 3], expected=[0, 0, 1, 1, 2, 3, 3]
  PASS case 3: input=[1], k=1, result=[1], expected=[1]
```

> [!NOTE]
> 在目前本機環境，`.NET CLI` 可能會在建置或執行前額外印出 `CSSM_ModuleLoad(): One or more parameters passed to a function were not valid.`。這不是本專案輸出的內容；範例程式輸出從 `LeetCode 80 - Remove Duplicates from Sorted Array II` 開始。

## 測試狀態

目前沒有獨立測試專案。可用以下方式驗證：

```bash
dotnet build leetcode_080/leetcode_080.csproj
dotnet run --project leetcode_080/leetcode_080.csproj
git diff --check
```

`dotnet run` 會檢查 `Main` 中的固定範例資料；`git diff --check` 用於確認沒有多餘空白或換行問題。
