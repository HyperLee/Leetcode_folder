# LeetCode 2390 - Removing Stars From a String

## 從字串中移除星號

- [English problem](https://leetcode.com/problems/removing-stars-from-a-string/)
- [中文題目](https://leetcode.cn/problems/removing-stars-from-a-string/)

給定只包含小寫英文字母與星號的字串 `s`。每次操作選擇一個星號，刪除該星號及其左側最近的非星號字元；回傳所有星號都移除後的唯一結果。

## 限制條件

- `1 <= s.Length <= 100000`。
- `s` 只包含小寫英文字母與 `'*'`。
- 輸入保證每次操作都能合法執行。
- 最終結果保證唯一。

## 核心不變量

從左到右掃描時，暫存序列恰好代表目前前綴完成所有刪除後仍存在的字元：

- 遇到字母便加入尾端。
- 遇到星號便移除尾端；題目保證尾端一定存在。

因此每個字元只會被加入一次並至多移除一次。容易出錯之處是誤刪較早的字元，或直接列舉 `Stack<char>` 而得到反向結果。

## 三種保留解法

### `RemoveStars`：`List<char>`

`List<char>` 的尾端就是左側最近且尚未刪除的字元。字母使用 `Add`，星號使用 `RemoveAt(Count - 1)`，最後轉為字串。

### `RemoveStars2`：`StringBuilder`

`StringBuilder` 同樣保存目前結果前綴；字母使用 `Append`，星號使用 `Remove(Length - 1, 1)`。這個版本直接表達可變字串的尾端刪除。

### `RemoveStars3`：`Stack<char>`

字母 push、星號 pop，直接呈現題目的 LIFO 性質。`Stack<char>` 從頂端開始列舉，因此建立結果前必須反轉，才能恢復原本的相對順序。

三個公開 API 都是純函式，不修改輸入、不輸出主控台，也不保留跨呼叫狀態。

| 方法 | 時間 | 結果空間 | 輔助空間 |
| --- | --- | --- | --- |
| `RemoveStars` | `O(n)` | `O(n)` | `O(n)`，`List<char>` |
| `RemoveStars2` | `O(n)` | `O(n)` | `O(n)`，`StringBuilder` |
| `RemoveStars3` | `O(n)` | `O(n)` | `O(n)`，Stack 與反轉陣列 |

## 逐步走查

輸入 `leet**cod*e`：

```plaintext
讀取 l/e/e/t：暫存 leet
讀取第一個 *：刪除 t，暫存 lee
讀取第二個 *：刪除 e，暫存 le
讀取 c/o/d：暫存 lecod
讀取第三個 *：刪除 d，暫存 leco
讀取 e：暫存 lecoe
```

最後回傳 `lecoe`。

## Acceptance harness

`Main` 是唯一 Console I/O 邊界。八組案例各自以預先定義且獨立於三個解法的 expected value 驗證三個公開方法，共 24 項精確比較；任何失敗都會設定 process exit code 為 `1`。

| # | 案例 | 預期 | 驗證目的 |
| ---: | --- | --- | --- |
| 1 | `leet**cod*e` | `lecoe` | 官方交錯刪除範例 |
| 2 | `erase*****` | 空字串 | 官方完整刪除範例 |
| 3 | `a` | `a` | 最小輸入且沒有星號 |
| 4 | `a*` | 空字串 | 最小完整刪除 |
| 5 | `abcdefghijklmnopqrstuvwxyz` | 原字串 | 不應誤刪沒有星號的輸入 |
| 6 | `ab*c*d` | `ad` | 每次刪除最近的存活字元 |
| 7 | `abc**d*e` | `ae` | 連續與交錯星號的狀態更新 |
| 8 | `"ab*"` 重複 33,333 次再接 `"z"` | 33,333 個 `a` 再接 `z` | 長度 100,000 上限與線性處理 |

上限案例仍比較完整字串，但輸出只顯示長度、前綴與後綴，避免列印巨大結果。

## 建置與執行

已從 repository 根目錄實際驗證：

```bash
dotnet build leetcode_2390/leetcode_2390/leetcode_2390.csproj --nologo
dotnet run --no-build --project leetcode_2390/leetcode_2390/leetcode_2390.csproj
```

若直接開啟題目根目錄 `leetcode_2390/`，使用：

```bash
dotnet build leetcode_2390/leetcode_2390.csproj --nologo
dotnet run --no-build --project leetcode_2390/leetcode_2390.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: Official example 1 [RemoveStars]; Input: "leet**cod*e"
Expected: "lecoe"
Actual: "lecoe"
PASS
Case: Official example 1 [RemoveStars2]; Input: "leet**cod*e"
Expected: "lecoe"
Actual: "lecoe"
PASS
Case: Official example 1 [RemoveStars3]; Input: "leet**cod*e"
Expected: "lecoe"
Actual: "lecoe"
PASS
Case: Official example 2 [RemoveStars]; Input: "erase*****"
Expected: ""
Actual: ""
PASS
Case: Official example 2 [RemoveStars2]; Input: "erase*****"
Expected: ""
Actual: ""
PASS
Case: Official example 2 [RemoveStars3]; Input: "erase*****"
Expected: ""
Actual: ""
PASS
Case: Minimum retained character [RemoveStars]; Input: "a"
Expected: "a"
Actual: "a"
PASS
Case: Minimum retained character [RemoveStars2]; Input: "a"
Expected: "a"
Actual: "a"
PASS
Case: Minimum retained character [RemoveStars3]; Input: "a"
Expected: "a"
Actual: "a"
PASS
Case: Minimum complete removal [RemoveStars]; Input: "a*"
Expected: ""
Actual: ""
PASS
Case: Minimum complete removal [RemoveStars2]; Input: "a*"
Expected: ""
Actual: ""
PASS
Case: Minimum complete removal [RemoveStars3]; Input: "a*"
Expected: ""
Actual: ""
PASS
Case: No stars [RemoveStars]; Input: "abcdefghijklmnopqrstuvwxyz"
Expected: "abcdefghijklmnopqrstuvwxyz"
Actual: "abcdefghijklmnopqrstuvwxyz"
PASS
Case: No stars [RemoveStars2]; Input: "abcdefghijklmnopqrstuvwxyz"
Expected: "abcdefghijklmnopqrstuvwxyz"
Actual: "abcdefghijklmnopqrstuvwxyz"
PASS
Case: No stars [RemoveStars3]; Input: "abcdefghijklmnopqrstuvwxyz"
Expected: "abcdefghijklmnopqrstuvwxyz"
Actual: "abcdefghijklmnopqrstuvwxyz"
PASS
Case: Interleaved removals [RemoveStars]; Input: "ab*c*d"
Expected: "ad"
Actual: "ad"
PASS
Case: Interleaved removals [RemoveStars2]; Input: "ab*c*d"
Expected: "ad"
Actual: "ad"
PASS
Case: Interleaved removals [RemoveStars3]; Input: "ab*c*d"
Expected: "ad"
Actual: "ad"
PASS
Case: Consecutive and interleaved removals [RemoveStars]; Input: "abc**d*e"
Expected: "ae"
Actual: "ae"
PASS
Case: Consecutive and interleaved removals [RemoveStars2]; Input: "abc**d*e"
Expected: "ae"
Actual: "ae"
PASS
Case: Consecutive and interleaved removals [RemoveStars3]; Input: "abc**d*e"
Expected: "ae"
Actual: "ae"
PASS
Case: 100,000-character mixed input [RemoveStars]; Input: "ab*" x 33333 + "z"
Expected: length=33334, prefix="aaaaa", suffix="aaaaz"
Actual: length=33334, prefix="aaaaa", suffix="aaaaz"
PASS
Case: 100,000-character mixed input [RemoveStars2]; Input: "ab*" x 33333 + "z"
Expected: length=33334, prefix="aaaaa", suffix="aaaaz"
Actual: length=33334, prefix="aaaaa", suffix="aaaaz"
PASS
Case: 100,000-character mixed input [RemoveStars3]; Input: "ab*" x 33333 + "z"
Expected: length=33334, prefix="aaaaa", suffix="aaaaz"
Actual: length=33334, prefix="aaaaa", suffix="aaaaz"
PASS
Summary: 24/24 checks passed.
```

## 舊版檔案整理

已逐檔移除舊式 `leetcode_2390.sln`、`App.config` 與 `Properties/AssemblyInfo.cs`。SDK-style `net10.0` 專案由 `leetcode_2390.csproj` 集中管理組件資訊與建置設定，因此不保留這些 .NET Framework 產物。

## 專案結構

```plaintext
leetcode_2390/
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
└── leetcode_2390/
    ├── Program.cs
    └── leetcode_2390.csproj
```
