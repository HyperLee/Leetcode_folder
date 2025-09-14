# LeetCode 966 - Vowel Spellchecker

本專案收錄 C# 的範例實作（位於 `leetcode_961` 專案），針對 LeetCode 題目 966："Vowel Spellchecker" 提供可執行的參考實作與詳細解題說明。

## 專案概述

- 範例程式檔案：`leetcode_961/Program.cs`
- 解法類型：哈希映射（HashMap / Dictionary）
- 語言：C#（使用 .NET 8 / net8.0）

本專案的程式會示範如何使用三種哈希資料結構來分別處理題目要求的三種比對優先順序（完全匹配、忽略大小寫匹配、忽略元音匹配）。README 主要聚焦在解題思路與如何在本機建置、執行範例測試。

## 題目重點（簡要）

給定一個字詞表 `wordlist` 與多個查詢 `queries`，要對每個查詢回傳最符合的字詞表單字（或空字串）：

優先順序：
1) 完全匹配（區分大小寫）
2) 忽略大小寫匹配（回傳字詞表中第一個符合此條件的原始單字）
3) 忽略元音匹配（先將元音視為通配符，再忽略大小寫，回傳字詞表中第一個符合的原始單字）
4) 若皆無匹配，回傳空字串

元音集合：`a, e, i, o, u`（不分大小寫）。

## 解法概述（高層）

使用三個資料結構：

- 完全匹配：`HashSet<string> wordsPerfect`，儲存字詞表中原始單字，支援 O(1) 查詢。
- 忽略大小寫匹配：`Dictionary<string,string> wordsCap`，鍵為單字小寫形式，值為字詞表中第一個以該小寫形式出現的原始單字。
- 忽略元音匹配：`Dictionary<string,string> wordsVow`，鍵為小寫且把元音替換為 `'*'` 的形式（稱為 devowel form），值為字詞表中第一個對應的原始單字。

建立索引時，對於 `wordsCap` 與 `wordsVow` 只在鍵不存在時寫入對應值，以保證回傳字詞表中**最先出現**的原始單字。

查詢過程（對單一 query）：
1) 若 `wordsPerfect` 含有原始 query（區分大小寫），回傳 query 本身。
2) 否則將 query 轉為小寫，若 `wordsCap` 有對應鍵則回傳其值。
3) 否則再把小寫 query 的元音都替換為 `'*'`，若 `wordsVow` 有對應鍵則回傳其值。
4) 若都沒有，回傳空字串（`""`）。

## 詳細實作步驟（逐步說明）

1. 初始化三個資料結構：

   - `wordsPerfect = new HashSet<string>()`
   - `wordsCap = new Dictionary<string,string>()`
   - `wordsVow = new Dictionary<string,string>()`

2. 對 `wordlist` 中每個 `word` 執行：

   - 把 `word` 加入 `wordsPerfect`。
   - 計算 `wordLower = word.ToLower()`，若 `wordsCap` 尚未包含 `wordLower`，則 `wordsCap[wordLower] = word`。
   - 計算 `devowel(wordLower)`（把所有元音替換為 `'*'`），若 `wordsVow` 尚未包含這個 voweled 形式，則 `wordsVow[devowel] = word`。

   這裡的設計保證了「先出現的原始單字」優先被保留。

3. 對每個 `query`：

   - 若 `wordsPerfect.Contains(query)`，回傳 `query`。
   - 計算 `qLower = query.ToLower()`，若 `wordsCap` 含有 `qLower`，回傳 `wordsCap[qLower]`。
   - 計算 `qDevowel = devowel(qLower)`，若 `wordsVow` 含有 `qDevowel`，回傳 `wordsVow[qDevowel]`。
   - 否則回傳空字串。

4. `devowel(s)` 的實作：建立 `StringBuilder` 並對每個字元執行 `IsVowel(c) ? '*' : c`，最後回傳新字串。

5. `IsVowel(c)`：將字元小寫化後比較是否為 `a|e|i|o|u`。

### 設計關鍵與注意事項

