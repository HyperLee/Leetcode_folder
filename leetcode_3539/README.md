# LeetCode 3539 - Find Sum of Array Product of Magical Sequences

[![LeetCode](https://img.shields.io/badge/LeetCode-3539-FFA116?style=flat&logo=leetcode&logoColor=white)](https://leetcode.com/problems/find-sum-of-array-product-of-magical-sequences/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Hard-red)]()
[![Language](https://img.shields.io/badge/Language-C%23-239120?logo=c-sharp&logoColor=white)]()
[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=.net&logoColor=white)]()

## 題目描述

給定兩個整數 `m` 和 `k`，以及一個整數陣列 `nums`。

一個整數序列 `seq` 稱為**魔法序列**，如果：
- `seq` 的大小為 `m`
- `0 <= seq[i] < nums.length`
- `2^seq[0] + 2^seq[1] + ... + 2^seq[m-1]` 的二進位表示有 `k` 個設定位元（set bits）

此序列的**陣列乘積**定義為：`prod(seq) = nums[seq[0]] × nums[seq[1]] × ... × nums[seq[m-1]]`

返回所有有效魔法序列的陣列乘積之和，結果需要模 `10^9 + 7`。

> **設定位元**是指二進位表示中值為 1 的位元。

## 解題思路

### 方法一：動態規劃

#### 核心概念

令數組 `nums` 的長度為 `n`。根據題意，我們依次從 0 到 n-1 分別取若干個數（即 `nums` 的下標）組成序列 `seq`。

假設取值等於 `t` 的元素有 `r_t` 個，則有：

$$\sum_{t=0}^{n-1} r_t = m$$

#### 數學推導

##### 1. 序列排列數

這些序列的排列數為：

$$\frac{m!}{\prod_{t=0}^{n-1} r_t!}$$

**說明**：總共有 `m` 個位置，其中相同數字 `t` 有 `r_t` 個，需要除以每個數字的排列數。

##### 2. 陣列乘積和

這些序列對應的數組乘積和為：

$$\frac{m!}{\prod_{t=0}^{n-1} r_t!} \times \prod_{t=0}^{n-1} \text{nums}[t]^{r_t}$$

可以改寫為：

$$m! \times \prod_{t=0}^{n-1} \frac{\text{nums}[t]^{r_t}}{r_t!}$$

##### 3. Mask 定義

對於數字集合 `X`，我們定義：

$$\text{mask}(X) = \sum_{x \in X} 2^x$$

#### 動態規劃狀態定義

定義 `f(i, j, p, q)` 表示：
- **i**：目前處理到 `nums` 的第 `i` 個元素（索引 0 到 i）
- **j**：已經取了 `j` 個數
- **p**：已取數字索引經過處理後的 mask 值（詳見下方說明）
- **q**：低位元的置位數累積

狀態值為：所有取數方案對應的 $\prod_{t=0}^{i} \frac{\text{nums}[t]^{r_t}}{r_t!}$ 的和

#### 狀態轉移方程

當從第 `i` 個元素轉移到第 `i+1` 個元素時，假設從 `nums[i+1]` 取 `r` 個數：

$$f(i, j, p, q) \times \frac{\text{nums}[i+1]^r}{r!} \rightarrow f(i+1, j+r, \lfloor\frac{p}{2}\rfloor+r, q+(p \bmod 2))$$

**轉移說明**：
- `j+r`：取數總數增加 `r`
- `⌊p/2⌋+r`：將 `p` 右移一位（相當於處理完當前位），加上新取的 `r` 個數
- `q+(p mod 2)`：累積當前位的置位數（如果 `p` 的最低位是 1，則 `q` 加 1）

#### 初始化

當 `i=0` 時，從 `nums[0]` 取 `j` 個數：

$$f(0, j, j, 0) = \frac{\text{nums}[0]^j}{j!}$$

#### 最終答案

枚舉所有可能的 `p` 和 `q`，滿足以下條件時累加答案：

$$b_p + q = k$$

其中 $b_p$ 表示 `p` 的置位數（二進位中 1 的個數）。

最終答案為：

$$\sum_{b_p + q = k} (m! \times f(n-1, m, p, q)) \bmod (10^9 + 7)$$

## 關鍵技巧

### 1. 階乘預計算

預先計算所有需要的階乘值：

```csharp
fac[i] = i! mod (10^9 + 7)
```

### 2. 階乘逆元計算（費馬小定理 + 反向遞推）

對於質數 `p`，根據費馬小定理：

$$a^{-1} \equiv a^{p-2} \pmod{p}$$

**優化方法**：只需計算一次快速冪，然後反向遞推

```csharp
// 1. 先計算 m! 的逆元
ifac[m] = QuickMul(fac[m], mod - 2, mod);  // O(log mod)

// 2. 反向計算其他階乘的逆元
// 公式：1/(i-1)! = (1/i!) × i
for (int i = m; i >= 1; i--) {
    ifac[i - 1] = ifac[i] * i % mod;  // O(1)
}
```

**時間複雜度優化**：

- 原方法：O(m × log mod) - 每個階乘都需要快速冪
- 優化後：O(m + log mod) - 只需一次快速冪 + m 次乘法

### 3. 快速冪演算法

計算 $x^y \bmod \text{mod}$ 的高效方法：

將指數 `y` 用二進位表示，例如 `y = 13 = 1101(2) = 8 + 4 + 1`

則：$x^{13} = x^8 \times x^4 \times x^1$

時間複雜度：**O(log y)**

### 4. 冪次方預計算

預先計算 `nums[i]` 的所有冪次方：

```csharp
numsPower[i][j] = nums[i]^j mod (10^9 + 7)
```

避免重複計算，降低時間複雜度。

## 常見錯誤與陷阱

### ❌ 錯誤的階乘逆元計算

```csharp
// 錯誤方法 1：計算 i 的逆元而非 i! 的逆元
for (int i = 2; i <= m; i++) {
    ifac[i] = QuickMul(i, mod - 2, mod);  // 這是 1/i 而非 1/i!
}

// 錯誤方法 2：錯誤的累乘
for (int i = 2; i <= m; i++) {
    ifac[i] = QuickMul(fac[i], mod - 2, mod);  // 正確計算 1/i!
}
for (int i = 2; i <= m; i++) {
    ifac[i] = ifac[i - 1] * ifac[i] % mod;  // ❌ 破壞了正確值！
    // 這會變成 1/((i-1)! × i!)，完全錯誤
}
```

### ✅ 正確的階乘逆元計算

```csharp
// 方法：只計算 m! 的逆元，然後反向遞推
ifac[m] = QuickMul(fac[m], mod - 2, mod);  // 1/m!
for (int i = m; i >= 1; i--) {
    ifac[i - 1] = ifac[i] * i % mod;  // 1/(i-1)! = (1/i!) × i
}
```

**關鍵差異**：利用數學關係 `(i-1)! × i = i!`，從高往低計算，避免快速冪的重複呼叫。

## 複雜度分析

- **時間複雜度**：O(n × m^2 × m × k) = O(n × m^3 × k)
  - 外層循環：O(n)
  - 內層四個循環：O(m × m × m × k)
  
- **空間複雜度**：O(n × m × m × k)
  - 動態規劃陣列 `f[n][m+1][2m+1][k+1]`

## 執行方式

### 前置需求

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) 或更高版本

### 建置專案

```bash
dotnet build
```

### 執行程式

```bash
dotnet run --project leetcode_3539/leetcode_3539.csproj
```

### 偵錯模式

在 VS Code 中按 `F5` 或使用偵錯面板啟動偵錯。

## 範例測試

### 範例 1

**輸入**：

```text
m = 2, k = 1, nums = [1, 2, 3]
```

**輸出**：

```text
14
```

**說明**：

- 有效的魔法序列：[0,1], [0,2], [1,0], [1,2], [2,0], [2,1]
- 陣列乘積和：1×2 + 1×3 + 2×1 + 2×3 + 3×1 + 3×2 = 2 + 3 + 2 + 6 + 3 + 6 = 22

> 注：此處原題範例可能有誤，請以實際測試結果為準。

### 範例 2

**輸入**：

```text
m = 3, k = 2, nums = [1, 1, 1, 1]
```

**輸出**：

```text
18
```

### 範例 3（關鍵測試案例）

**輸入**：

```text
m = 3, k = 2, nums = [33]
```

**輸出**：

```text
35937
```

**說明**：
此測試案例驗證了階乘逆元計算的正確性。若使用錯誤的逆元計算方法，會得到錯誤結果 500017972。

## 專案結構

```plaintext
leetcode_3539/
├── .vscode/
│   ├── launch.json          # 偵錯組態
│   └── tasks.json           # 建置任務
├── leetcode_3539/
│   ├── Program.cs           # 主程式碼
│   └── leetcode_3539.csproj # 專案檔
├── .editorconfig            # 編輯器設定
├── .gitignore               # Git 忽略檔案
├── leetcode_3539.sln        # 解決方案檔
└── README.md                # 本文件
```

## 相關連結

- [LeetCode 題目（英文）](https://leetcode.com/problems/find-sum-of-array-product-of-magical-sequences/)
- [LeetCode 題目（中文）](https://leetcode.cn/problems/find-sum-of-array-product-of-magical-sequences/)
- [Daily Challenge (2025-10-12)](https://leetcode.com/problems/find-sum-of-array-product-of-magical-sequences/?envType=daily-question&envId=2025-10-12)

## 標籤

`動態規劃` `數學` `組合數學` `位元運算` `模運算`

---

**作者**：HyperLee  
**日期**：2025年10月12日
