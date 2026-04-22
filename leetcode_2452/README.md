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

### 解法一 — 暴力枚舉

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

### 解法二 — Trie + DFS 剪枝

**概念與出發點**

暴力解每次比較都獨立進行，無法利用 dictionary 單詞之間共同的前綴資訊。若多個 dictionary 單詞共享前綴（例如 `"wood"` 與 `"wore"`），當某個前綴的累積差異已超過 2，Trie 可一次捨棄所有以此前綴為根的後續節點，達到**共用前綴、提前剪枝**的效果，大幅減少比較次數。

**詳細解法**

1. 將 `dictionary` 所有單詞插入 Trie（前綴樹），共用相同前綴節點以節省空間。
2. 對每個 `query`，以 **DFS（深度優先搜尋）** 遍歷 Trie：
   - 記錄當前字元位置 `pos` 與累積不匹配次數 `edits`。
   - **剪枝條件**：若 `edits > 2`，立即放棄此分支，不再遞迴深入。
   - **成功條件**：若 `pos == query.Length` 且當前節點標記為單詞結尾（`isEnd = true`），代表找到匹配。
3. 對每個位置，嘗試 Trie 中所有存在的子節點（最多 26 個），字元相同則 `edits` 不變，不同則 `edits + 1`。

**時間複雜度：** `O(m×L)` 建 Trie + `O(n×L×26²)` 查詢（剪枝後實際遠低於此上界）  
**空間複雜度：** `O(m×L×26)`（Trie 節點數）

---

### 解法三 — 萬用字元雜湊（Wildcard Hashing）

**概念與出發點**

若 query 與某個 dictWord 在位置 `i`、`j` 不同（漢明距離 = 2），則將兩個單詞的位置 `i` 和 `j` 都替換為萬用字元 `*` 後，所得的模式字串**完全相同**。因此，可以預先對 dictionary 所有單詞產生所有可能的「0/1/2 個萬用字元」模式，存入 `HashSet`，查詢時只需 O(L²) 產生模式並 O(1) 查表，大幅加快批次查詢速度。

**詳細解法**

1. **預處理**：對 `dictionary` 中每個長度為 L 的單詞，產生以下模式並存入 `HashSet<string>`：
   - **0 個萬用字元**：原始單詞（涵蓋漢明距離 = 0 的完全相同情況）
   - **1 個萬用字元**：依序將每個位置替換為 `*`（共 L 個模式，涵蓋漢明距離 ≤ 1）
   - **2 個萬用字元**：選取任意兩個位置替換為 `*`（共 C(L,2) 個模式，涵蓋漢明距離 ≤ 2）
2. **查詢**：對每個 `query`，同樣產生上述共 $1 + L + \binom{L}{2}$ 個模式，若任一模式存在於 HashSet，則此 query 符合條件。

**時間複雜度：** `O((m+n)×L²)` — m = |dictionary|，n = |queries|，L = 字串長度  
**空間複雜度：** `O(m×L²)`（HashSet 中儲存的模式數量）

---

## 逐步範例

### 解法一：暴力枚舉

#### 輸入

```text
queries    = ["word", "note", "ants", "wood"]
dictionary = ["wood", "joke", "moat"]
```

#### 逐步流程

逐一比對每個 `query` 與每個 `dictWord`，計算不同字元的數量：

| query | dictWord | 差異位置 | diffCount | 是否符合？ |
|-------|----------|---------|-----------|-----------|
| `word` | `wood` | 位置 2：`r`≠`o` | 1 | ✅ ≤ 2 → 加入，跳出 |
| `note` | `wood` | 位置 0：`n`≠`w`，位置 2：`t`≠`o`，位置 3：`e`≠`d` | 3 | ❌ |
| `note` | `joke` | 位置 0：`n`≠`j`，位置 2：`t`≠`k` | 2 | ✅ ≤ 2 → 加入，跳出 |
| `ants` | `wood` | 位置 0：`a`≠`w`，位置 1：`n`≠`o`，位置 2：`t`≠`o`，位置 3：`s`≠`d` | 4 | ❌ |
| `ants` | `joke` | 位置 0：`a`≠`j`，位置 1：`n`≠`o`，位置 2：`t`≠`k`，位置 3：`s`≠`e` | 4 | ❌ |
| `ants` | `moat` | 位置 0：`a`≠`m`，位置 1：`n`≠`o`，位置 3：`s`≠`t` | 3 | ❌ → 不加入 |
| `wood` | `wood` | （全部相同） | 0 | ✅ ≤ 2 → 加入，跳出 |

