# LeetCode 27 - Remove Element

以 C# / .NET 10 實作 LeetCode 第 27 題 `Remove Element`。專案維持單一 console app，`Main` 內建題目範例與邊界案例，可直接用 `dotnet run` 觀察雙指標解法如何原地壓縮陣列。

## 題目說明

給定整數陣列 `nums` 與整數 `val`，請原地移除所有等於 `val` 的元素，並回傳移除後有效元素的數量 `k`。題目只要求 `nums` 前 `k` 個元素正確，其餘位置的值與陣列尾端內容不重要。

- LeetCode: <https://leetcode.com/problems/remove-element/description/>
- LeetCode CN: <https://leetcode.cn/problems/remove-element/description/>

## 限制條件

- `0 <= nums.length <= 100`
- `0 <= nums[i] <= 50`
- `0 <= val <= 100`

## 解題概念與出發點

題目要求「原地」移除元素，代表不能把答案建立在另一個正式結果陣列中。另一方面，題目不要求保留被移除元素之後的內容，因此真正要維護的只有前段有效區間。

這讓解法可以聚焦在一件事：掃描整個陣列，遇到要保留的元素就往前覆寫。最後有效區間的長度，就是要回傳的答案 `k`。

## 解法一：雙指標原地覆寫

### 設計說明

使用兩個指標：

- `right` 從左到右掃描每一個元素。
- `left` 指向下一個可以放入「非 `val` 元素」的位置。

當 `nums[right] != val` 時，就把 `nums[right]` 覆寫到 `nums[left]`，接著讓 `left` 往前走一步。這樣整趟掃描結束後，陣列前 `left` 個位置就會全部是保留下來的值，而 `left` 本身就是新長度 `k`。

- 時間複雜度：`O(n)`
- 空間複雜度：`O(1)`

### 範例演示流程

#### 範例 1：`nums = [3, 2, 2, 3]`，`val = 3`

1. 初始狀態：`left = 0`，`right` 由左到右掃描。
2. `right = 0` 時，`nums[right] = 3`，等於 `val`，略過不寫入。
3. `right = 1` 時，`nums[right] = 2`，覆寫到 `nums[left]`，陣列前段變成 `[2, ...]`，`left = 1`。
4. `right = 2` 時，`nums[right] = 2`，覆寫到 `nums[left]`，陣列前段變成 `[2, 2, ...]`，`left = 2`。
5. `right = 3` 時，`nums[right] = 3`，等於 `val`，略過不寫入。
6. 掃描完成後回傳 `k = 2`，前 `2` 個有效元素為 `[2, 2]`。

#### 範例 2：`nums = [0, 1, 2, 2, 3, 0, 4, 2]`，`val = 2`

1. 初始狀態：`left = 0`。
2. 依序保留 `0`、`1`，此時前段有效區間為 `[0, 1]`，`left = 2`。
3. 遇到兩個 `2` 時直接略過，不擴張有效區間。
4. 繼續掃描到 `3`、`0`、`4` 時，依序覆寫到前段，形成 `[0, 1, 3, 0, 4]`。
5. 掃描完成後回傳 `k = 5`，前 `5` 個有效元素為 `[0, 1, 3, 0, 4]`。

### 邊界案例

目前 `Main` 也包含以下可直接執行的案例：

- 空陣列：驗證 `nums.Length == 0` 時直接回傳 `0`。
- 全部移除：驗證所有元素都等於 `val` 時，結果長度為 `0`。
- 完全保留：驗證沒有任何元素等於 `val` 時，`k` 會等於原陣列長度。

## 執行方式

```bash
dotnet build leetcode_027/leetcode_027.csproj
dotnet run --project leetcode_027/leetcode_027.csproj
```

> [!NOTE]
> 目前專案沒有獨立的測試專案，驗證方式以 `dotnet build` 與 `dotnet run` 內建案例輸出為主。

## 範例執行輸出

以下輸出會在完成建置後由 `dotnet run --project leetcode_027/leetcode_027.csproj` 產生：

```text
LeetCode 27 - Remove Element
--------------------------------------------------
[題目範例 1]
輸入陣列: [3, 2, 2, 3], val = 3
移除後長度 k = 2
前 k 個有效元素: [2, 2]
處理後完整陣列: [2, 2, 2, 3]

[題目範例 2]
輸入陣列: [0, 1, 2, 2, 3, 0, 4, 2], val = 2
移除後長度 k = 5
前 k 個有效元素: [0, 1, 3, 0, 4]
處理後完整陣列: [0, 1, 3, 0, 4, 0, 4, 2]

[邊界案例：空陣列]
輸入陣列: [], val = 1
移除後長度 k = 0
前 k 個有效元素: []
處理後完整陣列: []

[邊界案例：全部移除]
輸入陣列: [1, 1, 1, 1], val = 1
移除後長度 k = 0
前 k 個有效元素: []
處理後完整陣列: [1, 1, 1, 1]

[邊界案例：完全保留]
輸入陣列: [4, 5, 6], val = 3
移除後長度 k = 3
前 k 個有效元素: [4, 5, 6]
處理後完整陣列: [4, 5, 6]
```

## 專案結構

- [`leetcode_027/Program.cs`](leetcode_027/Program.cs)：題目入口、雙指標解法與可執行案例。
- [`docs/readme-template.md`](docs/readme-template.md)：本題 README 的撰寫模板參考。
