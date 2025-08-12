# leetcode_2787: Ways to Express an Integer as Sum of Powers

> [LeetCode 2787. Ways to Express an Integer as Sum of Powers](https://leetcode.com/problems/ways-to-express-an-integer-as-sum-of-powers/)

---

## 專案簡介

本專案提供三種 C# 動態規劃解法，解決「將一個正整數 n 表示為若干個不同正整數的 x 次冪之和」的所有方案數，並對 $10^9+7$ 取模。  
每個解法皆有詳細註解與說明，適合學習背包問題與動態規劃技巧。

---

## 問題描述

給定兩個正整數 $n$ 和 $x$，請回傳有多少組唯一整數集合 $[n_1, n_2, ..., n_k]$，使得 $n = n_1^x + n_2^x + ... + n_k^x$。  
每個 $n_i$ 需為不同正整數，答案需對 $10^9+7$ 取模。

---

## 三種解法詳細說明

### 1. 二維動態規劃（NumberOfWays）

#### 思路

- 將問題視為 0-1 背包：背包容量為 $n$，物品為 $1^x, 2^x, ..., m^x$（$m^x \leq n$）。
- 狀態 $dp[i, j]$：前 $i$ 個數字中，選擇若干個不同數字的 $x$ 次冪之和為 $j$ 的方案數。
- 狀態轉移：
  - 不選 $i$：$dp[i, j] = dp[i-1, j]$
  - 選 $i$：若 $j \geq i^x$，$dp[i, j] += dp[i-1, j-i^x]$

#### 程式碼片段
```csharp
int[,] dp = new int[maxBase + 1, n + 1];
dp[0, 0] = 1;
for (int i = 1; i <= maxBase; i++) {
    int power = (int)Math.Pow(i, x);
    for (int j = 0; j <= n; j++) {
        dp[i, j] = dp[i - 1, j];
        if (j >= power) {
            dp[i, j] = (dp[i, j] + dp[i - 1, j - power]) % mod;
        }
    }
}
return dp[maxBase, n];
```

#### 優點
- 狀態明確，易於理解與推導。
- 不易出錯，適合初學者。

#### 缺點
- 空間複雜度 $O(n \sqrt{n})$，當 $n$ 較大時記憶體消耗高。

---

### 2. 二維動態規劃（NumberOfWays2）

#### 思路

- 與第一種解法類似，但直接枚舉 $i$ 從 $1$ 到 $n$，每次計算 $i^x$。
- 狀態 $dp[i, j]$：前 $i$ 個數字中，選擇若干個不同數字的 $x$ 次冪之和為 $j$ 的方案數。
- 狀態轉移同上。

#### 程式碼片段
```csharp
long[,] dp = new long[n + 1, n + 1];
dp[0, 0] = 1;
for (int i = 1; i <= n; i++) {
    long val = (long)Math.Pow(i, x);
    for (int j = 0; j <= n; j++) {
        dp[i, j] = dp[i - 1, j];
        if (j >= val) {
            dp[i, j] = (dp[i, j] + dp[i - 1, j - (int)val]) % MOD;
        }
    }
}
return (int)dp[n, n];
```

#### 優點
- 實作簡單，與第一種解法幾乎等價。
- 直接枚舉，無需預先計算最大基底。

#### 缺點
- 空間複雜度 $O(n^2)$，當 $n$ 較大時記憶體消耗更高。
- 效能與第一種解法相近，但空間利用率較低。

---

### 3. 一維動態規劃（空間優化，NumberOfWays3）

#### 思路

- 空間優化版 0-1 背包。
- 狀態 $dp[j]$：和為 $j$ 的方案數。
- 每次嘗試加入 $i^x$，倒序更新 $dp$，確保每個數字只用一次。

#### 程式碼片段
```csharp
long[] dp = new long[n + 1];
dp[0] = 1;
for (int i = 1; i <= n; i++) {
    int val = (int)Math.Pow(i, x);
    if (val > n) break;
    for (int j = n; j >= val; j--) {
        dp[j] = (dp[j] + dp[j - val]) % MOD;
    }
}
return (int)dp[n];
```

#### 優點
- 空間複雜度降為 $O(n)$，大幅減少記憶體消耗。
- 適合 $n$ 較大時使用。
- 實作簡潔，效能佳。

#### 缺點
- 狀態壓縮後不易追蹤路徑，僅能計算方案數。
- 需特別注意倒序更新，避免重複選取同一數字。

---

## 三種解法比較

| 解法           | 狀態設計         | 時間複雜度         | 空間複雜度         | 優點                   | 缺點                   |
|----------------|------------------|--------------------|--------------------|------------------------|------------------------|
| NumberOfWays   | 二維 $dp[i, j]$  | $O(n \sqrt{n})$    | $O(n \sqrt{n})$    | 易懂、狀態明確          | 空間消耗較高           |
| NumberOfWays2  | 二維 $dp[i, j]$  | $O(n^2)$           | $O(n^2)$           | 實作簡單                | 空間消耗最高           |
| NumberOfWays3  | 一維 $dp[j]$     | $O(n \sqrt{n})$    | $O(n)$             | 空間效率最佳、效能佳     | 只計算方案數，路徑難追 |

- **建議**：若只需計算方案數，推薦使用 `NumberOfWays3`（空間優化版）；若需學習動態規劃狀態設計，可參考前兩種二維寫法。

---

## 執行方式

1. 使用 .NET 8+ 執行本專案
2. 編譯並執行 `Program.cs`，可直接看到三種解法的輸出結果

```bash
dotnet build
dotnet run --project leetcode_2787/leetcode_2787.csproj
```

---

## 範例輸出

```
NumberOfWays(n=10, x=2) = 1
NumberOfWays2(n=10, x=2) = 1
NumberOfWays(n=160, x=3) = 1
NumberOfWays2(n=160, x=3) = 1
NumberOfWays(n=100, x=2) = 3
NumberOfWays2(n=100, x=2) = 3
```

---

## 參考連結

- [LeetCode 2787 題目說明](https://leetcode.com/problems/ways-to-express-an-integer-as-sum-of-powers/)
- [0-1 背包問題教學](https://oi-wiki.org/dp/knapsack/)
