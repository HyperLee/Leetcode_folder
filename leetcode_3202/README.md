# LeetCode 3202 - 找出有效子序列的最大長度 II

## 問題描述

**題目連結：**
- [英文版](https://leetcode.com/problems/find-the-maximum-length-of-valid-subsequence-ii/description/?envType=daily-question&envId=2025-07-17)
- [中文版](https://leetcode.cn/problems/find-the-maximum-length-of-valid-subsequence-ii/description/?envType=daily-question&envId=2025-07-17)

給定一個整數陣列 `nums` 和一個正整數 `k`。nums 的一個長度為 x 的子序列 `sub` 被稱為有效，若滿足：
```
(sub[0] + sub[1]) % k == (sub[1] + sub[2]) % k == ... == (sub[x - 2] + sub[x - 1]) % k
```

請回傳 nums 最長有效子序列的長度。

## 解題方法

### 動態規劃解法

本題使用動態規劃來解決，核心思想是維護一個二維 DP 表格來記錄不同狀態下的最優解。

#### 狀態定義
- `dp[i][j]` 表示：最後兩個元素模 k 的餘數分別是 i 和 j 的有效子序列的最大長度

#### 解題思路
1. 根據有效子序列的定義，相鄰元素的和模 k 必須相同
2. 考慮子序列最後兩個元素的模 k 的餘數，一共有 k² 種可能性
3. 使用 `dp[i][j]` 表示最後兩個元素模 k 的餘數分別是 i 和 j 的有效子序列的最大長度
4. 遍歷 nums，對每個元素嘗試將其加入子序列，更新對應的 dp 值

## 解題流程

### 步驟 1：初始化
```csharp
int[,] dp = new int[k, k];  // 建立 k×k 的 DP 表格
int res = 0;                // 追蹤全域最大值
```

### 步驟 2：遍歷每個元素
```csharp
foreach (int num in nums)
{
    int mod = num % k;  // 計算當前元素模 k 的餘數
    
    // 嘗試將當前元素作為子序列的最後一個元素
    for (int prev = 0; prev < k; prev++)
    {
        // 狀態轉移方程式
        dp[prev, mod] = dp[mod, prev] + 1;
        res = Math.Max(res, dp[prev, mod]);
    }
}
```

### 步驟 3：返回結果
返回全域最大值 `res`

## 狀態轉移方程式詳解

### 核心轉移：`dp[prev, mod] = dp[mod, prev] + 1`

這個轉移方程式是演算法的核心，其含義為：

#### 轉移邏輯
假設我們有一個有效子序列：
```
[...] -> a -> b
```
其中 `a % k = mod`，`b % k = prev`

現在要加入新元素 `c`，其中 `c % k = mod`：
```
[...] -> a -> b -> c
```

#### 為什麼是 `dp[mod, prev] + 1`？

1. **原有子序列狀態**：`dp[mod, prev]` 表示以餘數 `mod` 為倒數第二個元素，餘數 `prev` 為最後一個元素的子序列長度

2. **新子序列狀態**：加入新元素後，新的子序列狀態變成：
   - 倒數第二個元素的餘數：`prev`
   - 最後一個元素的餘數：`mod`

3. **狀態轉移**：
   - 左邊 `dp[prev, mod]`：新子序列的狀態
   - 右邊 `dp[mod, prev] + 1`：原有子序列長度加 1

#### 視覺化範例

假設 `k = 3`，我們有序列：
```
nums = [1, 2, 4, 5]  // 餘數: [1, 2, 1, 2]
```

當處理元素 `4`（餘數 1）時：
- 考慮前一個元素餘數為 `2` 的情況
- 查看 `dp[1, 2]` 的值（以餘數 1 為倒數第二個，餘數 2 為最後一個的子序列長度）
- 更新 `dp[2, 1] = dp[1, 2] + 1`（以餘數 2 為倒數第二個，餘數 1 為最後一個的子序列長度）

### 有效性保證

這個轉移確保了有效子序列的性質：**相鄰元素的和模 k 必須相同**。

如果我們有：
- `(a + b) % k = target`
- `(b + c) % k = target`

那麼當我們知道 `b % k = prev` 和 `c % k = mod` 時，我們可以從之前以 `(mod, prev)` 結尾的子序列擴展到以 `(prev, mod)` 結尾的子序列。

## 複雜度分析

- **時間複雜度**：O(n × k)，其中 n 是陣列長度，k 是模數
  - 對每個元素需要遍歷 k 個可能的前一個餘數值
- **空間複雜度**：O(k²)，用於儲存 DP 表格

## 程式碼實作

```csharp
public int MaximumLength(int[] nums, int k)
{
    // dp[i][j] 表示最後兩個元素模 k 的餘數分別是 i 和 j 的有效子序列的最大長度
    int[,] dp = new int[k, k];
    int res = 0;
    
    // 遍歷陣列中的每個元素
    foreach (int num in nums)
    {
        // 計算當前元素模 k 的餘數
        int mod = num % k;
        
        // 嘗試將當前元素作為子序列的最後一個元素
        // 遍歷前一個元素模 k 的所有可能餘數
        for (int prev = 0; prev < k; prev++)
        {
            // 更新 dp[prev][mod]：以 prev 為倒數第二個元素，mod 為最後一個元素的子序列長度
            // dp[mod][prev] + 1 表示在原有子序列基礎上加入當前元素
            dp[prev, mod] = dp[mod, prev] + 1;
            
            // 更新全域最大值
            res = Math.Max(res, dp[prev, mod]);
        }
    }
    
    return res;
}
```

## 檔案結構

```
leetcode_3202/
├── Program.cs           # 主要程式碼實作
├── leetcode_3202.csproj # 專案設定檔案
└── README.md           # 本說明文件
```

## 執行環境

- **.NET 8.0**
- **C# 程式語言**
- **Visual Studio Code** 開發環境

## 重要特性

1. **動態規劃**：使用二維 DP 表格記錄狀態
2. **狀態轉移**：巧妙的轉移方程式確保子序列有效性
3. **空間最佳化**：只需要 O(k²) 的額外空間
4. **時間效率**：O(n × k) 的時間複雜度，適合大多數測試案例

## 關鍵洞察

這個問題的關鍵在於理解有效子序列的性質：相鄰元素的和模 k 必須相同。通過維護二維 DP 表格，我們能夠有效地追蹤所有可能的狀態轉移，並找到最長的有效子序列。

狀態轉移方程式 `dp[prev, mod] = dp[mod, prev] + 1` 體現了這種對稱性質，確保了演算法的正確性和效率。
