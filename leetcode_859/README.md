# LeetCode 859：Buddy Strings／親密字串

[英文題目](https://leetcode.com/problems/buddy-strings/) ·
[中文題目](https://leetcode.cn/problems/buddy-strings/)

這個 .NET 10 主控台專案實作 LeetCode 859：判斷是否能交換字串 `s` 中兩個不同
索引的字元，使結果等於 `goal`。公開方法 `BuddyStrings` 只負責計算；`Main` 集中
所有 acceptance harness 輸出。

## 題目限制

- `1 <= s.Length, goal.Length <= 2 * 10^4`。
- `s` 與 `goal` 只包含小寫英文字母。
- 交換必須選擇兩個不同索引，且恰好執行一次。

實作只處理 LeetCode 的有效輸入契約，不另外定義 `null`、無效字元或例外政策。

## 解法：單次掃描不同位置

`BuddyStrings` 先排除長度不同的輸入，再以一次掃描同時追蹤重複字母與 mismatch：

1. 若兩字串完全相同，交換後仍要相同，因此 `s` 必須至少包含一個重複字母，才能
   交換兩個不同索引但不改變字串內容。
2. 若兩字串不同，一次交換只可能修正兩個位置，所以 mismatch 必須恰好為兩個。
3. 設兩個不同位置為 `i`、`j`，必須同時滿足 `s[i] == goal[j]` 與
   `s[j] == goal[i]`。
4. 掃描到第三個 mismatch 時可立即回傳 `false`。

舊專案中的 `buddyStrings2` 與主要解法具有相同的漸進複雜度和相同目的，且命名與
公開 API 契約不一致，因此翻新後只保留：

```csharp
public static bool BuddyStrings(string s, string goal)
```

## 複雜度

令 `n` 為字串長度：

- 時間複雜度：`O(n)`，最多掃描字串一次。
- 結果空間：`O(1)`，只回傳布林值。
- 輔助空間：`O(1)`，26 個小寫字母的出現狀態與固定數量索引不隨輸入成長。

## 逐步走查

以 `s = "abcd"`、`goal = "abdc"` 為例：

```plaintext
索引 0、1：字元相同
索引 2：第一個 mismatch，s[2] = 'c'，goal[2] = 'd'
索引 3：第二個 mismatch，s[3] = 'd'，goal[3] = 'c'
交叉配對：'c' == goal[3] 且 'd' == goal[2]
結果：true
```

相對地，`s = "ab"`、`goal = "ab"` 雖然內容相同，但沒有重複字母；交換唯一兩個
不同索引會得到 `"ba"`，因此答案是 `false`。

## 可執行驗證

專案不建立正式測試專案；`Main` 的確定性驗收程式是目前的驗證機制。任一失敗都
會設定 `Environment.ExitCode = 1`。

| # | 案例 | 預期 | 驗證重點 |
|---:|---|---:|---|
| 1 | `"ab"`, `"ba"` | `true` | 官方一次交換案例 |
| 2 | `"ab"`, `"ab"` | `false` | 相同字串但無重複字母 |
| 3 | `"aa"`, `"aa"` | `true` | 相同字串且有重複字母 |
| 4 | `"a"`, `"a"` | `false` | 最短有效輸入 |
| 5 | `"abc"`, `"ab"` | `false` | 長度不同 |
| 6 | `"ab"`, `"ac"` | `false` | 同長度但恰好一個 mismatch |
| 7 | `"abcd"`, `"abdc"` | `true` | 恰兩個可交叉配對位置 |
| 8 | `"ab"`, `"ca"` | `false` | 兩處不同但無法交叉配對 |
| 9 | `"abcd"`, `"badc"` | `false` | 超過兩個 mismatch |
| 10 | 長度 20,000，尾端 `"bc" -> "cb"` | `true` | 題目上限 spot check |

## 建置與執行

請把外層 `leetcode_859/` 當成工作目錄：

```bash
dotnet build leetcode_859/leetcode_859.csproj --nologo
dotnet run --no-build --project leetcode_859/leetcode_859.csproj
```

## 最新驗證輸出

以下是執行第二個命令的完整輸出，也是本 README 唯一的 `text` fence：

```text
LeetCode 859 acceptance harness

Case 1: Official example: one valid swap
Input: s = "ab", goal = "ba"
PASS | Expected: true | Actual: true

Case 2: Official example: equal without duplicate
Input: s = "ab", goal = "ab"
PASS | Expected: false | Actual: false

Case 3: Official example: equal with duplicate
Input: s = "aa", goal = "aa"
PASS | Expected: true | Actual: true

Case 4: Minimum length
Input: s = "a", goal = "a"
PASS | Expected: false | Actual: false

Case 5: Different lengths
Input: s = "abc", goal = "ab"
PASS | Expected: false | Actual: false

Case 6: Exactly one difference
Input: s = "ab", goal = "ac"
PASS | Expected: false | Actual: false

Case 7: Exactly two cross-matching differences
Input: s = "abcd", goal = "abdc"
PASS | Expected: true | Actual: true

Case 8: Two differences without cross-match
Input: s = "ab", goal = "ca"
PASS | Expected: false | Actual: false

Case 9: More than two differences
Input: s = "abcd", goal = "badc"
PASS | Expected: false | Actual: false

Case 10: Maximum length spot check
Input: length = 20,000; shared prefix = 19,998 x 'a'; suffix = "bc" -> "cb"
PASS | Expected: true | Actual: true

Summary: 10/10 checks passed.
```

## 專案結構

```plaintext
leetcode_859/
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── README.md
├── docs/
│   ├── readme-template.md
│   └── superpowers/
│       ├── plans/
│       │   └── 2026-07-15-leetcode-859-net10-migration.md
│       └── specs/
│           └── 2026-07-15-leetcode-859-net10-migration-design.md
└── leetcode_859/
    ├── Program.cs
    └── leetcode_859.csproj
```
