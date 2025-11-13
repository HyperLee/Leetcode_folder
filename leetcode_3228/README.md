# 3228. Maximum Number of Operations to Move Ones to the End

## 題目 (來源)

- LeetCode 題號：3228
- 題目連結：[題目連結](https://leetcode.cn/problems/maximum-number-of-operations-to-move-ones-to-the-end/)

## 簡短說明

- 本專案實作位於 `leetcode_3228/leetcode_3228/Program.cs` 中的 `MaxOperations(string s)` 函式。
- 問題：輸入一個只包含 `'0'` 與 `'1'` 的二元字串，計算把所有 `'1'` 移到字串末端可執行的最大操作次數。

## 核心想法（直覺）

- 從左到右掃描字串，維護已遇到的 `'1'` 數量（記為 `onesCount`）。
- 每當遇到一段連續的零（zero segment），該零段可以讓它左側每個 `'1'` 各執行一次向右移動。
- 因此，對每一段連續零，把目前的 `onesCount` 累加到結果（`operations`）。
- 這是一個貪心 + 計數策略：一次掃描即可得到最大操作次數。

## 為何貪心是正確的（簡要正當性）

- 對任一連續零段，其能提供的最大操作數不可能超過「該段左側的 `'1'` 數量」。因此整體上界為對所有零段求左側 `'1'` 數量之和。
- 採用從左到右累計 `onesCount` 並在遇到每個零段時計入，恰好達到上述上界，故為最優。
- 補強直覺：若有非貪心的操作分配，可以透過交換論證把操作重新分配給更左側的 `'1'` 而不降低總數，最終可達到本演算法的分配。

## 時間與空間複雜度

- 時間複雜度：O(n)，其中 n = `s.Length`。每個索引最多被常數次訪問（零段跳過仍屬線性）。
- 額外空間：O(1)，僅使用少量整數變數（`onesCount`、`operations`、`index`）。
- 注意：回傳型別為 `int`，在極大輸入下（特別是交替字元造成大量累加）有整數溢位風險，必要時可改為 `long`。

## 邊界情況與函式行為

- `null`：函式會拋出 `ArgumentNullException`。
- 空字串 `""`：回傳 `0`（主迴圈不執行）。
- 全為 `'0'`（如 `"0"`, `"000"`）：回傳 `0`。
- 全為 `'1'`（如 `"1"`, `"111"`）：回傳 `0`。
- 交替字元（如 `"101010"`）：每個零為一段，會依序累加 `onesCount`，得到較大的總和，函式正確計算。
- 非 `'0'`/`'1'` 字元：目前程式僅檢查 `s[index] == '0'`，否則走 `else` 分支將其視作 `'1'`。若輸入有可能包含其他字元，建議在函式加入輸入驗證並在遇到非法字元時拋出 `ArgumentException`。

## 逐步範例（按字元掃描，列出 `index`、`onesCount`、`operations` 的變化）

- 註：內部邏輯會將連續的零視為一段，並在找到該零段末尾時將 `operations += onesCount`。

**範例 A：`"010010"`**

- 初始：`index=0`, `onesCount=0`, `operations=0`
- step0: `index=0`, `s[0]='0'`，零段長度為 1（下一為 '1'），`operations += 0` → `operations=0`，然後 `index++` → `index=1`
- step1: `index=1`, `s[1]='1'`，`onesCount++` → `onesCount=1`，`index=2`
- step2: `index=2`, `s[2]='0'`，發現下一為 `s[3]='0'`，跳過至 `index=3`（連續零段 index=2..3），離開內層後 `operations += onesCount` → `operations=1`，然後 `index++` → `index=4`
- step3: `index=4`, `s[4]='1'`，`onesCount++` → `onesCount=2`，`index=5`
- step4: `index=5`, `s[5]='0'`，零段長度 1，`operations += onesCount` → `operations=3`，`index=6`（迴圈結束）
- 結果：回傳 `3`。

**範例 B：`"101010"`**

- 初始：`index=0`, `onesCount=0`, `operations=0`
- step0: `index=0`, `s[0]='1'`，`onesCount=1`，`index=1`
- step1: `index=1`, `s[1]='0'`，零段長度 1，`operations += 1` → `operations=1`，`index=2`
- step2: `index=2`, `s[2]='1'`，`onesCount=2`，`index=3`
- step3: `index=3`, `s[3]='0'`，`operations += 2` → `operations=3`，`index=4`
- step4: `index=4`, `s[4]='1'`，`onesCount=3`，`index=5`
- step5: `index=5`, `s[5]='0'`，`operations += 3` → `operations=6`，`index=6`（結束）
- 結果：回傳 `6`。

## 執行說明（在本機測試）

- 專案入口為 `leetcode_3228/leetcode_3228/Program.cs`。
- 若要在 macOS（`zsh`）下執行：

```bash
cd /Users/qiuzili/Leetcode/Leetcode_folder/leetcode_3228
dotnet run --project leetcode_3228/leetcode_3228.csproj
```

- 注意：`Program.cs` 中 `Main` 已包含數組測試清單，執行後會逐一輸出各測資的 `MaxOperations` 結果。

## 改進建議（可選）

- **輸入驗證**：在 `MaxOperations` 開頭加入對字串是否只含 `'0'` 與 `'1'` 的檢查，若有非法字元則丟 `ArgumentException`，避免錯誤結果。
- **避免整數溢位**：若需處理非常長的字串，將 `operations` 與 `onesCount` 改用 `long`，或在回傳前檢查是否超出 `int.MaxValue`。
- **變數命名**：可將 `onesCount` 改為 `onesSeen` 或 `onesBefore`，`operations` 改為 `maxOperations`，讓程式更具可讀性。

---

如需我同時套用上述改進（例如把 `int` 改為 `long`、加入輸入驗證與變數重新命名），我可以再產生對應的補丁（`apply_patch`）供你審閱。

