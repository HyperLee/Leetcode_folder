# LeetCode 3379 - Transformed Array

[![LeetCode](https://img.shields.io/badge/LeetCode-3379-FFA116?style=flat&logo=leetcode&logoColor=white)](https://leetcode.com/problems/transformed-array/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Easy-5CB85C?style=flat)]()
[![Language](https://img.shields.io/badge/Language-C%23-239120?style=flat&logo=csharp&logoColor=white)]()

> 環狀陣列的索引轉換問題 - 使用模運算處理環狀結構與負數索引

## 題目說明

給定一個整數陣列 `nums`，將其視為**環狀陣列**（circular array）。請建立一個相同大小的陣列 `result`，對於每個索引 `i`（`0 <= i < nums.Length`），按照以下規則獨立計算：

- **若 `nums[i] > 0`**：從索引 `i` 向**右**移動 `nums[i]` 步（環狀），並將 `result[i]` 設為落腳索引處的值
- **若 `nums[i] < 0`**：從索引 `i` 向**左**移動 `|nums[i]|` 步（環狀），並將 `result[i]` 設為落腳索引處的值
- **若 `nums[i] == 0`**：將 `result[i]` 設為 `nums[i]`（即 `0`）

回傳新的陣列 `result`。

### 範例

**範例 1：**
```
輸入：nums = [3,-2,1,1]
輸出：[1,1,1,3]

解釋：
- 對於 nums[0] = 3：從索引 0 向右移動 3 步 → (0 + 3) % 4 = 3 → result[0] = nums[3] = 1
- 對於 nums[1] = -2：從索引 1 向左移動 2 步 → (1 - 2) % 4 = -1 → (-1 + 4) % 4 = 3 → result[1] = nums[3] = 1
- 對於 nums[2] = 1：從索引 2 向右移動 1 步 → (2 + 1) % 4 = 3 → result[2] = nums[3] = 1
- 對於 nums[3] = 1：從索引 3 向右移動 1 步 → (3 + 1) % 4 = 0 → result[3] = nums[0] = 3
```

**範例 2：**
```
輸入：nums = [-1,4,-1]
輸出：[-1,-1,4]

解釋：
- 對於 nums[0] = -1：從索引 0 向左移動 1 步 → (0 - 1 + 3) % 3 = 2 → result[0] = nums[2] = -1
- 對於 nums[1] = 4：從索引 1 向右移動 4 步 → (1 + 4) % 3 = 2 → result[1] = nums[2] = -1
- 對於 nums[2] = -1：從索引 2 向左移動 1 步 → (2 - 1) % 3 = 1 → result[2] = nums[1] = 4
```

### 限制條件

- `1 <= nums.length <= 100`
- `-100 <= nums[i] <= 100`

## 解題概念

### 核心思路

這道題的關鍵在於理解**環狀陣列**的特性，以及如何使用**模運算**來處理索引的循環。

### 出發點

1. **環狀結構**：陣列的首尾相連，當索引超出範圍時需要回繞
2. **方向性移動**：正數向右（索引增加）、負數向左（索引減少）
3. **索引計算**：使用模運算 `% n` 來處理環狀特性

### 關鍵難點

在大多數程式語言中（包括 C#），**負數取模的結果可能是負數**。例如：
- `-1 % 4 = -1`（在 C# 中）
- 但我們需要的結果是 `3`（對應環狀陣列中從索引 0 往左 1 步）

因此需要額外處理負數索引的情況。

## 解法說明

### 演算法步驟

1. 建立長度為 `n` 的結果陣列 `res`
2. 對於每個索引 `i`：
   - 計算目標索引：`targetIndex = ((i + nums[i]) % n + n) % n`
   - 將 `res[i]` 設為 `nums[targetIndex]`
3. 回傳 `res`

### 公式解析

```csharp
int targetIndex = ((i + nums[i]) % n + n) % n;
```

讓我們逐步拆解這個公式：

| 步驟 | 運算 | 說明 | 範例（n=4, i=1, nums[i]=-2） |
|------|------|------|------------------------------|
| 1 | `i + nums[i]` | 計算移動後的原始位置 | `1 + (-2) = -1` |
| 2 | `% n` | 第一次取模（可能產生負數） | `-1 % 4 = -1` |
| 3 | `+ n` | 將負數轉為正數範圍 | `-1 + 4 = 3` |
| 4 | `% n` | 第二次取模確保在 [0, n-1] | `3 % 4 = 3` |

#### 為什麼需要 `((x % n) + n) % n`？

這個技巧稱為「**正規化模運算**」（normalized modulo），用於確保結果始終為非負數：

- **情況 1（正數或零）**：`x % n` 已經在 `[0, n-1]` 範圍內，`+ n` 後再 `% n` 不影響結果
  - 例：`5 % 4 = 1` → `(1 + 4) % 4 = 1`
  
- **情況 2（負數）**：`x % n` 為負數，`+ n` 將其轉換為對應的正數索引
  - 例：`-1 % 4 = -1` → `(-1 + 4) % 4 = 3`
  - 例：`-5 % 4 = -1` → `(-1 + 4) % 4 = 3`

### 複雜度分析

- **時間複雜度**：`O(n)`
  - 只需遍歷陣列一次，每個元素的計算都是 O(1)
  
- **空間複雜度**：`O(n)`
  - 需要建立相同大小的結果陣列

## 詳細範例演示

### 範例：nums = [3, -2, 1, 1]

讓我們逐步追蹤每個元素的計算過程：

```
原始陣列：[3, -2, 1, 1]
索引：      0   1  2  3
```

#### 索引 0（nums[0] = 3）

```
目標 = ((0 + 3) % 4 + 4) % 4
     = (3 % 4 + 4) % 4
     = (3 + 4) % 4
     = 7 % 4
     = 3

result[0] = nums[3] = 1
```

移動路徑：`0 → 1 → 2 → 3`（向右 3 步）

#### 索引 1（nums[1] = -2）

```
目標 = ((1 + (-2)) % 4 + 4) % 4
     = ((-1) % 4 + 4) % 4
     = (-1 + 4) % 4
     = 3 % 4
     = 3

result[1] = nums[3] = 1
```

移動路徑：`1 → 0 → 3`（向左 2 步，環繞）

#### 索引 2（nums[2] = 1）

```
目標 = ((2 + 1) % 4 + 4) % 4
     = (3 % 4 + 4) % 4
     = (3 + 4) % 4
     = 7 % 4
     = 3

result[2] = nums[3] = 1
```

移動路徑：`2 → 3`（向右 1 步）

#### 索引 3（nums[3] = 1）

```
目標 = ((3 + 1) % 4 + 4) % 4
     = (4 % 4 + 4) % 4
     = (0 + 4) % 4
     = 4 % 4
     = 0

result[3] = nums[0] = 3
```

移動路徑：`3 → 0`（向右 1 步，環繞）

#### 最終結果

```
輸入：  [3, -2, 1, 1]
輸出：  [1,  1, 1, 3]
```

### 視覺化說明

```
環狀陣列結構：
    0(3)
   /    \
3(1)    1(-2)
   \    /
    2(1)

各位置的移動：
- 位置 0 (值=3)：順時針移動 3 步 → 到達位置 3 (值=1)
- 位置 1 (值=-2)：逆時針移動 2 步 → 到達位置 3 (值=1)
- 位置 2 (值=1)：順時針移動 1 步 → 到達位置 3 (值=1)
- 位置 3 (值=1)：順時針移動 1 步 → 到達位置 0 (值=3)
```

## 實作程式碼

```csharp
public int[] ConstructTransformedArray(int[] nums)
{
    int n = nums.Length;
    int[] res = new int[n];
    
    for(int i = 0; i < n; i++)
    {
        // 使用正規化模運算處理環狀索引
        int targetIndex = ((i + nums[i]) % n + n) % n;
        res[i] = nums[targetIndex];
    }
    
    return res;
}
```

## 執行與測試

建構專案：
```bash
dotnet build
```

執行程式（包含測試案例）：
```bash
dotnet run --project leetcode_3379
```

測試輸出：
```
測試案例 1: [3, -2, 1, 1] => [1, 1, 1, 3]
測試案例 2: [-1, 4, -1] => [-1, -1, 4]
測試案例 3: [0, 1, -1] => [0, 0, 0]
```

## 延伸思考

### 其他解法

雖然當前解法已經是最優解，但我們也可以用更直觀的方式實作（犧牲一些簡潔性）：

```csharp
public int[] ConstructTransformedArrayVerbose(int[] nums)
{
    int n = nums.Length;
    int[] res = new int[n];
    
    for(int i = 0; i < n; i++)
    {
        int targetIndex;
        if (nums[i] >= 0)
        {
            // 向右移動
            targetIndex = (i + nums[i]) % n;
        }
        else
        {
            // 向左移動，處理負數
            targetIndex = ((i + nums[i]) % n + n) % n;
        }
        res[i] = nums[targetIndex];
    }
    
    return res;
}
```

但使用統一的公式 `((i + nums[i]) % n + n) % n` 更簡潔且同時處理兩種情況。

### 相關題目

- [189. Rotate Array](https://leetcode.com/problems/rotate-array/) - 陣列旋轉
- [1700. Number of Students Unable to Eat Lunch](https://leetcode.com/problems/number-of-students-unable-to-eat-lunch/) - 環狀佇列模擬

---

**License**: MIT  
**Author**: [HyperLee](https://github.com/HyperLee)  
**Date**: 2026-02-05
