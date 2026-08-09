# LeetCode 75 — 顏色分類（Sort Colors）

這是一個使用 C# 與 .NET 10 實作的主控台教學專案。專案保留最符合題目進階要求的荷蘭國旗三指標解法：一次分類、常數額外空間，並直接修改輸入陣列。

## 題目說明

給定一個只包含 `0`、`1`、`2` 的整數陣列 `nums`，其中：

- `0` 代表紅色。
- `1` 代表白色。
- `2` 代表藍色。

請直接修改原陣列，使相同顏色彼此相鄰，並按照 `0、1、2` 的順序排列。不可呼叫內建排序函式。

限制條件：

- `n == nums.Length`
- `1 <= n <= 300`
- `nums[i]` 只能是 `0`、`1` 或 `2`

- [LeetCode 英文題目](https://leetcode.com/problems/sort-colors/)
- [LeetCode 中文題目](https://leetcode.cn/problems/sort-colors/)

## 解題核心：荷蘭國旗三指標

`SortColors` 使用三個指標，把陣列同時分成已分類與尚未分類的區域：

- `low`：下一個 `0` 應放置的位置。
- `mid`：目前正在分類的位置。
- `high`：下一個 `2` 應放置的位置。

每一步只處理 `nums[mid]`，並根據它是 `0`、`1` 或 `2` 更新對應邊界。方法不會建立排序結果陣列，而是直接修改呼叫端傳入的 `nums`。

## 區間不變量

每次進入迴圈時都維持以下四個區間：

```text
[0, low)       全部是 0
[low, mid)     全部是 1
[mid, high]    尚未分類
(high, n)      全部是 2
```

只要 `mid <= high`，尚未分類區間就仍有元素需要處理。當 `mid > high` 時，未分類區間為空，整個陣列便已依 `0、1、2` 排列完成。

## 演算法流程

### `nums[mid] == 0`

將目前元素與 `nums[low]` 交換。交換後左側多了一個確定的 `0`，因此 `low` 與 `mid` 都向右移動。

### `nums[mid] == 1`

目前元素已位於中間區段，不需要交換，只要讓 `mid` 向右移動。

### `nums[mid] == 2`

將目前元素與 `nums[high]` 交換，再讓 `high` 向左移動。右側換回來的元素尚未分類，所以 `mid` 必須留在原位，下一輪重新判斷。

以 `[2,0,2,1,1,0]` 為例：

```text
初始：[2,0,2,1,1,0]，low = 0，mid = 0，high = 5
遇到 2：與 high 交換 → [0,0,2,1,1,2]，high = 4
遇到 0：與 low 交換  → [0,0,2,1,1,2]，low = 1，mid = 1
遇到 0：與 low 交換  → [0,0,2,1,1,2]，low = 2，mid = 2
遇到 2：與 high 交換 → [0,0,1,1,2,2]，high = 3
兩個 1 依序略過，排序完成
```

## 正確性說明

迴圈開始前，四個區間的已分類部分都是空集合，因此不變量成立。每一輪會執行下列其中一項：

- 把 `0` 移入左側的 `0` 區間。
- 把 `1` 納入中間的 `1` 區間。
- 把 `2` 移入右側的 `2` 區間。

每種操作都不會破壞已分類區間，並且讓未分類區間至少縮小一格。迴圈結束時未分類區間為空，因此所有元素都已位於正確區段，陣列必然完成排序。

## 複雜度

- 時間複雜度：`O(n)`。每個元素只會被有限次數檢查或交換。
- 額外空間複雜度：`O(1)`。只使用三個指標與交換操作。

## 專案結構

```text
leetcode_075/
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_075.sln
└── leetcode_075/
    ├── leetcode_075.csproj
    └── Program.cs
```

- `Program.cs`：荷蘭國旗解法與八組 deterministic 驗證案例。
- `leetcode_075.csproj`：以 `net10.0` 為目標框架的主控台專案。

## 建置與執行

請從 `leetcode_075` 專案根目錄執行：

```powershell
dotnet restore leetcode_075/leetcode_075.csproj
dotnet build leetcode_075/leetcode_075.csproj --no-restore --nologo
dotnet run --project leetcode_075/leetcode_075.csproj --no-build
```

專案目前沒有獨立測試專案。`Main` 會複製每組輸入後呼叫 `SortColors`，避免原地排序改變測試資料；任何案例失敗時，程式會設定非零結束碼。

## 測試案例與實際輸出

八組案例涵蓋官方範例、最小長度、已排序、反向排列、全部相同、只含兩色，以及從右側換回尚未分類值的關鍵分支。

最新實際輸出：

```text
官方範例 1 | Input: [2,0,2,1,1,0] | Expected: [0,0,1,1,2,2] | Actual: [0,0,1,1,2,2] | PASS
官方範例 2 | Input: [2,0,1] | Expected: [0,1,2] | Actual: [0,1,2] | PASS
單一元素 | Input: [1] | Expected: [1] | Actual: [1] | PASS
已排序 | Input: [0,0,1,1,2,2] | Expected: [0,0,1,1,2,2] | Actual: [0,0,1,1,2,2] | PASS
反向排列 | Input: [2,2,1,1,0,0] | Expected: [0,0,1,1,2,2] | Actual: [0,0,1,1,2,2] | PASS
全部相同 | Input: [2,2,2] | Expected: [2,2,2] | Actual: [2,2,2] | PASS
只含兩色 | Input: [2,0,2,0] | Expected: [0,0,2,2] | Actual: [0,0,2,2] | PASS
右側換回未分類值 | Input: [2,2,0,1,0] | Expected: [0,0,1,2,2] | Actual: [0,0,1,2,2] | PASS

Overall: 8/8 passed.
```
