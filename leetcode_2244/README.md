# LeetCode 2244 - Minimum Rounds to Complete All Tasks

完成所有任務所需的最少輪數

## 題目描述

給定一個整數陣列 `tasks`，其中 `tasks[i]` 代表第 `i` 個任務的難度等級。

每一輪中，你可以完成 **2 個** 或 **3 個** 相同難度等級的任務。

回傳完成所有任務所需的**最少輪數**，如果無法完成所有任務則回傳 `-1`。

### 範例

**範例 1：**

```text
輸入: tasks = [2,2,3,3,2,4,4,4,4,4]
輸出: 4
解釋: 
- 難度 2: 出現 3 次 → 1 輪（完成 3 個）
- 難度 3: 出現 2 次 → 1 輪（完成 2 個）
- 難度 4: 出現 5 次 → 2 輪（3 + 2）
總計: 1 + 1 + 2 = 4 輪
```

**範例 2：**

```text
輸入: tasks = [2,3,3]
輸出: -1
解釋: 難度 2 只出現 1 次，無法完成（每輪至少要處理 2 個同難度任務）
```

### 限制條件

- `1 <= tasks.length <= 10^5`
- `1 <= tasks[i] <= 10^9`

## 解題概念與出發點

### 核心觀察

1. **分組思考**：每個難度的任務必須獨立處理，不同難度的任務不能混在同一輪
2. **最小單位限制**：每輪至少要處理 2 個任務，因此若某難度只有 1 個任務，則無解
3. **貪心策略**：為了最小化輪數，應該盡可能多使用「3 個一組」的方式

### 數學推導

對於任意數量 `n ≥ 2`，我們需要找到最少的輪數使得：

- `2a + 3b = n`（其中 `a` 為使用 2 個的輪數，`b` 為使用 3 個的輪數）
- 最小化 `a + b`

關鍵洞察：

| n   | 分解方式 | 輪數 | 公式結果 ⌈n/3⌉ |
| --- | -------- | ---- | -------------- |
| 2   | 2        | 1    | 1              |
| 3   | 3        | 1    | 1              |
| 4   | 2+2      | 2    | 2              |
| 5   | 3+2      | 2    | 2              |
| 6   | 3+3      | 2    | 2              |
| 7   | 3+2+2    | 3    | 3              |
| 8   | 3+3+2    | 3    | 3              |
| 9   | 3+3+3    | 3    | 3              |

**結論**：對於 `n ≥ 2`，最少輪數 = `⌈n/3⌉ = (n + 2) / 3`（整數除法）

也可以表示為：

```text
輪數 = n / 3 + (n % 3 != 0 ? 1 : 0)
```

## 解法詳解

### 解法一：LINQ GroupBy（MinimumRounds）

```csharp
public int MinimumRounds(int[] tasks)
{
    // Step 1: 使用 LINQ 建立「難度 → 數量」的字典
    var map = tasks.GroupBy(t => t).ToDictionary(g => g.Key, g => g.Count());

    var res = 0;

    // Step 2: 遍歷每個難度等級
    foreach (var kvp in map)
    {
        // 若某難度只有 1 個任務，無法完成
        if (kvp.Value == 1)
        {
            return -1;
        }

        // 計算該難度需要的輪數：⌈count/3⌉
        res += kvp.Value / 3;
        if (kvp.Value % 3 != 0)
        {
            res++;
        }
    }

    return res;
}
```

**特點**：

- 程式碼簡潔，易於閱讀
- 使用 LINQ 的 `GroupBy` 和 `ToDictionary` 一行完成分組統計

### 解法二：傳統 Dictionary 迴圈（MinimumRounds2）

```csharp
public int MinimumRounds2(int[] tasks)
{
    // Step 1: 手動建立字典統計每個難度的出現次數
    IDictionary<int, int> map = new Dictionary<int, int>();
    foreach (int task in tasks)
    {
        if (!map.ContainsKey(task))
        {
            map[task] = 1;
        }
        else
        {
            map[task]++;
        }
    }

    int res = 0;

    // Step 2: 計算總輪數
    foreach (int v in map.Values)
    {
        if (v == 1)
        {
            return -1;
        }
        res += v / 3;
        if (v % 3 != 0)
        {
            res++;
        }
    }

    return res;
}
```

**特點**：

- 程式碼較冗長，但邏輯清晰
- 避免 LINQ 的函式呼叫開銷，效能略優

## 演示流程

以 `tasks = [2,2,3,3,2,4,4,4,4,4]` 為例：

### Step 1：統計每個難度的數量

```text
遍歷陣列後建立字典：
{
  2 → 3,  // 難度 2 出現 3 次
  3 → 2,  // 難度 3 出現 2 次
  4 → 5   // 難度 4 出現 5 次
}
```

### Step 2：計算每個難度需要的輪數

```text
難度 2（數量 = 3）：
  - 3 / 3 = 1 輪
  - 3 % 3 = 0（無餘數）
  - 結果：1 輪

難度 3（數量 = 2）：
  - 2 / 3 = 0 輪
  - 2 % 3 = 2 ≠ 0（有餘數，+1 輪）
  - 結果：1 輪

難度 4（數量 = 5）：
  - 5 / 3 = 1 輪
  - 5 % 3 = 2 ≠ 0（有餘數，+1 輪）
  - 結果：2 輪（3 + 2）
```

### Step 3：加總

```text
總輪數 = 1 + 1 + 2 = 4
```

## 兩方法比較

| 比較項目       | MinimumRounds (LINQ) | MinimumRounds2 (傳統迴圈) |
| -------------- | -------------------- | ------------------------- |
| **程式碼行數** | 較少                 | 較多                      |
| **可讀性**     | 簡潔優雅             | 直觀明確                  |
| **執行效率**   | 略低（LINQ 開銷）    | 略高                      |
| **記憶體使用** | 相似（O(k)）         | 相似（O(k)）              |
| **適用場景**   | 追求程式碼簡潔       | 追求極致效能              |
| **維護性**     | 高                   | 高                        |

### 效能分析

- **時間複雜度**：兩者皆為 O(n)，其中 n 為任務數量
- **空間複雜度**：兩者皆為 O(k)，其中 k 為不同難度等級的數量

> [!NOTE]
> 在大多數情況下，兩種方法的效能差異微乎其微。選擇時可依團隊偏好或程式碼風格決定。

## 相關連結

- [LeetCode 題目頁面（英文）](https://leetcode.com/problems/minimum-rounds-to-complete-all-tasks/description/)
- [力扣題目頁面（簡體中文）](https://leetcode.cn/problems/minimum-rounds-to-complete-all-tasks/description/)

## 執行方式

```bash
cd leetcode_2244
dotnet run
```
