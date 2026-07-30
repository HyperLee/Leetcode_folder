# LeetCode 383 — Ransom Note 贖金信

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)
![LeetCode Easy](https://img.shields.io/badge/LeetCode-Easy-00AF9B)

這是一個以 .NET 10 console project 實作的字串計數教學範例。專案保留直觀的
`List<char>` 搜尋移除法，並加入固定 26 格陣列的線性時間解法，用來比較
「直接模擬剪下字母」與「先統計庫存再消耗需求」兩種思考方式。

程式內建七組固定案例，會對兩種解法逐一比較預期值與實際值，共執行
14 項 PASS／FAIL 驗證。

## 快速連結

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：List 搜尋與移除](#解法一list-搜尋與移除)
- [解法二：固定 26 格字母計數](#解法二固定-26-格字母計數)
- [兩種解法比較](#兩種解法比較)
- [可執行案例](#可執行案例)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)

## 題目說明

給定兩個字串 `ransomNote` 與 `magazine`，判斷能否從 `magazine` 取出足夠的
字母組成 `ransomNote`：

- 能夠組成時回傳 `true`。
- 缺少任一需要的字母時回傳 `false`。
- `magazine` 中每一個位置的字元最多只能使用一次。
- `magazine` 可以包含沒有被使用的多餘字元，字母順序也不需要與
  `ransomNote` 相同。

例如，`ransomNote = "aa"`、`magazine = "aab"` 時，雜誌提供兩個 `a`，
足以組成勒索信，因此回傳 `true`。若雜誌是 `"ab"`，則只有一個 `a`，
結果為 `false`。

題目連結：[LeetCode 383 — Ransom Note](https://leetcode.com/problems/ransom-note/)

## 限制條件

- `1 <= ransomNote.length, magazine.length <= 10^5`
- `ransomNote` 與 `magazine` 只包含小寫英文字母。

> [!NOTE]
> 本專案依原題輸入契約設計，不另外驗證空字串、大寫字母或其他字元。
> 特別是固定陣列解法會直接使用 `letter - 'a'` 作為索引。

## 解題概念與出發點

本題的核心不是字母所在位置，而是 `magazine` 是否能提供
`ransomNote` 所需的每一份字母數量。

以 `ransomNote = "aa"` 為例，僅檢查 `magazine.Contains('a')` 並不足夠：
這只能證明至少存在一個 `a`，無法證明有兩個可分別使用的 `a`。因此，每次
匹配成功後，都必須反映「這個實體字元已被消耗」：

1. 解法一直接從可用字元清單移除已匹配的項目。
2. 解法二將對應字母的剩餘次數減一。

兩者維護的資訊相同，差別在於可用資源的表示方式。List 保留每個字元實體，
概念接近從雜誌剪下字母；計數陣列只保留 26 種字母各自的庫存數量，
因此不必反覆搜尋或搬移清單元素。

兩種解法都先檢查 `magazine.Length < ransomNote.Length`。每個雜誌字元只能
使用一次，所以雜誌總字元數較少時必定失敗，可以立即回傳 `false`。

## 解法一：List 搜尋與移除

### 設計說明

`CanConstruct` 將兩個字串轉成 `List<char>`。接著依序處理勒索信中的每個字母：

1. 使用 `IndexOf` 在目前尚未使用的雜誌字元中尋找相同字母。
2. 找不到時，表示該字母沒有庫存，立即回傳 `false`。
3. 找到時，以 `RemoveAt` 移除該位置，表示這一份字元已被使用。
4. 所有勒索信字母都完成匹配後回傳 `true`。

### 關鍵不變量

每次迴圈開始時，`magazines` 清單恰好保存所有「尚未被前面勒索信字母使用」
的雜誌字元。成功匹配後移除一個實體項目，可以確保後續相同字母無法重複使用
同一份資源。

### 正確性

- 如果 `IndexOf` 找不到目前需要的字母，尚未使用的雜誌字元中便沒有可供消耗
  的同字母資源，所以不可能完成勒索信。
- 如果找得到，移除其中一個同字母項目正好對應使用一次雜誌字元，且不影響
  其他尚未使用的字元。
- 若迴圈能處理完所有勒索信字母，代表每一個需求都匹配到不同的雜誌字元，
  因此可以組成勒索信。

### 範例演示

輸入 `ransomNote = "aa"`、`magazine = "aab"`：

| 步驟 | 需要的字母 | 搜尋結果 | 移除後的可用雜誌字元 |
| ---: | --- | --- | --- |
| 初始 | — | — | `[a, a, b]` |
| 1 | `a` | 索引 `0` | `[a, b]` |
| 2 | `a` | 索引 `0` | `[b]` |

兩個 `a` 都各自找到並消耗一個雜誌字元，因此回傳 `true`。

### 複雜度與取捨

令 `r` 為 `ransomNote` 長度，`m` 為 `magazine` 長度：

- 建立兩個 List 需要 `O(r + m)` 時間與空間。
- 每個勒索信字母的 `IndexOf` 最壞需要掃描 `O(m)` 個項目。
- `RemoveAt` 可能需要搬移後續項目，最壞同樣是 `O(m)`。
- 總時間複雜度最壞為 `O(r × m)`；當兩個字串長度相近時可視為 `O(n²)`。
- 輔助空間複雜度為 `O(r + m)`。

這種寫法直接呈現題目「找到一個就剪下一個」的敘事，容易理解，但不適合
接近 `10^5` 長度上限的大型輸入。

## 解法二：固定 26 格字母計數

### 設計說明

`CanConstruct2` 利用題目只包含 `a` 到 `z` 的條件，建立 `int[26]`：

1. 走訪 `magazine`，以 `letter - 'a'` 取得索引並將庫存加一。
2. 走訪 `ransomNote`，準備消耗目前字母的庫存。
3. 若對應計數已是零，代表該字母從未出現或已被用完，立即回傳 `false`。
4. 否則將計數減一，繼續處理下一個需求。
5. 所有需求都成功消耗後回傳 `true`。

### 關鍵不變量

處理勒索信的任一步之前，`letterCounts[index]` 表示該字母在雜誌中出現的
總次數，減去先前勒索信字母已消耗的次數。計數永遠代表目前可以繼續使用的
實際庫存。

### 正確性

- 統計完成後，陣列精確保存雜誌中每個小寫英文字母的可用次數。
- 每處理一個勒索信字母就將相同字母的計數減一，因此每份庫存最多使用一次。
- 若消耗前計數為零，代表需求量已超過供應量，勒索信不可能完成。
- 若所有需求都成功消耗，則每個勒索信字元都對應到一份不同的雜誌庫存，
  因此可以組成勒索信。

### 範例演示

輸入 `ransomNote = "aa"`、`magazine = "aab"`。統計雜誌後，
非零庫存為 `a: 2, b: 1`：

| 步驟 | 需要的字母 | 消耗前庫存 | 消耗後庫存 | 判斷 |
| ---: | --- | ---: | ---: | --- |
| 1 | `a` | 2 | 1 | 庫存足夠，繼續 |
| 2 | `a` | 1 | 0 | 庫存足夠，繼續 |

所有需求都完成，雖然還剩一個未使用的 `b`，仍應回傳 `true`。

### 複雜度與取捨

- 統計雜誌需要 `O(m)` 時間。
- 消耗勒索信需求需要 `O(r)` 時間。
- 總時間複雜度為 `O(r + m)`。
- 陣列固定為 26 格，輔助空間複雜度為 `O(26)`，簡化為 `O(1)`。

固定陣列避免 List 的反覆線性搜尋與元素搬移，能穩定處理題目允許的大型輸入。
代價是索引設計直接依賴小寫英文字母契約；若字元集合擴大，應改用
`Dictionary<char, int>` 或其他適合的頻率表。

## 兩種解法比較

| 比較項目 | List 搜尋移除 | 固定 26 格計數 |
| --- | --- | --- |
| 主要 API | `CanConstruct` | `CanConstruct2` |
| 資源表示 | 每個尚未使用的字元實體 | 每種字母的剩餘次數 |
| 消耗方式 | `IndexOf` 後 `RemoveAt` | 對應計數減一 |
| 不足判斷 | 搜尋結果為 `-1` | 消耗前計數為 `0` |
| 時間複雜度 | 最壞 `O(r × m)` | `O(r + m)` |
| 輔助空間 | `O(r + m)` | `O(1)` |
| 優勢 | 模擬剪貼過程，概念直觀 | 固定空間、線性時間 |
| 限制 | 大量搜尋與搬移成本較高 | 依賴僅含 `a` 到 `z` 的契約 |

## 可執行案例

| 案例 | `ransomNote` | `magazine` | 預期 | 驗證重點 |
| ---: | --- | --- | --- | --- |
| 1 | `"a"` | `"b"` | `false` | 官方範例：需要的字母不存在 |
| 2 | `"aa"` | `"ab"` | `false` | 官方範例：重複字母數量不足 |
| 3 | `"aa"` | `"aab"` | `true` | 官方範例：重複字母數量足夠 |
| 4 | `"a"` | `"a"` | `true` | 題目允許的最小長度 |
| 5 | `"ab"` | `"a"` | `false` | 雜誌較短時提早結束 |
| 6 | `"abc"` | `"cba"` | `true` | 字母順序不影響結果 |
| 7 | `"ab"` | `"adcb"` | `true` | 雜誌可以包含多餘字元 |

每一組案例都會交給兩種解法，並將實際布林值與手動設定的預期值直接比較。
七組案例乘以兩種解法，共有 14 項驗證。

目前沒有獨立的自動化測試專案；本教學範例以成功建置及固定 console harness
全部顯示 `PASS` 作為行為驗收。

## 專案結構

```
leetcode_383/
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_383/
    ├── leetcode_383.csproj
    └── Program.cs
```

- `leetcode_383/Program.cs`：進入點、七組案例、驗證流程及兩種演算法。
- `leetcode_383/leetcode_383.csproj`：目標框架為 `net10.0` 的 console project。
- `docs/readme-template.md`：首次建立 README 時使用的內容與驗證指引。
- `README.md`：題目、兩種解法、演示流程及實際執行結果。

## 建置與執行

需要安裝支援 `net10.0` 的 .NET 10 SDK。請從此 repository 根目錄執行：

```bash
dotnet restore leetcode_383/leetcode_383.csproj --nologo
dotnet build leetcode_383/leetcode_383.csproj --no-restore --nologo
dotnet run --project leetcode_383/leetcode_383.csproj --no-build
```

## 實際執行結果

以下內容來自 fresh
`dotnet run --project leetcode_383/leetcode_383.csproj --no-build`：

```text
案例 1：官方範例 1：找不到字母
  輸入：ransomNote = "a", magazine = "b"
  預期：False
  List 搜尋移除：False => PASS
  固定 26 格計數：False => PASS

案例 2：官方範例 2：重複字母不足
  輸入：ransomNote = "aa", magazine = "ab"
  預期：False
  List 搜尋移除：False => PASS
  固定 26 格計數：False => PASS

案例 3：官方範例 3：重複字母足夠
  輸入：ransomNote = "aa", magazine = "aab"
  預期：True
  List 搜尋移除：True => PASS
  固定 26 格計數：True => PASS

案例 4：最小長度且內容相同
  輸入：ransomNote = "a", magazine = "a"
  預期：True
  List 搜尋移除：True => PASS
  固定 26 格計數：True => PASS

案例 5：雜誌長度不足
  輸入：ransomNote = "ab", magazine = "a"
  預期：False
  List 搜尋移除：False => PASS
  固定 26 格計數：False => PASS

案例 6：字母順序不同
  輸入：ransomNote = "abc", magazine = "cba"
  預期：True
  List 搜尋移除：True => PASS
  固定 26 格計數：True => PASS

案例 7：雜誌包含多餘字元
  輸入：ransomNote = "ab", magazine = "adcb"
  預期：True
  List 搜尋移除：True => PASS
  固定 26 格計數：True => PASS

總結：14/14 項演算法驗證通過
```

## 驗證方式

完成修改後依序確認：

- `dotnet restore` 成功還原專案。
- `dotnet build` 為 0 個警告、0 個錯誤。
- console harness 的 14 項演算法驗證全部顯示 `PASS`。
- README 記錄的完整輸出與 fresh `dotnet run` 結果一致。
- `git diff --check` 無多餘空白或換行錯誤。
