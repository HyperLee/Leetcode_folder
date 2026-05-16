# LeetCode 35：搜索插入位置

這個專案是 LeetCode 35「Search Insert Position / 搜索插入位置」的 C# console 實作，使用二分查找在 `O(log n)` 時間內找出目標值索引，或目標值不存在時應插入的排序位置。

## 題目說明

給定一個由不重複整數組成、且已依遞增順序排序的陣列 `nums`，以及整數 `target`：

- 如果 `target` 存在於 `nums`，回傳它的索引。
- 如果 `target` 不存在，回傳它依照排序順序應該插入的位置。
- 演算法時間複雜度必須是 `O(log n)`。

## 限制條件

LeetCode 原題限制如下：

- `1 <= nums.length <= 10^4`
- `-10^4 <= nums[i] <= 10^4`
- `nums` 以遞增順序排序，且所有元素不重複。
- `-10^4 <= target <= 10^4`

目前實作也能自然處理空陣列，空陣列會回傳插入位置 `0`。

## 解題概念與出發點

插入位置 `pos` 需要滿足：

```text
nums[pos - 1] < target <= nums[pos]
```

如果 `target` 已存在，回傳的索引也正是第一個大於等於 `target` 的位置。因此問題可以轉換成：

> 在排序陣列中，找出第一個 `>= target` 的下標。

這就是 lower bound 形式的二分查找。實作中先將答案預設為 `nums.Length`，代表 `target` 大於所有元素時要插入到陣列尾端。每次找到 `nums[mid] >= target` 時，就先記錄 `mid`，再往左半邊繼續搜尋更小的可行位置。

## 方法一：二分查找

設計重點：

- `left` 與 `right` 維護尚未排除的搜尋範圍。
- `mid = left + (right - left) / 2` 避免索引加總溢位。
- 當 `nums[mid] >= target`，`mid` 是目前可行答案，記錄後縮小右邊界。
- 當 `nums[mid] < target`，插入位置一定在右半邊，移動左邊界。
- 迴圈結束後回傳記錄到的 `answer`。

時間複雜度為 `O(log n)`，空間複雜度為 `O(1)`。

### 範例演示流程

以 `nums = [1, 3, 5, 6]`、`target = 2` 為例：

| 步驟 | left | right | mid | nums[mid] | 判斷 | answer |
| --- | ---: | ---: | ---: | ---: | --- | ---: |
| 1 | 0 | 3 | 1 | 3 | `3 >= 2`，記錄 1，往左找 | 1 |
| 2 | 0 | 0 | 0 | 1 | `1 < 2`，往右找 | 1 |

搜尋結束時 `answer = 1`，因此 `2` 應插入索引 `1`，陣列可維持排序。

## 專案結構

```text
.
├── docs/readme-template.md
├── leetcode_035/Program.cs
└── leetcode_035/leetcode_035.csproj
```

## 建置與執行

從專案根目錄執行：

```bash
dotnet build leetcode_035/leetcode_035.csproj
```

執行內建範例資料：

```bash
dotnet run --project leetcode_035/leetcode_035.csproj
```

範例輸出：

```text
nums = [1, 3, 5, 6], target = 5, expected = 2, actual = 2 => PASS
nums = [1, 3, 5, 6], target = 2, expected = 1, actual = 1 => PASS
nums = [1, 3, 5, 6], target = 0, expected = 0, actual = 0 => PASS
nums = [1, 3, 5, 6], target = 7, expected = 4, actual = 4 => PASS
nums = [1], target = 0, expected = 0, actual = 0 => PASS
nums = [], target = 8, expected = 0, actual = 0 => PASS
```

> [!NOTE]
> 在目前 macOS/.NET 環境中，`dotnet build` 或 `dotnet run` 可能會先輸出 `CSSM_ModuleLoad(): One or more parameters passed to a function were not valid.`。這是環境訊息，不是此程式的範例輸出。

## 驗證指令

本專案目前沒有獨立測試專案；使用 console 範例與建置結果驗證：

```bash
dotnet build leetcode_035/leetcode_035.csproj
dotnet run --project leetcode_035/leetcode_035.csproj
git diff --check
```
