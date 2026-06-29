# LeetCode 228 - Summary Ranges

這個專案是 LeetCode 228「Summary Ranges / 彙總區間」的 C# 解法練習。程式會將已排序且不重複的整數陣列整理成最小的連續區間列表，並在 `Main` 中提供可直接執行的範例驗證。

## 題目說明

給定一個已排序且所有元素皆唯一的整數陣列 `nums`。範圍 `[a,b]` 表示從 `a` 到 `b` 的所有整數，且包含兩端。

請回傳最小的已排序範圍列表，使其能精確涵蓋 `nums` 中的所有數字：

- 如果範圍只有一個數字，輸出 `"a"`。
- 如果範圍包含多個連續數字，輸出 `"a->b"`。
- 每個 `nums` 元素都必須剛好被一個範圍涵蓋。
- 範圍內不能包含任何不在 `nums` 中的數字。

## 限制條件

- `0 <= nums.length <= 20`
- `-2^31 <= nums[i] <= 2^31 - 1`
- `nums` 中的所有值都唯一。
- `nums` 已依遞增順序排序。

## 解題概念與出發點

因為輸入已經排序且沒有重複元素，所以相鄰元素是否屬於同一個範圍，只需要檢查下一個值是否剛好等於目前值加一。

解題時不需要額外排序，也不需要雜湊表記錄出現過的數字。核心想法是：

1. 使用左指標 `start` 記錄目前連續區間的起點。
2. 使用右指標 `end` 往右延伸目前區間。
3. 只要 `nums[end + 1] == nums[end] + 1`，就代表區間仍然連續。
4. 一旦遇到斷點或到達陣列尾端，就輸出目前區間。
5. 將 `start` 移到下一個尚未處理的位置，繼續尋找下一段範圍。

程式在連續性判斷中使用 `(long)nums[end] + 1L`，避免在整數邊界附近進行 `int` 加法時產生溢位風險。

## 解法設計：雙指標掃描

主要方法是 `SummaryRanges(int[] nums)`，它會回傳 `IList<string>`。

### 設計流程

1. 初始化 `start = 0`，表示第一段範圍從索引 `0` 開始。
2. 在每一輪中，令 `end = start`。
3. 當右側還有元素，且 `nums[end + 1]` 正好等於 `nums[end] + 1` 時，持續遞增 `end`。
4. 迴圈停止時，`nums[start]` 到 `nums[end]` 就是一段最長連續區間。
5. 呼叫 `BuildRange(nums[start], nums[end])` 產生輸出字串：
   - 如果起點等於終點，輸出單一數字，例如 `"7"`。
   - 如果起點不同於終點，輸出範圍，例如 `"0->2"`。
6. 設定 `start = end + 1`，處理下一段尚未彙總的數字。

### 為什麼這樣可以得到最小範圍列表

每次從 `start` 開始都會把能延伸的連續數字全部納入同一段，直到下一個數字不連續才停止。因此每一段都是目前起點能形成的最長合法區間。所有區間依照原陣列順序輸出，所以結果自然保持排序，且不會遺漏或重複涵蓋任何元素。

### 複雜度

- 時間複雜度：`O(n)`，每個元素最多被右指標掃描一次。
- 空間複雜度：`O(1)` 額外空間；不計入輸出列表本身。

## 範例演示流程

### 範例 1

輸入：

```text
[0, 1, 2, 4, 5, 7]
```

流程：

1. `0, 1, 2` 連續，形成 `"0->2"`。
2. `4, 5` 連續，形成 `"4->5"`。
3. `7` 沒有連續鄰居，形成 `"7"`。

輸出：

```text
["0->2", "4->5", "7"]
```

### 範例 2

輸入：

```text
[0, 2, 3, 4, 6, 8, 9]
```

流程：

1. `0` 與 `2` 不連續，形成 `"0"`。
2. `2, 3, 4` 連續，形成 `"2->4"`。
3. `6` 與 `8` 不連續，形成 `"6"`。
4. `8, 9` 連續，形成 `"8->9"`。

輸出：

```text
["0", "2->4", "6", "8->9"]
```

### 邊界案例

- 空陣列會輸出空列表 `[]`。
- 單一元素會輸出單點範圍，例如 `[5]` 對應 `["5"]`。
- 負數與跨越 `0` 的連續段可用同樣邏輯處理。
- `int.MinValue` 附近的連續值也能正確彙總。

## 專案結構

```text
.
├── docs/
│   └── readme-template.md
├── leetcode_228/
│   ├── Program.cs
│   └── leetcode_228.csproj
└── README.md
```

## 建置、執行與測試

請從 repository 根目錄執行：

```bash
dotnet build leetcode_228/leetcode_228.csproj --nologo
dotnet run --project leetcode_228/leetcode_228.csproj
dotnet test leetcode_228/leetcode_228.csproj --nologo
git diff --check
```

> [!NOTE]
> 目前此 repository 沒有獨立的 xUnit/NUnit/MSTest 測試專案；`dotnet test leetcode_228/leetcode_228.csproj --nologo` 主要用來確認專案可還原與建置。實際案例驗證由 `Main` 中的固定範例輸出提供。

## 範例執行輸出

以下輸出來自：

```bash
dotnet run --project leetcode_228/leetcode_228.csproj
```

```text
Summary Ranges sample verification

Case 1: LeetCode 範例 1：多段連續區間
Input: [0, 1, 2, 4, 5, 7]
Expected: ["0->2", "4->5", "7"]
Actual: ["0->2", "4->5", "7"]
Result: PASS

Case 2: LeetCode 範例 2：單點與短區間交錯
Input: [0, 2, 3, 4, 6, 8, 9]
Expected: ["0", "2->4", "6", "8->9"]
Actual: ["0", "2->4", "6", "8->9"]
Result: PASS

Case 3: 空陣列：沒有任何範圍
Input: []
Expected: []
Actual: []
Result: PASS

Case 4: 單一元素：輸出單點
Input: [5]
Expected: ["5"]
Actual: ["5"]
Result: PASS

Case 5: 負數區間：跨越 0 的連續段
Input: [-3, -2, -1, 1, 2, 4]
Expected: ["-3->-1", "1->2", "4"]
Actual: ["-3->-1", "1->2", "4"]
Result: PASS

Case 6: int.MinValue 邊界：仍可正確串接連續值
Input: [-2147483648, -2147483647, -1, 0, 2]
Expected: ["-2147483648->-2147483647", "-1->0", "2"]
Actual: ["-2147483648->-2147483647", "-1->0", "2"]
Result: PASS

Passed 6/6 cases.
```
