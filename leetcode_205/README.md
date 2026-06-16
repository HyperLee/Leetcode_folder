# LeetCode 205. Isomorphic Strings

## 題目連結

- 英文題目: [205. Isomorphic Strings](https://leetcode.com/problems/isomorphic-strings/description/)
- 中文題目: [205. 同構字串](https://leetcode.cn/problems/isomorphic-strings/description/)

## 題目說明

給定兩個字串 `s` 與 `t`，請判斷它們是否為同構字串。

所謂同構，意思是：

- `s` 中的每一個字元都可以被某個字元替換成 `t`
- 同一個來源字元在所有位置都必須替換成同一個目標字元
- 不同來源字元不能對應到同一個目標字元
- 字元可以對應到自己

因此，這題的核心不是單純比較兩個字串是否長得像，而是要確認「字元映射規則」是否全程一致，並且這個映射必須是一對一的雙射關係。

## 限制條件

根據 LeetCode 題意，常見限制如下：

- `1 <= s.length <= 5 * 10^4`
- `t.length == s.length`
- `s` 和 `t` 由任意有效 ASCII 字元組成

在這個專案的實作中，三種解法都額外先做了長度檢查：

- 若 `s.Length != t.Length`，直接回傳 `false`

這樣即使未來把方法拿去其他地方單獨呼叫，也能避免因索引不對齊造成例外。

## 解題概念與出發點

這題真正要確認的事情有兩層：

1. `s` 中同一個字元，是否總是對應到 `t` 中同一個字元
2. `t` 中同一個字元，是否沒有被 `s` 中兩個不同字元共用

換句話說，這題是在驗證「模式一致」與「映射唯一」。

本專案保留三種思路，目的不是追求更多寫法，而是讓同一題能從不同角度理解：

1. 把字串轉成「首次出現位置序列」，直接比較模式是否一致
2. 使用兩張 `Dictionary<char, char>` 做雙向映射驗證
3. 使用單張 `Dictionary<char, char>` 搭配 `ContainsValue` 檢查目標字元是否已被佔用

三種方法都能正確解題，但它們強調的觀察點不同：

- 解法一強調「重複模式」
- 解法二強調「雙射」
- 解法三強調「單向映射 + 目標唯一性」

## 解法一：首次出現位置序列比對

### 設計思路

如果兩個字串是同構的，那麼它們每個字元「第一次出現的位置」所形成的模式應該完全一致。

例如：

- `paper` 的首次出現位置序列是 `[0, 1, 0, 3, 4]`
- `title` 的首次出現位置序列也是 `[0, 1, 0, 3, 4]`

因為兩邊的模式相同，所以可以判定它們是同構字串。

這個方法把原本的字元映射問題，轉成「兩個模式陣列是否相等」的問題。

### 為什麼可行

若某個字元在來源字串中重複出現，那麼它在目標字串中也必須於相同的結構位置重複出現；否則模式就會不同。

所以只要：

- `s` 的每一個位置都能對應到 `s[i]` 的首次出現位置
- `t` 的每一個位置都能對應到 `t[i]` 的首次出現位置
- 最終兩份序列完全一致

就代表兩個字串具有相同的重複結構。

### 演算法流程

1. 若兩個字串長度不同，直接回傳 `false`
2. 走訪 `s` 的每個字元，將 `s.IndexOf(s[i])` 放進 `indexS`
3. 走訪 `t` 的每個字元，將 `t.IndexOf(t[i])` 放進 `indexT`
4. 比較 `indexS` 與 `indexT` 是否完全相同
5. 若相同則回傳 `true`，否則回傳 `false`

### 範例演示流程

以 `s = "paper"`、`t = "title"` 為例：

1. `paper`
   - `p` 第一次出現在索引 `0`
   - `a` 第一次出現在索引 `1`
   - 第二個 `p` 第一次出現仍然是索引 `0`
   - `e` 第一次出現在索引 `3`
   - `r` 第一次出現在索引 `4`
   - 得到 `[0, 1, 0, 3, 4]`
2. `title`
   - `t` 第一次出現在索引 `0`
   - `i` 第一次出現在索引 `1`
   - 第二個 `t` 第一次出現仍然是索引 `0`
   - `l` 第一次出現在索引 `3`
   - `e` 第一次出現在索引 `4`
   - 得到 `[0, 1, 0, 3, 4]`
3. 兩個序列完全一致，因此判定為 `true`

### 複雜度

- 時間複雜度: `O(n^2)`
- 空間複雜度: `O(n)`

這裡的主要成本來自 `IndexOf` 每次都可能回頭搜尋，因此雖然寫法直觀，但不是三種解法裡效能最佳的版本。

## 解法二：雙向 Dictionary 驗證雙射

### 設計思路

這個方法同時維護兩張表：

- `sTot`: 記錄 `s` 的某個字元應該映射到 `t` 的哪個字元
- `tTos`: 記錄 `t` 的某個字元應該由 `s` 的哪個字元映射而來

只檢查單方向是不夠的。因為：

- `a -> x`
- `b -> x`

這在單向看起來都能成立，但其實違反「不同來源字元不能對應到同一目標字元」的規則。

所以必須正向、反向都驗證，才能保證是雙射。

### 為什麼可行

每當走到索引 `i` 時：

- 若 `s[i]` 之前已經映射過，就必須仍然對應到同一個 `t[i]`
- 若 `t[i]` 之前已經被映射過，就必須仍然來自同一個 `s[i]`

只要有任何一邊衝突，就可以立刻判定不是同構字串。

### 演算法流程

1. 若兩個字串長度不同，直接回傳 `false`
2. 建立 `sTot` 與 `tTos`
3. 由左到右掃描每個位置
4. 檢查 `s[i] -> t[i]` 是否與既有映射衝突
5. 檢查 `t[i] -> s[i]` 是否與既有反向映射衝突
6. 若都沒衝突，更新兩張表
7. 全部掃描完成後回傳 `true`

### 範例演示流程

以 `s = "badc"`、`t = "baba"` 為例：

1. 索引 `0`
   - `b -> b`
   - `b <- b`
   - 兩張表都合法
2. 索引 `1`
   - `a -> a`
   - `a <- a`
   - 兩張表都合法
3. 索引 `2`
   - `d -> b`
   - 這時反向表發現 `b` 早已對應到來源字元 `b`
   - 代表兩個不同來源字元 `b`、`d` 都想映射到同一個目標字元 `b`
4. 因此直接回傳 `false`

### 複雜度

- 時間複雜度: `O(n)`
- 空間複雜度: `O(n)`

這是三種寫法中最平衡的一種：邏輯清楚、效能也穩定。

## 解法三：單向 Dictionary + 目標字元唯一性檢查

### 設計思路

這個方法只保留一張 `Dictionary<char, char>`，負責記錄 `s -> t` 的關係。

當某個來源字元第一次出現時：

1. 先確認這個目標字元 `t[i]` 是否已經被其他來源字元使用
2. 若尚未使用，就建立 `s[i] -> t[i]`

當某個來源字元不是第一次出現時：

1. 直接檢查這次遇到的 `t[i]` 是否仍與之前記錄一致

這種做法不用維護第二張表，但要額外使用 `ContainsValue` 來確保不會出現多個來源字元共用同一目標字元。

### 為什麼可行

這個方法把問題拆成兩件事：

- 來源字元是否穩定映射到同一目標字元
- 新目標字元是否已經被別人搶先占用

只要這兩個條件都成立，就等價於雙射驗證。

### 演算法流程

1. 若兩個字串長度不同，直接回傳 `false`
2. 建立一張 `pairs` 字典
3. 逐一掃描每個位置
4. 若 `s[i]` 尚未出現：
   - 檢查 `t[i]` 是否已出現在 `pairs` 的值集合中
   - 若已存在，回傳 `false`
   - 否則建立映射
5. 若 `s[i]` 已出現：
   - 檢查 `pairs[s[i]]` 是否等於目前的 `t[i]`
   - 若不相等，回傳 `false`
6. 若整趟掃描都無衝突，回傳 `true`

### 範例演示流程

以 `s = "aa"`、`t = "ab"` 為例：

1. 索引 `0`
   - `a` 尚未建立映射
   - `t[0]` 的字元是 `a`，而且它尚未被其他來源字元使用
   - 建立映射 `a -> a`
2. 索引 `1`
   - `s[1]` 仍然是 `a`
   - 但 `pairs['a']` 目前記錄的是 `a`
   - 現在 `t[1]` 卻是 `b`
3. 同一來源字元 `a` 對應到兩個不同目標字元，規則被破壞
4. 因此回傳 `false`

### 複雜度

- 時間複雜度: `O(n^2)`
- 空間複雜度: `O(n)`

這個版本的可讀性不錯，但 `ContainsValue` 會反覆掃描整張字典，所以效能上不如解法二。

## 三種解法比較

| 面向 | 解法一：首次出現位置序列 | 解法二：雙向 Dictionary | 解法三：單向 Dictionary + `ContainsValue` |
| --- | --- | --- | --- |
| 核心觀察 | 模式是否一致 | 映射是否為雙射 | 單向映射是否穩定且目標不重複 |
| 時間複雜度 | `O(n^2)` | `O(n)` | `O(n^2)` |
| 空間複雜度 | `O(n)` | `O(n)` | `O(n)` |
| 優點 | 直觀、好理解 | 效能穩定、語意完整 | 寫法精簡、容易展示單向映射思維 |
| 可能缺點 | `IndexOf` 反覆搜尋較慢 | 程式碼稍長 | `ContainsValue` 會拖慢效率 |
| 推薦程度 | 適合教學與模式觀察 | 最推薦的正式解法 | 適合比較不同驗證策略 |

## Main 測試案例說明

本專案的 `Main` 不是單純印出一個答案，而是直接呼叫 `RunSamples()`，跑完六組固定案例後列出每種解法的實際回傳值與 `PASS` 或 `FAIL`。

| 案例 | 輸入 | 預期 | 測試目的 |
| --- | --- | --- | --- |
| Case 1 | `s = "egg"`, `t = "add"` | `true` | 最基本的一對一映射 |
| Case 2 | `s = "foo"`, `t = "bar"` | `false` | 同一來源字元不應映射到不同目標字元 |
| Case 3 | `s = "paper"`, `t = "title"` | `true` | 經典重複模式完全一致的成功案例 |
| Case 4 | `s = "badc"`, `t = "baba"` | `false` | 驗證不同來源字元映射到同一目標字元時會失敗 |
| Case 5 | `s = "abc"`, `t = "abc"` | `true` | 驗證字元可映射到自己 |
| Case 6 | `s = "aa"`, `t = "ab"` | `false` | 驗證同一來源字元不能對應兩個不同目標字元 |

輸出格式固定包含：

- `Input`
- `Expected`
- `IsIsomorphic`
- `IsIsomorphic2`
- `IsIsomorphic3`
- `Overall`

這樣做的好處是 README 裡的案例、程式輸出與實際驗證流程會完全一致。

## 可用命令

請從 repository root `/Users/qiuzili/Leetcode/Leetcode_folder/leetcode_205` 執行以下命令：

```bash
dotnet build leetcode_205/leetcode_205.csproj
dotnet run --project leetcode_205/leetcode_205.csproj
git diff --check
```

補充說明：

- 這個 repo 目前沒有獨立測試專案，因此不把 `dotnet test` 當成標準驗證命令
- `dotnet run` 的輸出就是下方 README 記錄的實際結果

## 實際執行輸出

以下內容來自實際執行 `dotnet run --project leetcode_205/leetcode_205.csproj`：

```text
LeetCode 205 - Isomorphic Strings
==================================================
Case 1 - 最基本的一對一映射
Input: s = "egg", t = "add"
Expected: true
IsIsomorphic: true | PASS
IsIsomorphic2: true | PASS
IsIsomorphic3: true | PASS
Overall: PASS

Case 2 - 同一字元對到不同字元，應判定失敗
Input: s = "foo", t = "bar"
Expected: false
IsIsomorphic: false | PASS
IsIsomorphic2: false | PASS
IsIsomorphic3: false | PASS
Overall: PASS

Case 3 - 經典重複模式完全一致的成功案例
Input: s = "paper", t = "title"
Expected: true
IsIsomorphic: true | PASS
IsIsomorphic2: true | PASS
IsIsomorphic3: true | PASS
Overall: PASS

Case 4 - 不同來源字元映射到同一目標字元，違反雙射
Input: s = "badc", t = "baba"
Expected: false
IsIsomorphic: false | PASS
IsIsomorphic2: false | PASS
IsIsomorphic3: false | PASS
Overall: PASS

Case 5 - 每個字元都映射到自己也算同構
Input: s = "abc", t = "abc"
Expected: true
IsIsomorphic: true | PASS
IsIsomorphic2: true | PASS
IsIsomorphic3: true | PASS
Overall: PASS

Case 6 - 同字元若對到不同目標字元就不是同構
Input: s = "aa", t = "ab"
Expected: false
IsIsomorphic: false | PASS
IsIsomorphic2: false | PASS
IsIsomorphic3: false | PASS
Overall: PASS

Summary: 6/6 cases passed.
```

## 結論

如果你想用最穩定、也最符合正式解題思路的方式，建議優先看解法二：

- 它直接把題目的雙射要求翻譯成兩張映射表
- 時間複雜度為 `O(n)`
- 可讀性與效能都最平衡

如果你是在學習這題，則可以按這個順序理解：

1. 先看解法一，理解什麼叫做「模式一致」
2. 再看解法三，理解單向映射與目標唯一性的關係
3. 最後看解法二，建立最完整、也最實務的雙向映射版本
