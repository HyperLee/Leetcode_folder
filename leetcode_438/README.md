# LeetCode 438：找出字串中所有字母異位詞

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/C%23-Console-239120)
![LeetCode](https://img.shields.io/badge/LeetCode-438-FFA116)

這是一個使用 C# 與 .NET 10 實作的教學型主控台專案。程式以「字母剩餘配額」搭配滑動
視窗，在一次線性掃描中找出所有字母異位詞的起始索引，並由 `Main` 中的七個固定案例提供
可重複執行的 Expected/Actual 驗證。

- [LeetCode English](https://leetcode.com/problems/find-all-anagrams-in-a-string/description/)
- [LeetCode 中文](https://leetcode.cn/problems/find-all-anagrams-in-a-string/description/)

## 題目說明

給定兩個字串 `s` 與 `p`，找出 `s` 中所有與 `p` 互為字母異位詞的子字串，並回傳這些
子字串的起始索引。字母異位詞使用完全相同的字母與出現次數，但排列順序可以不同；答案
順序不限。

例如：

| 輸入 | 輸出 | 說明 |
| --- | --- | --- |
| `s = "cbaebabacd"`、`p = "abc"` | `[0, 6]` | `"cba"` 與 `"bac"` 都由 `a`、`b`、`c` 各一個組成。 |
| `s = "abab"`、`p = "ab"` | `[0, 1, 2]` | `"ab"`、`"ba"`、`"ab"` 都是 `p` 的異位詞。 |

## 限制條件

- `1 <= s.Length, p.Length <= 3 * 10^4`
- `s` 與 `p` 只包含小寫英文字母。
- 公開方法預期 `p` 非空，且兩個輸入皆符合上述字元限制。
- 回傳索引依掃描順序自然遞增；演算法不修改輸入字串。

> [!NOTE]
> Acceptance harness 額外保留 `s = ""` 的防禦性案例，用來確認既有實作會回傳空集合。
> 空字串不屬於官方題目限制；本專案不為限制外的空 `p` 定義行為。

## 解題概念與出發點

最直接的做法，是列舉 `s` 中每個長度為 `p.Length` 的子字串，再逐一統計或排序字母後與
`p` 比較。若 `n = s.Length`、`m = p.Length`，重複統計每個視窗最壞需要
`O((n - m + 1) * m)` 時間，因為相鄰視窗雖然只差兩個字元，卻重新做了幾乎相同的工作。

滑動視窗的核心改善，是在右端加入一個字元時只更新一次計數，左端移除字元時也只更新一次。
由於輸入只含 `a` 到 `z`，可以使用固定長度 26 的陣列，而不需要 Dictionary：

- `count[c - 'a']` 初始表示 `p` 還需要幾個字元 `c`。
- 右指針納入字元時，將該字元的配額減一。
- 若配額變成負數，代表視窗內這個字元太多；移動左指針並歸還移出的字元配額。
- 當視窗有效且長度恰好等於 `p.Length`，視窗中的字母頻率必定與 `p` 完全相同。

令：

- `n` 為 `s` 的長度。
- `m` 為 `p` 的長度。
- `k` 為答案索引數量。

## 解法：字母配額 + 滑動視窗

### 設計說明

`FindAnagrams` 使用 `left` 與 `right` 表示目前視窗的左右邊界：

1. 若 `s` 為空或比 `p` 短，不可能存在長度為 `p.Length` 的子字串，直接回傳空結果。
2. 掃描 `p`，建立 26 格 `count` 陣列。正數表示視窗仍缺少該字母。
3. 讓 `right` 從左到右掃描 `s`，每納入一個字元就消耗一份配額。
4. 若剛加入的字元配額小於零，表示它在視窗中出現過多。持續移動 `left`，並歸還每個移出
   字元的配額，直到這個超額狀態消失。
5. 此時所有字母配額都不會是負數。若視窗長度又剛好是 `m`，視窗共消耗了 `p` 的全部
   `m` 份配額，因此每一種字母的數量都與 `p` 相同，記錄 `left`。

### 視窗不變量

每次完成 `while` 收縮後，視窗都維持以下條件：

1. 視窗內沒有任何字母的數量超過 `p` 的需求。
2. `count` 中的每個值皆為非負數，代表各字母尚未被視窗使用的數量。
3. 視窗長度不可能大於 `m`。若長度等於 `m`，所有剩餘配額總和必為零，所以視窗就是
   `p` 的一個排列。

這也是程式只需檢查 `right - left + 1 == p.Length`，不必在每一步重新比較 26 格陣列的
原因。

### 正確性說明

演算法每次將 `s[right]` 加入視窗後，先扣除它的配額。若該字元超額，`while` 會從左側
依序移除字元，直到視窗重新滿足所有字母均未超額，因此不會把含有錯誤頻率的視窗加入答案。

反過來，任何長度為 `m` 的有效視窗都包含恰好 `m` 個字元；既然每種字母都沒有超過 `p`
的需求，而 `p` 的需求總數也正好是 `m`，視窗不可能缺少某個字母而不讓另一個字母超額。
所以該視窗與 `p` 的字母頻率完全相同。`right` 會走訪每個位置，因此所有符合條件的視窗
都會被檢查並記錄。

### 複雜度

| 項目 | 複雜度 | 原因 |
| --- | --- | --- |
| 時間 | `O(n + m)` | 建立 `p` 的配額需 `O(m)`；左右指針各自最多走過 `s` 一次。 |
| 輔助空間 | `O(1)` | `count` 固定只有 26 格，不隨輸入長度成長。 |
| 結果空間 | `O(k)` | 儲存找到的 `k` 個起始索引。 |

## 範例演示流程

使用 `s = "cbaebabacd"`、`p = "abc"`。初始需求為 `a:1, b:1, c:1`，`left = 0`。
表格中的視窗是完成必要收縮後的狀態：

| `right` | 加入字元 | 收縮後 `left` | 有效視窗 | 判斷 |
| ---: | :---: | ---: | --- | --- |
| 0 | `c` | 0 | `"c"` | 長度 1，不記錄。 |
| 1 | `b` | 0 | `"cb"` | 長度 2，不記錄。 |
| 2 | `a` | 0 | `"cba"` | 長度 3 且配額完全用完，記錄索引 `0`。 |
| 3 | `e` | 4 | `""` | `e` 不在需求中，收縮到移除 `e` 才恢復有效。 |
| 4 | `b` | 4 | `"b"` | 長度 1，不記錄。 |
| 5 | `a` | 4 | `"ba"` | 長度 2，不記錄。 |
| 6 | `b` | 5 | `"ab"` | `b` 超額，移除左側第一個 `b`。 |
| 7 | `a` | 6 | `"ba"` | `a` 超額，移除左側第一個 `a`。 |
| 8 | `c` | 6 | `"bac"` | 長度 3 且配額完全用完，記錄索引 `6`。 |
| 9 | `d` | 10 | `""` | `d` 不在需求中，收縮到移除 `d`。 |

最後回傳 `[0, 6]`。

## Acceptance Harness

專案沒有 xUnit、NUnit 或 MSTest 專案。`Main` 會執行七個固定案例，使用 `SequenceEqual`
比較預期與實際索引，並輸出 PASS/FAIL。任一案例失敗時，process exit code 會設為 1。

| # | 案例 | `s` | `p` | 預期 |
| ---: | --- | --- | --- | --- |
| 1 | 官方範例一 | `"cbaebabacd"` | `"abc"` | `[0, 6]` |
| 2 | 官方範例二 | `"abab"` | `"ab"` | `[0, 1, 2]` |
| 3 | 空來源字串（防禦性案例） | `""` | `"abc"` | `[]` |
| 4 | 目標字串較長 | `"ab"` | `"abc"` | `[]` |
| 5 | 所有字母相同 | `"aaaaaaa"` | `"aa"` | `[0, 1, 2, 3, 4, 5]` |
| 6 | 等長完全匹配 | `"abc"` | `"abc"` | `[0]` |
| 7 | 沒有異位詞 | `"abc"` | `"xyz"` | `[]` |

## 建置、驗證與執行

需求：

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

從此題目的 repository root 執行：

```bash
dotnet restore leetcode_438/leetcode_438.csproj
dotnet build leetcode_438/leetcode_438.csproj --nologo --no-restore
dotnet run --project leetcode_438/leetcode_438.csproj --no-build
git diff --check
```

目前沒有正式測試專案，因此實際行為驗證由 `Main` 中的 acceptance harness 完成。

### 實際執行輸出

以下內容來自完成建置後的 fresh run：

```text
案例: 官方範例一
s: "cbaebabacd"
p: "abc"
Expected: [0, 6]
Actual: [0, 6]
Result: PASS

案例: 官方範例二
s: "abab"
p: "ab"
Expected: [0, 1, 2]
Actual: [0, 1, 2]
Result: PASS

案例: 空來源字串（防禦性案例）
s: ""
p: "abc"
Expected: []
Actual: []
Result: PASS

案例: 目標字串較長
s: "ab"
p: "abc"
Expected: []
Actual: []
Result: PASS

案例: 所有字母相同
s: "aaaaaaa"
p: "aa"
Expected: [0, 1, 2, 3, 4, 5]
Actual: [0, 1, 2, 3, 4, 5]
Result: PASS

案例: 等長完全匹配
s: "abc"
p: "abc"
Expected: [0]
Actual: [0]
Result: PASS

案例: 沒有異位詞
s: "abc"
p: "xyz"
Expected: []
Actual: []
Result: PASS

Summary: 7/7 checks passed.
```

## 專案結構

```plaintext
.
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
├── leetcode_438.sln
└── leetcode_438/
    ├── Program.cs
    └── leetcode_438.csproj
```
