# LeetCode 1680 — 連續二進位數字的串接

本專案為 [LeetCode 1680](https://leetcode.com/problems/concatenation-of-consecutive-binary-numbers/description/?envType=daily-question&envId=2026-02-28) 的 C# (.NET 10) 解法，實作兩種解題方式並附有詳細註解說明。

---

## 題目描述

給定一個正整數 `n`，將 `1` 到 `n` 的每個整數的**二進位表示**依序串接成一個二進位字串，回傳其**十進位數值對** $10^9 + 7$ **取餘數**的結果。

**限制條件：** $1 \le n \le 10^5$

### 範例

| n | 二進位串接 | 十進位數值 |
|---|---|---|
| 1 | `"1"` | **1** |
| 3 | `"1" + "10" + "11"` = `"11011"` | **27** |
| 12 | `"1"+"10"+"11"+"100"+...+"1100"` | **505379714** |

---

## 核心概念

串接二進位字串等同於**左移加法**運算：

$$\text{result} = (\text{result} \ll w) + i \pmod{10^9+7}$$

其中 $w$ 為當前整數 $i$ 的位元長度。此方式完全避免字串操作，時間複雜度維持 $O(n)$，空間複雜度為 $O(1)$。

> [!NOTE]
> 由於 $n \le 10^5$ 且位元長度最多可達 17，左移後的數值可能溢位 `int`。
> 因此累加器宣告為 `long`，並在每個步驟對 $10^9+7$ 取餘數。

---

## 解法

### 解法一 — 使用 `BitOperations` 的暴力法

**核心思路：** 利用 `w = 32 - BitOperations.LeadingZeroCount((uint)i)` 直接計算每個 `i` 的位元長度。

```csharp
public int ConcatenatedBinary(int n)
{
    const int MOD = 1_000_000_007;
    long res = 0;
    for (int i = 1; i <= n; i++)
    {
        int w = 32 - BitOperations.LeadingZeroCount((uint)i);
        res = ((res << w) + i) % MOD;
    }
    return (int)res;
}
```

- **時間複雜度：** $O(n)$
- **空間複雜度：** $O(1)$
- `BitOperations.LeadingZeroCount` 在 x86-64 架構上對應單一硬體指令（`BSR`/`LZCNT`），每次呼叫為 $O(1)$。

---

### 解法二 — 遞增位元計數最佳化法

**核心思路：** `i` 的位元長度只有在 `i` 為 2 的冪次時才增加 1，可透過經典技巧 `i & (i - 1) == 0` 在 $O(1)$ 內偵測。維護一個執行中的計數器 `bits`，取代每次迭代都呼叫 `LeadingZeroCount`。

```csharp
public int ConcatenatedBinary2(int n)
{
    const int MOD = 1_000_000_007;
    long res = 0;
    int bits = 0;
    for (int i = 1; i <= n; i++)
    {
        if ((i & (i - 1)) == 0) bits++;   // 2 的冪次邊界
        res = ((res << bits) + i) % MOD;
    }
    return (int)res;
}
```

- **時間複雜度：** $O(n)$
- **空間複雜度：** $O(1)$
- 避免每次迭代進行 API 呼叫；分支具有高度可預測性（僅觸發 $\lfloor\log_2 n\rfloor$ 次）。

---

## 逐步範例

### 範例 A — n = 3

以**解法二**逐步執行 `n = 3`：

| i | 是否為 2 的冪次？ | bits | 位移公式 | res |
|---|---|---|---|---|
| 1 | 是（`1 & 0 == 0`） | 1 | `(0 << 1) + 1` | **1** |
| 2 | 是（`2 & 1 == 0`） | 2 | `(1 << 2) + 2` | **6** |
| 3 | 否 | 2 | `(6 << 2) + 3` | **27** |

二進位驗證：`"1"` + `"10"` + `"11"` = `"11011"` = $1 \times 16 + 1 \times 8 + 0 \times 4 + 1 \times 2 + 1 = 27$ ✓

---

### 範例 B — n = 4

以**解法一**逐步執行 `n = 4`：

| i | w（`LeadingZeroCount`） | 位移公式 | res |
|---|---|---|---|
| 1 | `32-31=1` | `(0 << 1) + 1` | **1** |
| 2 | `32-30=2` | `(1 << 2) + 2` | **6** |
| 3 | `32-30=2` | `(6 << 2) + 3` | **27** |
| 4 | `32-29=3` | `(27 << 3) + 4` | **220** |

二進位驗證：`"1"+"10"+"11"+"100"` = `"110111100"` = $220$ ✓

---

## 執行專案

```bash
dotnet run --project leetcode_1680/leetcode_1680.csproj
```

預期輸出：

```
=== 解法一：BitOperations 暴力法 ===
n=01  result=           1  [PASS]
n=03  result=          27  [PASS]
n=12  result=   505379714  [PASS]

=== 解法二：位元計數最佳化法 ===
n=01  result=           1  [PASS]
n=03  result=          27  [PASS]
n=12  result=   505379714  [PASS]
```

## 環境需求

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
