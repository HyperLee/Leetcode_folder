# LeetCode 151 — Reverse Words in a String (反轉字串中的單詞)

> C# 程式範例：`Program.cs` 實作了 2 種解法（手動解析、Split + join），並包含測試範例。

## 簡介 🧭

給定一個輸入字串 `s`，請反轉字串中的單詞順序。

- 單詞由連續的非空格字元組成
- 輸入可能包含前導或尾隨的空白，或多個連續空白；回傳字串應將單詞以單一空格分隔，且不包含多餘空白

相關題號、連結：
- Problem: 151. Reverse Words in a String
- English: https://leetcode.com/problems/reverse-words-in-a-string/
- 中文： https://leetcode.cn/problems/reverse-words-in-a-string/

---

## 範例（Examples）

- 輸入: `"the sky is blue"` → 回傳: `"blue is sky the"`
- 輸入: `"  hello world  "` → 回傳: `"world hello"`
- 輸入: `"a good   example"` → 回傳: `"example good a"`
- 輸入: `"    "` → 回傳: `""`（空字串）

---

## 解題概念 (Idea) 💡

題目的核心是：提取字串中的單詞、移除多餘空白，並反轉單詞順序後以單一空白連接。

常見做法有下列兩種：
- 方法 A：手動掃描字串以擷取單詞，並在組合結果時反轉順序
- 方法 B：使用內建的 `Split()` 或其他工具切割成單詞陣列，然後反向組合

`Program.cs` 中已示範兩種作法：`ReverseWords`（手動解析）、`ReverseWords_2`（Split + join）。下面分別說明。

---

## 解法一：手動解析（ReverseWords）

### 概念

從左到右掃描字串，遇到非空字元則標記為單詞開頭；繼續往後直到空格或字串尾為止，擷取該單詞。
每次擷取後採「把新單詞插入在結果最前面」的方法，這樣在一次遍歷中就可以完成單詞擷取與順序反轉。

### 主要流程（概要）

1. 初始化一個 `StringBuilder` 當作結果（sb）
2. 以 index `i` 從 0 掃描原始字串 `s`
3. 當 `s[i]` 為空格時跳過
4. 否則從 `start = i` 開始，一直跑到下個空格或字串結尾（`i` 移動）
5. 擷取 `s.Substring(start, i - start)` 做為單詞
6. 如果 `sb` 是空的則 `Append(word)`，否則 `Insert(0, word + ' ')`（將新單詞放到字首）

### 範例推演（`a good   example`）

- 初始：sb = ""
- 讀到 `a` → sb 輸出 `'a'`
- 讀到 `good` → 插在前面 → sb = `"good a"`
- 讀到 `example` → 插在最前面 → sb = `"example good a"`
- 回傳 `"example good a"`

### 時間/空間複雜度

- 時間：最壞情況 O(n^2) — 因為 `StringBuilder.Insert(0, ...)` 會將內容往後移動，重複插入在前端的總成本可能為 O(n^2)（n 為字元數量或單詞數量的相關量）
- 空間：O(n)（回傳字串需要額外的記憶體）

### 優點／缺點

- 優點：一次遍歷的設計，程式碼無需分配字串陣列。避免大型 Split/正則的額外開銷（在語意上更直接）
- 缺點：`Insert(0, ...)` 使得效率在最壞情況下較差（O(n^2)），對長字串或大量單詞不友善；實作也稍微複雜些

---

## 解法二：Split + Reverse（ReverseWords_2）

### 概念

利用語言內建的 `Split()`（或其他函式）把輸入字串拆成單詞陣列（會過濾掉空元素），然後從陣列尾端往前把單詞串接為結果字串。

### 主要流程（概要）

1. `Trim()` 去除頭尾空白
2. 使用 `Split()` 或正則/自訂切割取得單詞陣列
3. 反向遍歷單詞陣列，使用 `StringBuilder` 加上單一空格串接成結果
4. 結果再次 `Trim()` 與正規化

### 範例推演（`a good   example`）

- `Trim()` -> `a good   example`（結尾與開頭沒有空白）
- `Split()` -> `["a", "good", "example"]`
- 從陣列尾部反向組合 -> `"example good a"`

### 時間/空間複雜度

- 時間：O(n)（掃描、分割、組合都是線性）
- 空間：O(n)（分割後的字串陣列與結果字串需要額外的記憶體）

### 優點／缺點

- 優點：實作簡潔、易懂、時間複雜度為 O(n)
- 缺點：分配陣列與各種字串會增加空間消耗；使用 `Split` 或正則會有額外解析成本

---

## 比較兩個方法 ✅ vs ⚠️

| 考量 | 手動解析 (`ReverseWords`) | Split + Reverse (`ReverseWords_2`) |
|---|---:|---:|
| 可讀性 | ⚠️ 較難讀、較多指標操作 | ✅ 易讀、明確語意
| 時間複雜度 | ⚠️ 最壞 O(n^2)，實際視 `Insert` 成本 | ✅ O(n)
| 空間複雜度 | ✅ 較少臨時資料（但結果字串仍需 O(n)）| ⚠️ O(n)（陣列 + 尾再合併）
| 工程面（快速實作） | ⚠️ 實作較細節導向 | ✅ 更直觀與短小

使用建議：若追求簡潔與穩定效能，`ReverseWords_2`（Split + Reverse）通常是首選；若有必要避免額外陣列分配（例如很嚴格的記憶體限制或需線流處理），可考慮手動解析，但應避免在 `StringBuilder` 上不斷 `Insert(0, ...)`，改為將單詞收集到 `List<string>` 再反向 join，以避免 O(n^2) 的效能退化。

---

## 程式碼與測試

`Program.cs` 已內建範例測試（示範了多個測資），可直接執行來驗證兩個方法的行為是否一致：

```pwsh
cd c:\GitHubFolder\LeetCodeFolder\Leetcode_folder\leetcode_151
# 建構 solution
dotnet build leetcode_151.sln -c Debug
# 執行 project
dotnet run --project leetcode_151/leetcode_151.csproj
```

程式輸出示範：每個測資會比較 `ReverseWords` 與 `ReverseWords_2` 的結果並印出是否與預期相符。

---

## 小結 ✨

- 這個題目核心在於安全地擷取單詞、清除多餘空白，再反轉順序輸出
- `ReverseWords_2`（Split + Join）通常是最直觀且效能穩定的做法
- 如果想最小化額外空間或要以單一掃描的方式完成，選擇手動解析，但應避免在結果上頻繁 `Insert(0, ...)` 造成效率退化

---

若你想要我把手動解析方法改為「收集單詞到 `List<string>` 再反轉 join」以確保時間複雜度為 O(n)，我可以協助修改 `Program.cs` 並加入更多測試案例。