#### 輸出

```text
["word", "note", "wood"]
```

#### 測試案例 2

```text
queries    = ["yes"]
dictionary = ["not"]
```

| query | dictWord | 差異 | diffCount | 是否符合？ |
|-------|----------|------|-----------|-----------|
| `yes` | `not` | 位置 0：`y`≠`n`，位置 1：`e`≠`o`，位置 2：`s`≠`t` | 3 | ❌ |

**輸出：** `[]`

---

### 解法二：Trie + DFS 剪枝

#### 輸入

```text
queries    = ["word", "note", "ants", "wood"]
dictionary = ["wood", "joke", "moat"]
```

#### Trie 結構（插入 dictionary 後）

```
root
├─ j → o → k → e [END]
├─ m → o → a → t [END]
└─ w → o → o → d [END]
```

#### DFS 追蹤（以查詢 `"ants"` 為例，展示剪枝效果）

| pos | 當前 Trie 分支 | query[pos] | 累積 edits | 動作 |
|-----|--------------|-----------|-----------|------|
| 0 | root → `j` | `a` | 0+1=1 | 繼續 |
| 1 | `j` → `o` | `n` | 1+1=2 | 繼續 |
| 2 | `j→o` → `k` | `t` | 2+1=**3** > 2 | ✂️ 剪枝，放棄此分支 |
| 0 | root → `m` | `a` | 0+1=1 | 繼續 |
| 1 | `m` → `o` | `n` | 1+1=2 | 繼續 |
| 2 | `m→o` → `a` | `t` | 2+1=**3** > 2 | ✂️ 剪枝，放棄此分支 |
| 0 | root → `w` | `a` | 0+1=1 | 繼續 |
| 1 | `w` → `o` | `n` | 1+1=2 | 繼續 |
| 2 | `w→o` → `o` | `t` | 2+1=**3** > 2 | ✂️ 剪枝，放棄此分支 |

所有分支均在 pos=2 就被剪枝，`"ants"` 不符合條件，**完全不需要抵達任何葉節點**。

相較之下，查詢 `"word"` 時沿 `w→o→o→d` 分支，僅在 pos=2 遇到 `r`≠`o`（edits=1），最終抵達 `[END]` 且 edits=1 ≤ 2，成功匹配。

#### 輸出

```text
["word", "note", "wood"]
```

---

### 解法三：萬用字元雜湊

#### 輸入

```text
queries    = ["word", "note", "ants", "wood"]
dictionary = ["wood", "joke", "moat"]
```

#### 預處理：為 `"wood"` 產生的萬用字元模式

| 萬用字元數 | 產生的模式 |
|-----------|----------|
| 0 個 | `wood` |
| 1 個 | `*ood`、`w*od`、`wo*d`、`woo*` |
| 2 個 | `**od`、`*o*d`、`*oo*`、`w**d`、`w*o*`、`wo**` |

`"joke"` 與 `"moat"` 同樣各自產生模式並存入 HashSet，此處省略。

#### 查詢：以 `"word"` 為例

為 `"word"` 產生的模式包含 `*o*d`（位置 0 和 2 替換為 `*`）。  
`"wood"` 的模式同樣包含 `*o*d`（位置 0 和 2 替換為 `*`）。  
兩者在 HashSet 中找到**相同模式** `*o*d` → 匹配成功，`"word"` 加入結果。

#### 所有查詢結果摘要

| query | 匹配模式 | 對應 dictWord | 是否符合？ |
|-------|---------|-------------|----------|
| `word` | `*o*d` | `wood` | ✅ |
| `note` | `*o*e` | `joke` | ✅ |
| `ants` | — | — | ❌ |
| `wood` | `wood` | `wood` | ✅ |

#### 輸出

```text
["word", "note", "wood"]
```

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
Test 1 (暴力解): [word, note, wood]
Test 1 (Trie):   [word, note, wood]
Test 1 (雜湊):   [word, note, wood]
Test 2 (暴力解): []
Test 2 (Trie):   []
Test 2 (雜湊):   []
```
