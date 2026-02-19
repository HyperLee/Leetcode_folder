# LeetCode 696 — Count Binary Substrings

> **Daily Question · 2026-02-19**

A C# solution for [LeetCode 696: Count Binary Substrings](https://leetcode.com/problems/count-binary-substrings/description/?envType=daily-question&envId=2026-02-19).

---

## Problem Description

Given a binary string `s`, return the number of non-empty substrings that satisfy **both** of the following conditions:

1. The substring contains **equal numbers of `0`s and `1`s**.
2. All `0`s and all `1`s in the substring are **grouped consecutively** (no interleaving).

Substrings that appear at different positions are counted separately.

**Constraints:**

- `1 <= s.length <= 10^5`
- `s[i]` is either `'0'` or `'1'`

### Examples

| Input | Output | Explanation |
|-------|--------|-------------|
| `"00110011"` | `6` | Valid substrings: `"0011"`, `"01"`, `"1100"`, `"10"`, `"0011"`, `"01"` |
| `"10101"` | `4` | Valid substrings: `"10"`, `"01"`, `"10"`, `"01"` |

---

## Solution Concept

### Key Observation — Group Consecutive Characters

Split the string into **runs** of identical characters. For example:

```
"000011100"  →  ["0000", "111", "00"]
               lengths: [4, 3, 2]
```

Any valid substring must span **exactly two adjacent groups**. If adjacent groups have lengths `x` and `y`, the number of valid substrings they contribute is:

$$\text{count} = \min(x, y)$$

This is because we can form substrings of lengths $2, 4, \dots, 2 \times \min(x, y)$, each centered at the boundary between the two groups.

### Algorithm

Instead of materialising all groups, a single pass suffices by tracking:

- `prev` — size of the **previous** run
- `curr` — size of the **current** run

Whenever a character transition is detected, `min(prev, curr)` is added to the answer, then `prev` is replaced by `curr` and `curr` is reset to `1`. After the loop, one final accumulation handles the last run.

**Time complexity:** $O(n)$  
**Space complexity:** $O(1)$

---

## Step-by-Step Walkthrough

### Example: `s = "000011100"`

Traverse character by character, building group sizes:

```
Index:   0  1  2  3  4  5  6  7  8
Char:    0  0  0  0  1  1  1  0  0
```

| Step | Event | prev | curr | count |
|------|-------|------|------|-------|
| i=1..3 | same char '0' | 0 | 4 | 0 |
| i=4 | transition 0→1 | 4 | 1 | 0 + min(0,4)=0 |
| i=5..6 | same char '1' | 4 | 3 | 0 |
| i=7 | transition 1→0 | 3 | 1 | 0 + min(4,3)=3 |
| i=8 | same char '0' | 3 | 2 | 3 |
| end | final flush | 3 | 2 | 3 + min(3,2)=5 |

**Result: 5** ✓

The 5 valid substrings are:

```
"0001"  →  positions [3..6]   (1×'0' + 1×'1' core, length 2)
"000111" →  positions [1..6]  (3×'0' + 3×'1', length 6)  — wait
```

More precisely, for adjacent groups `"0000"` and `"111"` (min=3):

| Substring | From group boundary |
|-----------|---------------------|
| `"01"` | 1 char each side |
| `"0011"` | 2 chars each side |
| `"000111"` | 3 chars each side |

For adjacent groups `"111"` and `"00"` (min=2):

| Substring | From group boundary |
|-----------|---------------------|
| `"10"` | 1 char each side |
| `"1100"` | 2 chars each side |

Total = 3 + 2 = **5** ✓

---

## Solution Concept — Method 2: Group and Count

### Key Observation — Materialise All Groups

Split the string into **runs** of identical characters and store each run's length in a `groups` array. For example:

```
"00111011"  →  groups = [2, 3, 1, 2]
                          ↑   ↑  ↑  ↑
                         "00"|"111"|"0"|"11"
```

Because adjacent elements in `groups` always represent **different** characters, any pair of adjacent values `(u, v)` can form exactly $\min(u, v)$ valid substrings:

$$\text{contribution of each adjacent pair} = \min(u,\, v)$$

Summing contributions across all adjacent pairs gives the final answer.

### Algorithm

**Phase 1 — Build the groups array:**
Use a pointer `ptr` to walk through the string. For each new character, count how many consecutive identical characters follow and append that count to `groups`.

**Phase 2 — Accumulate contributions:**
Iterate over all adjacent pairs `(groups[i-1], groups[i])` and add `Math.Min(groups[i-1], groups[i])` to the result.

