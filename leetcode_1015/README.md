# LeetCode 1015: Smallest Integer Divisible by K

[![C#](https://img.shields.io/badge/C%23-13.0-purple?style=flat-square&logo=csharp)](https://docs.microsoft.com/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/.NET-8.0-blue?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![LeetCode](https://img.shields.io/badge/LeetCode-Medium-orange?style=flat-square&logo=leetcode)](https://leetcode.com/problems/smallest-integer-divisible-by-k/)

## 📝 題目描述

給定一個正整數 `k`，找出**最小正整數 `n`**，使得：
1. `n` 可以被 `k` 整除
2. `n` 的十進位表示**僅包含數字 1**（例如：1, 11, 111, 1111, ...）

回傳 `n` 的**長度**（即數字 1 的個數）。如果不存在這樣的 `n`，則回傳 `-1`。

> [!NOTE]
> 這種僅由數字 1 組成的數字稱為 **Repunit**（重複單位數），源自英文 "repeated unit"。

### 題目連結

- 🔗 [LeetCode (English)](https://leetcode.com/problems/smallest-integer-divisible-by-k/)
- 🔗 [力扣 (中文)](https://leetcode.cn/problems/smallest-integer-divisible-by-k/)

### 範例

| 輸入 | 輸出 | 說明 |
|:----:|:----:|:-----|
| `k = 1` | `1` | 最小的 Repunit 是 `1`，`1 % 1 = 0` ✓ |
| `k = 2` | `-1` | 任何 Repunit 的個位數都是 1，無法被 2 整除 |
| `k = 3` | `3` | `111 % 3 = 0`，長度為 3 |
| `k = 7` | `6` | `111111 % 7 = 0`，長度為 6 |

### 限制條件

- `1 <= k <= 10^5`

---

## 💡 解題出發點

### 問題分析

首先，讓我們觀察 Repunit 的特性：

```
R(1) = 1
R(2) = 11
R(3) = 111
R(4) = 1111
...
```

我們可以發現一個重要的**遞推關係**：

$$R(n) = R(n-1) \times 10 + 1$$

例如：
- $R(2) = R(1) \times 10 + 1 = 1 \times 10 + 1 = 11$
- $R(3) = R(2) \times 10 + 1 = 11 \times 10 + 1 = 111$

### 直覺解法的問題

最直覺的想法是：直接產生 Repunit 並檢查是否可被 `k` 整除。

```csharp
// ⚠️ 這個方法會溢位！
long n = 1;
int length = 1;
while (n % k != 0) {
    n = n * 10 + 1;  // 很快就會超過 long 的範圍
    length++;
}
```

**問題**：當 `k` 很大時，對應的 Repunit 可能非常長（最多 `k` 位數），遠超過 64-bit 整數範圍！

### 關鍵洞察：餘數運算

我們其實**不需要儲存整個 Repunit**，只需要追蹤它除以 `k` 的**餘數**即可！

根據模運算的性質：

$$(a \times 10 + 1) \mod k = ((a \mod k) \times 10 + 1) \mod k$$

這意味著我們可以用餘數來遞推：

$$remainder(n) = (remainder(n-1) \times 10 + 1) \mod k$$

當 `remainder == 0` 時，就找到了答案！

---

## 🔍 解法流程與詳細步驟推演

### 步驟 1：排除無解情況

**關鍵觀察**：如果 `k` 可以被 2 或 5 整除，則**必定無解**。

**證明**：
- 任何 Repunit 的個位數都是 `1`
- 能被 2 整除的數，個位數必須是 0, 2, 4, 6, 8
- 能被 5 整除的數，個位數必須是 0 或 5
- 因此，個位數為 1 的數**不可能**被 2 或 5 整除

```csharp
if (k % 2 == 0 || k % 5 == 0) return -1;
```

### 步驟 2：初始化

```csharp
int remainder = 1 % k;  // R(1) = 1 的餘數
int length = 1;         // 當前 Repunit 的長度
```

### 步驟 3：迭代尋找答案

使用餘數遞推公式，持續尋找餘數為 0 的情況：

```csharp
while (remainder != 0 && length <= k)
{
    remainder = (remainder * 10 + 1) % k;
    length++;
}
```

**為什麼最多只需要 `k` 次迭代？**

根據**鴿籠原理**（Pigeonhole Principle）：
- 餘數只可能是 0 到 k-1，共 `k` 種可能值
- 如果在 `k` 次迭代內沒有出現餘數為 0，則必定出現重複的餘數
- 一旦餘數開始重複，就會進入循環，永遠找不到 0

> [!IMPORTANT]
> 實際上，若 `k` 不是 2 或 5 的倍數，在 `k` 次迭代內**必定**能找到餘數為 0 的情況。這是數論中的一個定理。

### 步驟 4：回傳結果

```csharp
return remainder == 0 ? length : -1;
```

---

## 📊 詳細推演範例

### 範例 1：k = 3

| 迭代 | Repunit | 計算過程 | 餘數 |
|:----:|:-------:|:---------|:----:|
| 1 | 1 | `1 % 3` | 1 |
| 2 | 11 | `(1 × 10 + 1) % 3 = 11 % 3` | 2 |
| 3 | 111 | `(2 × 10 + 1) % 3 = 21 % 3` | **0** ✓ |

**答案**：3（即 `111` 可被 3 整除）

### 範例 2：k = 7

| 迭代 | Repunit | 計算過程 | 餘數 |
|:----:|:-------:|:---------|:----:|
| 1 | 1 | `1 % 7` | 1 |
| 2 | 11 | `(1 × 10 + 1) % 7 = 11 % 7` | 4 |
| 3 | 111 | `(4 × 10 + 1) % 7 = 41 % 7` | 6 |
| 4 | 1111 | `(6 × 10 + 1) % 7 = 61 % 7` | 5 |
| 5 | 11111 | `(5 × 10 + 1) % 7 = 51 % 7` | 2 |
| 6 | 111111 | `(2 × 10 + 1) % 7 = 21 % 7` | **0** ✓ |

**答案**：6（即 `111111` 可被 7 整除）

### 範例 3：k = 2（無解情況）

由於 `2 % 2 == 0`，直接返回 `-1`。

**驗證**：
- `1 % 2 = 1` ✗
- `11 % 2 = 1` ✗
- `111 % 2 = 1` ✗
- ...（所有 Repunit 除以 2 的餘數都是 1）

---

## 🧮 複雜度分析

| 項目 | 複雜度 | 說明 |
|:----:|:------:|:-----|
| **時間複雜度** | $O(k)$ | 最多迭代 `k` 次 |
| **空間複雜度** | $O(1)$ | 只使用常數額外空間 |

---

## 💻 完整程式碼

```csharp
/// <summary>
/// 返回僅由數字 1 組成且可被 k 整除的最短正整數的長度。
/// 若無解則返回 -1。
/// </summary>
static int SmallestRepunitDivByK(int k)
{
    // 無效輸入檢查
    if (k <= 0) return -1;

    // 若 k 可被 2 或 5 整除，則無解
    if (k % 2 == 0 || k % 5 == 0) return -1;

    int remainder = 1 % k;
    int length = 1;

    while (remainder != 0 && length <= k)
    {
        remainder = (remainder * 10 + 1) % k;
        length++;
    }

    return remainder == 0 ? length : -1;
}
```

---

## 🔗 相關知識

### Repunit（重複單位數）

Repunit 是一個數論概念，指的是僅由數字 1 組成的正整數。在不同進位制下都有 Repunit：

- 十進位：1, 11, 111, 1111, ...
- 二進位：1, 11, 111, ... (即 1, 3, 7, ... 的十進位表示)

### 鴿籠原理

鴿籠原理（Pigeonhole Principle）指出：如果有 n+1 個物體放入 n 個容器，則至少有一個容器包含兩個或更多物體。

在本題中：
- 餘數只有 k 種可能（0 到 k-1）
- 如果計算 k+1 次餘數，必定有重複
- 重複意味著進入循環，永遠找不到 0

---

## 📚 參考資源

- [Wikipedia - Repunit](https://en.wikipedia.org/wiki/Repunit)
- [Pigeonhole Principle](https://en.wikipedia.org/wiki/Pigeonhole_principle)

---

## 🏃 執行方式

```bash
# 建構專案
dotnet build

# 執行程式
dotnet run
```

---

<div align="center">

**Happy Coding!** 🎉

</div>
