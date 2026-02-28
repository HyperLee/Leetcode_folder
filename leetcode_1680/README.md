# LeetCode 1680 — Concatenation of Consecutive Binary Numbers

A C# (.NET 10) solution for [LeetCode 1680](https://leetcode.com/problems/concatenation-of-consecutive-binary-numbers/description/?envType=daily-question&envId=2026-02-28), implementing two approaches with detailed annotations.

---

## Problem Description

Given a positive integer `n`, concatenate the **binary representations** of every integer from `1` to `n` in order to form a single binary string, then return its **decimal value modulo** $10^9 + 7$.

**Constraints:** $1 \le n \le 10^5$

### Example

| n | Binary concatenation | Decimal value |
|---|---|---|
| 1 | `"1"` | **1** |
| 3 | `"1" + "10" + "11"` = `"11011"` | **27** |
| 12 | `"1"+"10"+"11"+"100"+...+"1100"` | **505379714** |

---

## Core Concept

Concatenating a binary string is equivalent to a **left-shift + add** operation:

$$\text{result} = (\text{result} \ll w) + i \pmod{10^9+7}$$

where $w$ is the bit-length of the current integer $i$. This avoids string manipulation entirely and keeps the algorithm $O(n)$ time with $O(1)$ space.

> [!NOTE]
> Because $n \le 10^5$ and bit-lengths reach up to 17, the shifted value can overflow `int`.
> The accumulator is declared as `long` and reduced modulo $10^9+7$ at every step.

---

## Solutions

### Solution 1 — Brute-force with `BitOperations`

**Key idea:** Compute the bit-length of each `i` directly via
`w = 32 - BitOperations.LeadingZeroCount((uint)i)`.

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

- **Time complexity:** $O(n)$
- **Space complexity:** $O(1)$
- `BitOperations.LeadingZeroCount` maps to a single hardware instruction (`BSR`/`LZCNT`) on x86-64, so each call is $O(1)$.

---

### Solution 2 — Incremental bit-count optimisation

**Key idea:** The bit-length of `i` increases by 1 **only** when `i` is an exact power of 2, detectable in $O(1)$ via the classic trick `i & (i - 1) == 0`. Maintain a running counter `bits` instead of calling `LeadingZeroCount` every iteration.

```csharp
public int ConcatenatedBinary2(int n)
{
    const int MOD = 1_000_000_007;
    long res = 0;
    int bits = 0;
    for (int i = 1; i <= n; i++)
    {
        if ((i & (i - 1)) == 0) bits++;   // power-of-two boundary
        res = ((res << bits) + i) % MOD;
    }
    return (int)res;
}
```

- **Time complexity:** $O(n)$
- **Space complexity:** $O(1)$
- Avoids the API call on every iteration; the branch is highly predictable (fires only $\lfloor\log_2 n\rfloor$ times).

---

## Worked Examples

### Example A — n = 3

Step through **Solution 2** with `n = 3`:

| i | Is power of 2? | bits | Shift formula | res |
|---|---|---|---|---|
| 1 | Yes (`1 & 0 == 0`) | 1 | `(0 << 1) + 1` | **1** |
| 2 | Yes (`2 & 1 == 0`) | 2 | `(1 << 2) + 2` | **6** |
| 3 | No | 2 | `(6 << 2) + 3` | **27** |

Binary check: `"1"` + `"10"` + `"11"` = `"11011"` = $1 \times 16 + 1 \times 8 + 0 \times 4 + 1 \times 2 + 1 = 27$ ✓

---

### Example B — n = 4

Step through **Solution 1** with `n = 4`:

| i | w (`LeadingZeroCount`) | Shift formula | res |
|---|---|---|---|
| 1 | `32-31=1` | `(0 << 1) + 1` | **1** |
| 2 | `32-30=2` | `(1 << 2) + 2` | **6** |
| 3 | `32-30=2` | `(6 << 2) + 3` | **27** |
| 4 | `32-29=3` | `(27 << 3) + 4` | **220** |

Binary check: `"1"+"10"+"11"+"100"` = `"110111100"` = $220$ ✓

---

## Running the Project

```bash
dotnet run --project leetcode_1680/leetcode_1680.csproj
```

Expected output:

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

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