- 在建立索引時對 `wordsCap` & `wordsVow` 使用「只在鍵不存在時寫入」的策略，以保留字詞表中第一個出現的原始單字，符合題目要求。
- Devowel 形式將所有元音視為相同的通配符 `'*'`，因此任何元音間的差異都會被忽略。
- 全部的比較（除完全匹配外）都會先轉為小寫處理，確保大小寫不影響匹配。

## 時間與空間複雜度

- 建構索引：對每個單字長度為 L 的單字做常數時間的處理（toLower、devowel），總計 O(N * L)。
- 查詢：每個 query 亦為 O(L) 操作（worst-case 檢查三個 hash 查詢，但每個查詢為 O(L) 的字串轉換 + O(1) 的字典查詢），總計 O(Q * L)。
- 空間：三個資料結構儲存字詞表資訊，需 O(N * L) 空間。

## 範例

程式內含兩組測試資料（可在 `Main` 看到）：

- Test 1（混合大小寫）：

  wordlist: `["KiTe","kite","hare","Hare"]`

  queries: `["kite","Kite","KiTe","Hare","HARE","Hear","hear","keti","keet","keto"]`

  範例輸出（程式執行時會列印）：

  - `kite` -> `kite`  (完全匹配)
  - `Kite` -> `KiTe`  (忽略大小寫，回傳字詞表中第一個出現的小寫對應)
  - `KiTe` -> `KiTe`
  - `Hare` -> `Hare`
  - `HARE` -> `hare`  (忽略大小寫)
  - `Hear` -> `` (無匹配)
  - `hear` -> `` (無匹配)
  - `keti` -> `KiTe`  (忽略元音)
  - `keet` -> ``
  - `keto` -> `KiTe`

- Test 2（元音與大小寫示範）：

  wordlist: `["yellow"]`

  queries: `["YellOw","yellow","YEllow","yollow","yllw"]`

  範例輸出：

  - `YellOw` -> `yellow` (忽略大小寫)
  - `yellow` -> `yellow`
  - `YEllow` -> `yellow`
  - `yollow` -> `yellow` (忽略元音)
  - `yllw` -> ``

實際輸出請以執行結果為準（`Program.Main` 已印出這些測試的結果）。

## 如何 建置 與 執行

此專案使用 .NET SDK（範例使用 net8.0）。

在 PowerShell（Windows）或其他相容終端機中執行：

```powershell
# 在專案根目錄（含 leetcode_961.sln）的情況下
dotnet build leetcode_961/leetcode_961.csproj

# 執行已建置的程式
dotnet run --project leetcode_961/leetcode_961.csproj
```

輸出會在主控台列印 Test 1 與 Test 2 的查詢與結果，便於手動驗證解法的正確性。

## 檔案說明

- `leetcode_961/Program.cs`：主程式與解法實作，包含 `Spellchecker`, `Solve`, `Devowel`, `IsVowel` 等方法，以及 `Main` 中的測試程式。
- `leetcode_961/leetcode_961.csproj`：.NET 專案設定檔。
- `leetcode_961.sln`：解決方案檔。

## 可改進/延伸（建議）

- 新增單元測試（xUnit/NUnit）來自動化驗證多組情境（happy path、邊界條件、重複字詞表元素）。
- 若要處理更多語言或字元集，應擴充 `IsVowel` 的定義或使用 Unicode 分類方法。
- 若字詞表非常大且要求低延遲，可考慮使用更高效的字串雜湊或壓縮索引策略。

---

若你希望我幫忙 建立 單元測試或把 README 翻成英文版本，我可以接著執行這些步驟。歡迎告訴我下一步需求。
# Leetcode_folder
 存放Leetcode題目解法<br>
英文- https://leetcode.com/problemset/all/ <br>
中文- https://leetcode.cn/problemset/all/ <br>

// 2024/12/25 memo <br>
原先都使用 傳統的 .NET Framework 專案建立 <br>
目前逐步汰換更改為 .NET Core 專案 (.NET 8.0)。 <br>