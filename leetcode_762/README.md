# LeetCode 762 — Prime Number of Set Bits in Binary Representation

> 二進位表示中質數個計算置位

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![Language](https://img.shields.io/badge/language-C%23-239120)](https://learn.microsoft.com/dotnet/csharp/)
[![Difficulty](https://img.shields.io/badge/difficulty-Easy-brightgreen)](https://leetcode.com/problems/prime-number-of-set-bits-in-binary-representation/)

---

## 題目說明

### English

Given two integers `left` and `right`, return the count of numbers in the inclusive range `[left, right]` having a **prime number of set bits** in their binary representation.

The **number of set bits** an integer has is the number of `1`s present when written in binary.

**Constraints:**
- `1 <= left <= right <= 10^6`
- `0 <= right - left <= 10^4`

### 繁體中文

給定兩個整數 `left` 和 `right`，返回在包含範圍 `[left, right]` 中，二進位表示中**置位數量為質數**的整數個數。

一個整數的**置位數**是其二進位表示中 `1` 的個數。

**限制條件：**
- `1 <= left <= right <= 10^6`
- `0 <= right - left <= 10^4`

---

## 解題概念與出發點

### 問題拆解

本題需要同時解決兩個子問題：

| 子問題 | 說明 | 對應題目 |
|--------|------|----------|
| 計算置位數 | 計算整數二進位表示中 `1` 的個數 | LeetCode 191. Number of 1 Bits |
| 判斷質數 | 判斷一個整數是否為質數 | LeetCode 204. Count Primes（方法一）|

### 關鍵觀察

由於 `right <= 10^6 < 2^20`，任一數的置位數最多為 **20**。  
因此 `IsPrime` 的輸入範圍極小，試除法最多僅迭代 `√20 ≈ 4` 次，效率極高。

需要判斷的質數只可能出現在 `{2, 3, 5, 7, 11, 13, 17, 19}` 之中。

### 方法二額外觀察（位元遮罩快取）

因為合法的置位數只有 0 ～ 20，而其中的質數集合固定不變，
可以將這 8 個質數壓縮成一個 32 位元整數 **mask**：

$$\text{mask} = 665772_{10} = \underbrace{10100010100010101100}_{\text{二進位，共 20 位元}}$$

| 位元位置 (i) | mask 第 i 位 | i 是否為質數 |
|:-----------:|:-----------:|:------------:|
| 2  | **1** | ✅ 是 |
| 3  | **1** | ✅ 是 |
| 5  | **1** | ✅ 是 |
| 7  | **1** | ✅ 是 |
| 11 | **1** | ✅ 是 |
| 13 | **1** | ✅ 是 |
| 17 | **1** | ✅ 是 |
| 19 | **1** | ✅ 是 |
| 其餘 | 0 | ❌ 否 |

判斷方式：設置位數為 `c`，僅需一次位元運算 `(1 << c) & mask != 0` 即可取代試除法。

---

## 解法詳細說明

### 方法一：列舉 + 位元運算 + 試除法

**整體流程：**

```
for x in [left, right]:
    bits = BitCount(x)       // 計算 x 的 popcount（置位數）
    if IsPrime(bits):        // 判斷置位數是否為質數
        result++
```

---

### `BitCount` — Hamming Weight（SWAR 演算法）

這是一個經典的「Population Count」平行加法演算法，時間複雜度 **O(1)**，所有步驟均為固定次數的位元運算，等效於 C# 內建的 `int.PopCount()`。

**核心思路：** 將 32 個位元分組，逐步累加各組的 1 的個數。

```
原始值：  i = 0b...10110101  (以 8 位元為例)

Step 1：每 2 位元一組，計算組內 1 的個數
  遮罩 0x55 = ...01010101
  i = i - ((i >> 1) & 0x55)
  → 各 2-bit 組存放該組的位元 1 個數

Step 2：每 4 位元一組，累加相鄰兩個 2-bit 計數
  遮罩 0x33 = ...00110011
  i = (i & 0x33) + ((i >> 2) & 0x33)

Step 3：每 8 位元一組，累加相鄰兩個 4-bit 計數，遮罩去高位污染
  遮罩 0x0f = ...00001111
  i = (i + (i >> 4)) & 0x0f

Step 4/5：累加 2 個 byte、再累加高低 16-bit
  i = i + (i >> 8)
  i = i + (i >> 16)

最終：取低 6 位元（最大值 32，6 位元足夠表示）
  return i & 0x3f
```

---

### `IsPrime` — 試除法（Trial Division）

```
若 x < 2：非質數，return false
從 i = 2 試除到 √x：
    若 x % i == 0：有因數，return false
通過所有測試：return true
```

> [!NOTE]
> 0 和 1 **不是**質數，需特別處理。
>
> 只需試除到 √x，原因：若 x = a × b 且 a ≤ b，則 a ≤ √x。  
> 因此找到最小因數只需搜尋至 √x。

---

### 方法二：列舉 + 位元運算 + 位元遮罩快取（Bitmask Lookup）

**整體流程：**

```
const mask = 665772  // = 0b10100010100010101100，第 i 位為 1 代表 i 是質數

for x in [left, right]:
    c = BitCount(x)              // 計算置位數
    if (1 << c) & mask != 0:    // 查表判斷 c 是否為質數
        result++
```

**mask 的建構原理：**

$$\text{mask} = \sum_{p \in \{2,3,5,7,11,13,17,19\}} 2^p = 4 + 8 + 32 + 128 + 2048 + 8192 + 131072 + 524288 = 665772$$

**查表原理說明：**

```
置位數 c = 3 時：
  1 << 3  = 0b00000000000000000000000000001000
  mask    = 0b00000000000010100010100010101100
  AND結果  = 0b00000000000000000000000000001000  ≠ 0  → 3 是質數 ✓

置位數 c = 4 時：
  1 << 4  = 0b00000000000000000000000000010000
  mask    = 0b00000000000010100010100010101100
  AND結果  = 0b00000000000000000000000000000000  = 0  → 4 不是質數 ✗
```

> [!TIP]
> 方法二以單次位元 AND 運算取代方法一的 `IsPrime` 迴圈，  
> 雖然兩者均為 O(1)，但方法二的常數係數更小、分支更少。

---

## 演示範例

### 範例輸入：`left = 6, right = 10`

| 數字 | 二進位 | 置位數 | 是否質數 | 計入？ |
|------|--------|--------|----------|--------|
| 6    | `110`  | 2      | ✅ 是    | ✓      |
| 7    | `111`  | 3      | ✅ 是    | ✓      |
| 8    | `1000` | 1      | ❌ 否    | ✗      |
| 9    | `1001` | 2      | ✅ 是    | ✓      |
| 10   | `1010` | 2      | ✅ 是    | ✓      |

**輸出：4**

---

### 範例輸入：`left = 10, right = 15`

| 數字 | 二進位 | 置位數 | 是否質數 | 計入？ |
|------|--------|--------|----------|--------|
| 10   | `1010` | 2      | ✅ 是    | ✓      |
| 11   | `1011` | 3      | ✅ 是    | ✓      |
| 12   | `1100` | 2      | ✅ 是    | ✓      |
| 13   | `1101` | 3      | ✅ 是    | ✓      |
| 14   | `1110` | 3      | ✅ 是    | ✓      |
| 15   | `1111` | 4      | ❌ 否    | ✗      |

**輸出：5**

---

### 方法二演示：`left = 6, right = 10`

**mask = 665772 = `...10100010100010101100₂`**

| 數字 | 二進位 | 置位數 c | `1 << c` | `(1 << c) & mask` | 計入？ |
|------|--------|:--------:|----------|:-----------------:|:------:|
| 6    | `110`  | 2 | `0b000100` | ≠ 0（bit 2 為 1）| ✓ |
| 7    | `111`  | 3 | `0b001000` | ≠ 0（bit 3 為 1）| ✓ |
| 8    | `1000` | 1 | `0b000010` | = 0（bit 1 為 0）  | ✗ |
| 9    | `1001` | 2 | `0b000100` | ≠ 0（bit 2 為 1）| ✓ |
| 10   | `1010` | 2 | `0b000100` | ≠ 0（bit 2 為 1）| ✓ |

**輸出：4**

---

### 方法二演示：`left = 10, right = 15`

| 數字 | 二進位 | 置位數 c | `1 << c` | `(1 << c) & mask` | 計入？ |
|------|--------|:--------:|----------|:-----------------:|:------:|
| 10   | `1010` | 2 | `0b000100` | ≠ 0（bit 2 為 1）| ✓ |
| 11   | `1011` | 3 | `0b001000` | ≠ 0（bit 3 為 1）| ✓ |
| 12   | `1100` | 2 | `0b000100` | ≠ 0（bit 2 為 1）| ✓ |
| 13   | `1101` | 3 | `0b001000` | ≠ 0（bit 3 為 1）| ✓ |
| 14   | `1110` | 3 | `0b001000` | ≠ 0（bit 3 為 1）| ✓ |
| 15   | `1111` | 4 | `0b010000` | = 0（bit 4 為 0）  | ✗ |

**輸出：5**

---

## 複雜度分析

### 方法一

| 項目 | 值 |
|------|----|
| 時間複雜度 | O((right − left) × 1) = **O(n)**，其中 n = right − left + 1 |
| 空間複雜度 | **O(1)**，僅使用常數額外空間 |
| `BitCount` 複雜度 | **O(1)**（固定 5 步位元運算） |
| `IsPrime` 複雜度 | **O(√k)**，k 最大為 20，近似 **O(1)** |

### 方法二

| 項目 | 值 |
|------|----|  
| 時間複雜度 | O((right − left) × 1) = **O(n)**，其中 n = right − left + 1 |
| 空間複雜度 | **O(1)**，僅使用常數額外空間 |
| `BitCount` 複雜度 | **O(1)**（固定 5 步位元運算） |
| 質數判斷複雜度 | **O(1)**（單次位元 AND 運算，無迴圈） |

> 方法一與方法二的漸進複雜度相同，但方法二的質數查詢以位元運算取代迴圈，實際常數更小。

---

## 執行方式

```bash
dotnet run --project leetcode_762/leetcode_762.csproj
```

## 參考資料

- [LeetCode 762 (EN)](https://leetcode.com/problems/prime-number-of-set-bits-in-binary-representation/)
- [LeetCode 762 (CN)](https://leetcode.cn/problems/prime-number-of-set-bits-in-binary-representation/)
- [LeetCode 191. Number of 1 Bits](https://leetcode.com/problems/number-of-1-bits/)
- [LeetCode 204. Count Primes](https://leetcode.com/problems/count-primes/)
- [Hamming Weight — Wikipedia](https://en.wikipedia.org/wiki/Hamming_weight)
