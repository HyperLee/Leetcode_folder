# LeetCode 1009 — Complement of Base 10 Integer

A C# solution to [LeetCode 1009](https://leetcode.com/problems/complement-of-base-10-integer/) demonstrating a bitwise manipulation approach to compute the complement of a non-negative integer.

## Problem

> Given a non-negative integer `n`, return its **complement** — the integer obtained by flipping every `0` to `1` and every `1` to `0` in its binary representation.

**Constraints:** `0 <= n < 10^9`

### Examples

| Input `n` | Binary  | Complement binary | Output |
|:---------:|:-------:|:-----------------:|:------:|
| 5         | `101`   | `010`             | 2      |
| 7         | `111`   | `000`             | 0      |
| 10        | `1010`  | `0101`            | 5      |
| 0         | `0`     | `1`               | 1      |

## Solution Approach — Bit Manipulation with Mask

### Why not flip all 32 bits?

A computer stores integers in 32 bits. For example, `n = 5` is internally represented as:

```
0000 0000 0000 0000 0000 0000 0000 0101
```

Flipping all 32 bits would turn the leading zeros into ones, producing the wrong result. We only want to flip the **significant bits** — from the least significant bit up to (and including) the highest set bit.

### Algorithm

1. **Find the position `i` of the highest set bit** by iterating over `[1, 30]`:

$$2^i \leq n < 2^{i+1}$$

2. **Construct a mask** with exactly `i + 1` bits all set to `1`:

$$\text{mask} = 2^{i+1} - 1$$

3. **XOR `n` with the mask** to flip exactly those bits:

| Bit in `n` | Bit in `mask` | `n XOR mask` |
|:----------:|:-------------:|:------------:|
| 0          | 1             | 1 ✓          |
| 1          | 1             | 0 ✓          |
| 0 (high)   | 0             | 0 (unchanged)|

> [!NOTE]
> When `i = 30`, computing `1 << 31` would overflow a signed 32-bit integer.
> The code handles this edge case by using the constant `0x7FFFFFFF` (= $2^{31} - 1$) directly.

### Step-by-step walkthrough: `n = 5`

```
n     = 5  →  binary:  0 … 0  1  0  1
                               ↑
                        highest bit at position i = 2

mask  = 2^(2+1) - 1 = 7  →  binary:  0 … 0  1  1  1

n XOR mask:
    0 … 0  1  0  1   (5)
  ⊕ 0 … 0  1  1  1   (7)
  ─────────────────
    0 … 0  0  1  0   (2)  ✓
```

### Complexity

| Complexity | Value |
|:----------:|:-----:|
| Time       | O(log n) — at most 30 iterations |
| Space      | O(1) — no extra data structures |

## Project Structure

```
leetcode_1009/
├── leetcode_1009.sln
└── leetcode_1009/
    ├── leetcode_1009.csproj
    └── Program.cs          # Solution implementation and test cases
```

## Running the Solution

```bash
dotnet run --project leetcode_1009/leetcode_1009.csproj
```

Expected output:

```
BitwiseComplement(5)  = 2
BitwiseComplement(7)  = 0
BitwiseComplement(10) = 5
BitwiseComplement(0)  = 1
```

## References

- [LeetCode 1009 (English)](https://leetcode.com/problems/complement-of-base-10-integer/description/?envType=daily-question&envId=2026-03-11)
- [LeetCode 1009 (中文)](https://leetcode.cn/problems/complement-of-base-10-integer/description/?envType=daily-question&envId=2026-03-11)
