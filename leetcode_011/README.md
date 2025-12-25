# LeetCode 11 - 盛最多水的容器

> Container With Most Water

C# 解法實作，使用雙指針演算法求解最大盛水容器問題。

## 題目描述

**LeetCode 11. Container With Most Water**

- [題目連結（英文）](https://leetcode.com/problems/container-with-most-water/description/?envType=study-plan-v2&envId=leetcode-75)
- [題目連結（中文）](https://leetcode.cn/problems/container-with-most-water/description/)

### 題目說明

給定一個長度為 `n` 的整數陣列 `height`。畫出 `n` 條垂直線，第 `i` 條線的兩個端點為 `(i, 0)` 與 `(i, height[i])`。

找出兩條線，與 x 軸一起構成一個容器，使該容器能容納最多的水。

回傳該容器能儲存的最大水量。

**注意**：容器不能傾斜。

### 範例

**範例 1：**

```
輸入：height = [1,8,6,2,5,4,8,3,7]
輸出：49
解釋：上圖中的垂直線由陣列 [1,8,6,2,5,4,8,3,7] 表示。
在此情況下，容器能容納的最大水量（藍色部分）為 49。
```

**範例 2：**

```
輸入：height = [1,1]
輸出：1
```

### 限制條件

- `n == height.length`
- `2 <= n <= 10^5`
- `0 <= height[i] <= 10^4`

## 解題概念

### 問題核心

這道題目的核心是找出兩條垂直線，使它們與 x 軸形成的容器能容納最多的水。容器的盛水量取決於：

1. **高度**：由較短的那條線決定（木桶原理）
2. **寬度**：兩條線之間的距離

因此，面積公式為：

```
面積 = min(height[i], height[j]) × (j - i)
```

### 為什麼不能用暴力解法？

暴力解法需要檢查所有可能的線對（兩兩組合），時間複雜度為 O(n²)。當 n 達到 10^5 時，這會導致超時。

### 解題思路：雙指針法

**核心策略**：從兩端開始，逐步向中間收縮，尋找最大面積。

#### 為什麼雙指針有效？

1. **初始狀態**：左指針在最左端（index 0），右指針在最右端（index n-1）
   - 此時寬度最大
   - 我們需要在縮小寬度的過程中，盡可能增加高度

2. **移動策略**：每次移動較短的那一邊
   - 如果移動較長的一邊，新的面積不可能變大
     - 原因：高度仍受限於短邊，而寬度變小了
   - 移動較短的一邊，有機會遇到更高的柱子
     - 可能找到更大的面積

3. **正確性保證**：
   - 每次移動都會捨棄不可能產生更大面積的配對
   - 不會錯過最優解

## 解法詳細說明

### 演算法步驟

```
1. 初始化左指針 left = 0，右指針 right = n - 1
2. 初始化最大面積 max = 0
3. 當 left < right 時，重複以下步驟：
   a. 計算當前面積 = min(height[left], height[right]) × (right - left)
   b. 更新 max = max(max, 當前面積)
   c. 如果 height[left] < height[right]，則 left++
   d. 否則，right--
4. 返回 max
```

### 複雜度分析

- **時間複雜度**：O(n)
  - 每個元素最多被訪問一次
  - 左右指針各移動最多 n 次
  
- **空間複雜度**：O(1)
  - 只使用常數個變數

### 程式碼實作

```csharp
public int MaxArea(int[] height)
{
    int left = 0;
    int right = height.Length - 1;
    int max = 0;

    while (left < right)
    {
        int area = Math.Min(height[left], height[right]) * (right - left);
        max = Math.Max(max, area);

        if (height[left] < height[right])
        {
            left++;
        }
        else
        {
            right--;
        }
    }
    
    return max;
}
```

## 範例演示

### 範例 1：height = [1, 8, 6, 2, 5, 4, 8, 3, 7]

讓我們逐步追蹤演算法的執行過程：

```
初始狀態：
陣列: [1, 8, 6, 2, 5, 4, 8, 3, 7]
索引:  0  1  2  3  4  5  6  7  8
       ↑                       ↑
      left                   right
```

**迭代 1：**
```
left = 0, right = 8
height[0] = 1, height[8] = 7
面積 = min(1, 7) × (8 - 0) = 1 × 8 = 8
max = 8
移動：height[0] < height[8]，所以 left++ → left = 1
```

**迭代 2：**
```
left = 1, right = 8
height[1] = 8, height[8] = 7
面積 = min(8, 7) × (8 - 1) = 7 × 7 = 49
max = 49
移動：height[1] > height[8]，所以 right-- → right = 7
```

**迭代 3：**
```
left = 1, right = 7
height[1] = 8, height[7] = 3
面積 = min(8, 3) × (7 - 1) = 3 × 6 = 18
max = 49（不更新）
移動：height[1] > height[7]，所以 right-- → right = 6
```

**迭代 4：**
```
left = 1, right = 6
height[1] = 8, height[6] = 8
面積 = min(8, 8) × (6 - 1) = 8 × 5 = 40
max = 49（不更新）
移動：height[1] == height[6]，執行 else，right-- → right = 5
```

**迭代 5：**
```
left = 1, right = 5
height[1] = 8, height[5] = 4
面積 = min(8, 4) × (5 - 1) = 4 × 4 = 16
max = 49（不更新）
移動：height[1] > height[5]，所以 right-- → right = 4
```

**迭代 6：**
```
left = 1, right = 4
height[1] = 8, height[4] = 5
面積 = min(8, 5) × (4 - 1) = 5 × 3 = 15
max = 49（不更新）
移動：height[1] > height[4]，所以 right-- → right = 3
```

**迭代 7：**
```
left = 1, right = 3
height[1] = 8, height[3] = 2
面積 = min(8, 2) × (3 - 1) = 2 × 2 = 4
max = 49（不更新）
移動：height[1] > height[3]，所以 right-- → right = 2
```

**迭代 8：**
```
left = 1, right = 2
height[1] = 8, height[2] = 6
面積 = min(8, 6) × (2 - 1) = 6 × 1 = 6
max = 49（不更新）
移動：height[1] > height[2]，所以 right-- → right = 1
```

**結束：** left == right，迴圈結束

**最終答案：49**

### 範例 2：height = [4, 3, 2, 1, 4]

```
初始狀態：
陣列: [4, 3, 2, 1, 4]
索引:  0  1  2  3  4
       ↑           ↑
      left       right

迭代 1：
面積 = min(4, 4) × 4 = 16
max = 16
移動：相等，right-- → right = 3

迭代 2：
面積 = min(4, 1) × 3 = 3
max = 16
移動：左邊大，right-- → right = 2

迭代 3：
面積 = min(4, 2) × 2 = 4
max = 16
移動：左邊大，right-- → right = 1

迭代 4：
面積 = min(4, 3) × 1 = 3
max = 16
移動：左邊大，right-- → right = 0

結束：left == right

答案：16
```

## 執行方式

### 建構專案

```bash
dotnet build
```

### 執行程式

```bash
dotnet run
```

### 預期輸出

```
測試案例 1: height = [1, 8, 6, 2, 5, 4, 8, 3, 7]
最大面積 = 49

測試案例 2: height = [1, 1]
最大面積 = 1

測試案例 3: height = [4, 3, 2, 1, 4]
最大面積 = 16

測試案例 4: height = [1, 2, 1]
最大面積 = 2
```

## 重點整理

### 關鍵概念

1. **木桶原理**：容器的高度由短邊決定
2. **雙指針技巧**：從兩端向中間收縮
3. **貪心策略**：每次移動較短的一邊

### 常見錯誤

- ❌ 移動較長的指針 → 不會找到更大面積
- ❌ 隨機移動指針 → 可能錯過最優解
- ❌ 使用暴力解法 → 時間複雜度 O(n²) 會超時

### 學習要點

> [!TIP]
> 這題是雙指針演算法的經典應用，需要理解：
> - 為什麼移動短邊是正確的策略
> - 如何證明不會錯過最優解
> - 時間複雜度如何從 O(n²) 優化到 O(n)

> [!NOTE]
> 雙指針法適用於許多陣列問題，特別是需要在有序或部分有序的資料中尋找配對的情況。

## 參考資料

- [iT 邦幫忙 - LeetCode Container With Most Water](https://ithelp.ithome.com.tw/articles/10228493)
- [LeetCode 中文官方題解](https://leetcode.cn/problems/container-with-most-water/solution/sheng-zui-duo-shui-de-rong-qi-by-leetcode-solution/)
- [雙指針法詳解](https://leetcode.cn/problems/container-with-most-water/solution/container-with-most-water-shuang-zhi-zhen-fa-yi-do/)

## 相關題目

- [LeetCode 42. Trapping Rain Water](https://leetcode.com/problems/trapping-rain-water/) - 接雨水（困難）
- [LeetCode 15. 3Sum](https://leetcode.com/problems/3sum/) - 三數之和
- [LeetCode 16. 3Sum Closest](https://leetcode.com/problems/3sum-closest/) - 最接近的三數之和
