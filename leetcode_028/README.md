# LeetCode 028 - Find the Index of the First Occurrence in a String

以 C# 實作 LeetCode 28，示範如何在 `haystack` 中找出 `needle` 第一次出現的位置。本專案目前提供兩種解法：

- 暴力法
- KMP（Knuth-Morris-Pratt）

主程式會直接執行固定測資，列出兩種解法的結果，方便比對輸出與理解解題流程。

## 題目說明

給定兩個字串 `haystack` 和 `needle`，回傳 `needle` 在 `haystack` 中第一次出現的索引；如果不存在，則回傳 `-1`。

題目連結：

- LeetCode: <https://leetcode.com/problems/find-the-index-of-the-first-occurrence-in-a-string/description/>
- LeetCode 中文站: <https://leetcode.cn/problems/find-the-index-of-the-first-occurrence-in-a-string/description/>

## 限制條件

- `1 <= haystack.length, needle.length <= 10^4`
- `haystack` 和 `needle` 只包含小寫英文字母

## 解題概念與出發點

最直接的想法，是把 `needle` 放到 `haystack` 的每一個可能起點上逐字比較；這樣容易寫，也容易驗證正確性，但一旦比對失敗，就會重複檢查很多已經看過的字元。

進一步觀察可發現，當模式字串前半段已經匹配成功時，若在中途失敗，並不一定要整段重來。KMP 正是利用這個性質，先對 `needle` 預處理出 LPS（Longest Prefix Suffix）陣列，讓指標可以跳到下一個仍然可能成立的位置，將時間複雜度降為線性。

## 解法一：暴力法

### 設計說明

- 從 `haystack` 的每個可能起點開始檢查
- 逐字比較對應位置是否和 `needle` 相同
- 一旦遇到不相同的字元，立即中止目前起點的比對，換下一個起點
- 第一個完整匹配的位置就是答案；全部檢查完仍未匹配則回傳 `-1`

### 複雜度

- 時間複雜度：`O(n * m)`
- 空間複雜度：`O(1)`

### 範例演示流程

以 `haystack = "sadbutsad"`、`needle = "sad"` 為例：

1. 從索引 `0` 開始，比較 `haystack[0..2]` 與 `"sad"`。
2. `s == s`、`a == a`、`d == d`，完整匹配成功。
3. 因為第一個起點就成功，所以直接回傳 `0`，不需要再檢查索引 `1` 之後的位置。

## 解法二：KMP

### 設計說明

- 先為 `needle` 建立 LPS 陣列，記錄每個位置結尾時「最長相同真前綴與真後綴長度」
- 使用兩個指標同步走訪 `haystack` 與 `needle`
- 若字元相同，兩個指標一起前進
- 若字元不同且 `needle` 指標不在起點，依照 LPS 回退 `needle` 指標，不重複比較已知可重用的部分
- 若 `needle` 指標走完整段字串，表示找到第一個完整匹配

### 複雜度

- 時間複雜度：`O(n + m)`
- 空間複雜度：`O(m)`

### 範例演示流程

以 `haystack = "mississippi"`、`needle = "issip"` 為例：

1. 先為 `needle = "issip"` 建立 LPS 陣列，結果為 `[0, 0, 0, 1, 0]`。
2. 從 `haystack[0] = 'm'` 開始比較，和 `needle[0] = 'i'` 不同，因此 `haystack` 指標前進。
3. 到 `haystack[1] = 'i'` 後，依序匹配 `i`、`s`、`s`、`i`、`p`。
4. 當 `needle` 指標走到結尾時，代表完整匹配成功。
5. 回傳起始索引 `6 - 5 + 1 = 4`，也就是 `"issip"` 第一次出現在 `haystack` 的位置。

### LPS 是什麼

這裡的正確縮寫是 `LPS`，不是 `LSP`。`LPS` 全名是 `Longest Prefix Suffix`，意思是：

- `LPS[i]` 看的是 `pattern[0..i]` 這一段
- 它記錄的是「最長的真前綴」和「真後綴」相同時的長度
- 這裡的「真」代表不能把整段字串自己算進去

