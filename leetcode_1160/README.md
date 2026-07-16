# LeetCode 1160：拼寫單字

![C#](https://img.shields.io/badge/C%23-Console-512BD4?logo=csharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)

這個專案使用 C# 實作 LeetCode 1160「Find Words That Can Be Formed by Characters」，並以可直接執行的主控台案例比較兩種解法：逐字移除與字頻統計。

- [英文題目](https://leetcode.com/problems/find-words-that-can-be-formed-by-characters/description/)
- [中文題目](https://leetcode.cn/problems/find-words-that-can-be-formed-by-characters/description/)

## 題目說明

給定一個字串陣列 `words` 與一個字串 `chars`。如果某個單字可以使用 `chars` 中的字元拼出來，該單字就是 good string。

判斷時必須注意兩件事：

1. `chars` 中的每個字元，在組成同一個單字時只能使用一次。
2. 不同單字要分開判斷；開始檢查下一個單字時，可以重新使用完整的 `chars`。

最後回傳所有 good strings 的長度總和。

### 限制條件

- `1 <= words.length <= 1000`
- `1 <= words[i].length, chars.length <= 100`
- `words[i]` 與 `chars` 都只包含小寫英文字母。

限制與範例可參考 [LeetCode 1160 官方頁面](https://leetcode.com/problems/find-words-that-can-be-formed-by-characters/editorial/)。

### 官方範例一

```text
輸入：words = ["cat", "bt", "hat", "tree"], chars = "atach"
輸出：6
```

`cat` 與 `hat` 都可以被組成，所以答案是 `3 + 3 = 6`。

### 官方範例二

```text
輸入：words = ["hello", "world", "leetcode"], chars = "welldonehoneyr"
輸出：10
```

`hello` 與 `world` 都可以被組成，所以答案是 `5 + 5 = 10`。

## 解題概念與出發點

最重要的問題不是「`chars` 是否包含某個字母」，而是「`chars` 是否包含足夠數量的每一個字母」。

例如 `chars = "ab"`：

- 可以組成 `"a"`，因為有一個 `a`。
- 可以組成 `"ab"`，因為 `a` 與 `b` 各有一個。
- 不能組成 `"aa"`，因為第二個 `a` 已經沒有可用字元。

因此，每一種解法都必須記錄字元的剩餘數量。這個專案採用兩種不同表示方式：

1. 找到字元後，直接從可用字串移除一個字元。
2. 先統計每個字元出現幾次，再比較供應量與需求量。

## 解法比較

| 解法 | 核心資料結構 | 判斷方式 | 時間複雜度 | 額外空間 |
| --- | --- | --- | --- | --- |
| `CountCharacters` | 可用字元的字串副本 | 找到一個就移除一個 | `O(Σ|word| × |chars|)` | `O(|chars|)` |
| `CountCharacters2` | `Dictionary<char, int>` | 比較每個字元的供應量與需求量 | 平均 `O(|chars| + Σ|word|)` | 題目限制下為 `O(26)` |

`Σ|word|` 表示 `words` 中所有單字的字元總數。

## 解法一：逐字尋找並移除

### 設計方式

`CountCharacters` 對每個單字建立一份完整的 `chars` 副本。接著依序處理單字中的每個字元：

1. 在剩餘字串中尋找目前字元。
2. 找得到時，移除其中一個相符字元。
3. 找不到時，立即判定這個單字無法組成。
4. 整個單字都配對成功後，才把單字長度加入答案。

移除已使用的字元，是這個解法處理重複字母的關鍵。若只使用 `Contains` 而不移除，`chars = "ab"` 就會被錯誤判定為能組成 `"aa"`。

### 範例演示

使用 `words = ["cat", "bt", "hat", "tree"]`、`chars = "atach"`。

#### `cat`

| 需要字元 | 使用前剩餘字元 | 處理結果 | 使用後剩餘字元 |
| --- | --- | --- | --- |
| `c` | `atach` | 找到並移除 | `atah` |
| `a` | `atah` | 找到並移除 | `tah` |
| `t` | `tah` | 找到並移除 | `ah` |

三個字元都成功配對，累加 `cat.Length = 3`。

#### `bt`

下一個單字重新取得完整的 `"atach"`。第一個需要的字元 `b` 不存在，因此立即失敗，不累加長度。

#### `hat`

```text
atach --使用 h--> atac --使用 a--> tac --使用 t--> ac
```

三個字元都成功配對，再累加 `hat.Length = 3`。

#### `tree`

先找到 `t`，但下一個字元 `r` 不存在，因此失敗。

最後結果為：

```text
cat.Length + hat.Length = 3 + 3 = 6
```

### 複雜度

`Contains`、`IndexOf` 與 `Remove` 都可能掃描或複製可用字串。若單字總字元數為 `Σ|word|`，最壞時間複雜度約為 `O(Σ|word| × |chars|)`；每次檢查單字時需保存可用字串副本，額外空間為 `O(|chars|)`。

這個解法直觀、容易用「真的拿走一個字元」理解，但字串會被反覆搜尋與建立，因此資料量較大時不如字頻解法有效率。

## 解法二：使用字頻雜湊表

### 設計方式

`CountCharacters2` 不修改字串，而是把字元數量轉成 `Dictionary<char, int>`：

1. 先統計 `chars` 的可用字頻，而且只需統計一次。
2. 對每個單字重新建立需求字頻。
3. 比較單字中每個字元的需求量與可用量。
4. 只要任一需求量較大，立即判定失敗。
5. 所有字元都足夠時，累加單字長度。

### 範例演示

`chars = "atach"` 的可用字頻為：

| 字元 | `a` | `t` | `c` | `h` |
| --- | ---: | ---: | ---: | ---: |
| 可用數量 | 2 | 1 | 1 | 1 |

逐一比較各單字：

| 單字 | 需求字頻 | 判斷 | 結果 |
| --- | --- | --- | --- |
| `cat` | `c:1, a:1, t:1` | 每個需求都不超過可用量 | 通過，累加 3 |
| `bt` | `b:1, t:1` | `b` 的可用量是 0 | 失敗 |
| `hat` | `h:1, a:1, t:1` | 每個需求都不超過可用量 | 通過，累加 3 |
| `tree` | `t:1, r:1, e:2` | `r` 與 `e` 沒有可用數量 | 失敗 |

總和同樣是 `3 + 3 = 6`。

以重複字母為例，若 `chars = "aa"`：

- `"a"` 需要 `a:1`，可以組成。
- `"aa"` 需要 `a:2`，可以組成。
- `"aaa"` 需要 `a:3`，超過可用的 `a:2`，無法組成。

### 複雜度

建立 `chars` 字頻需要 `O(|chars|)`，建立與檢查所有單字字頻需要 `O(Σ|word|)`，所以平均時間複雜度是 `O(|chars| + Σ|word|)`。

題目只允許 26 個小寫英文字母，因此字典最多保存 26 種鍵，額外空間可視為 `O(26)`，也就是常數空間。這個解法較適合大量單字或較長字串。

## 可執行驗證案例

`Main` 會同時使用兩種解法執行以下案例：

| 案例 | `words` | `chars` | 預期結果 | 驗證目的 |
| --- | --- | --- | ---: | --- |
| LeetCode Example 1 | `cat, bt, hat, tree` | `atach` | 6 | 官方基本案例 |
| LeetCode Example 2 | `hello, world, leetcode` | `welldonehoneyr` | 10 | 官方較長字串案例 |
| Repeated letters | `a, aa, aaa, b` | `aa` | 3 | 驗證重複字元數量限制 |
| No word can be formed | `ab, cd` | `a` | 0 | 驗證全部失敗時回傳 0 |

只要任一解法結果不符合預期，程式會輸出 `FAIL` 並設定非零結束碼。

## 建置與執行

需要安裝支援 .NET 10 的 .NET SDK。請在此 README 所在的專案根目錄執行：

```powershell
dotnet build leetcode_1160/leetcode_1160.csproj
dotnet run --project leetcode_1160/leetcode_1160.csproj
```

檢查格式與多餘空白：

```powershell
dotnet format leetcode_1160/leetcode_1160.csproj --verify-no-changes
git diff --check
```

## 實際執行輸出

以下內容來自 `dotnet run --project leetcode_1160/leetcode_1160.csproj`：

```text
Case 1 - LeetCode Example 1
  words = ["cat", "bt", "hat", "tree"]
  chars = "atach"
  CountCharacters : expected=6, actual=6, PASS
  CountCharacters2: expected=6, actual=6, PASS

Case 2 - LeetCode Example 2
  words = ["hello", "world", "leetcode"]
  chars = "welldonehoneyr"
  CountCharacters : expected=10, actual=10, PASS
  CountCharacters2: expected=10, actual=10, PASS

Case 3 - Repeated letters
  words = ["a", "aa", "aaa", "b"]
  chars = "aa"
  CountCharacters : expected=3, actual=3, PASS
  CountCharacters2: expected=3, actual=3, PASS

Case 4 - No word can be formed
  words = ["ab", "cd"]
  chars = "a"
  CountCharacters : expected=0, actual=0, PASS
  CountCharacters2: expected=0, actual=0, PASS

Overall: PASS (4/4 cases, 8/8 checks)
```

## 專案結構

```text
leetcode_1160/
├── docs/
│   └── readme-template.md
├── leetcode_1160/
│   ├── leetcode_1160.csproj
│   └── Program.cs
└── README.md
```

- `Program.cs`：包含兩種解法、固定案例與 PASS/FAIL runner。
- `leetcode_1160.csproj`：目標框架為 .NET 10 的 console 專案設定。
- `docs/readme-template.md`：建立此初始 README 時使用的結構指引。
