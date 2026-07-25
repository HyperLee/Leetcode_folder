# LeetCode 2108 — Find First Palindromic String in the Array

> 找出陣列中的第一個回文字串｜.NET 10 主控台專案

- [English problem](https://leetcode.com/problems/find-first-palindromic-string-in-the-array/)
- [中文題目](https://leetcode.cn/problems/find-first-palindromic-string-in-the-array/)

## 題目說明

給定字串陣列 `words`，依原順序回傳第一個回文字串。回文字串從左到右與從右到左讀取時
完全相同；若陣列中沒有回文字串，回傳空字串 `""`。

題目限制：

- `1 <= words.length <= 100`
- `1 <= words[i].length <= 100`
- `words[i]` 只包含小寫英文字母

## 雙指標解法

公開 API：

```csharp
public static string FirstPalindrome(string[] words)
public static bool IsPalindrome(string word)
```

`FirstPalindrome` 依輸入順序逐一檢查字串。每個候選字串交給 `IsPalindrome`，一旦得到
`true` 就立即回傳；因此回傳的一定是原陣列中的第一個回文，而不是任意一個回文。全部候選
都失敗時才回傳空字串。

`IsPalindrome` 將 `left` 放在字串開頭、`right` 放在結尾。每輪比較兩個對稱位置：

- 字元不同時可立即回傳 `false`。
- 字元相同時，`left` 右移、`right` 左移。
- 指標相遇或交錯，代表所有對稱位置都相同，回傳 `true`。

兩個方法都只讀取輸入，不排序、不改寫陣列或字串，也不輸出主控台。這比建立反轉字串再比較
少了每個候選字串的額外配置。

### 核心不變量與易錯處

- 外層掃描順序必須與 `words` 相同；不能先排序，也不能跳過較早的回文。
- 雙指標每次比較的 `word[left]` 與 `word[right]` 都是對稱位置。
- 只要一組對稱字元不同，就已足以否定回文。
- 長度 1 的字串不進入迴圈，仍應判定為回文。
- 沒有任何匹配時必須回傳空字串，而不是最後一個候選或 `null`。

### 逐步範例

以 `words = ["abc", "car", "ada", "racecar", "cool"]` 為例：

```plaintext
abc：a != c，不是回文
car：c != r，不是回文
ada：a == a，指標向中心移動後完成檢查，是回文
```

`ada` 是依輸入順序遇到的第一個回文，因此立即回傳；即使後面的 `racecar` 也是回文，也不能
取代較早的答案。

### 複雜度

令 `n` 為陣列長度，`k` 為最長字串長度。

| 方法 | 時間 | 輔助空間 | 結果空間 |
| --- | --- | --- | --- |
| `FirstPalindrome` | `O(n × k)` | `O(1)` | `O(1)` |
| `IsPalindrome` | `O(k)` | `O(1)` | `O(1)` |

`FirstPalindrome` 最壞情況會檢查所有字串的所有對稱位置。回傳值是原輸入中的既有字串或空
字串常值，不建立與輸入規模成長的結果集合。

## Acceptance Harness

`Main` 是唯一的 console I/O 邊界。八個 `FirstPalindrome` 案例各自驗證結果與輸入陣列未被
修改，共 16 個檢查；另以六個案例直接驗證公開 helper `IsPalindrome`，總計 22 個檢查。任何
失敗都會將 process exit code 設為 `1`。

| # | `FirstPalindrome` 輸入摘要 | 預期 | 驗證目的 |
| ---: | --- | --- | --- |
| 1 | 官方範例 1 | `ada` | 第一個匹配優先於後續回文 |
| 2 | 官方範例 2 | `racecar` | 跳過非回文後找到答案 |
| 3 | 官方範例 3 | `""` | 沒有回文時回傳空字串 |
| 4 | `["a"]` | `a` | 最小輸入與單字元回文 |
| 5 | `["abba","level"]` | `abba` | 第一項即為偶數長回文 |
| 6 | `["abca","cdc"]` | `cdc` | 排除內部不等後繼續搜尋 |
| 7 | 一個長度 100 的全 `a` 字串 | 該字串 | 字串長度上限 |
| 8 | 99 個 `"ab"` 加最後一個 `"z"` | `z` | 陣列長度上限與完整掃描 |

直接檢查 `IsPalindrome` 的 `"a"`、`"aa"`、`"aba"`、`"ab"`、`"abca"` 與長度 100
回文，涵蓋單字元、奇偶長度、外層不等、內層不等及上限。

## 建置與執行

已從 repository 根目錄驗證：

```bash
dotnet build leetcode_2108/leetcode_2108/leetcode_2108.csproj --nologo
dotnet run --no-build --project leetcode_2108/leetcode_2108/leetcode_2108.csproj
```

若直接開啟題目根目錄 `leetcode_2108/`，使用：

```bash
dotnet build leetcode_2108/leetcode_2108.csproj --nologo
dotnet run --no-build --project leetcode_2108/leetcode_2108.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: 1 - Official example 1
Input: words=[abc, car, ada, racecar, cool]
PASS FirstPalindrome result | Expected: ada | Actual: ada
PASS FirstPalindrome input preserved | Expected: True | Actual: True

Case: 2 - Official example 2
Input: words=[notapalindrome, racecar]
PASS FirstPalindrome result | Expected: racecar | Actual: racecar
PASS FirstPalindrome input preserved | Expected: True | Actual: True

Case: 3 - Official example 3
Input: words=[def, ghi]
PASS FirstPalindrome result | Expected: "" | Actual: ""
PASS FirstPalindrome input preserved | Expected: True | Actual: True

Case: 4 - Minimum input
Input: words=[a]
PASS FirstPalindrome result | Expected: a | Actual: a
PASS FirstPalindrome input preserved | Expected: True | Actual: True

Case: 5 - First even-length palindrome
Input: words=[abba, level]
PASS FirstPalindrome result | Expected: abba | Actual: abba
PASS FirstPalindrome input preserved | Expected: True | Actual: True

Case: 6 - Reject inner mismatch and continue
Input: words=[abca, cdc]
PASS FirstPalindrome result | Expected: cdc | Actual: cdc
PASS FirstPalindrome input preserved | Expected: True | Actual: True

Case: 7 - Maximum word length
Input: words=[a x 100]
PASS FirstPalindrome result | Expected: aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa | Actual: aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
PASS FirstPalindrome input preserved | Expected: True | Actual: True

Case: 8 - Maximum array length with final match
Input: words=[ab x 99, z]
PASS FirstPalindrome result | Expected: z | Actual: z
PASS FirstPalindrome input preserved | Expected: True | Actual: True

Palindrome check: 1 - Single character
Input: word=a
PASS IsPalindrome result | Expected: True | Actual: True

Palindrome check: 2 - Even-length palindrome
Input: word=aa
PASS IsPalindrome result | Expected: True | Actual: True

Palindrome check: 3 - Odd-length palindrome
Input: word=aba
PASS IsPalindrome result | Expected: True | Actual: True

Palindrome check: 4 - Outer mismatch
Input: word=ab
PASS IsPalindrome result | Expected: False | Actual: False

Palindrome check: 5 - Inner mismatch
Input: word=abca
PASS IsPalindrome result | Expected: False | Actual: False

Palindrome check: 6 - Maximum word length
Input: word=a x 100
PASS IsPalindrome result | Expected: True | Actual: True

Summary: 22/22 checks passed.
```

## 專案結構

```plaintext
leetcode_2108/
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_2108/
    ├── Program.cs
    └── leetcode_2108.csproj
```
