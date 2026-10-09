# LeetCode 22：Generate Parentheses（括號生成）

以 C# / .NET 10 實作兩種解法：**暴力列舉完整候選後驗證**、**只探索有效前綴的回溯法**。執行專案即可看到自動檢查，無須輸入資料。

- [題目與限制](#題目與限制)
- [執行與驗證](#執行與驗證)
- [解法一暴力列舉](#解法一暴力列舉)
- [解法二有效前綴回溯](#解法二有效前綴回溯)
- [正確性與複雜度比較](#正確性與複雜度比較)
- [自動檢查與實際輸出](#自動檢查與實際輸出)

## 題目與限制

給定 `n` 對括號，產生**所有格式正確的括號組合**。每個結果必須使用恰好 `n` 個左括號與 `n` 個右括號，並且括號正確配對。

來源：[LeetCode 22 英文題目](https://leetcode.com/problems/generate-parentheses/description/) · [中文題目](https://leetcode.cn/problems/generate-parentheses/description/)

官方限制為 `1 ≤ n ≤ 8`，輸出是字串集合，題目不要求特定順序。本專案沿用這個輸入契約，不另提供範圍外輸入的驗證或錯誤處理。

```text
輸入：n = 1
輸出：["()"]

輸入：n = 3
輸出：["((()))", "(()())", "(())()", "()(())", "()()()"]
```

只有左右括號總數相同還不夠。例如 `)(` 的總數相同，但第一個右括號沒有左括號可配對。用 `balance = 左括號數 − 右括號數` 表示尚未配對的左括號數，合法字串須同時滿足：

1. 任一前綴的 `balance ≥ 0`。
2. 完整字串的 `balance = 0`。
3. 長度為 `2n`，且只包含 `(` 與 `)`。

這些條件是兩種解法共同的出發點，差別在於**何時排除不可能的候選**。

## 執行與驗證

需要 .NET 10 SDK。在本 README 所在目錄依序執行：

```sh
dotnet restore leetcode_022/leetcode_022.csproj
dotnet build leetcode_022/leetcode_022.csproj --no-restore
dotnet run --project leetcode_022/leetcode_022.csproj --no-build
dotnet format leetcode_022/leetcode_022.csproj --verify-no-changes --no-restore
```

修改程式後必須重新建置，再使用 `--no-build` 執行。`Main` 不使用命令列參數，也不等待按鍵；通過時結束碼為 `0`，任何檢查失敗時為 `1`。沒有獨立的測試框架，測試就是入口執行的固定案例。

XML 文件語法可另外用下列命令檢查，不需要修改專案設定：

```sh
dotnet build leetcode_022/leetcode_022.csproj --no-restore -t:Rebuild -p:GenerateDocumentationFile=true -p:TreatWarningsAsErrors=true
git diff --check
```

本次以上命令均通過；一般建置與 XML rebuild 都為 0 警告、0 錯誤，格式驗證與差異空白檢查無診斷。完整執行紀錄見文末。

| 位置 | 用途 |
| --- | --- |
| `leetcode_022/Program.cs` | 題目 XML、入口測試、兩種解法與驗證 helper |
| `leetcode_022/leetcode_022.csproj` | .NET 10 可執行專案 |
| `.vscode/` | VS Code 建置與 `Debug leetcode_022` 設定 |
| `docs/readme-template.md` | 初次 README 的結構參考 |

## 解法一：暴力列舉

### 設計與資料流

入口為 `GenerateParenthesis(int n)`，先建立新的結果集合與 `char[2 * n]`，再呼叫 `generateAll(current, 0, combinations)`。每一個位置都有 `(` 或 `)` 兩種選擇，因此深度 `2n` 的完整二元決策樹共有 `2^(2n) = 4^n` 個葉節點。

`generateAll` 的參數有明確分工：

| 參數 | 含義與不變條件 |
| --- | --- |
| `current` | 所有遞迴層共用的候選陣列，長度固定為 `2n` |
| `pos` | 下一個填入位置；只有 `[0, pos)` 是本次呼叫已確定的前綴 |
| `result` | 收集通過驗證的完整字串 |

當 `pos < current.Length`，先寫 `current[pos] = '('` 並遞迴，再覆寫同一位置為 `')'` 並遞迴。只有 `pos == current.Length` 才呼叫 `isValid`。這個方法刻意不在生成階段追蹤 balance，所以即使前綴已經無效，仍然會生成完整候選。

**陣列後綴可能保留上一條路徑的資料，但不能把它當作本次已確定內容。** 例如 `n = 2`，左子樹返回後，根層寫入右括號，陣列可能看似 `))))`；根層此時只有第一格確定，後三格仍會在遞迴過程重新填入。葉節點才有資格讀取整個陣列。

不需要像 StringBuilder 那樣「移除」字元，因為每層只負責固定索引，下一個分支以覆寫切換；更深層不會改動本層以前的前綴。`new string(current)` 複製當下內容，之後覆寫陣列不會改變已收集的答案。

### 完整候選如何判定

`isValid(char[] current)` 的輸入契約是只包含括號的完整候選。每讀取 `(` 就增加 balance，每讀取 `)` 就減少 balance；**先更新，再判斷是否負值**。

| 已讀前綴 | 下一字元 | 操作前 balance | 判斷時 balance | 完成後結果 |
| --- | --- | --- | --- | --- |
| 空字串 | `(` | 0 | 1 | 繼續 |
| `(` | `)` | 1 | 0 | 繼續 |
| `()` | `)` | 0 | -1 | 立即回傳 false |

一旦負值，代表這個前綴已有右括號找不到左括號。未來加入左括號也無法修復這個更早的前綴，所以可以立刻結束驗證。若全程沒有負值，最後仍要檢查 `balance == 0`，避免接受 `((()` 之類留下未配對左括號的字串。

注意：這是**驗證完整候選時提前返回**，不是在生成決策樹時剪枝；暴力法依然到達全部葉節點。由於 helper 預期字元只有括號，原本的 `else` 直接視為右括號；入口測試的獨立驗證器則會另外排除其他字元。

### n = 2：全部 16 個候選

以下依照程式「先左、再右」的搜尋順序。負值出現時 `isValid` 就停止，後面的字元不再讀取；表中的 balance 也只列出實際讀取部分。

| 次序 | 完整候選 | 逐字 balance | 判定與原因 |
| --- | --- | --- | --- |
| 1 | `((((` | 1 → 2 → 3 → 4 | 淘汰：最後為 4，仍有未配對左括號 |
| 2 | `((()` | 1 → 2 → 3 → 2 | 淘汰：最後為 2，仍有未配對左括號 |
| 3 | `(()(` | 1 → 2 → 1 → 2 | 淘汰：最後為 2，仍有未配對左括號 |
| 4 | `(())` | 1 → 2 → 1 → 0 | 接受：前綴皆非負，最後為 0 |
| 5 | `()((` | 1 → 0 → 1 → 2 | 淘汰：最後為 2，仍有未配對左括號 |
| 6 | `()()` | 1 → 0 → 1 → 0 | 接受：前綴皆非負，最後為 0 |
| 7 | `())(` | 1 → 0 → -1 | 淘汰：第 3 字元形成負前綴 |
| 8 | `()))` | 1 → 0 → -1 | 淘汰：第 3 字元形成負前綴 |
| 9 | `)(((` | -1 | 淘汰：第 1 字元形成負前綴 |
| 10 | `)(()` | -1 | 淘汰：第 1 字元形成負前綴 |
| 11 | `)()(` | -1 | 淘汰：第 1 字元形成負前綴 |
| 12 | `)())` | -1 | 淘汰：第 1 字元形成負前綴 |
| 13 | `))((` | -1 | 淘汰：第 1 字元形成負前綴 |
| 14 | `))()` | -1 | 淘汰：第 1 字元形成負前綴 |
| 15 | `)))(` | -1 | 淘汰：第 1 字元形成負前綴 |
| 16 | `))))` | -1 | 淘汰：第 1 字元形成負前綴 |

因此只有 `(())` 與 `()()` 加入結果。

### n = 3：代表路徑與結果

生成路徑中，每一列的「完成後」只顯示已確定的陣列前綴，不顯示未確定後綴。

| 操作前前綴 | pos | 寫入字元 | 判斷時狀態 | 完成後前綴 |
| --- | --- | --- | --- | --- |
| 空字串 | 0 | `(` | 仍未填滿，不驗證 | `(` |
| `(` | 1 | `(` | 仍未填滿，不驗證 | `((` |
| `((` | 2 | `(` | 仍未填滿，不驗證 | `(((` |
| `(((` | 3 | `)` | 仍未填滿，不驗證 | `((()` |
| `((()` | 4 | `)` | 仍未填滿，不驗證 | `((())` |
| `((())` | 5 | `)` | 下一層 pos = 6，檢查完整候選 | `((()))` |

這條路徑的 balance 為 `1 → 2 → 3 → 2 → 1 → 0`，會被接受；完整候選 `())(((` 則在第三個字元變成 `-1` 時淘汰。實際生成仍先從 `((((((` 開始，以上只擷取其中一條代表路徑。

所有 `64` 個完整候選經驗證後，得到：

```text
((()))
(()())
(())()
()(())
()()()
```

## 解法二：有效前綴回溯

### 從暴力法找到可提前判斷的條件

如果目前已經使用 `n` 個左括號，就不能再加入左括號；如果沒有尚未配對的左括號，就不能加入右括號。這些事情在字串尚未完成時就知道，不需要先生成無效候選。

入口為 `GenerateParenthesis2(int n)`，建立新的結果集合與空 `StringBuilder`，再呼叫 `backtrack(ans, cur, 0, 0, n)`。

| 狀態 | 含義 |
| --- | --- |
| `cur` | 共用的有效前綴 |
| `open` | 前綴中左括號數 |
| `close` | 前綴中右括號數 |
| `max` | 目標括號對數，即 n |

每次進入 helper 都維持 `cur.Length = open + close` 與 `0 ≤ close ≤ open ≤ max`，所以 `open - close` 就是尚未配對的左括號數。

### 分支、完成條件與還原

1. 若 `cur.Length == max * 2`，由不變量可推得 `open = close = max`。保存 `cur.ToString()` 的獨立快照並返回，不必再掃描 balance。
2. 若 `open < max`，可以加入 `(`，遞迴時傳入 `open + 1`。
3. 若 `close < open`，可以加入 `)`，遞迴時傳入 `close + 1`。嚴格小於表示至少還有一個未配對左括號。
4. 每個分支遞迴返回後，以 `cur.Remove(cur.Length - 1, 1)` 移除**本層加入的那一個字元**，讓下一個分支看到原本的前綴。

計數參數是值傳遞，不需要手動減一；StringBuilder 是共用物件，必須明確還原。子層先還原子層的加入，本層返回時才還原本層的加入，因此移除的永遠是自己最後加入的字元。

兩個判斷必須是獨立 `if`。例如 `n = 2` 且前綴為 `(` 時，`open < max` 與 `close < open` 都成立：必須先探索 `((`，還原回 `(`，再探索 `()`。若使用 `else if`，右分支會被跳過，遺漏 `()()`。

### n = 2：完整搜尋與狀態還原

以下完整列出進入節點、分支操作、保存與還原。狀態記作 `前綴；open, close`，`ε` 是空字串。判斷式中的數字是**本層操作前**的值；新增字元後的計數是傳入子層的值。

| 步驟 | 操作前狀態 | 判斷時／操作 | 完成後狀態 |
| --- | --- | --- | --- |
| 1 | `ε`；0, 0 | 進入：長度 0，未達 4 | `ε`；0, 0 |
| 2 | `ε`；0, 0 | 左分支：0 < 2 成立，Append `(` | `(`；1, 0 |
| 3 | `(`；1, 0 | 進入：長度 1，未達 4 | `(`；1, 0 |
| 4 | `(`；1, 0 | 左分支：1 < 2 成立，Append `(` | `((`；2, 0 |
| 5 | `((`；2, 0 | 進入：長度 2，未達 4 | `((`；2, 0 |
| 6 | `((`；2, 0 | 左分支：2 < 2 不成立，跳過 | `((`；2, 0 |
| 7 | `((`；2, 0 | 右分支：0 < 2 成立，Append `)` | `(()`；2, 1 |
| 8 | `(()`；2, 1 | 進入：長度 3，未達 4 | `(()`；2, 1 |
| 9 | `(()`；2, 1 | 左分支：2 < 2 不成立，跳過 | `(()`；2, 1 |
| 10 | `(()`；2, 1 | 右分支：1 < 2 成立，Append `)` | `(())`；2, 2 |
| 11 | `(())`；2, 2 | 進入：長度 4，等於 4 | `(())`；2, 2 |
| 12 | `(())`；2, 2 | 保存 `(())` 快照，return | `(())`；2, 2 |
| 13 | `(())`；2, 2 | 子層返回，移除本層的 `)` | `(()`；2, 1 |
| 14 | `(()`；2, 1 | 本層分支結束，返回呼叫者 | `(()`；2, 1 |
| 15 | `(()`；2, 1 | 子層返回，移除本層的 `)` | `((`；2, 0 |
| 16 | `((`；2, 0 | 本層分支結束，返回呼叫者 | `((`；2, 0 |
| 17 | `((`；2, 0 | 子層返回，移除本層的 `(` | `(`；1, 0 |
| 18 | `(`；1, 0 | 右分支：0 < 1 成立，Append `)` | `()`；1, 1 |
| 19 | `()`；1, 1 | 進入：長度 2，未達 4 | `()`；1, 1 |
| 20 | `()`；1, 1 | 左分支：1 < 2 成立，Append `(` | `()(`；2, 1 |
| 21 | `()(`；2, 1 | 進入：長度 3，未達 4 | `()(`；2, 1 |
| 22 | `()(`；2, 1 | 左分支：2 < 2 不成立，跳過 | `()(`；2, 1 |
| 23 | `()(`；2, 1 | 右分支：1 < 2 成立，Append `)` | `()()`；2, 2 |
| 24 | `()()`；2, 2 | 進入：長度 4，等於 4 | `()()`；2, 2 |
| 25 | `()()`；2, 2 | 保存 `()()` 快照，return | `()()`；2, 2 |
| 26 | `()()`；2, 2 | 子層返回，移除本層的 `)` | `()(`；2, 1 |
| 27 | `()(`；2, 1 | 本層分支結束，返回呼叫者 | `()(`；2, 1 |
| 28 | `()(`；2, 1 | 子層返回，移除本層的 `(` | `()`；1, 1 |
| 29 | `()`；1, 1 | 右分支：1 < 1 不成立，跳過 | `()`；1, 1 |
| 30 | `()`；1, 1 | 本層分支結束，返回呼叫者 | `()`；1, 1 |
| 31 | `()`；1, 1 | 子層返回，移除本層的 `)` | `(`；1, 0 |
| 32 | `(`；1, 0 | 本層分支結束，返回呼叫者 | `(`；1, 0 |
| 33 | `(`；1, 0 | 子層返回，移除本層的 `(` | `ε`；0, 0 |
| 34 | `ε`；0, 0 | 右分支：0 < 0 不成立，跳過 | `ε`；0, 0 |
| 35 | `ε`；0, 0 | 本層分支結束，返回呼叫者 | `ε`；0, 0 |

表中還原前的計數用來標示剛完成的子路徑；父層自己的 `open`、`close` 始終維持操作前的值。根層最後回到空字串，結果集合保存 `(())`、`()()` 兩份快照。

這裡不生成 `)(`、`(((` 等不可能的前綴，也不會在完整字串上呼叫 `isValid`。

### n = 3：五條完整結果路徑

每條路徑列出**加入字元後**傳給子層的 `(open, close)`，起點都是空字串 `(0, 0)`。

| 結果 | 加入後狀態，依序對應六個字元 | 保存時條件 |
| --- | --- | --- |
| `((()))` | (1,0) → (2,0) → (3,0) → (3,1) → (3,2) → (3,3) | 長度 6，左右皆 3 |
| `(()())` | (1,0) → (2,0) → (2,1) → (3,1) → (3,2) → (3,3) | 長度 6，左右皆 3 |
| `(())()` | (1,0) → (2,0) → (2,1) → (2,2) → (3,2) → (3,3) | 長度 6，左右皆 3 |
| `()(())` | (1,0) → (1,1) → (2,1) → (3,1) → (3,2) → (3,3) | 長度 6，左右皆 3 |
| `()()()` | (1,0) → (1,1) → (2,1) → (2,2) → (3,2) → (3,3) | 長度 6，左右皆 3 |

例如第一個結果保存後，先還原最後加入的右括號，再逐層返回；在 `((` 的層級，左分支完成後還原回 `((`，獨立的右分支加入 `)`，便可繼續探索 `(()())` 與 `(())()`。到 `(` 層級再還原並探索右分支，才得到後兩個結果。

## 正確性與複雜度比較

### 為何完整且沒有重複

**暴力法：**每一格都探索兩種選擇，所以所有長度 `2n` 的括號字串恰好各生成一次。完整候選通過前綴與最終 balance 檢查才加入結果，因此沒有非法結果；任何有效組合都位於這棵完整決策樹內，因此不會遺漏。不同選擇路徑對應不同字串，不必額外去重。

**回溯法：**初始狀態符合不變量，左分支維持 `open ≤ n`，右分支維持 `close ≤ open`。只有長度 `2n` 才輸出，因此每個答案都有 `n` 對括號且所有前綴有效。反過來，任何合法答案的下一個左括號必然滿足 `open < n`，下一個右括號必然滿足 `close < open`，所以合法路徑都能被探索。每個字元位置的選擇也唯一決定字串，因此沒有重複。

回溯的每個有效前綴都能完成：先補完剩餘左括號，再補右括號即可。這也是剪枝不會錯刪合法解的重要依據。

### 時間與空間

有效組合數是 Catalan 數：`Cₙ = (1 / (n + 1)) · binomial(2n, n)`。

| 比較項目 | 暴力法 | 回溯法 |
| --- | --- | --- |
| 生成策略 | 所有完整候選生成後才驗證 | 維持合法前綴，只探索可完成路徑 |
| 時間上界 | `O(n · 4ⁿ)` | `O(n · Cₙ)` |
| 輔助空間，不含輸出 | `O(n)`：陣列與遞迴堆疊 | `O(n)`：StringBuilder 與遞迴堆疊 |
| 結果空間 | `O(n · Cₙ)` | `O(n · Cₙ)` |
| 結果隔離 | `new string(current)` | `cur.ToString()` |

暴力法有 `4ⁿ` 個葉節點，每個候選驗證至多讀取 `2n` 個字元；即使部分驗證提前返回，也可用 `O(n · 4ⁿ)` 作上界。保存有效字串還要複製 `2n` 個字元。

回溯法至少要輸出 `Cₙ` 個長度 `2n` 的字串，所以複製結果本身就需要 `Ω(n · Cₙ)` 時間。搜尋節點是有效答案路徑的前綴聯集，其數量不超過 `1 + 2n · Cₙ`；單次加入、移除與條件判斷為常數操作，因此總時間為 `O(n · Cₙ)`。遞迴深度最多 `2n`，輔助空間為 `O(n)`。

不能把回溯總時間寫成 `O(Cₙ)`，也不能把包含結果的總空間寫成 `O(n)`：每個答案的字元必須實際建立並保存。

| n | 暴力法完整候選數 4ⁿ | 有效結果數 Cₙ |
| --- | --- | --- |
| 1 | 4 | 1 |
| 2 | 16 | 2 |
| 3 | 64 | 5 |
| 4 | 256 | 14 |
| 5 | 1024 | 42 |
| 6 | 4096 | 132 |
| 7 | 16384 | 429 |
| 8 | 65536 | 1430 |

回溯也會拜訪中間前綴，不能把有效結果數誤認成全部遞迴呼叫數。以上比較未包含入口測試的集合比對、排序顯示等額外成本。

## 自動檢查與實際輸出

`Main` 一次執行 26 項檢查：

- 16 項單解法案例：兩種方法各測試 `n = 1…8`，預期數量採固定的 Catalan 數。
- 8 項跨解法集合比對：忽略順序，確認兩種方法產生相同集合。
- 2 項重複呼叫：同一實例依序執行多個 n 後，再執行 `n = 3`；確認舊結果仍正確、新結果正確且回傳不同集合物件。

`ValidateCombinations` 獨立檢查預期數量、沒有重複、字元只有括號、長度 `2n`、前綴 balance 非負與結尾歸零。`n = 1、2、3` 額外對照完整固定答案；較大的 n 以已知組合數、合法性及唯一性驗證。合法集合的大小達到已知全部答案數，便可確認沒有遺漏。

跨方法的 `SetEquals` 本身不檢查重複，因此單解法的唯一性檢查不能省略。`valid=True` 代表全部單解法條件通過，不只是字串 balance 正確。

小案例透過 `FormatCombinations` 使用 `StringComparer.Ordinal` 排序顯示，讓紀錄穩定；集合比較不依賴演算法的輸出順序，排序也不修改原集合。較大案例只輸出數量與驗證結果。

以下為本次 `dotnet run --project leetcode_022/leetcode_022.csproj --no-build` 的實際標準輸出；結束碼為 `0`：

```text
Case: OnePair_ReturnsOneCombination / GenerateParenthesis
Expected: count=1, valid=True, combinations=["()"]
Actual: count=1, valid=True, combinations=["()"]
Result: PASS
Case: OnePair_ReturnsOneCombination / GenerateParenthesis2
Expected: count=1, valid=True, combinations=["()"]
Actual: count=1, valid=True, combinations=["()"]
Result: PASS
Case: n=1 / BothMethods_ReturnSameSet
Expected: sameSet=True
Actual: sameSet=True
Result: PASS
Case: TwoPairs_ReturnsTwoCombinations / GenerateParenthesis
Expected: count=2, valid=True, combinations=["(())", "()()"]
Actual: count=2, valid=True, combinations=["(())", "()()"]
Result: PASS
Case: TwoPairs_ReturnsTwoCombinations / GenerateParenthesis2
Expected: count=2, valid=True, combinations=["(())", "()()"]
Actual: count=2, valid=True, combinations=["(())", "()()"]
Result: PASS
Case: n=2 / BothMethods_ReturnSameSet
Expected: sameSet=True
Actual: sameSet=True
Result: PASS
Case: ThreePairs_ReturnsFiveUniqueCombinations / GenerateParenthesis
Expected: count=5, valid=True, combinations=["((()))", "(()())", "(())()", "()(())", "()()()"]
Actual: count=5, valid=True, combinations=["((()))", "(()())", "(())()", "()(())", "()()()"]
Result: PASS
Case: ThreePairs_ReturnsFiveUniqueCombinations / GenerateParenthesis2
Expected: count=5, valid=True, combinations=["((()))", "(()())", "(())()", "()(())", "()()()"]
Actual: count=5, valid=True, combinations=["((()))", "(()())", "(())()", "()(())", "()()()"]
Result: PASS
Case: n=3 / BothMethods_ReturnSameSet
Expected: sameSet=True
Actual: sameSet=True
Result: PASS
Case: FourPairs_ReturnsFourteenCombinations / GenerateParenthesis
Expected: count=14, valid=True
Actual: count=14, valid=True
Result: PASS
Case: FourPairs_ReturnsFourteenCombinations / GenerateParenthesis2
Expected: count=14, valid=True
Actual: count=14, valid=True
Result: PASS
Case: n=4 / BothMethods_ReturnSameSet
Expected: sameSet=True
Actual: sameSet=True
Result: PASS
Case: FivePairs_ReturnsFortyTwoCombinations / GenerateParenthesis
Expected: count=42, valid=True
Actual: count=42, valid=True
Result: PASS
Case: FivePairs_ReturnsFortyTwoCombinations / GenerateParenthesis2
Expected: count=42, valid=True
Actual: count=42, valid=True
Result: PASS
Case: n=5 / BothMethods_ReturnSameSet
Expected: sameSet=True
Actual: sameSet=True
Result: PASS
Case: SixPairs_Returns132Combinations / GenerateParenthesis
Expected: count=132, valid=True
Actual: count=132, valid=True
Result: PASS
Case: SixPairs_Returns132Combinations / GenerateParenthesis2
Expected: count=132, valid=True
Actual: count=132, valid=True
Result: PASS
Case: n=6 / BothMethods_ReturnSameSet
Expected: sameSet=True
Actual: sameSet=True
Result: PASS
Case: SevenPairs_Returns429Combinations / GenerateParenthesis
Expected: count=429, valid=True
Actual: count=429, valid=True
Result: PASS
Case: SevenPairs_Returns429Combinations / GenerateParenthesis2
Expected: count=429, valid=True
Actual: count=429, valid=True
Result: PASS
Case: n=7 / BothMethods_ReturnSameSet
Expected: sameSet=True
Actual: sameSet=True
Result: PASS
Case: EightPairs_Returns1430Combinations / GenerateParenthesis
Expected: count=1430, valid=True
Actual: count=1430, valid=True
Result: PASS
Case: EightPairs_Returns1430Combinations / GenerateParenthesis2
Expected: count=1430, valid=True
Actual: count=1430, valid=True
Result: PASS
Case: n=8 / BothMethods_ReturnSameSet
Expected: sameSet=True
Actual: sameSet=True
Result: PASS
Case: RepeatedCalls_ReturnFreshUnchangedResults / GenerateParenthesis
Expected: freshAndUnchanged=True
Actual: freshAndUnchanged=True
Result: PASS
Case: RepeatedCalls_ReturnFreshUnchangedResults / GenerateParenthesis2
Expected: freshAndUnchanged=True
Actual: freshAndUnchanged=True
Result: PASS
Summary: 26/26 checks passed.
```
