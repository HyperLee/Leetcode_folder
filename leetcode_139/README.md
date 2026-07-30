# LeetCode 139 — Word Break（單詞拆分）

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![LeetCode](https://img.shields.io/badge/LeetCode-139-FFA116)](https://leetcode.com/problems/word-break/)

這是一個可直接建置與執行的 .NET 10 主控台教學專案，使用「動態規劃」與「廣度優先搜尋」兩種觀點解決 Word Break，並透過 7 筆固定案例交叉驗證兩種解法。

## 快速連結

- [題目說明](#題目說明)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：動態規劃](#解法一動態規劃)
- [解法二：廣度優先搜尋](#解法二廣度優先搜尋)
- [建置與執行](#建置與執行)
- [完整執行結果](#完整執行結果)

## 題目說明

給定字串 `s` 與字串字典 `wordDict`，如果 `s` 可以完整拆分成一個或多個字典中的單字，回傳 `true`；否則回傳 `false`。

字典中的單字不需要全部使用，同一個單字可以重複使用。題目只詢問「是否存在」合法拆分，不需要回傳實際拆分內容。

### 官方範例

| 輸入字串 | 字典 | 輸出 | 說明 |
| --- | --- | --- | --- |
| `"leetcode"` | `["leet", "code"]` | `true` | 可拆成 `"leet" + "code"` |
| `"applepenapple"` | `["apple", "pen"]` | `true` | 可拆成 `"apple" + "pen" + "apple"`，其中 `"apple"` 重複使用 |
| `"catsandog"` | `["cats", "dog", "sand", "and", "cat"]` | `false` | 所有可能切分最後都無法完整抵達字串尾端 |

### 限制條件

以下限制依 [LeetCode 139 官方題目](https://leetcode.com/problems/word-break/)：

- `1 <= s.Length <= 300`
- `1 <= wordDict.Count <= 1000`
- `1 <= wordDict[i].Length <= 20`
- `s` 與 `wordDict[i]` 只包含小寫英文字母。
- `wordDict` 中的字串互不相同。

## 解題概念與出發點

本題最重要的觀察不是「每次挑最長或最短的單字」，而是判斷哪些**切分位置可以抵達**。

以 `"leetcode"` 為例，索引可以畫成：

```text
0 ----"leet"----> 4 ----"code"----> 8
```

- 索引 `0` 表示尚未取用任何字元。
- 如果 `s[start..end]` 是字典單字，就能從切分位置 `start` 前往 `end`。
- 只要能從 `0` 抵達 `s.Length`，整個字串就能完整拆分。

動態規劃依索引順序計算「哪些位置可達」；BFS 則把索引當成圖節點，由可達位置向外搜尋。兩者其實是在不同資料結構上表達同一個可達性問題。

### 解法比較

令：

- `n` 為 `s.Length`。
- `d` 為字典單字數量。
- `w` 為字典內所有單字的總字元數。
- `m` 為最長字典單字長度，題目限制下 `m <= 20`。

| 解法 | 狀態表示 | 搜尋方向 | 時間複雜度 | 輔助空間 |
| --- | --- | --- | --- | --- |
| 動態規劃 | `dp[i]` 表示前 `i` 個字元可拆分 | 依 `i` 由左至右列舉分割點 | `O(w + n³)` | `O(d + n)` |
| BFS | 佇列保存尚待展開的可達索引 | 從索引 `0` 逐層探索 | `O(w + n³)` | `O(d + n × m)`；在 `m <= 20` 下可視為 `O(d + n)` |

> [!NOTE]
> 兩個實作都會呼叫 C# `Substring`。現代 .NET 會為子字串配置並複製字元，HashSet 查詢也需要計算該子字串的雜湊值，因此一次候選檢查不是理想化的 `O(1)`。把所有候選子字串長度加總後，最壞時間為 `O(n³)`；執行期間也會產生 `O(n³)` 等級的累計暫時字串配置量，但單一暫時子字串最長為 `O(n)`。

兩種公開方法都只讀取 `s` 與 `wordDict`，不會改動呼叫端傳入的資料。

## 解法一：動態規劃

### 設計

`WordBreak` 使用長度為 `n + 1` 的布林陣列：

```text
dp[i] = s 的前 i 個字元是否能由字典單字完整拼接
```

1. 設定 `dp[0] = true`。空前綴是所有拆分路徑的起點，不代表題目允許空字串輸入。
2. 依序計算 `dp[1]` 到 `dp[n]`。
3. 對每個結尾位置 `i`，列舉分割點 `j`：
   - `dp[j]` 必須為 `true`，代表分割點以前已能合法拆分。
   - `s.Substring(j, i - j)` 必須存在於字典。
4. 兩個條件都成立時，把 `dp[i]` 設為 `true`。同一個 `i` 已證明可達後即可停止列舉其他 `j`。
5. 回傳 `dp[n]`。

這個狀態設計會保留所有可行前綴，不會因為過早選擇某個局部單字而錯過其他切分方式。

### 範例演示：`"leetcode"`

字典為 `["leet", "code"]`：

| `i` | 檢查重點 | `dp[i]` |
| ---: | --- | --- |
| 0 | 基礎狀態：空前綴 | `true` |
| 1–3 | `"l"`、`"le"`、`"lee"` 均不在字典 | `false` |
| 4 | `dp[0] = true` 且 `s[0..4] = "leet"` 在字典 | `true` |
| 5–7 | 從可達位置切出的 `"c"`、`"co"`、`"cod"` 均不是完整字典單字 | `false` |
| 8 | `dp[4] = true` 且 `s[4..8] = "code"` 在字典 | `true` |

最後 `dp[8] = true`，因此回傳 `true`。

## 解法二：廣度優先搜尋

### 設計

`WordBreak2` 把切分索引視為圖節點：

1. 把起點 `0` 放入佇列。
2. 每次取出一個 `start`，列舉所有 `end`，建立候選子字串 `s[start..end]`。
3. 如果候選子字串存在於字典：
   - `end == s.Length`：已抵達終點，立即回傳 `true`。
   - 否則把 `end` 加入佇列，稍後繼續從該位置搜尋。
4. `visited` 記錄已展開的起點。同一索引之後能形成的候選完全相同，因此不必重複展開。
5. 佇列耗盡仍未抵達尾端時回傳 `false`。

### 範例演示：`"leetcode"`

字典為 `["leet", "code"]`：

| 步驟 | 佇列／節點 | 動作 |
| ---: | --- | --- |
| 1 | `[0]` | 從索引 `0` 開始 |
| 2 | 取出 `0` | 找到 `"leet"`，其結尾索引為 `4`，把 `4` 加入佇列 |
| 3 | `[4]` | 下一個可達前綴是 `"leet"` |
| 4 | 取出 `4` | 找到 `"code"`，其結尾索引為 `8` |
| 5 | `8 == s.Length` | 抵達字串尾端，回傳 `true` |

### 為什麼局部匹配仍可能失敗

對 `"catsandog"`：

- 從索引 `0` 可以匹配 `"cat"` 抵達 `3`，或匹配 `"cats"` 抵達 `4`。
- 從 `3` 可用 `"sand"` 抵達 `7`；從 `4` 也可用 `"and"` 抵達 `7`。
- 剩餘字串是 `"og"`，不在字典中。
- 雖然前半段存在多條合法路徑，沒有任何路徑能抵達索引 `9`，因此結果仍為 `false`。

DP 會得到 `dp[7] = true` 但 `dp[9] = false`；BFS 則會展開索引 `7` 後找不到新節點。兩種解法以不同方式得到相同結論。

## 可執行案例

`Main` 會讓兩種解法分別執行以下 7 筆案例，因此總共有 14 項驗證：

| 案例 | `s` | `wordDict` | 預期 | 驗證目的 |
| ---: | --- | --- | --- | --- |
| 1 | `"leetcode"` | `["leet", "code"]` | `true` | 基本成功拆分 |
| 2 | `"applepenapple"` | `["apple", "pen"]` | `true` | 重複使用同一單字 |
| 3 | `"catsandog"` | `["cats", "dog", "sand", "and", "cat"]` | `false` | 局部匹配但無法完成 |
| 4 | `"aaaaaaa"` | `["aaaa", "aaa"]` | `true` | 組合不同長度單字 |
| 5 | `"cars"` | `["car", "ca", "rs"]` | `true` | 必須選擇可抵達終點的分割點 |
| 6 | `"a"` | `["a"]` | `true` | 最小合法輸入 |
| 7 | `"aaaaab"` | `["a", "aa", "aaa"]` | `false` | 長前綴可拆但尾端失敗 |

## 建置與執行

### 環境需求

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

從此 repository 根目錄執行：

```bash
dotnet restore leetcode_139/leetcode_139.csproj
dotnet build leetcode_139/leetcode_139.csproj --nologo
dotnet run --project leetcode_139/leetcode_139.csproj --no-build
```

目前沒有獨立的自動化測試專案；驗收方式是成功建置，並執行 `Main` 中固定且會自行比對預期值的案例。

## 完整執行結果

```text
案例 1
輸入：s = "leetcode", wordDict = ["leet", "code"]
預期：true
動態規劃：true => PASS
廣度優先搜尋：true => PASS

案例 2
輸入：s = "applepenapple", wordDict = ["apple", "pen"]
預期：true
動態規劃：true => PASS
廣度優先搜尋：true => PASS

案例 3
輸入：s = "catsandog", wordDict = ["cats", "dog", "sand", "and", "cat"]
預期：false
動態規劃：false => PASS
廣度優先搜尋：false => PASS

案例 4
輸入：s = "aaaaaaa", wordDict = ["aaaa", "aaa"]
預期：true
動態規劃：true => PASS
廣度優先搜尋：true => PASS

案例 5
輸入：s = "cars", wordDict = ["car", "ca", "rs"]
預期：true
動態規劃：true => PASS
廣度優先搜尋：true => PASS

案例 6
輸入：s = "a", wordDict = ["a"]
預期：true
動態規劃：true => PASS
廣度優先搜尋：true => PASS

案例 7
輸入：s = "aaaaab", wordDict = ["a", "aa", "aaa"]
預期：false
動態規劃：false => PASS
廣度優先搜尋：false => PASS

總結：14/14 項驗證通過
```

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_139/
│   ├── Program.cs
│   └── leetcode_139.csproj
└── leetcode_139.sln
```

- `leetcode_139/Program.cs`：兩種演算法、測試案例與主控台驗證入口。
- `leetcode_139/leetcode_139.csproj`：目標框架為 `net10.0` 的可執行專案。
- `docs/readme-template.md`：首次建立 README 時採用的文件指引。
