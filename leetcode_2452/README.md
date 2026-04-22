# LeetCode 2452 — Words Within Two Edits of Dictionary

> **Daily Question** · 2026-04-22 · Difficulty: Medium

## Problem Description

Given two string arrays `queries` and `dictionary`, where all words in each array have the **same length** and consist of lowercase English letters.

In **one edit**, you can take a word from `queries` and change **any single letter** to any other letter.

Find all words from `queries` that can equal some word from `dictionary` after **at most two edits**.

Return the matching words **in the same order** they appear in `queries`.

**LeetCode links:**
- English: https://leetcode.com/problems/words-within-two-edits-of-dictionary/description/
- 中文: https://leetcode.cn/problems/words-within-two-edits-of-dictionary/description/

### Constraints

- `1 <= queries.length, dictionary.length <= 100`
- `n == queries[i].length == dictionary[j].length`
- `1 <= n <= 100`
- All words consist of lowercase English letters only.

---

## Solution Concept

### Key Insight — Hamming Distance

Because all words share the **same length**, the problem reduces to computing the **Hamming distance** between every pair `(query, dictWord)`.

> The **Hamming distance** between two equal-length strings is the number of positions at which the corresponding characters differ.

If `hammingDistance(query, dictWord) ≤ 2`, the `query` can be transformed into `dictWord` with at most two edits, so it qualifies.

### Approach — Brute Force

Given the small constraints (at most 100 words, each at most 100 characters), an `O(n × m × L)` brute force is efficient enough:

1. For each `query` in `queries`:
2. For each `dictWord` in `dictionary`:
   - Count positions where `query[i] ≠ dictWord[i]` → `diffCount`
3. If `diffCount ≤ 2`, add `query` to the result and break the inner loop (no need to check further).
4. Return the collected result list.

Because we iterate `queries` in order and never reorder the result list, the output order is naturally preserved.

**Time Complexity:** `O(n × m × L)` — n = |queries|, m = |dictionary|, L = word length  
**Space Complexity:** `O(1)` extra (excluding the output list)

---

## Walkthrough Example

### Input

```text
queries    = ["word", "note", "ants", "wood"]
dictionary = ["wood", "joke", "moat"]
```

### Step-by-step Process

We check each `query` against every `dictWord` and count differing characters:

| query | dictWord | Diffs (position) | diffCount | Qualifies? |
|-------|----------|-----------------|-----------|------------|
| `word` | `wood` | pos 2: `r`≠`o` | 1 | ✅ ≤ 2 → add, break |
| `note` | `wood` | pos 0: `n`≠`w`, pos 1: `o`≠`o`✓, pos 2: `t`≠`o`, pos 3: `e`≠`d` → 3 | 3 | ❌ |
| `note` | `joke` | pos 0: `n`≠`j`, pos 2: `t`≠`k` | 2 | ✅ ≤ 2 → add, break |
| `ants` | `wood` | pos 0: `a`≠`w`, pos 1: `n`≠`o`, pos 2: `t`≠`o`, pos 3: `s`≠`d` → 4 | 4 | ❌ |
| `ants` | `joke` | pos 0: `a`≠`j`, pos 1: `n`≠`o`, pos 2: `t`≠`k`, pos 3: `s`≠`e` → 4 | 4 | ❌ |
| `ants` | `moat` | pos 0: `a`≠`m`, pos 1: `n`≠`o`, pos 3: `s`≠`t` | 3 | ❌ → not added |
| `wood` | `wood` | (all same) | 0 | ✅ ≤ 2 → add, break |

### Output

```text
["word", "note", "wood"]
```

### Test Case 2

```text
queries    = ["yes"]
dictionary = ["not"]
```

| query | dictWord | Diffs | diffCount | Qualifies? |
|-------|----------|-------|-----------|------------|
| `yes` | `not` | pos 0: `y`≠`n`, pos 1: `e`≠`o`, pos 2: `s`≠`t` | 3 | ❌ |

**Output:** `[]`

---

## Running the Project

```bash
# Build
dotnet build leetcode_2452/leetcode_2452.csproj

# Run
dotnet run --project leetcode_2452/leetcode_2452.csproj
```

Expected output:

```
Test 1: [word, note, wood]
Test 2: []
```
