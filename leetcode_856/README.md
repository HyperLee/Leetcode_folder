# LeetCode 856 — Score of Parentheses

A C# solution set for [LeetCode 856: Score of Parentheses](https://leetcode.com/problems/score-of-parentheses/description/), demonstrating three distinct algorithmic approaches with detailed explanations.

---

## Problem Statement

Given a **balanced parentheses string** `s`, return its score according to the following rules:

| Rule | Score |
|------|-------|
| `()` | 1 |
| `AB` | `score(A) + score(B)` |
| `(A)` | `2 × score(A)` |

**Constraints:** `2 <= s.length <= 50`, `s` consists of `(` and `)` only, `s` is a balanced parentheses string.

### Examples

```
Input: "()"       → Output: 1
Input: "(())"     → Output: 2
Input: "()()"     → Output: 2
Input: "(()(()))→ Output: 6
```

---

## Core Insight

No matter how deeply nested, every `()` pair at depth `k` contributes exactly $2^k$ to the final score (where depth is the number of enclosing bracket pairs). The three methods below each exploit this insight differently.

---

## Approaches

### Method 1 — Bit-shift Depth Counting

**Concept:** Avoid storing intermediate results altogether. As one traverses the string, maintain a depth counter `cnt`. Each time a bare `()` is encountered (i.e., `s[i-1] == '('` when seeing `)`), add `1 << cnt` to the score.

**Why it works:** `cnt` at that moment equals the nesting depth of the innermost `()`, so `1 << cnt` equals $2^{cnt}$, the exact contribution of that pair.

| Complexity | Value |
|-----------|-------|
| Time | O(n) |
| Space | **O(1)** — only two integer variables |

```csharp
public int ScoreOfParentheses(string s)
{
    int score = 0, cnt = 0;
    for (int i = 0; i < s.Length; i++)
    {
        if (s[i] == '(') cnt++;
        else
        {
            cnt--;
            if (s[i - 1] == '(')
                score += 1 << cnt;   // 2^depth
        }
    }
    return score;
}
```

---

### Method 2 — Stack Simulation

**Concept:** Treat the string as an empty string plus `s` itself and assign each layer of brackets its own "score frame" on a stack.

- `(` → push `0` (open a new frame)
- `)` → pop `v` (inner score); pop `w` (outer accumulated); push `w + max(2*v, 1)`
  - If `v == 0` the inner content is empty, so `()` scores 1.
  - Otherwise, `(A)` scores `2 * score(A)`.

| Complexity | Value |
|-----------|-------|
| Time | O(n) |
| Space | O(n) — stack depth up to n/2 |

```csharp
public int ScoreOfParentheses2(string s)
{
    var stack = new Stack<int>();
    stack.Push(0);
    foreach (char c in s)
    {
        if (c == '(') stack.Push(0);
        else
        {
            int v = stack.Pop();
            int w = stack.Pop();
            stack.Push(w + Math.Max(2 * v, 1));
        }
    }
    return stack.Peek();
}
```

---

### Method 3 — Divide and Conquer

**Concept:** Every balanced parentheses string must be in one of two forms:

- **`(A)`** — the entire string is wrapped by one outermost pair.
- **`A + B`** — two or more independent balanced sub-strings concatenated.

Use a running balance (`+1` for `(`, `−1` for `)`) to locate the first point where `bal == 0`. That split point determines the form.

| Complexity | Value |
|-----------|-------|
| Time | O(n²) worst case |
| Space | O(n) — recursion stack |

```csharp
public int ScoreOfParentheses3(string s)
{
    if (s.Length == 2) return 1;    // base case: "()"

    int bal = 0, n = s.Length, len = 0;
    for (int i = 0; i < n; i++)
    {
        bal += (s[i] == '(' ? 1 : -1);
        if (bal == 0) { len = i + 1; break; }
    }

    return len == n
        ? 2 * ScoreOfParentheses3(s.Substring(1, n - 2))   // (A)
        : ScoreOfParentheses3(s.Substring(0, len))           // A
          + ScoreOfParentheses3(s.Substring(len));           // + B
}
```

---

## Walkthrough Examples

### Example 1 — `"()()"` → 2

#### Method 1 (Bit-shift)

| i | char | cnt | s[i-1] | score |
|---|------|-----|--------|-------|
| 0 | `(`  | 1   | —      | 0     |
| 1 | `)`  | 0   | `(`  ✓ | **1** (`1<<0`) |
| 2 | `(`  | 1   | —      | 1     |
| 3 | `)`  | 0   | `(`  ✓ | **2** (`1<<0`) |