**Time complexity:** $O(n)$  
**Space complexity:** $O(n)$ — the `groups` array stores at most $n$ elements (contrast with Method 1's $O(1)$ space)

### Comparison with Method 1

| | Method 1 (Two Pointers) | Method 2 (Group Array) |
|---|---|---|
| Space | $O(1)$ | $O(n)$ |
| Passes | 1 | 2 |
| Readability | Compact | More explicit |

---

## Step-by-Step Walkthrough — Method 2

### Example: `s = "00111011"`

**Phase 1 — Build groups:**

```
Index:  0  1  2  3  4  5  6  7
Char:   0  0  1  1  1  0  1  1
```

| ptr range | char | groupLen | groups after |  
|-----------|------|----------|---------------|
| 0 → 2 | `'0'` | 2 | `[2]` |
| 2 → 5 | `'1'` | 3 | `[2, 3]` |
| 5 → 6 | `'0'` | 1 | `[2, 3, 1]` |
| 6 → 8 | `'1'` | 2 | `[2, 3, 1, 2]` |

**Phase 2 — Accumulate contributions:**

| Adjacent pair | u | v | min(u, v) | res |
|---------------|---|---|-----------|-----|
| groups[0], groups[1] | 2 | 3 | 2 | 2 |
| groups[1], groups[2] | 3 | 1 | 1 | 3 |
| groups[2], groups[3] | 1 | 2 | 1 | 4 |

**Result: 4**

The 4 valid substrings are:

| groups pair | min | Substrings formed |
|-------------|-----|-------------------|
| `[2, 3]` → `"00"` & `"111"` | 2 | `"01"`, `"0011"` |
| `[3, 1]` → `"111"` & `"0"` | 1 | `"10"` |
| `[1, 2]` → `"0"` & `"11"` | 1 | `"01"` |

---

## Solution Concept — Method 3: Space-Optimised Group Scan

### Key Observation — Rolling Single Variable

Method 2 materialises a full `groups` array, but during the accumulation phase only **two adjacent values** are ever needed at once — `groups[i-1]` and `groups[i]`. Therefore the entire array can be replaced by a single scalar variable `last` that carries the **previous group's length** into the next iteration.

This insight shrinks space complexity from $O(n)$ to $O(1)$ while keeping the same two-pointer group-scanning logic, combining the best traits of both earlier methods — the explicit readability of Method 2 and the constant space of Method 1.

### Algorithm

Use a pointer `ptr` to walk the string. For each new group:

1. Record the current character `c`.
2. Count consecutive occurrences → `count`.
3. Accumulate `min(count, last)` into the answer.
4. Set `last = count` and proceed to the next group.

When `ptr` reaches the end all adjacent pairs have been processed; no final flush step is needed.

**Time complexity:** $O(n)$  
**Space complexity:** $O(1)$

### Comparison of All Three Methods

| | Method 1 | Method 2 | Method 3 |
|---|---|---|---|
| Space | $O(1)$ | $O(n)$ | $O(1)$ |
| Passes | 1 (index loop) | 2 (build + scan) | 1 (pointer loop) |
| Extra variable | `prev`, `curr` | `groups[]` | `last` |
| Readability | Compact | Most explicit | Balanced |

---

## Step-by-Step Walkthrough — Method 3

### Example: `s = "00111011"`

```
Index:  0  1  2  3  4  5  6  7
Char:   0  0  1  1  1  0  1  1
```

Initial state: `ptr = 0`, `last = 0`, `ans = 0`

| Iteration | ptr range | char | count | min(count, last) | ans | last |
|-----------|-----------|------|-------|-------------------|-----|------|
| 1 | 0 → 2 | `'0'` | 2 | min(2, 0) = 0 | 0 | 2 |
| 2 | 2 → 5 | `'1'` | 3 | min(3, 2) = 2 | 2 | 3 |
| 3 | 5 → 6 | `'0'` | 1 | min(1, 3) = 1 | 3 | 1 |
| 4 | 6 → 8 | `'1'` | 2 | min(2, 1) = 1 | 4 | 2 |

**Result: 4** ✓

Notice that the `last = 0` initialisation ensures the very first group contributes `0`, which is correct — a single isolated group cannot form any valid substring on its own.

The 4 valid substrings are identical to Method 2's result:

| Group pair | Substrings formed |
|------------|-------------------|
| `"00"` & `"111"` (min=2) | `"01"`, `"0011"` |
| `"111"` & `"0"` (min=1) | `"10"` |
| `"0"` & `"11"` (min=1) | `"01"` |

---

## Project Structure

```
leetcode_696/
├── leetcode_696.slnx          # Solution file
└── leetcode_696/
    ├── leetcode_696.csproj    # .NET 10 project
    └── Program.cs             # Solution implementation with test cases
```

## Getting Started

**Prerequisites:** [.NET 10 SDK](https://dotnet.microsoft.com/download)

```bash
# Build
dotnet build

# Run (executes all test cases)
dotnet run --project leetcode_696/leetcode_696.csproj
```

Expected output:

```
輸入: "00110011" → 輸出: 6
輸入: "10101" → 輸出: 4
輸入: "000011100" → 輸出: 5
```

> [!NOTE]
> The loop intentionally starts at index `1` to avoid an out-of-range access when reading `s[i - 1]`.