先釐清名詞：

- 前綴（prefix）：從字串開頭往右取出的部分
- 後綴（suffix）：從字串結尾往左對齊的部分
- 真前綴 / 真後綴（proper prefix / proper suffix）：不能等於整個字串本身

例如字串 `abab`：

- 前綴有 `a`、`ab`、`aba`、`abab`
- 後綴有 `b`、`ab`、`bab`、`abab`
- 如果排除整段字串本身，最長同時是前綴又是後綴的是 `ab`
- 所以這一段的 LPS 長度就是 `2`

用 `pattern = "ababaca"` 來看，每一格 `LPS[i]` 的意思如下：

| `i` | `pattern[0..i]` | 最長相同真前綴 / 真後綴 | `LPS[i]` |
| --- | --- | --- | --- |
| `0` | `a` | 無 | `0` |
| `1` | `ab` | 無 | `0` |
| `2` | `aba` | `a` | `1` |
| `3` | `abab` | `ab` | `2` |
| `4` | `ababa` | `aba` | `3` |
| `5` | `ababac` | 無 | `0` |
| `6` | `ababaca` | `a` | `1` |

因此整個 LPS 陣列就是：

```text
[0, 0, 1, 2, 3, 0, 1]
```

### `BuildLps` 怎麼計算

目前 [leetcode_028/Program.cs](leetcode_028/Program.cs) 中的 `BuildLps` 使用兩個核心變數：

- `patternIndex`：目前正在計算哪一格 `LPS`
- `prefixLength`：目前已知、可以延續的相同前後綴長度

流程可以直接對照程式理解：

1. `LPS[0]` 一定是 `0`，因為單一字元不可能有非空的真前綴和真後綴。
2. `patternIndex` 從 `1` 開始往右掃。
3. 如果 `pattern[patternIndex] == pattern[prefixLength]`，表示可以把目前共同前後綴再延長一格。
4. 如果不相等，而且 `prefixLength > 0`，不要急著把 `LPS[patternIndex]` 寫成 `0`，而是先回退到 `LPS[prefixLength - 1]`，看看較短的前綴能不能接上現在這個字元。
5. 如果不相等，而且 `prefixLength == 0`，代表這一格真的接不起來，`LPS[patternIndex]` 就是 `0`，然後處理下一格。

還是用 `pattern = "ababaca"` 來看，會更接近實際執行過程：

這個表格裡，每一步實際上比較的都是：

- `pattern[patternIndex]`：目前正在決定 `LPS[patternIndex]` 的字元
- `pattern[prefixLength]`：目前候選前綴中，下一個想拿來接上的字元

也就是說，`比較結果` 欄位其實完整寫法應該理解成：

- `pattern[patternIndex] == pattern[prefixLength]`
- 或 `pattern[patternIndex] != pattern[prefixLength]`

| `patternIndex` | 目前字元 | 比較結果 | `prefixLength` 變化 | 寫入的 `LPS` |
| --- | --- | --- | --- | --- |
| `1` | `b` | `pattern[1] = 'b'` 和 `pattern[0] = 'a'`，所以 `b != a` | `0 -> 0` | `LPS[1] = 0` |
| `2` | `a` | `pattern[2] = 'a'` 和 `pattern[0] = 'a'`，所以 `a == a` | `0 -> 1` | `LPS[2] = 1` |
| `3` | `b` | `pattern[3] = 'b'` 和 `pattern[1] = 'b'`，所以 `b == b` | `1 -> 2` | `LPS[3] = 2` |
| `4` | `a` | `pattern[4] = 'a'` 和 `pattern[2] = 'a'`，所以 `a == a` | `2 -> 3` | `LPS[4] = 3` |
| `5` | `c` | `pattern[5] = 'c'` 和 `pattern[3] = 'b'`，所以 `c != b`，先回退 | `3 -> 1` | 暫不寫入 |
| `5` | `c` | `pattern[5] = 'c'` 和 `pattern[1] = 'b'`，所以 `c != b`，再回退 | `1 -> 0` | 暫不寫入 |
| `5` | `c` | `pattern[5] = 'c'` 和 `pattern[0] = 'a'`，所以 `c != a`，無法延續 | `0 -> 0` | `LPS[5] = 0` |
| `6` | `a` | `pattern[6] = 'a'` 和 `pattern[0] = 'a'`，所以 `a == a` | `0 -> 1` | `LPS[6] = 1` |

