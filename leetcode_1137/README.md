# LeetCode 1137 - N-th Tribonacci Number

第 N 個泰波那契數的 C# 解法實作。

## 題目描述

泰波那契數列（Tribonacci sequence）定義如下：

- $T_0 = 0$
- $T_1 = 1$  
- $T_2 = 1$
- 對於 $n \geq 0$，有 $T_{n+3} = T_n + T_{n+1} + T_{n+2}$

給定整數 `n`，請回傳第 n 個泰波那契數 $T_n$ 的值。

**限制條件：**

- `0 <= n <= 37`
- 答案保證在 32 位元整數範圍內

**相關連結：**

- [LeetCode 英文版](https://leetcode.com/problems/n-th-tribonacci-number/description/)
- [LeetCode 中文版](https://leetcode.cn/problems/n-th-tribonacci-number/description/)

## 解題概念與思路

### 核心觀察

題目已經直接給出**狀態轉移方程**：

$$T_{n+3} = T_n + T_{n+1} + T_{n+2}$$

這本質上是一道**模擬題**，我們只需要按照公式從前往後計算即可。

### 解題策略：迭代實作動態規劃

採用**滑動視窗**的概念，使用三個變數記錄連續的泰波那契數，從小到大依序計算：

1. **空間優化**：不需要儲存整個數列，只需維護最近三個數
2. **時間效率**：線性時間複雜度 O(n)
3. **簡潔實作**：避免遞迴帶來的重複計算和堆疊溢位問題

## 演算法詳解

### 變數定義

```text
a = T(i-3)  // 前三個位置的值
b = T(i-2)  // 前兩個位置的值  
c = T(i-1)  // 前一個位置的值
d = T(i)    // 當前要計算的值
```

### 狀態轉移

每次迭代計算 `d = a + b + c`，然後滑動視窗：

```text
a ← b ← c ← d
```

### 程式碼實作

```csharp
public int Tribonacci(int n)
{
    // 基底情況
    if (n == 0) return 0;
    if (n == 1 || n == 2) return 1;

    // 初始化：a=T(0), b=T(1), c=T(2)
    int a = 0, b = 1, c = 1, d = 0;
    
    // 迭代計算 T(3) 到 T(n)
    for (int i = 3; i <= n; i++)
    {
        d = a + b + c;  // 狀態轉移
        a = b;          // 滑動視窗
        b = c;
        c = d;
    }
    
    return d;
}
```

### 複雜度分析

| 指標 | 複雜度 | 說明 |
|------|--------|------|
| 時間 | O(n) | 單次迴圈遍歷 |
| 空間 | O(1) | 僅使用 4 個變數 |

## 範例演示

### 範例 1：計算 T(4)

**輸入：** `n = 4`

**執行流程：**

| 步驟 | i | a (T_{i-3}) | b (T_{i-2}) | c (T_{i-1}) | d = a+b+c |
|------|---|-------------|-------------|-------------|-----------|
| 初始 | - | 0 | 1 | 1 | - |
| 1 | 3 | 0 | 1 | 1 | **2** |
| 2 | 4 | 1 | 1 | 2 | **4** |

**輸出：** `4`

**驗證：** 數列為 0, 1, 1, 2, 4 → T(4) = 4 ✓

### 範例 2：計算 T(25)

**輸入：** `n = 25`

**輸出：** `1389537`

數列開頭為：0, 1, 1, 2, 4, 7, 13, 24, 44, 81, 149, ...

## 執行專案

### 環境需求

- .NET 10.0 或更新版本

### 建構與執行

```bash
# 建構專案
dotnet build

# 執行程式
dotnet run --project leetcode_1137/leetcode_1137.csproj
```

### 預期輸出

```text
=== 泰波那契數列測試 ===
T(n) = T(n-1) + T(n-2) + T(n-3)
T(0) = 0, T(1) = 1, T(2) = 1

Tribonacci(0) = 0
Tribonacci(1) = 1
Tribonacci(2) = 1
Tribonacci(3) = 2
Tribonacci(4) = 4
Tribonacci(5) = 7
Tribonacci(10) = 149
Tribonacci(25) = 1389537
```

## 相關題目

- [509. Fibonacci Number](https://leetcode.com/problems/fibonacci-number/) - 費波那契數（雙項遞推）
- [70. Climbing Stairs](https://leetcode.com/problems/climbing-stairs/) - 爬樓梯（類似 DP 結構）
- [746. Min Cost Climbing Stairs](https://leetcode.com/problems/min-cost-climbing-stairs/) - 最小花費爬樓梯
