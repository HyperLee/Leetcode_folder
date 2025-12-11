# LeetCode 3531 - Count Covered Buildings

> **統計被覆蓋的建築**

這是一個 LeetCode 每日題目的 C# 解決方案，解決建築覆蓋問題。

## 題目描述

給定一個正整數 `n`，代表 `n × n` 的城市。再給定一個二維陣列 `buildings`，其中 `buildings[i] = [x, y]` 表示位於座標 `[x, y]` 的唯一建築物。

如果某一棟建築物在四個方向（**左、右、上、下**）上至少各有一棟建築物，則該建築物被視為「**被覆蓋**」。

**回傳被覆蓋的建築物數量。**

### 範例

#### 範例 1

```text
輸入: n = 3, buildings = [[0,0],[1,1],[2,2]]
輸出: 1
解釋: 只有建築 [1,1] 被覆蓋，因為它的左邊有 [0,0]，右下方有 [2,2]
```

#### 範例 2

```text
輸入: n = 4, buildings = [[1,1],[1,2],[2,1],[2,2]]
輸出: 0
解釋: 所有建築都在邊界上，沒有被完全覆蓋
```

#### 範例 3

```text
輸入: n = 5, buildings = [[0,2],[1,1],[1,3],[2,0],[2,2],[2,4],[3,1],[3,3],[4,2]]
輸出: 3
解釋: [1,1], [2,2], [3,3] 這三個建築被覆蓋
```

## 解題思路

### 核心概念

被覆蓋的建築意味著該建築**不在任何邊界上**。換句話說：

- 在該建築所在的**行**中，它既不是最左邊也不是最右邊的建築
- 在該建築所在的**列**中，它既不是最上面也不是最下面的建築

### 算法步驟

1. **統計邊界值**：遍歷所有建築，記錄每一行和每一列的最大值和最小值
   - `maxRow[y]`：第 y 行的最大 x 座標
   - `minRow[y]`：第 y 行的最小 x 座標
   - `maxCol[x]`：第 x 列的最大 y 座標
   - `minCol[x]`：第 x 列的最小 y 座標

2. **檢查覆蓋條件**：對於每個建築 `(x, y)`，檢查是否滿足：
   - `x > minRow[y] && x < maxRow[y]`（不在行邊界）
   - `y > minCol[x] && y < maxCol[x]`（不在列邊界）

### 流程推演

以範例 3 為例：`n = 5, buildings = [[0,2],[1,1],[1,3],[2,0],[2,2],[2,4],[3,1],[3,3],[4,2]]`

#### 步驟 1：統計邊界值

- 第 0 行：只有 [2,0]，minRow[0] = maxRow[0] = 2
- 第 1 行：[1,1], [3,1]，minRow[1] = 1, maxRow[1] = 3
- 第 2 行：[0,2], [2,2], [4,2]，minRow[2] = 0, maxRow[2] = 4
- 第 3 行：[1,3], [3,3]，minRow[3] = 1, maxRow[3] = 3
- 第 4 行：只有 [2,4]，minRow[4] = maxRow[4] = 2

#### 步驟 2：檢查每個建築

- `[0,2]`：x=0 不滿足 x > minRow[2] (0)，不被覆蓋
- `[1,1]`：滿足 1 > minRow[1] (1) ❌，實際上 1 = 1，不被覆蓋
- `[2,2]`：滿足 2 > minRow[2] (0) 且 2 < maxRow[2] (4)，且 2 > minCol[2] (0) 且 2 < maxCol[2] (4)，**被覆蓋** ✓
- ...

> **注意**：條件必須是**嚴格大於**和**嚴格小於**，不能等於邊界值。

## 複雜度分析

- **時間複雜度**：O(buildings.length)，需要遍歷建築陣列兩次
- **空間複雜度**：O(n)，需要建立四個長度為 n+1 的陣列

## 程式碼實現

```csharp
public int CountCoveredBuildings(int n, int[][] buildings)
{
    // 建立陣列來記錄每一行和每一列的最大值和最小值
    int[] maxRow = new int[n + 1];  // 每一行的最大 x 座標
    int[] minRow = new int[n + 1];  // 每一行的最小 x 座標
    int[] maxCol = new int[n + 1];  // 每一列的最大 y 座標
    int[] minCol = new int[n + 1];  // 每一列的最小 y 座標

    // 初始化最小值為最大可能值
    Array.Fill(minRow, n + 1);
    Array.Fill(minCol, n + 1);
    
    // 第一次遍歷：統計每一行和每一列的邊界值
    foreach(var building in buildings)
    {
        int x = building[0];  // 列座標
        int y = building[1];  // 行座標
        
        // 更新第 y 行的 x 座標範圍
        maxRow[y] = Math.Max(maxRow[y], x);
        minRow[y] = Math.Min(minRow[y], x);
        
        // 更新第 x 列的 y 座標範圍
        maxCol[x] = Math.Max(maxCol[x], y);
        minCol[x] = Math.Min(minCol[x], y);
    }

    // 第二次遍歷：檢查每個建築是否被覆蓋
    int res = 0;
    foreach(var building in buildings)
    {
        int x = building[0];
        int y = building[1];
        
        // 檢查建築是否被覆蓋：
        // 1. x > minRow[y] && x < maxRow[y]：在該行中不是最左也不是最右
        // 2. y > minCol[x] && y < maxCol[x]：在該列中不是最上也不是最下
        if (x > minRow[y] && x < maxRow[y] && y > minCol[x] && y < maxCol[x])
        {
            res++;  // 該建築被覆蓋
        }
    }
    return res;
}
```

## 如何執行

1. 確保您已安裝 .NET 8.0 或更新版本
2. 複製專案到本地
3. 在終端中執行：

   ```bash
   dotnet run
   ```

程式將自動執行三個測試案例並顯示結果。

## 關鍵洞察

1. **邊界思維**：將「被覆蓋」的問題轉換為「不在邊界」的問題
2. **座標系統**：正確理解 x 代表列、y 代表行的座標對應關係
3. **條件判斷**：使用嚴格不等式確保建築不在邊界上

**座標系統**：正確理解 x 代表列、y 代表行的座標對應關係

示意圖（n = 5 範例）：

```text
x →  0     1     2     3     4
y 0  [0,0] [1,0] [2,0] [3,0] [4,0]
    1  [0,1] [1,1] [2,1] [3,1] [4,1]
    2  [0,2] [1,2] [2,2] [3,2] [4,2]
    3  [0,3] [1,3] [2,3] [3,3] [4,3]
    4  [0,4] [1,4] [2,4] [3,4] [4,4]
```

註：座標 [x,y] 中，x 是水平（列）索引，由左至右遞增；y 是垂直（行）索引，由上至下遞增。舉例來說：

- [2,0] 在最上排中間；
- [0,2] 在左邊中間；
- [4,2] 在右邊中間；
- [2,2] 在正中央。

---

**相關題目**：[LeetCode 3531 - Count Covered Buildings](https://leetcode.com/problems/count-covered-buildings/description/?envType=daily-question&envId=2025-12-11)
**參考解題文章**：
 - [力扣官方题解](https://leetcode.cn/problems/count-covered-buildings/solutions/3843942/tong-ji-bei-fu-gai-de-jian-zhu-by-leetco-x6q3/?envType=daily-question&envId=2025-12-11)
 - [灵茶山艾府](https://leetcode.cn/problems/count-covered-buildings/solutions/3663296/pai-xu-er-fen-cha-zhao-pythonjavacgo-by-z2c5d/?envType=daily-question&envId=2025-12-11)