Result: **2** ✓

#### Method 2 (Stack)

```
init  → [0]
'('   → [0, 0]
')'   → v=0, w=0 → push max(0,1)=1 → [1]
'('   → [1, 0]
')'   → v=0, w=1 → push 1+1=2    → [2]
peek  → 2
```

Result: **2** ✓

#### Method 3 (Divide & Conquer)

```
"()()"  len=2, n=4 → A+B form
  A = "()"  → 1
  B = "()"  → 1
  A+B = 2
```

Result: **2** ✓

---

### Example 2 — `"(())"` → 2

#### Method 1 (Bit-shift)

| i | char | cnt | s[i-1] | score |
|---|------|-----|--------|-------|
| 0 | `(`  | 1   | —      | 0     |
| 1 | `(`  | 2   | —      | 0     |
| 2 | `)`  | 1   | `(`  ✓ | **2** (`1<<1`) |
| 3 | `)`  | 0   | `)`  ✗ | 2     |

Result: **2** ✓

#### Method 2 (Stack)

```
init  → [0]
'('   → [0, 0]
'('   → [0, 0, 0]
')'   → v=0, w=0 → push max(0,1)=1  → [0, 1]
')'   → v=1, w=0 → push 0+max(2,1)=2→ [2]
peek  → 2
```

Result: **2** ✓

#### Method 3 (Divide & Conquer)

```
"(())"  bal reaches 0 at i=3, len=4=n → (A) form
  A = "()"  → 1
  2 * 1 = 2
```

Result: **2** ✓

---

### Example 3 — `"(()(()))"` → 6

#### Method 1 (Bit-shift)

| i | char | cnt | s[i-1] | score |
|---|------|-----|--------|-------|
| 0 | `(`  | 1   | —      | 0     |
| 1 | `(`  | 2   | —      | 0     |
| 2 | `)`  | 1   | `(`  ✓ | **2** (`1<<1`) |
| 3 | `(`  | 2   | —      | 2     |
| 4 | `(`  | 3   | —      | 2     |
| 5 | `)`  | 2   | `(`  ✓ | **6** (`1<<2=4`, total 2+4) |
| 6 | `)`  | 1   | `)`  ✗ | 6     |
| 7 | `)`  | 0   | `)`  ✗ | 6     |

Result: **6** ✓

#### Method 2 (Stack)

```
init      → [0]
'('       → [0, 0]
'('       → [0, 0, 0]
')'       → v=0, w=0 → push 1       → [0, 1]
'('       → [0, 1, 0]
'('       → [0, 1, 0, 0]
')'       → v=0, w=0 → push 1       → [0, 1, 1]
')'       → v=1, w=1 → push 1+2=3   → [0, 3]
')'       → v=3, w=0 → push 0+6=6   → [6]
peek      → 6
```

Result: **6** ✓

#### Method 3 (Divide & Conquer)

```
"(()(()))"  len=8=n → (A) form
  A = "()(())"
    len=2, A+B form
      A = "()"         → 1
      B = "(())"
        len=4=n → (A) form
          inner = "()" → 1
          2 * 1 = 2
      A+B = 1+2 = 3
  2 * 3 = 6
```

Result: **6** ✓

---

## Comparison

| Method | Time | Space | Key Data Structure | Best Suited For |
|--------|------|-------|--------------------|-----------------|
| 1 — Bit-shift | O(n) | **O(1)** | Variables only | Competitive / memory-critical |
| 2 — Stack | O(n) | O(n)  | Stack | Clean and generalizable |
| 3 — Divide & Conquer | O(n²) | O(n) | Recursion | Conceptual understanding |

> [!TIP]
> Method 1 is the most efficient. Method 2 is the most idiomatic for stack-based bracket problems. Method 3 directly mirrors the recursive definition in the problem statement, making it the easiest to reason about from first principles.

---

## Getting Started

```bash
# Build
dotnet build leetcode_856/leetcode_856.csproj

# Run
dotnet run --project leetcode_856/leetcode_856.csproj
```

Expected output:

```
=== LeetCode 856: Score of Parentheses ===

Input: "()" | Expected: 1
  方法一 (位元計算): 1 [PASS]
  方法二 (堆疊):     1 [PASS]
  方法三 (分治):     1 [PASS]

Input: "(())" | Expected: 2
  ...
```