這裡最容易卡住的是回退那兩步。`patternIndex = 5` 時，前面原本累積到長度 `3`，也就是以為 `aba` 這段可以延續；但現在遇到 `c`，接不起來，所以要退回去試試看「較短但仍可能成立」的共同前後綴。這就是：

```csharp
prefixLength = longestPrefixSuffix[prefixLength - 1];
```

它的意思不是「整段重算」，而是「跳到上一個仍可能接上的候選長度」。

### LPS 的用途

LPS 的作用不是直接找答案，而是讓 KMP 在比對失敗時知道 `needle` 應該退到哪裡。

在 `StrStrKmp` 中，當 `haystack[haystackIndex]` 和 `needle[needleIndex]` 不同時：

- 如果 `needleIndex == 0`，代表連第一個字元都沒對上，只能讓 `haystackIndex++`
- 如果 `needleIndex > 0`，代表前面已有一段匹配成功，這時就可以使用：

```csharp
needleIndex = longestPrefixSuffix[needleIndex - 1];
```

這表示：

- 已匹配的前半段裡，有一部分其實同時也是後綴
- 那一部分不需要重比
- `haystackIndex` 可以留在原地
- 只把 `needleIndex` 退到下一個合理位置，繼續比較

這就是 KMP 能保持線性時間的關鍵，因為它避免了暴力法那種「失敗就整段從頭開始」的重複工作。

### 對照目前 README 內的 `issip` 範例

目前示範測資中的 `needle = "issip"`，它的 LPS 陣列是：

```text
[0, 0, 0, 1, 0]
```

原因是：

- `i`：前後綴都沒有重疊，`0`
- `is`：沒有重疊，`0`
- `iss`：沒有重疊，`0`
- `issi`：前綴 `i` 和後綴 `i` 相同，所以是 `1`
- `issip`：最後一個字元變成 `p`，原本那個長度 `1` 的重疊也接不起來，所以回到 `0`

### 常見誤解

- LPS 不是「最長重複子字串」；它只關心前綴和後綴是否重疊。
- LPS 記錄的是「長度」，不是索引位置。
- LPS 是先對 `pattern` 自己做預處理，不需要先看 `haystack`。
- 某一格 `LPS[i] = 0` 不代表那個字元沒出現過，只代表這一段沒有可重用的前後綴重疊。

## 目前實作

程式集中在 [leetcode_028/Program.cs](leetcode_028/Program.cs)，包含：

- `StrStrBruteForce`：暴力法
- `StrStrKmp`：KMP 解法
- `BuildLps`：建立 KMP 所需的 LPS 陣列
- `RunDemoCases`：執行固定測資並輸出比對結果

## 建置與執行

```bash
dotnet build leetcode_028/leetcode_028.csproj
dotnet run --project leetcode_028/leetcode_028.csproj
```

## 範例輸出

```text
LeetCode 28 - Find the Index of the First Occurrence in a String

Case 1: haystack = "sadbutsad", needle = "sad", expected = 0
Brute force: 0 (PASS)
KMP: 0 (PASS)

Case 2: haystack = "leetcode", needle = "leeto", expected = -1
Brute force: -1 (PASS)
KMP: -1 (PASS)

Case 3: haystack = "aaaaa", needle = "bba", expected = -1
Brute force: -1 (PASS)
KMP: -1 (PASS)

Case 4: haystack = "mississippi", needle = "issip", expected = 4
Brute force: 4 (PASS)
KMP: 4 (PASS)
```

## 驗證方式

本 repository 目前沒有獨立的測試專案，驗證方式如下：

- 先執行 `dotnet build` 確認可以成功編譯
- 再執行 `dotnet run` 確認固定測資輸出與預期答案一致
- 最後執行 `git diff --check` 確認沒有多餘空白或換行問題
