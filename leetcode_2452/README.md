# LeetCode 2452 — 兩次編輯內的字典單詞

> **每日題目** · 2026-04-22 · 難度：中等

## 題目描述

給定兩個字串陣列 `queries` 和 `dictionary`，其中每個陣列中的所有單詞長度**相同**，且只由小寫英文字母組成。

**一次編輯**是指從 `queries` 中取出一個單詞，將其中**任意一個字母**替換成另一個字母。

找出 `queries` 中所有能在**至多兩次編輯**後等於 `dictionary` 中某個單詞的單詞。

回傳符合條件的單詞，**順序與其在 `queries` 中出現的順序相同**。

**LeetCode 連結：**
- English: https://leetcode.com/problems/words-within-two-edits-of-dictionary/description/
- 中文: https://leetcode.cn/problems/words-within-two-edits-of-dictionary/description/

### 限制條件

- `1 <= queries.length, dictionary.length <= 100`
- `n == queries[i].length == dictionary[j].length`
- `1 <= n <= 100`
- 所有單詞僅由小寫英文字母組成。

---

## 解題思路

### 核心觀察 — 漢明距離（Hamming Distance）

由於所有單詞長度**相同**，問題可以轉化為計算每對 `(query, dictWord)` 之間的**漢明距離**。

> **漢明距離**是指兩個等長字串中，對應位置字元不同的位置數量。

若 `hammingDistance(query, dictWord) ≤ 2`，則該 `query` 最多經過兩次編輯即可變成 `dictWord`，符合條件。

### 解法 — 暴力枚舉

由於限制條件較小（最多 100 個單詞，每個單詞最多 100 個字元），`O(n × m × L)` 的暴力枚舉已足夠：

1. 對 `queries` 中的每個 `query`：
2. 對 `dictionary` 中的每個 `dictWord`：
   - 計算 `query[i] ≠ dictWord[i]` 的位置數量 → `diffCount`
3. 若 `diffCount ≤ 2`，將 `query` 加入結果並跳出內層迴圈（不需再繼續比對）。
4. 回傳收集到的結果列表。

由於我們按順序迭代 `queries` 且從不重新排序結果列表，輸出順序自然保留。

**時間複雜度：** `O(n × m × L)` — n = |queries|，m = |dictionary|，L = 單詞長度  
**空間複雜度：** `O(1)` 額外空間（不含輸出列表）

---

## 逐步範例

### 輸入

```text
queries    = ["word", "note", "ants", "wood"]
dictionary = ["wood", "joke", "moat"]
```

### 逐步流程

逐一比對每個 `query` 與每個 `dictWord`，計算不同字元的數量：

| query | dictWord | 差異位置 | diffCount | 是否符合？ |
|-------|----------|---------|-----------|-----------|
| `word` | `wood` | 位置 2：`r`≠`o` | 1 | ✅ ≤ 2 → 加入，跳出 |
| `note` | `wood` | 位置 0：`n`≠`w`，位置 1：`o`≠`o`✓，位置 2：`t`≠`o`，位置 3：`e`≠`d` → 3 | 3 | ❌ |
| `note` | `joke` | 位置 0：`n`≠`j`，位置 2：`t`≠`k` | 2 | ✅ ≤ 2 → 加入，跳出 |
| `ants` | `wood` | 位置 0：`a`≠`w`，位置 1：`n`≠`o`，位置 2：`t`≠`o`，位置 3：`s`≠`d` → 4 | 4 | ❌ |
| `ants` | `joke` | 位置 0：`a`≠`j`，位置 1：`n`≠`o`，位置 2：`t`≠`k`，位置 3：`s`≠`e` → 4 | 4 | ❌ |
| `ants` | `moat` | 位置 0：`a`≠`m`，位置 1：`n`≠`o`，位置 3：`s`≠`t` | 3 | ❌ → 不加入 |
| `wood` | `wood` | （全部相同） | 0 | ✅ ≤ 2 → 加入，跳出 |

### 輸出

```text
["word", "note", "wood"]
```

### 測試案例 2

```text
queries    = ["yes"]
dictionary = ["not"]
```

| query | dictWord | 差異 | diffCount | 是否符合？ |
|-------|----------|------|-----------|-----------|
| `yes` | `not` | 位置 0：`y`≠`n`，位置 1：`e`≠`o`，位置 2：`s`≠`t` | 3 | ❌ |

**輸出：** `[]`

---

## 執行專案

```bash
# 建構
dotnet build leetcode_2452/leetcode_2452.csproj

# 執行
dotnet run --project leetcode_2452/leetcode_2452.csproj
```

預期輸出：

```
Test 1: [word, note, wood]
Test 2: []
```
